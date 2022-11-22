using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiaryMenu : MonoBehaviour
{
    [SerializeField] private float duration = 0.5f;

    [SerializeField] private CanvasGroup diaryMenu;

    private void Start()
    {
        ForceClose();
    }

    #region Custom Method

    public void Open()
    {
        diaryMenu.gameObject.SetActive(true);
        diaryMenu.DOFade(1f, duration);
        diaryMenu.interactable = true;
    }

    public void Close()
    {
        diaryMenu.DOFade(0f, duration).OnComplete(() => diaryMenu.gameObject.SetActive(false));
        diaryMenu.interactable = false;
    }
    
    private void ForceClose()
    {
        diaryMenu.alpha = 0f;
        diaryMenu.interactable = false;
        diaryMenu.gameObject.SetActive(false);
    }
    
    public void QuitGame()
    {
        Debug.LogWarning("Exit");
        Application.Quit();
    }

    #endregion
    
}
