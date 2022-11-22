using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Director : MonoBehaviour
{
    public Animatronics Animatronic => _animatronic;
    Animatronics _animatronic;
    
    private void OnEnable()
    {
        _animatronic = transform.GetComponentInChildren<Animatronics>();
    }
}
