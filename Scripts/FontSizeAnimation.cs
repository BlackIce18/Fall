using UnityEngine;
using TMPro;

public class FontSizeAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textToAnimate;
    [SerializeField] private float _defaultFontSize;
    [SerializeField] private float _fontSizeToIncrease;
    [SerializeField] private bool _interpolate = false;
    public void IncreaseFontSize()
    {
        ChangeFontSize(_fontSizeToIncrease);
    }

    public void BackToDefaultSize()
    {
        ChangeFontSize(_defaultFontSize);
    }

    private void ChangeFontSize(float fontSize)
    {
        _textToAnimate.fontSize = fontSize;
    }
}
