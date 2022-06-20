using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITextIcon : UIObject
{
    [SerializeField] private Image _imageObject;
    [SerializeField] private TMP_Text _textObject;

    public string Text
    {
        set => _textObject.text = value;
    }

    public Sprite Icon
    {
        set => _imageObject.sprite = value;
    }
}