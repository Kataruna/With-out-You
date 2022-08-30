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
        Silvia,
        January,
    }

    public enum Mood
    {
        Neutral,
        Angry,
        Sad,
        Curious,
    }
}
