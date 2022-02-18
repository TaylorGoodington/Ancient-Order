using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using static Utility;

public class SaveDataController : MonoBehaviour
{
    public static SaveDataController Instance;

    private List<SaveFileSaveData> allSaveData;

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
        //TODO Remove after testing
        Debug.Log(Application.persistentDataPath);
        File.Delete(Application.persistentDataPath + "/Game_Data.sav");

        InitializeGameData();
    }

    private void InitializeGameData()
    {
        allSaveData = new List<SaveFileSaveData>();

        if (File.Exists(Application.persistentDataPath + "/Game_Data.sav"))
        {
            FileStream saveFile = File.Open(Application.persistentDataPath + "/Game_Data.sav", FileMode.Open);
            using (saveFile)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                allSaveData = (List<SaveFileSaveData>)binaryFormatter.Deserialize(saveFile);
            }
        }
        else
        {
            //First Execution Creates the save file

            BuildInitialGameData();

            WriteAllSaveData();
        }
    }

    private void BuildInitialGameData()
    {
        SaveData saveData = new SaveData();

        List<PlayerCharactersReactionData> playerReactionData = new List<PlayerCharactersReactionData>();
        playerReactionData.Add(new PlayerCharactersReactionData(Reaction.Block, 0, 1));
        playerReactionData.Add(new PlayerCharactersReactionData(Reaction.Dodge, 0, 1));
        playerReactionData.Add(new PlayerCharactersReactionData(Reaction.Parry, 0, 1));
        saveData.playerReactionData = playerReactionData;

        List<PlayerCharactersPathData> playerPathData = new List<PlayerCharactersPathData>();
        playerPathData.Add(new PlayerCharactersPathData(Utility.Path.Fire, 0, 0));
        playerPathData.Add(new PlayerCharactersPathData(Utility.Path.Water, 0, 0));
        playerPathData.Add(new PlayerCharactersPathData(Utility.Path.Air, 0, 0));
        playerPathData.Add(new PlayerCharactersPathData(Utility.Path.Earth, 0, 0));
        playerPathData.Add(new PlayerCharactersPathData(Utility.Path.Shadow, 0, 0));
        playerPathData.Add(new PlayerCharactersPathData(Utility.Path.Light, 0, 0));
        saveData.playerPathData = playerPathData;

        List<PlayerCharactersPersonalityData> playerPersonalityData = new List<PlayerCharactersPersonalityData>();
        playerPersonalityData.Add(new PlayerCharactersPersonalityData(PersonalityCategory.Patience, 0.16f));
        playerPersonalityData.Add(new PlayerCharactersPersonalityData(PersonalityCategory.Empathy, 0.16f));
        playerPersonalityData.Add(new PlayerCharactersPersonalityData(PersonalityCategory.Cunning, 0.16f));
        playerPersonalityData.Add(new PlayerCharactersPersonalityData(PersonalityCategory.Logic, 0.16f));
        playerPersonalityData.Add(new PlayerCharactersPersonalityData(PersonalityCategory.Kindness, 0.16f));
        playerPersonalityData.Add(new PlayerCharactersPersonalityData(PersonalityCategory.Charisma, 0.16f));
        saveData.playerPersonalityData = playerPersonalityData;

        List<CharacterCoreTrainingAttributeData> playerCoreTrainingAttributeData = new List<CharacterCoreTrainingAttributeData>();
        playerCoreTrainingAttributeData.Add(new CharacterCoreTrainingAttributeData(CoreTrainingAttribute.Stamina, 5));
        playerCoreTrainingAttributeData.Add(new CharacterCoreTrainingAttributeData(CoreTrainingAttribute.Power, 5));
        playerCoreTrainingAttributeData.Add(new CharacterCoreTrainingAttributeData(CoreTrainingAttribute.Speed, 5));
        saveData.playerCoreTrainingAttributeData = playerCoreTrainingAttributeData;

        Dictionary<Priority, Utility.Path> playerPathPriority = new Dictionary<Priority, Utility.Path> { { Priority.Primary, Utility.Path.Fire }, { Priority.Secondary, Utility.Path.Water } };
        saveData.playerPathPriority = playerPathPriority;
        RankDegree playerRankDegree = RankDegree.First;
        saveData.playerRankDegree = playerRankDegree;
        RankEchelon playerRankEchelon = RankEchelon.Stone;
        saveData.playerRankEchelon = playerRankEchelon;

        List<int> recruitedPartyMemberCharacterIds = new List<int>();
        recruitedPartyMemberCharacterIds.Add(INITIAL_ALLY_LEFT_CHARACTER_ID); ;
        recruitedPartyMemberCharacterIds.Add(INITIAL_ALLY_RIGHT_CHARACTER_ID);
        saveData.recruitedPartyMemberCharacterIds = recruitedPartyMemberCharacterIds;

        List<PartyMemberPosition> partyMemberPositions = new List<PartyMemberPosition>();
        partyMemberPositions.Add(new PartyMemberPosition(Utility.PartyMemberPosition.Left, INITIAL_ALLY_LEFT_CHARACTER_ID));
        partyMemberPositions.Add(new PartyMemberPosition(Utility.PartyMemberPosition.Right, INITIAL_ALLY_RIGHT_CHARACTER_ID));
        saveData.partyMemberPositions = partyMemberPositions;


        List<PartyMemberCombatData> partyMemberCombatData = new List<PartyMemberCombatData>();

        #region Starter Ally 1 Combat Data Initialization

        List<PlayerCharactersReactionData> reactionData = new List<PlayerCharactersReactionData>();
        reactionData.Add(new PlayerCharactersReactionData(Reaction.Block, 0, 1));
        reactionData.Add(new PlayerCharactersReactionData(Reaction.Dodge, 0, 1));
        reactionData.Add(new PlayerCharactersReactionData(Reaction.Parry, 0, 1));

        List<PlayerCharactersPathData> pathData = new List<PlayerCharactersPathData>();
        pathData.Add(new PlayerCharactersPathData(Utility.Path.Fire, 0, 0));
        pathData.Add(new PlayerCharactersPathData(Utility.Path.Water, 0, 0));
        pathData.Add(new PlayerCharactersPathData(Utility.Path.Air, 0, 0));
        pathData.Add(new PlayerCharactersPathData(Utility.Path.Earth, 0, 0));
        pathData.Add(new PlayerCharactersPathData(Utility.Path.Shadow, 0, 0));
        pathData.Add(new PlayerCharactersPathData(Utility.Path.Light, 0, 0));

        List<CharacterCoreTrainingAttributeData> coreTrainingAttributeData = new List<CharacterCoreTrainingAttributeData>();
        coreTrainingAttributeData.Add(new CharacterCoreTrainingAttributeData(CoreTrainingAttribute.Stamina, 5));
        coreTrainingAttributeData.Add(new CharacterCoreTrainingAttributeData(CoreTrainingAttribute.Power, 5));
        coreTrainingAttributeData.Add(new CharacterCoreTrainingAttributeData(CoreTrainingAttribute.Speed, 5));

        Dictionary<Priority, Utility.Path> pathPriorities = new Dictionary<Priority, Utility.Path> { { Priority.Primary, Utility.Path.Air }, { Priority.Secondary, Utility.Path.Light } };

        partyMemberCombatData.Add(new PartyMemberCombatData(INITIAL_ALLY_LEFT_CHARACTER_ID, reactionData, pathData, coreTrainingAttributeData, pathPriorities));

        #endregion

        #region Starter Ally 2 Combat Data Initialization

        reactionData = new List<PlayerCharactersReactionData>();
        reactionData.Add(new PlayerCharactersReactionData(Reaction.Block, 0, 1));
        reactionData.Add(new PlayerCharactersReactionData(Reaction.Dodge, 0, 1));
        reactionData.Add(new PlayerCharactersReactionData(Reaction.Parry, 0, 1));

        pathData = new List<PlayerCharactersPathData>();
        pathData.Add(new PlayerCharactersPathData(Utility.Path.Fire, 0, 0));
        pathData.Add(new PlayerCharactersPathData(Utility.Path.Water, 0, 0));
        pathData.Add(new PlayerCharactersPathData(Utility.Path.Air, 0, 0));
        pathData.Add(new PlayerCharactersPathData(Utility.Path.Earth, 0, 0));
        pathData.Add(new PlayerCharactersPathData(Utility.Path.Shadow, 0, 0));
        pathData.Add(new PlayerCharactersPathData(Utility.Path.Light, 0, 0));

        coreTrainingAttributeData = new List<CharacterCoreTrainingAttributeData>();
        coreTrainingAttributeData.Add(new CharacterCoreTrainingAttributeData(CoreTrainingAttribute.Stamina, 5));
        coreTrainingAttributeData.Add(new CharacterCoreTrainingAttributeData(CoreTrainingAttribute.Power, 5));
        coreTrainingAttributeData.Add(new CharacterCoreTrainingAttributeData(CoreTrainingAttribute.Speed, 5));

        pathPriorities = new Dictionary<Priority, Utility.Path> { { Priority.Primary, Utility.Path.Earth }, { Priority.Secondary, Utility.Path.Shadow } };

        partyMemberCombatData.Add(new PartyMemberCombatData(INITIAL_ALLY_RIGHT_CHARACTER_ID, reactionData, pathData, coreTrainingAttributeData, pathPriorities));

        #endregion

        saveData.partyMemberCombatData = partyMemberCombatData;


        allSaveData.Add(new SaveFileSaveData(SaveSlot.Aplha, saveData));
        allSaveData.Add(new SaveFileSaveData(SaveSlot.Beta, saveData));
        allSaveData.Add(new SaveFileSaveData(SaveSlot.Omega, saveData));
    }

    private void WriteAllSaveData()
    {
        FileStream saveFile = File.Create(Application.persistentDataPath + "/Game_Data.sav");
        using (saveFile)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(saveFile, allSaveData);
        }
    }

    public void SaveGame(SaveData saveData)
    {
        //TODO Write code for putting save data from MasterControl back into the allsaveData List.

        WriteAllSaveData();
    }

    public SaveData RetrieveSaveData(SaveSlot saveSlot)
    {
        SaveData saveData = allSaveData.Find(x => x.saveSlot == saveSlot).saveData;

        return saveData;
    }
}

