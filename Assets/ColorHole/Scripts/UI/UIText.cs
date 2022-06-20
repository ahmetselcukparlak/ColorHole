using TMPro;
using UnityEngine;

public class UIText : UIObject
{
    [SerializeField] private TMP_Text _textObject;

    public string Text
    {
        set => _textObject.text = value;
    }
}