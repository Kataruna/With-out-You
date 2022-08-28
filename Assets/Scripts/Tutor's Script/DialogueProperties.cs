using System;
using UnityEngine;

[Serializable]
public class DialogueProperties
{
    public Character character;
    public Mood mood;
    public string message;
    
    public enum Character
    {
        a,
        b,
    }

    public enum Mood
    {
        neutral,
        angry,
        sad,
        curious,
    }
}
