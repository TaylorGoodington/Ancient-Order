using System;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class CombatSetDataController : MonoBehaviour
{
    public static CombatSetDataController Instance;
    private List<CombatSetData> combatSetDatabase;

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
        GenerateCombatSetInformation();
    }

    //Loops through all the children and make a list to work from.
    private void GenerateCombatSetInformation()
    {
        combatSetDatabase = new List<CombatSetData>();

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            combatSetDatabase.Add(new CombatSetData(child.GetComponent<CombatSetObject>().id, child.gameObject));
        }
    }

    public GameObject RetrieveSet(int setId)
    {
        GameObject set = combatSetDatabase.Find(x => x.id == setId).setObject;
        return set;
    }

}

[Serializable]
public struct CombatSetData
{
    public int id;
    public GameObject setObject;

    public CombatSetData(int id, GameObject setObject)
    {
        this.id = id;
        this.setObject = setObject;
    }
}