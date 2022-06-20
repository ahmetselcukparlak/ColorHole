using UnityEngine;

public class UILevelBar : UIObject, ILevelBar
{
    [SerializeField] private TMPro.TMP_Text _levelText;

    private int _level;
    public virtual int Level
    {
        get => _level;
        set
        {
            _level = value;

            if (_levelText != null)
            {
                _levelText.text = $"LEVEL {value}";
            }
        }
    }
}
