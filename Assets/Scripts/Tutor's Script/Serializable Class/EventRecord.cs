using System;

[Serializable]
public class EventRecord
{
    public string eventName;
    public bool status;

    public EventRecord()
    {
            
    }

    public EventRecord(string eventName, bool status)
    {
        this.eventName = eventName;
        this.status = status;
    }
}
