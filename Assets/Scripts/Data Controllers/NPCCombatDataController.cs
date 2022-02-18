using System;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class NPCCombatDataController : MonoBehaviour
{
    //TODO
    //Rewrite controller to be structured around new scenario data. Location will be handeled by the set, and character models will be retrieved by the 
    //character model controller. This controller will only be responsible for storing and retreiving scenario data, and making adjustments to the data
    //if the player is calling in a fight against a scenario of a lower rank.

    public static NPCCombatDataController Instance;
    private List<NPCCombatData> npcCombatDatabase;

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
        npcCombatDatabase = new List<NPCCombatData>();

        npcCombatDatabase.Add(new NPCCombatData(0, 1, new Dictionary<CoreTrainingAttribute, float> { { CoreTrainingAttribute.Stamina, 0.34f }, { CoreTrainingAttribute.Power, 0.33f }, { CoreTrainingAttribute.Speed, 0.33f } }, new Dictionary<Priority, Reaction> { { Priority.Primary, Reaction.Dodge }, { Priority.Secondary, Reaction.Block }, { Priority.Tertiary, Reaction.Parry } }, new Dictionary<Priority, Path> { { Priority.Primary, Path.Light }, { Priority.Secondary, Path.Earth }, { Priority.Tertiary, Path.Fire } }));
        npcCombatDatabase.Add(new NPCCombatData(0, 2, new Dictionary<CoreTrainingAttribute, float> { { CoreTrainingAttribute.Stamina, 0.34f }, { CoreTrainingAttribute.Power, 0.33f }, { CoreTrainingAttribute.Speed, 0.33f } }, new Dictionary<Priority, Reaction> { { Priority.Primary, Reaction.Block }, { Priority.Secondary, Reaction.Dodge }, { Priority.Tertiary, Reaction.Parry } }, new Dictionary<Priority, Path> { { Priority.Primary, Path.Shadow }, { Priority.Secondary, Path.Water }, { Priority.Tertiary, Path.Light } }));
        npcCombatDatabase.Add(new NPCCombatData(0, 3, new Dictionary<CoreTrainingAttribute, float> { { CoreTrainingAttribute.Stamina, 0.34f }, { CoreTrainingAttribute.Power, 0.33f }, { CoreTrainingAttribute.Speed, 0.33f } }, new Dictionary<Priority, Reaction> { { Priority.Primary, Reaction.Parry }, { Priority.Secondary, Reaction.Dodge }, { Priority.Tertiary, Reaction.Block } }, new Dictionary<Priority, Path> { { Priority.Primary, Path.Air }, { Priority.Secondary, Path.Fire }, { Priority.Tertiary, Path.Shadow } }));
        npcCombatDatabase.Add(new NPCCombatData(1, 4, new Dictionary<CoreTrainingAttribute, float> { { CoreTrainingAttribute.Stamina, 0.34f }, { CoreTrainingAttribute.Power, 0.33f }, { CoreTrainingAttribute.Speed, 0.33f } }, new Dictionary<Priority, Reaction> { { Priority.Primary, Reaction.Block }, { Priority.Secondary, Reaction.Dodge }, { Priority.Tertiary, Reaction.Parry } }, new Dictionary<Priority, Path> { { Priority.Primary, Path.Earth }, { Priority.Secondary, Path.Fire }, { Priority.Tertiary, Path.Light } }));
        npcCombatDatabase.Add(new NPCCombatData(1, 5, new Dictionary<CoreTrainingAttribute, float> { { CoreTrainingAttribute.Stamina, 0.34f }, { CoreTrainingAttribute.Power, 0.33f }, { CoreTrainingAttribute.Speed, 0.33f } }, new Dictionary<Priority, Reaction> { { Priority.Primary, Reaction.Block }, { Priority.Secondary, Reaction.Dodge }, { Priority.Tertiary, Reaction.Parry } }, new Dictionary<Priority, Path> { { Priority.Primary, Path.Earth }, { Priority.Secondary, Path.Fire }, { Priority.Tertiary, Path.Light } }));
        npcCombatDatabase.Add(new NPCCombatData(1, 6, new Dictionary<CoreTrainingAttribute, float> { { CoreTrainingAttribute.Stamina, 0.34f }, { CoreTrainingAttribute.Power, 0.33f }, { CoreTrainingAttribute.Speed, 0.33f } }, new Dictionary<Priority, Reaction> { { Priority.Primary, Reaction.Block }, { Priority.Secondary, Reaction.Dodge }, { Priority.Tertiary, Reaction.Parry } }, new Dictionary<Priority, Path> { { Priority.Primary, Path.Earth }, { Priority.Secondary, Path.Fire }, { Priority.Tertiary, Path.Light } }));
        npcCombatDatabase.Add(new NPCCombatData(2, 7, new Dictionary<CoreTrainingAttribute, float> { { CoreTrainingAttribute.Stamina, 0.34f }, { CoreTrainingAttribute.Power, 0.33f }, { CoreTrainingAttribute.Speed, 0.33f } }, new Dictionary<Priority, Reaction> { { Priority.Primary, Reaction.Block }, { Priority.Secondary, Reaction.Dodge }, { Priority.Tertiary, Reaction.Parry } }, new Dictionary<Priority, Path> { { Priority.Primary, Path.Earth }, { Priority.Secondary, Path.Fire }, { Priority.Tertiary, Path.Light } }));
        npcCombatDatabase.Add(new NPCCombatData(2, 8, new Dictionary<CoreTrainingAttribute, float> { { CoreTrainingAttribute.Stamina, 0.34f }, { CoreTrainingAttribute.Power, 0.33f }, { CoreTrainingAttribute.Speed, 0.33f } }, new Dictionary<Priority, Reaction> { { Priority.Primary, Reaction.Block }, { Priority.Secondary, Reaction.Dodge }, { Priority.Tertiary, Reaction.Parry } }, new Dictionary<Priority, Path> { { Priority.Primary, Path.Earth }, { Priority.Secondary, Path.Fire }, { Priority.Tertiary, Path.Light } }));
        npcCombatDatabase.Add(new NPCCombatData(2, 9, new Dictionary<CoreTrainingAttribute, float> { { CoreTrainingAttribute.Stamina, 0.34f }, { CoreTrainingAttribute.Power, 0.33f }, { CoreTrainingAttribute.Speed, 0.33f } }, new Dictionary<Priority, Reaction> { { Priority.Primary, Reaction.Block }, { Priority.Secondary, Reaction.Dodge }, { Priority.Tertiary, Reaction.Parry } }, new Dictionary<Priority, Path> { { Priority.Primary, Path.Earth }, { Priority.Secondary, Path.Fire }, { Priority.Tertiary, Path.Light } }));
        npcCombatDatabase.Add(new NPCCombatData(3, 10, new Dictionary<CoreTrainingAttribute, float> { { CoreTrainingAttribute.Stamina, 0.4f }, { CoreTrainingAttribute.Power, 0.3f }, { CoreTrainingAttribute.Speed, 0.3f } }, new Dictionary<Priority, Reaction> { { Priority.Primary, Reaction.Dodge }, { Priority.Secondary, Reaction.Block }, { Priority.Tertiary, Reaction.Parry } }, new Dictionary<Priority, Path> { { Priority.Primary, Path.Shadow }, { Priority.Secondary, Path.Earth }, { Priority.Tertiary, Path.Water } }));
        npcCombatDatabase.Add(new NPCCombatData(3, 11, new Dictionary<CoreTrainingAttribute, float> { { CoreTrainingAttribute.Stamina, 0.3f }, { CoreTrainingAttribute.Power, 0.4f }, { CoreTrainingAttribute.Speed, 0.3f } }, new Dictionary<Priority, Reaction> { { Priority.Primary, Reaction.Block }, { Priority.Secondary, Reaction.Dodge }, { Priority.Tertiary, Reaction.Parry } }, new Dictionary<Priority, Path> { { Priority.Primary, Path.Air }, { Priority.Secondary, Path.Water }, { Priority.Tertiary, Path.Earth } }));
        npcCombatDatabase.Add(new NPCCombatData(3, 12, new Dictionary<CoreTrainingAttribute, float> { { CoreTrainingAttribute.Stamina, 0.3f }, { CoreTrainingAttribute.Power, 0.3f }, { CoreTrainingAttribute.Speed, 0.4f } }, new Dictionary<Priority, Reaction> { { Priority.Primary, Reaction.Parry }, { Priority.Secondary, Reaction.Dodge }, { Priority.Tertiary, Reaction.Block } }, new Dictionary<Priority, Path> { { Priority.Primary, Path.Fire }, { Priority.Secondary, Path.Light }, { Priority.Tertiary, Path.Air } }));

    }

    public List<int> IdentifyOpponentCharacterIdsForCombatScene(int teamId)
    {
        List<int> opponentIds = new List<int>();
        int counter = 0;

        foreach (NPCCombatData opponent in npcCombatDatabase)
        {
            if (opponent.teamId == teamId)
            {
                opponentIds.Add(opponent.characterId);
                counter++;
            }
        }

        return opponentIds;
    }

    public NPCCombatData GetNPCCombatData(int characterId)
    {
        return npcCombatDatabase.Find(x => x.characterId == characterId);
    }
}

[Serializable]
public struct NPCCombatData
{
    public int teamId;
    public int characterId;
    public Dictionary<CoreTrainingAttribute, float> attributeEmphasisList;
    public Dictionary<Priority, Reaction> reactionProgressionPrioitiesList;
    public Dictionary<Priority, Path> pathAttunementPrioritiesList;

    public NPCCombatData(int teamId, int characterId, Dictionary<CoreTrainingAttribute, float> attributeEmphasisList, Dictionary<Priority, Reaction> reactionProgressionPrioitiesList,
    Dictionary<Priority, Path> pathAttunementPrioritiesList)
    {
        this.teamId = teamId;
        this.characterId = characterId;
        this.attributeEmphasisList = attributeEmphasisList;
        this.reactionProgressionPrioitiesList = reactionProgressionPrioitiesList;
        this.pathAttunementPrioritiesList = pathAttunementPrioritiesList;
    }
}