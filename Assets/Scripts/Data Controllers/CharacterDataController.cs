using System;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class CharacterDataController : MonoBehaviour
{
    public static CharacterDataController Instance;
    private List<CharacterData> characterDatabase;
    private List<int> potentialPartyMembers;

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
        potentialPartyMembers = new List<int>();
        potentialPartyMembers.Add(712);
        potentialPartyMembers.Add(713);
        potentialPartyMembers.Add(714);

        characterDatabase = new List<CharacterData>();

        #region NPCs
        characterDatabase.Add(new CharacterData(1, "Freddy", 1, new Dictionary<PersonalityCategory, float> { { PersonalityCategory.Patience, 0.2f }, { PersonalityCategory.Empathy, 0.1f }, { PersonalityCategory.Cunning, 0.2f }, { PersonalityCategory.Logic, 0.2f }, { PersonalityCategory.Kindness, 0.2f }, { PersonalityCategory.Charisma, 0.1f } }));
        characterDatabase.Add(new CharacterData(2, "Bob", 2, new Dictionary<PersonalityCategory, float> { { PersonalityCategory.Patience, 0.05f }, { PersonalityCategory.Empathy, 0.05f }, { PersonalityCategory.Cunning, 0.05f }, { PersonalityCategory.Logic, 0.15f }, { PersonalityCategory.Kindness, 0.35f }, { PersonalityCategory.Charisma, 0.35f } }));
        characterDatabase.Add(new CharacterData(3, "Craig", 3, new Dictionary<PersonalityCategory, float> { { PersonalityCategory.Patience, 0.35f }, { PersonalityCategory.Empathy, 0.35f }, { PersonalityCategory.Cunning, 0.15f }, { PersonalityCategory.Logic, 0.05f }, { PersonalityCategory.Kindness, 0.05f }, { PersonalityCategory.Charisma, 0.05f } }));
        characterDatabase.Add(new CharacterData(4, "Jake", 4, new Dictionary<PersonalityCategory, float> { { PersonalityCategory.Patience, 0.15f }, { PersonalityCategory.Empathy, 0.27f }, { PersonalityCategory.Cunning, 0.24f }, { PersonalityCategory.Logic, 0.11f }, { PersonalityCategory.Kindness, 0.18f }, { PersonalityCategory.Charisma, 0.05f } }));
        characterDatabase.Add(new CharacterData(5, "Jim", 5, new Dictionary<PersonalityCategory, float> { { PersonalityCategory.Patience, 0.23f }, { PersonalityCategory.Empathy, 0.2f }, { PersonalityCategory.Cunning, 0.14f }, { PersonalityCategory.Logic, 0.11f }, { PersonalityCategory.Kindness, 0.06f }, { PersonalityCategory.Charisma, 0.26f } }));
        characterDatabase.Add(new CharacterData(6, "Jordan", 6, new Dictionary<PersonalityCategory, float> { { PersonalityCategory.Patience, 0.17f }, { PersonalityCategory.Empathy, 0.21f }, { PersonalityCategory.Cunning, 0.11f }, { PersonalityCategory.Logic, 0.34f }, { PersonalityCategory.Kindness, 0.11f }, { PersonalityCategory.Charisma, 0.06f } }));
        characterDatabase.Add(new CharacterData(7, "Jake", 4, new Dictionary<PersonalityCategory, float> { { PersonalityCategory.Patience, 0.18f }, { PersonalityCategory.Empathy, 0.07f }, { PersonalityCategory.Cunning, 0.23f }, { PersonalityCategory.Logic, 0.09f }, { PersonalityCategory.Kindness, 0.31f }, { PersonalityCategory.Charisma, 0.12f } }));
        characterDatabase.Add(new CharacterData(8, "Jim", 5, new Dictionary<PersonalityCategory, float> { { PersonalityCategory.Patience, 0.07f }, { PersonalityCategory.Empathy, 0.05f }, { PersonalityCategory.Cunning, 0.26f }, { PersonalityCategory.Logic, 0.05f }, { PersonalityCategory.Kindness, 0.17f }, { PersonalityCategory.Charisma, 0.4f } }));
        characterDatabase.Add(new CharacterData(9, "Jordan", 6, new Dictionary<PersonalityCategory, float> { { PersonalityCategory.Patience, 0.21f }, { PersonalityCategory.Empathy, 0.32f }, { PersonalityCategory.Cunning, 0.09f }, { PersonalityCategory.Logic, 0.05f }, { PersonalityCategory.Kindness, 0.15f }, { PersonalityCategory.Charisma, 0.18f } }));
        characterDatabase.Add(new CharacterData(10, "Taylor", 7, new Dictionary<PersonalityCategory, float> { { PersonalityCategory.Patience, 0.33f }, { PersonalityCategory.Empathy, 0.05f }, { PersonalityCategory.Cunning, 0.34f }, { PersonalityCategory.Logic, 0.07f }, { PersonalityCategory.Kindness, 0.06f }, { PersonalityCategory.Charisma, 0.15f } }));
        characterDatabase.Add(new CharacterData(11, "Katelyn", 8, new Dictionary<PersonalityCategory, float> { { PersonalityCategory.Patience, 0.09f }, { PersonalityCategory.Empathy, 0.2f }, { PersonalityCategory.Cunning, 0.23f }, { PersonalityCategory.Logic, 0.18f }, { PersonalityCategory.Kindness, 0.12f }, { PersonalityCategory.Charisma, 0.18f } }));
        characterDatabase.Add(new CharacterData(12, "Luna", 9, new Dictionary<PersonalityCategory, float> { { PersonalityCategory.Patience, 0.29f }, { PersonalityCategory.Empathy, 0.34f }, { PersonalityCategory.Cunning, 0.21f }, { PersonalityCategory.Logic, 0.06f }, { PersonalityCategory.Kindness, 0.05f }, { PersonalityCategory.Charisma, 0.05f } }));

        #endregion

        #region PCs
        characterDatabase.Add(new CharacterData(712, "Huey", 4, new Dictionary<PersonalityCategory, float> { { PersonalityCategory.Patience, 0.11f }, { PersonalityCategory.Empathy, 0.11f }, { PersonalityCategory.Cunning, 0.3f }, { PersonalityCategory.Logic, 0.22f }, { PersonalityCategory.Kindness, 0.09f }, { PersonalityCategory.Charisma, 0.17f } }));
        characterDatabase.Add(new CharacterData(713, "Sluth", 5, new Dictionary<PersonalityCategory, float> { { PersonalityCategory.Patience, 0.09f }, { PersonalityCategory.Empathy, 0.2f }, { PersonalityCategory.Cunning, 0.21f }, { PersonalityCategory.Logic, 0.34f }, { PersonalityCategory.Kindness, 0.08f }, { PersonalityCategory.Charisma, 0.08f } }));
        characterDatabase.Add(new CharacterData(714, "Pop", 15, new Dictionary<PersonalityCategory, float> { { PersonalityCategory.Patience, 0.22f }, { PersonalityCategory.Empathy, 0.08f }, { PersonalityCategory.Cunning, 0.29f }, { PersonalityCategory.Logic, 0.05f }, { PersonalityCategory.Kindness, 0.17f }, { PersonalityCategory.Charisma, 0.19f } }));

        #endregion
    }

    public List<int> RetreiveCharacterModelIds(List<int> characterIds)
    {
        List<int> modelIds = new List<int>();

        foreach (int characterId in characterIds)
        {
            modelIds.Add(characterDatabase.Find(x => x.id == characterId).modelId);
        }

        return modelIds;
    }

    public float FindPersonalityValue(int characterId, PersonalityCategory personalityCategory)
    {
        CharacterData characterData = characterDatabase.Find(x => x.id == characterId);
        return characterData.personalityList[personalityCategory];
    }
}

[Serializable]
public struct CharacterData
{
    public int id;
    public string name;
    public int modelId;
    public Dictionary<PersonalityCategory, float> personalityList;

    public CharacterData(int id, string name, int modelId, Dictionary<PersonalityCategory, float> personalityList)
    {
        this.id = id;
        this.name = name;
        this.modelId = modelId;
        this.personalityList = personalityList;
    }
}