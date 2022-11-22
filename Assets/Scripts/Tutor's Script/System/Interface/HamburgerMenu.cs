using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HamburgerMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup menuButton;
    [SerializeField] private CanvasGroup subMenu;
    [SerializeField] private Animator selfAnimator;
    [SerializeField] private Animator[] otherMenuAnimators;
    [SerializeField, Range(0f, .5f)] private float timeBetweenAnimations = 0.5f;
    
    private bool _isOpen = false;
    
    private static readonly int Open = Animator.StringToHash("open");
    private static readonly int Close = Animator.StringToHash("close");
    private static readonly int In = Animator.StringToHash("in");
    private static readonly int Out = Animator.StringToHash("out");
    private static readonly int Enable = Animator.StringToHash("enable");
    private static readonly int Disable = Animator.StringToHash("disable");

    private void Start()
    {
        subMenu.interactable = false;
    }

    public void Toggle()
    {
        _isOpen = !_isOpen;
        
        StopAllCoroutines();
        HardResetAnim();

        if (_isOpen)
        {
            selfAnimator.SetTrigger(Open);
            
            StartCoroutine(Delay(In, timeBetweenAnimations));
            
            subMenu.interactable = true;
        }
        else
        {
            selfAnimator.SetTrigger(Close);
            
            StartCoroutine(Delay(Out, timeBetweenAnimations));
            
            subMenu.interactable = false;
        }
    }

    IEnumerator Delay(int state, float delay)
    {
        foreach (Animator menuAnim in otherMenuAnimators)
        {
            yield return new WaitForSeconds(delay);
            menuAnim.SetTrigger(state);
        }
    }

    private void HardResetAnim()
    {
        selfAnimator.ResetTrigger(Open);
        selfAnimator.ResetTrigger(Close);
        selfAnimator.ResetTrigger(Enable);
        selfAnimator.ResetTrigger(Disable);
        
        foreach (Animator menuAnim in otherMenuAnimators)
        {
            menuAnim.ResetTrigger(In);
            menuAnim.ResetTrigger(Out);
        }
    }
    
    public void SetMenuButtonInteractable(bool interactable)
    {
        if (interactable)
        {
            selfAnimator.SetTrigger(Enable);
            //menuButton.DOFade(1f, timeBetweenAnimations);
            menuButton.interactable = true;
        }
        else
        {
            menuButton.interactable = false;
            //menuButton.DOFade(0f, timeBetweenAnimations);
            selfAnimator.SetTrigger(Disable);
        }
    }
}
