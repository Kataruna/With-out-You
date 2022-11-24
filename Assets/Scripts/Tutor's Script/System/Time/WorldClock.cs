using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(4)]
public class WorldClock : MonoBehaviour
{
    [SerializeField, Range(0f,23f)] int hour;
    [SerializeField, Range(0f,59f)] int minute;
    [SerializeField] private int round;
    [SerializeField, Tooltip("โว่ โหหหหหหหหหหห")] private bool noAnimation;
    
    private void OnEnable()
    {
        if(!noAnimation) FastForward();
        else Skip();
    }

    [ContextMenu("Fast Forward")]
    public void FastForward()
    {
        UniStorm.UniStormManager.Instance.FastForwardTo(hour, minute);
    }

    // [ContextMenu("Spin")]
    // public void Spin()
    // {
    //     LightingManager.Instance.UpdateTime(timeOfDay, round);
    // }

    [ContextMenu("Skip")]
    public void Skip()
    { 
        UniStorm.UniStormManager.Instance.SetTime(hour, minute);
    }
}
