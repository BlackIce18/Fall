using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TextCharsAnimation : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _textField;
    [SerializeField] private float _time;
    [SerializeField] private string _caret;

    private string _textToWrite = "";
    private string _writtedText = "";
    private int _currentCaretPosition = -1;
    private Queue<char> _text;
    private float _timePerChar = 0f;
    private string _tempCaret = "";

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        if (!CheckTextField())
        {
            _text = new Queue<char>();
            _textToWrite = _textField.text;
            _text.Clear();
            for (int i = 0; i < _textToWrite.Length; i++)
            {
                _text.Enqueue(_textToWrite[i]);
            }
            _timePerChar = CalculateTimePerChar(_textToWrite, _time);
            Write(_text, _textField);
        }
    }
    public IEnumerator CaretAnimation()
    {
        _tempCaret = _caret;
        yield return new WaitForSeconds(_timePerChar);
        _tempCaret = "";
        yield return new WaitForSeconds(_timePerChar);
        StartCoroutine(CaretAnimation());
    }

    public void Write(Queue<char> text, TMPro.TextMeshProUGUI textField)
    {
        StartCoroutine(WriteText(text, textField));
    }

    public IEnumerator WriteText(Queue<char> text, TMPro.TextMeshProUGUI textField)
    {
        float delayAfterFinishWritting = 0f;
        StartCoroutine(CaretAnimation());

        foreach (char c in text)
        {
            textField.text = _writtedText + _tempCaret;
            yield return new WaitForSeconds(_timePerChar);
            textField.text = _writtedText + c;
            _writtedText += c;
        }

        yield return new WaitForSeconds(delayAfterFinishWritting);
        StopCoroutine(CaretAnimation());
    }

    private float CalculateTimePerChar(string text, float fullTime)
    {
        return fullTime / text.Length;
    }

    private bool CheckTextField()
    {
        Debug.LogWarning("TextField is null!");
        return _textField == null;
    }
}
