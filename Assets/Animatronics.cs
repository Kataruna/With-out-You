using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Animatronics : MonoBehaviour
{
    public Characters ActorName => actorName;
    
    [SerializeField] private Animator selfAnim;
    [SerializeField] private Characters actorName;
    
    private bool _facingRight = true;
    
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    
    public enum Characters
    {
        Jane,
    }

    private void Start()
    {
        selfAnim.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Director.Instance.Recruit(this);
    }

    private void OnDisable()
    {
        Director.Instance.Remove(this);
    }

    public void Walk()
    {
        selfAnim.SetBool(IsMoving, true);
    }

    public void Stand()
    {
        selfAnim.SetBool(IsMoving, false);
    }

    public void Disappear()
    {
        selfAnim.gameObject.SetActive(false);
    }

    public void Appear()
    {
        selfAnim.gameObject.SetActive(true);
    }

    public void DoFlip()
    {
        _facingRight = !_facingRight;
        
        if(!_facingRight) transform.Rotate(0f, 180f, 0f);
        else transform.Rotate(0f, 0, 0f);
    }

    public void Play()
    {
        gameObject.GetComponent<PlayableDirector>().Play();
    }
}
