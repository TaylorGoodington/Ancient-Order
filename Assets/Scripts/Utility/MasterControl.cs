using UnityEngine;
using System.Collections.Generic;
using static Utility;

public class MasterControl : MonoBehaviour
{
    public static MasterControl Instance;

    private SaveSlot currentFile;
    private InputStates inputState;
    private OverworldController overworldPlayer;

    #region Player Data
    private List<PlayerCharactersReactionData> playerReactionData;
    private List<PlayerCharactersPathData> playerPathData;
    private List<PlayerCharactersPersonalityData> playerPersonalityData;
    private List<CharacterCoreTrainingAttributeData> playerCoreTrainingAttributeData;
    Dictionary<Priority, Path> playerPathPriority;
    private RankDegree playerRankDegree;
    private RankEchelon playerRankEchelon;
    #endregion

    #region Party Member Data
    private List<int> recruitedPartyMemberCharacterIds;
    private List<PartyMemberPosition> partyMemberPositions;
    private List<PartyMemberCombatData> partyMemberCombatData;
    #endregion

    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        #region List Initialization

        #region Player Data
        playerReactionData = new List<PlayerCharactersReactionData>();
        playerPathData = new List<PlayerCharactersPathData>();
        playerPersonalityData = new List<PlayerCharactersPersonalityData>();
        playerCoreTrainingAttributeData = new List<CharacterCoreTrainingAttributeData>();
        #endregion

        #region Party Member Data
        recruitedPartyMemberCharacterIds = new List<int>();
        partyMemberPositions = new List<PartyMemberPosition>();
        #endregion

        #endregion

        #region Testing
        currentFile = SaveSlot.Aplha;
        overworldPlayer = GameObject.FindGameObjectWithTag("Overworld_Controller").GetComponent<OverworldController>();
        inputState = InputStates.Overworld;
        CameraManager.Instance.ActivateOverWorldCamera();
        #endregion
    }

    public void SelectGameData()
    {
        SaveData saveData = SaveDataController.Instance.RetrieveSaveData(currentFile);

        #region Player Data
        playerReactionData = saveData.playerReactionData;
        playerPathData = saveData.playerPathData;
        playerPersonalityData = saveData.playerPersonalityData;
        playerCoreTrainingAttributeData = saveData.playerCoreTrainingAttributeData;
        playerPathPriority = saveData.playerPathPriority;
        playerRankDegree = saveData.playerRankDegree;
        playerRankEchelon = saveData.playerRankEchelon;
        #endregion

        #region Party Member Data
        recruitedPartyMemberCharacterIds = saveData.recruitedPartyMemberCharacterIds;
        partyMemberPositions = saveData.partyMemberPositions;
        partyMemberCombatData = saveData.partyMemberCombatData;
        #endregion
    }

    public SaveSlot GetCurrentGameDataFile ()
    {
        return currentFile;
    }

    public InputStates GetCurrentInputState ()
    {
        return inputState;
    }

    public OverworldController GetOverworldPlayer ()
    {
        return overworldPlayer;
    }

    public List<int> GetPartyMemberCharacterIds()
    {
        List<int> partyMemberIds = new List<int>();

        partyMemberIds.Add(partyMemberPositions.Find(x => x.position == Utility.PartyMemberPosition.Left).characterId);
        partyMemberIds.Add(partyMemberPositions.Find(x => x.position == Utility.PartyMemberPosition.Right).characterId);

        return partyMemberIds;
    }

    public void TurnOffInputState()
    {
        inputState = InputStates.None;
    }

    #region Combat Initializtion
    public void IntitializeCombat()
    {
        inputState = InputStates.Combat;
        CombatController.Instance.InitializeScenario();

        //at this point the initialization should be complete. I don't want to leave programming on the stack as I wait for user input
        //I will have to develop a system where whenever an event intersects a process, the event handler will be able to tell someone
        //like mastercontrol to continue, then that controller would know which function to continue calling...
        //just doing some testing with this line and source control

    }

    public float GetPlayerTeamEchelonDegreeNumber ()
    {
        return RankDataController.Instance.GetEchelonDegreeNumber(playerRankEchelon, playerRankDegree);
    }

    public int GetPlayerCoreTrainingAttribute (CoreTrainingAttribute coreTrainingAttributes)
    {
        return playerCoreTrainingAttributeData.Find(x => x.attribute == coreTrainingAttributes).value;
    }

    public Path GetPlayerPathFromPriority (Priority priority)
    {
        return playerPathPriority[priority];
    }

    public int GetPlayerPathAscensions (Path path)
    {
        return playerPathData.Find(x => x.path == path).ascensionGems;
    }

    public int GetPlayerReactionRank (Reaction reaction)
    {
        return playerReactionData.Find(x => x.reaction == reaction).rank;
    }

    public float GetPlayerPersonalityValue(PersonalityCategory personality)
    {
        return playerPersonalityData.Find(x => x.personality == personality).value;
    }

    public int GetPartyMemberCharacterId (Utility.PartyMemberPosition position)
    {
        return partyMemberPositions.Find(x => x.position == position).characterId;
    }

    public int GetPartyMemberCoreTrainingAttribute(int partyMemberCharacterId, CoreTrainingAttribute coreTrainingAttribute)
    {
        return partyMemberCombatData.Find(x => x.characterId == partyMemberCharacterId).coreTrainingAttributeData.Find(x => x.attribute == coreTrainingAttribute).value;
    }

    public Path GetPartyMemberPathPriority(int partyMemberCharacterId, Priority priority)
    {
        return partyMemberCombatData.Find(x => x.characterId == partyMemberCharacterId).pathPriorities[priority];
    }

    public int GetPartyMemberPathAscensions(int partyMemberCharacterId, Path path)
    {
        return partyMemberCombatData.Find(x => x.characterId == partyMemberCharacterId).pathData.Find(x => x.path == path).ascensionGems;
    }

    public int GetPartyMemberReactionRank(int partyMemberCharacterId, Reaction reaction)
    {
        return partyMemberCombatData.Find(x => x.characterId == partyMemberCharacterId).reactionData.Find(x => x.reaction == reaction).rank;
    }
    #endregion
}