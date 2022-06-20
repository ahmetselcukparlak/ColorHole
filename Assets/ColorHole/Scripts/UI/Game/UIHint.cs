using TMPro;
using UnityEngine;

public class UIHint : UIObject
{
    public bool showOnlyFirstLevel;

    [SerializeField] private TMP_Text _hintText;

    public string Text
    {
        set
        {
            if (!_hintText)
                return;

            _hintText.text = value;
        }
    }
}