using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FadeAble : MonoBehaviour
{
    [SerializeField] private bool initialStage = true;
    private CanvasGroup _canvasGroup;
    private float _duration = 0.5f;
        
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.gameObject.SetActive(initialStage);
    }

    public void Fade(bool appear)
    {
        _canvasGroup.gameObject.SetActive(true);
        
        if (appear)
        {
            _canvasGroup.DOFade(1f, _duration);
            _canvasGroup.interactable = true;
        }
        else
        {
            _canvasGroup.DOFade(0f, _duration).OnComplete(() => _canvasGroup.gameObject.SetActive(false));
            _canvasGroup.interactable = false;
        }
    }
}
