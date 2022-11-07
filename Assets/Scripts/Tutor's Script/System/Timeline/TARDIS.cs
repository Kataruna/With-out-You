using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TARDIS : Singleton<TARDIS>
{
    // First of all Yes, name of this script is an Easter Egg. Cause I'm a Doctor Who fan.
    // It's not exactly like the TARDIS from the show, but it's close enough... Maybe?

    public WorldProperties.Timeline activeTimeline;
    public WorldProperties.World activeWorld;
    public int orderOfAppearance;
}
