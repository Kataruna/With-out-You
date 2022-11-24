using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;

[Serializable]
public class DialogueProperties
{
    public Mode mode;
    public Character character;
    public string name;
    public Mood mood;
    public string message;
    public Choices[] choices;
    public string eventKey;
    public bool eventStatus;
    public UnityEvent events;
    public WorldProperties.Timeline timeline;
    public WorldProperties.World world;
    public bool doChangeOnThisState;
    public Animatronics.Characters animatronic;
    public TimelineAsset actionScript;
    public int targetTime;

    public enum Character
    {
        Silvia,
        January,
        Jane,
        Silvy,
        Unknown,
    }

    public enum Mood
    {
        Neutral,
        Angry,
        Sad,
        Curious,
        Happy,
        None,
    }

    public enum Mode
    {
        MainCharacter,
        SideCharacter,
        Choice,
        SwitchMood,
        UpdateEvent,
        Event,
        TimelineChange,
        Action,
    }
}

[Serializable]
public class Choices
{
    [Tooltip("ข้อความที่จะขึ้นบนตัวเลือก")] public string choice;
    [Tooltip("ใส่แปลนบทสนทนาลงไป")] public Dialogue blueprint;
    [Tooltip("ระบุชื่อของเหตุการแบบ Camel Case (goodChoice)\n*เว้นว่างไว้หากไม่ใช้*")] public string eventKey;
    [Tooltip("ตั้งค่าเหตุการณ์ว่าเหตุการณ์ด้านบนจะทำงาน หรือปิดการทำงาน")] public bool eventValue;
}
