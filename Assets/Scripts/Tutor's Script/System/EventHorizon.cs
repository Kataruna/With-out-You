using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHorizon : MonoBehaviour
{
    public Dictionary<string, bool> EventsHorizon => eventHorizon;
    
    public List<EventRecord> eventList = new List<EventRecord>();
    private Dictionary<string, bool> eventHorizon = new Dictionary<string, bool>();
    
    public static EventHorizon Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(Instance);
        
        foreach (var events in eventList)
        {
            eventHorizon[events.eventName] = events.status;
        }
    }

    public void UpdateEvent(string key, bool value)
    {
        if (eventHorizon.ContainsKey(key))
        {
            eventHorizon[key] = value;
            Debug.Log($"Updated {key} to {eventHorizon[key]}");

            eventList.Find((x) => x.eventName == key).status = value;
        }
        
        
        else Debug.LogWarning($"Can find any key name {key} in Event Horizon");
    }
}
