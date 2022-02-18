using System;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class CombatScenarioEventDataController : MonoBehaviour
{
    public static CombatScenarioEventDataController Instance;
    private List<CombatScenarioEvent> combatScenarioEventDatabase;

    private void Start()
    {
        combatScenarioEventDatabase = new List<CombatScenarioEvent>();

    }
}

[Serializable]
public struct CombatScenarioEvent
{
    //scenario Id
    //event trigger type
    //event trigger character id
    //event trigger result...?

}