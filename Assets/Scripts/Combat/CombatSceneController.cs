using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Utility;

//Controller is responsible for arranging the scene for combat. This includes:
//Gathering the correct models
//Getting the correct set
//putting the models on the set
//starting the set music
//and telling the other controllers to take back their pieces when combat wraps up

public class CombatSceneController : MonoBehaviour
{
    public static CombatSceneController Instance;

    private GameObject targetSet = null;
    private Dictionary<int, GameObject> combatantCharacterIdAndModelDictionary;

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

    public void ArrangeSceneFromScenario(int setId, List<int> characterIds)
    {
        combatantCharacterIdAndModelDictionary = new Dictionary<int, GameObject>();

        //Finds the set
        targetSet = CombatSetDataController.Instance.RetrieveSet(setId);
        
        //Starts the combat music
        MusicManager.Instance.StartCombatMusic(targetSet.GetComponent<CombatSetObject>().music);

        //Retrieves the model Id List
        List<int> combatantModelIdList = CharacterDataController.Instance.RetreiveCharacterModelIds(characterIds);

        //Retrieves the models
        List<GameObject> models = ModelDataController.Instance.RetrieveTargetModels(combatantModelIdList);

        for (int i = 0; i < characterIds.Count; i++)
        {
            combatantCharacterIdAndModelDictionary.Add(characterIds[i], models[i]);
        }

        //place combatants on set
        targetSet.GetComponent<CombatSetObject>().PlaceModels(models);
    }

    public void SetCombatantAnimationState(int characterId, CombatantAnimationState animationState)
    {
        combatantCharacterIdAndModelDictionary[characterId].GetComponent<Animator>().Play(animationState.ToString());
    }

    public void BreakDownScene()
    {

    }
}