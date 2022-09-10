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
    public Choices[] choices;
    
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

    public DialogueProperties(Mode mode, Character character, string name, Mood mood, string message)
    {
        this.mode = mode;
        this.character = character;
        this.name = name;
        this.mood = mood;
        this.message = message;
    }
}

[Serializable]
public class Choices
{
    [Tooltip("ข้อความที่จะขึ้นบนตัวเลือก")]public string choice;
    [Tooltip("ใส่แปลนบทสนทนาลงไป")]public Dialogue blueprint;
}
