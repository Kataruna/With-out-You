using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WorldLine
{
    public GameObject worldLineObject;
    public WorldProperties.Timeline timeline;
    public WorldProperties.World worldLine;
    public int worldOrder;
}

public class WorldProperties
{
    public enum Timeline
    {
        Past,
        Present
    }
    
    public enum World
    {
        Untouched,
        Modified
    }
}
