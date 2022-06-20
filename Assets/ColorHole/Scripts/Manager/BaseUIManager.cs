using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseUIManager : Singleton<BaseUIManager>
{
    public string gameSceneName = "Master Scene";

    [Header("Panels")]
    public UIObject gamePanel;
    public UIObject levelCompletedPanel;
    public UIObject levelFailedPanel;

    [Header("UI Game Equipment")]
    public UIHint tutorialHint;
    public UIObject levelBar;

    public event EventHandler onNextButtonClick;
    public event EventHandler onRetryButtonClick;

    public virtual void OnNextButtonClick()
    {
        onNextButtonClick?.Invoke(this, EventArgs.Empty);

        SceneManager.LoadScene(gameSceneName);
        DOTween.KillAll();
    }

    public virtual void OnRetryButtonClick()
    {
        onRetryButtonClick?.Invoke(this, EventArgs.Empty);

        SceneManager.LoadScene(gameSceneName);
        DOTween.KillAll();
    }
}

public abstract class BaseUIManager<T> : BaseUIManager where T : BaseUIManager<T>
{
    public new static T Instance => BaseUIManager.Instance as T;
}
