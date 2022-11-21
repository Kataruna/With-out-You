using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DiaryMenu : MonoBehaviour
{
    [SerializeField] private float duration = 0.5f;

    [SerializeField] private GameObject diaryMenu;

    private void Start()
    {
        diaryMenu.SetActive(false);
    }

    #region Custom Method

    public void Open()
    {
        diaryMenu.SetActive(true);
    }

    public void Close()
    {
        diaryMenu.SetActive(false);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion
    
}