[Serializable]
public struct SaveData
{
    #region Player Data
    public List<PlayerCharactersReactionData> playerReactionData;
    public List<PlayerCharactersPathData> playerPathData;
    public List<PlayerCharactersPersonalityData> playerPersonalityData;
    public List<CharacterCoreTrainingAttributeData> playerCoreTrainingAttributeData;
    public Dictionary<Priority, Utility.Path> playerPathPriority;
    public RankDegree playerRankDegree;
    public RankEchelon playerRankEchelon;
    #endregion

    #region Party Member Data
    public List<int> recruitedPartyMemberCharacterIds;
    public List<PartyMemberPosition> partyMemberPositions;
    public List<PartyMemberCombatData> partyMemberCombatData;
    #endregion

    //public Location playerLocation;

    public SaveData(List<PlayerCharactersReactionData> playerReactionData, List<PlayerCharactersPathData> playerPathData, List<PlayerCharactersPersonalityData> playerPersonalityData, 
        List<CharacterCoreTrainingAttributeData> playerCoreTrainingAttributeData, Dictionary<Priority, Utility.Path> playerPathPriority, RankDegree playerRankDegree, RankEchelon playerRankEchelon, 
        List<int> recruitedPartyMemberCharacterIds, List<PartyMemberPosition> partyMemberPositions, List<PartyMemberCombatData> partyMemberCombatData)
    {
        this.playerReactionData = playerReactionData;
        this.playerPathData = playerPathData;
        this.playerPersonalityData = playerPersonalityData;
        this.playerCoreTrainingAttributeData = playerCoreTrainingAttributeData;
        this.playerPathPriority = playerPathPriority;
        this.playerRankDegree = playerRankDegree;
        this.playerRankEchelon = playerRankEchelon;

        this.recruitedPartyMemberCharacterIds = recruitedPartyMemberCharacterIds;
        this.partyMemberPositions = partyMemberPositions;
        this.partyMemberCombatData = partyMemberCombatData;
    }
}

[Serializable]
public struct SaveFileSaveData
{
    public SaveSlot saveSlot;
    public SaveData saveData;

    public SaveFileSaveData(SaveSlot saveSlot, SaveData saveData)
    {
        this.saveSlot = saveSlot;
        this.saveData = saveData;
    }
}