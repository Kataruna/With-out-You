using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SongDetail
{
    public string trackName;
    public string artist;
    public AudioClip trackFile;
    [Range(0f, 1f)] public float defaultVolume;

    public SongDetail()
    {
        trackName = null;
        artist = null;
        defaultVolume = 0.5f;
    }
}
