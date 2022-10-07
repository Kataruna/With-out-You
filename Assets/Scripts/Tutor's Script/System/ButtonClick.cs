using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text text;
    [SerializeField] private float fadeTime;

    private bool _isPressed;

    private Color _black;
    private Color _white;

    public void SetFadeTime(float duration)
    {
        fadeTime = duration;
    }

    public void SetText(string word)
    {
        text.text = word;
    }

    public void SetupButton(Color black, Color white)
    {
        _black = black;
        _white = white;
    }

    public void Press()
    {
        _isPressed = !_isPressed;
        
        StopAllCoroutines();
        
        if (_isPressed)
        {
            StartCoroutine(FadingToColor(button, _black));
            StartCoroutine(FadingToColor(text, _white));
        }
        else
        {
            StartCoroutine(FadingToColor(button, _white));
            StartCoroutine(FadingToColor(text, _black));
        }
    }

    public void ForceSelect()
    {
        _isPressed = true;
        
        StartCoroutine(FadingToColor(button, _black));
        StartCoroutine(FadingToColor(text, _white));
    }

    public void ForceDeselect()
    {
        _isPressed = false;
        
        StartCoroutine(FadingToColor(button, _white));
        StartCoroutine(FadingToColor(text, _black));
    }
    
    IEnumerator FadingToColor(Button focusUI,Color targetColor)
    {
        Image buttonStyle = focusUI.GetComponent<Image>();

        Color startColor = buttonStyle.color;
        
        float elapseTime = 0f;

        while (elapseTime < fadeTime)
        {
            elapseTime += Time.deltaTime;
            buttonStyle.color = Color.Lerp(startColor, targetColor, (elapseTime / fadeTime));
            yield return null;
        }
    }
    
    IEnumerator FadingToColor(TMP_Text focusUI,Color targetColor)
    {
        Color startColor = focusUI.color;
        
        float elapseTime = 0f;

        while (elapseTime < fadeTime)
        {
            elapseTime += Time.deltaTime;
            focusUI.color = Color.Lerp(startColor, targetColor, (elapseTime / fadeTime));
            yield return null;
        }
    }
}
