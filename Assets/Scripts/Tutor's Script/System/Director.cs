using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Director : MonoBehaviour
{
    public static Director Instance;

    public Dictionary<string, PlayableDirector> Actors => _actors;

    private Animatronics[] _users;
    private Dictionary<string, PlayableDirector> _actors = new Dictionary<string, PlayableDirector>();

    private void Awake()
    {
        Instance = this;
        
        _users = FindObjectsOfType<Animatronics>();

        foreach (Animatronics user in _users)
        {
            Recruit(user);
        }
    }
    
    private void Recruit(Animatronics user)
    {
        _actors.Add(user.ActorName.ToString().ToLower(), user.GetComponent<PlayableDirector>());
    }
}
