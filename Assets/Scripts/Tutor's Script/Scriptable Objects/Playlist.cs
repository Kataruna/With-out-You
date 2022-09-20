using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Presentation/Playlist")]
public class Playlist : ScriptableObject
{
    public SongDetail[] song = new SongDetail[1] {new SongDetail()};
}
