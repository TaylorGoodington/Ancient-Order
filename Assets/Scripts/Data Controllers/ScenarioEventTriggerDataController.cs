using System;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class ScenarioEventTriggerDataController : MonoBehaviour
{
    public static ScenarioEventTriggerDataController Instance;

    private List<ScenarioEventTriggerData> scenarioEventTriggerDatabase;
    private List<ScenarioEventTriggerData> currentScenarioEventTriggerList;

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
        scenarioEventTriggerDatabase = new List<ScenarioEventTriggerData>();
        currentScenarioEventTriggerList = new List<ScenarioEventTriggerData>();

        //scenarioEventTriggerDatabase.Add(new ScenarioEventTriggerData());

    }

    //Pulls togethere a list of event triggers for the current scenario
    public void InitializeCurrentScenarioEventTriggerList(int scenarioId)
    {
        currentScenarioEventTriggerList = scenarioEventTriggerDatabase.FindAll(x => x.scenarioId == scenarioId);
    }

    //Looks through the current scenario event trigger list and returns an event Id to the caller, if found.
    public int CheckForEventTrigger(int targetCharacterId, EventTriggerType eventTriggerType)
    {
        int eventId = currentScenarioEventTriggerList.Find(x => x.eventTriggerType == eventTriggerType && x.triggerCharacterId == targetCharacterId).eventId;

        return eventId;
    }
}

[Serializable]
public struct ScenarioEventTriggerData
{
    public int eventId;
    public int scenarioId;
    public EventTriggerType eventTriggerType;
    public int triggerCharacterId;

    public ScenarioEventTriggerData(int eventId, int scenarioId, EventTriggerType eventTriggerType, int triggerCharacterId)
    {
        this.eventId = eventId;
        this.scenarioId = scenarioId;
        this.eventTriggerType = eventTriggerType;
        this.triggerCharacterId = triggerCharacterId;
    }
}