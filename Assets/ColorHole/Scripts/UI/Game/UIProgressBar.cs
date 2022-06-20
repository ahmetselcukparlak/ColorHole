using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIProgressBar : UIObject, IProgressBar
{
    [SerializeField] protected TMP_Text progressText;
    [SerializeField] protected Image progressImage;

    private float _progress;
    public virtual float Progress
    {
        get => _progress;
        set
        {
            _progress = Mathf.Clamp01(value);

            if (progressText)
                progressText.text = $"{Mathf.Ceil(_progress * 100)}%";

            //if (progressImage)
                //progressImage.DOFillAmount(_progress, .3f);
        }
    }
}
