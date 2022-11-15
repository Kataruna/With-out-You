using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HamburgerMenu : MonoBehaviour
{
    [SerializeField] private Button selfButton;
    [SerializeField] private Animator selfAnimator;
    [SerializeField] private Animator[] otherMenuAnimators;
    [SerializeField, Range(0f, .5f)] private float timeBetweenAnimations = 0.5f;
    
    private bool _isOpen = false;
    
    private static readonly int Open = Animator.StringToHash("open");
    private static readonly int Close = Animator.StringToHash("close");
    private static readonly int In = Animator.StringToHash("in");
    private static readonly int Out = Animator.StringToHash("out");
    private static readonly int Enable = Animator.StringToHash("enable");

    public void Toggle()
    {
        _isOpen = !_isOpen;
        
        StopAllCoroutines();

        if (_isOpen)
        {
            selfAnimator.SetTrigger(Open);
            
            StartCoroutine(Delay(In, timeBetweenAnimations));
        }
        else
        {
            selfAnimator.SetTrigger(Close);
            
            StartCoroutine(Delay(Out, timeBetweenAnimations));
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

    public void SetState(bool state)
    {
        selfAnimator.SetBool(Enable, state);
        selfButton.enabled = state;
    }
}
