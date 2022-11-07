using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelWorld : MonoBehaviour
{
    public List<WorldLine> worldLines = new List<WorldLine>();
    private Dictionary<string, WorldLine> worldLineName = new Dictionary<string, WorldLine>();

    private TARDIS _tardis;

    private void Awake()
    {
        _tardis = TARDIS.Instance;
        
        foreach (var world in worldLines)
        {
            string key = world.timeline.ToString() + world.worldLine.ToString() + world.worldOrder.ToString();
            
            Debug.Log($"WorldLine \"{key}\" have been added to the dictionary");

            worldLineName[key] = world;
        }
    }

    private void Start()
    {
        TimelineJump();
    }

    [ContextMenu("Timeline Jump")]
    public void TimelineJump()
    {
        string key = _tardis.activeTimeline.ToString() + _tardis.activeWorld.ToString() +
                     _tardis.orderOfAppearance.ToString();
        
        Debug.Log($"Load \"{key}\"");
        
        foreach (var world in worldLines)
        {
            if(world.worldLineObject.activeSelf) world.worldLineObject.SetActive(false);
        }
        
        worldLineName[key].worldLineObject.SetActive(true);
        Debug.Log("Loaded");
    }
}