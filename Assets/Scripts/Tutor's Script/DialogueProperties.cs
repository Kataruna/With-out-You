using System;
using UnityEngine;

[Serializable]
public class DialogueProperties
{
    public Mode mode;
    public Character character;
    public string name;
    public Mood mood;
    public string message;
    public string[] choices;
    
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

    public enum Mode
    {
        MainCharacter,
        SideCharacter,
        Choice
    }
}
