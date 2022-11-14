using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-2)]
public class TARDIS : Singleton<TARDIS>
{
    // First of all Yes, name of this script is an Easter Egg. Cause I'm a Doctor Who fan.
    // It's not exactly like the TARDIS from the show, but it's close enough... Maybe?

    public WorldProperties.Timeline activeTimeline = WorldProperties.Timeline.Present;
    public WorldProperties.World activeWorld = WorldProperties.World.Untouched;
    public int orderOfAppearance = 1;
    
    public void SetDestination(WorldProperties.Timeline timeline, WorldProperties.World world, int order)
    {
        activeTimeline = timeline;
        activeWorld = world;
        orderOfAppearance = order;
    }
}
