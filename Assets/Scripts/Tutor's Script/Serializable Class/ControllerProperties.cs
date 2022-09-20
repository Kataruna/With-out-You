using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ControllerProperties
{
    public GameObject controller;
    public Type type;
    
    public enum Type
    {
        GameController,
        Setting,
        AudioSource,
        
    }
}
