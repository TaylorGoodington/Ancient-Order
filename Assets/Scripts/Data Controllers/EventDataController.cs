using System;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class EventDataController : MonoBehaviour
{
    public static EventDataController Instance;

    private List<EventData> eventDatabase;
    private EventCaller eventCaller;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        eventDatabase = new List<EventData>();

        eventDatabase.Add(new EventData(0, EventName.Tutorial_1));

    }

    //Passed the event Id and executes code
    public void ProcessEvent(int id)
    {
        EventName eventName = eventDatabase.Find(x => x.id == id).eventName;

        if (eventName == EventName.Tutorial_1)
        {
            //instructions for event processing
        }
        else if (eventName == EventName.Tutorial_2)
        {
            //instructions for event processing
        }
    }


    private void EventComplete()
    {
        if (eventCaller == EventCaller.Combat_Control)
        {
            //tell combat control to continue with its shit
        }
    }
}

[Serializable]
public struct EventData
{
    public int id;
    public EventName eventName;

    public EventData(int id, EventName eventName)
    {
        this.id = id;
        this.eventName = eventName;
    }
}