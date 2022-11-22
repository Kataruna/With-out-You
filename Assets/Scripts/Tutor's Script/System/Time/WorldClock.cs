using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldClock : MonoBehaviour
{
    public static WorldClock Instance;
    [SerializeField, Range(0f,24f)] float timeOfDay;
    [SerializeField] private int round;

    private LightingManager _sky;
    private void Awake()
    {
        Instance = this;
        
        _sky = GetComponent<LightingManager>();
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
}
