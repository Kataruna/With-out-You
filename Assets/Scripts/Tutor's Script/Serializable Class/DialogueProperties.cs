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
}

[Serializable]
public class Choices
{
    [Tooltip("ข้อความที่จะขึ้นบนตัวเลือก")] public string choice;
    [Tooltip("ใส่แปลนบทสนทนาลงไป")] public Dialogue blueprint;
    [Tooltip("ระบุชื่อของเหตุการแบบ Camel Case (goodChoice)\n*เว้นว่างไว้หากไม่ใช้*")] public string eventKey;
    [Tooltip("ตั้งค่าเหตุการณ์ว่าเหตุการณ์ด้านบนจะทำงาน หรือปิดการทำงาน")] public bool eventValue;
}
