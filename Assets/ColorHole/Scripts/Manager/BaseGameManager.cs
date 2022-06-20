using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public abstract class BaseGameManager : Singleton<BaseGameManager>
{
    public bool isDebug;
    public int debugLevel = 1;

    public bool autoStartLevel;

    private int _currentLevel;
    private float _playTimeStartValue;
    private bool _isPlayTimerStarted;

    public Camera GameCamera;
    [Header("Game Levels")]
    [SerializeField] private List<Level> Levels;
    [SerializeField] private Level _level;
    public Level Level { get => _level; set => _level = value; }

    [SerializeField] private ColorManager _colorManager;
    public ColorManager ColorManager { get => _colorManager; set => _colorManager = value; }

    protected override void Awake()
    {
        base.Awake();
        debugLevel = Math.Max(1, debugLevel);
    }

    protected virtual void Start()
    {
        GameCamera = Camera.main;
        if (autoStartLevel)
        {
            StartLevel(PlayerProgression.Level);
        }
    }

    public void StartLevel(int level)
    {
        CurrentLevel = isDebug ? debugLevel : level;
        Level = Levels[(CurrentLevel - 1) % Levels.Count];
        Level.Active();
        ColorManager.SetColors();
        OnLevelStart();
        Game.isGameOver = false;
    }

    public virtual void OnLevelStart()
    {
        // UI
        if (BaseUIManager.Instance.gamePanel)
            BaseUIManager.Instance.gamePanel.SetActive(true);

        if (BaseUIManager.Instance.levelCompletedPanel)
            BaseUIManager.Instance.levelCompletedPanel.SetActive(false);

        if (BaseUIManager.Instance.levelFailedPanel)
            BaseUIManager.Instance.levelFailedPanel.SetActive(false);

        if (BaseUIManager.Instance.tutorialHint && BaseUIManager.Instance.tutorialHint.showOnlyFirstLevel && CurrentLevel == 1)
            BaseUIManager.Instance.tutorialHint.SetActive(true);
    }

    public virtual void OnLevelCompleted()
    {
        // UI
        //if (BaseUIManager.Instance.gamePanel)
        //BaseUIManager.Instance.gamePanel.SetActive(false);

        if (BaseUIManager.Instance.levelCompletedPanel)
            BaseUIManager.Instance.levelCompletedPanel.SetActive(true);

        // Prefs
        if (!isDebug)
        {
            PlayerProgression.Level = CurrentLevel + 1;
        }

        // Timer
        Debug.Log($"Level completed! ({PlayTime:0.00} seconds)");
    }

    public virtual void OnLevelFailed()
    {
        // UI
        // if (BaseUIManager.Instance.gamePanel)
        //     BaseUIManager.Instance.gamePanel.SetActive(false);

        if (BaseUIManager.Instance.levelFailedPanel)
            BaseUIManager.Instance.levelFailedPanel.SetActive(true);

        // Timer
        Debug.Log($"Level failed! ({PlayTime:0.00} seconds)");
    }

    public void StartPlayTimer()
    {
        _isPlayTimerStarted = true;
        _playTimeStartValue = Time.time;
    }

    public int CurrentLevel
    {
        get => Math.Max(_currentLevel, 1);
        protected set
        {
            _currentLevel = Math.Max(value, 1);

            if (BaseUIManager.Instance.levelBar is ILevelBar levelBar)
                levelBar.Level = CurrentLevel;
        }
    }

    public float PlayTime => _isPlayTimerStarted ? Time.time - _playTimeStartValue : 0f;

}