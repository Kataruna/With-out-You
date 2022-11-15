using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class ParallelWorld : MonoBehaviour
{
    public List<WorldLine> worldLines = new List<WorldLine>();
    private Dictionary<string, WorldLine> worldLineName = new Dictionary<string, WorldLine>();
    
    private string[] worldLineNames;

    private TARDIS _tardis;
    public static ParallelWorld Instance;

    private void Awake()
    {
        Instance = this;
        
        _tardis = TARDIS.Instance;

        int lines = worldLines.Count;

        worldLineNames = new string[lines];
        
        // foreach (var world in worldLines)
        // {
        //     string key = world.timeline.ToString() + world.worldLine.ToString() + world.worldOrder.ToString();
        //     
        //     Debug.Log($"WorldLine \"{key}\" have been added to the dictionary");
        //
        //     worldLineName[key] = world;
        // }

        for (int i = 0; i < lines; i++)
        {
            string key = worldLines[i].timeline.ToString() + worldLines[i].worldLine.ToString() + worldLines[i].worldOrder.ToString();
            
            Debug.Log($"WorldLine \"{key}\" have been added to the dictionary");

            worldLineNames[i] = key;
            worldLineName[key] = worldLines[i];
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