using System;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class CombatScneraioDataController : MonoBehaviour
{
    public static CombatScneraioDataController Instance;
    private List<CombatScenario> combatScenarioDatabase;

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
        combatScenarioDatabase = new List<CombatScenario>();

        //scenario 0 is reserved for calling in a fight at a gym.
        combatScenarioDatabase.Add(new CombatScenario(1, 0, 0));

    }

    public CombatScenario RetrieveCombatScenario(int scenarioId)
    {
        return combatScenarioDatabase.Find(x => x.id == scenarioId);
    }
}

[Serializable]
public struct CombatScenario
{
    public int id;
    public int teamId;
    public int setId;

    public CombatScenario(int id, int teamId, int setId)
    {
        this.id = id;
        this.teamId = teamId;
        this.setId = setId;
    }
}