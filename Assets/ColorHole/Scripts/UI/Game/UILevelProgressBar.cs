using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILevelProgressBar : UIObject, ILevelBar, IProgressBar
{
    [SerializeField] private TMP_Text _currentLevelText;
    [SerializeField] private TMP_Text _nextLevelText;
    [SerializeField] private TMP_Text _progressText;
    [SerializeField] private Image _progressImage;

    private int _level;
    public virtual int Level
    {
        get => _level;
        set
        {
            _level = value;

            if (_currentLevelText != null)
            {
                _currentLevelText.text = value.ToString();
            }

            if (_nextLevelText != null)
            {
                _nextLevelText.text = (value + 1).ToString();
            }
        }
    }

    private float _progress;
    public virtual float Progress
    {
        get => _progress;
        set
        {
            _progress = Mathf.Clamp01(value);

            if (_progressText)
                _progressText.text = $"{Mathf.Ceil(_progress * 100)}%";

            if (_progressImage)
                _progressImage.DOFillAmount(_progress, 0.4f);
        }
    }
}
