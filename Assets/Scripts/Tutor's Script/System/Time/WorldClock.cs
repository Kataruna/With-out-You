using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldClock : MonoBehaviour
{
    [SerializeField, Range(0f,24f)] float timeOfDay;
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
        LightingManager.Instance.UpdateTime(timeOfDay);
    }

    [ContextMenu("Spin")]
    public void Spin()
    {
        LightingManager.Instance.UpdateTime(timeOfDay, round);
    }

    [ContextMenu("Skip")]
    public void Skip()
    {
        LightingManager.Instance.SkipToTime(timeOfDay);
    }
}
