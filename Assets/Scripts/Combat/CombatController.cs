using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Utility;

public class CombatController : MonoBehaviour
{
    public static CombatController Instance;

    private int staminaModifier = 5;

    private CombatState state;

    private int combatScenarioId; 
    private CombatScenario combatScenario;
    private float opponentCohesionFactor;
    private Dictionary<CombatantType, int> positionData;
    [SerializeField] private List<BaselineCombatantData> baselineCombatantData;
    [SerializeField] private List<CurrentCombatantData> currentCombatantData;

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
        #region Testing

        combatScenarioId = 1;

        #endregion
    }

    private void Update()
    {
        if (state == CombatState.Initializing)
        {
            //Nada
        }
        if (state == CombatState.Progressing)
        {
            //run the clock looking for actions
        }
    }

    public void InitializeScenario()
    {
        Debug.Log("Initializing");
        state = CombatState.Initializing;
        MasterControl.Instance.TurnOffInputState();
        UserInterfaceManager.Instance.TransitionOutForCombatInitalization();
        InitializeScene();
        InitializeCharacterCombatData();
        InitializeCombatCamera();
        UserInterfaceManager.Instance.TransitionInForCombatStartInitalization();



        //check for pre combat, post initialization dialogue on the scenario events
        //allow player the chance to change their path and their party members path
        //combat ui update and transitions in
        //move to next phase of combat
    }

    public void InitializeNonScenario(int teamId, int setId)
    {

    }

    private void InitializeCombatCamera()
    {
        Vector3 setPosition = CombatSetDataController.Instance.RetrieveSet(combatScenario.setId).transform.position;
        CameraManager.Instance.ActivateCombatCamera(setPosition);
    }

    private void InitializeScene()
    {
        combatScenario = CombatScneraioDataController.Instance.RetrieveCombatScenario(combatScenarioId);

        List<int> characterIds = NPCCombatDataController.Instance.IdentifyOpponentCharacterIdsForCombatScene(combatScenario.teamId);
        characterIds.AddRange(MasterControl.Instance.GetPartyMemberCharacterIds());
        characterIds.Add(PLAYER_CHARACTER_ID);

        positionData = new Dictionary<CombatantType, int>();
        for (int i = 0; i < characterIds.Count; i++)
        {
            if (i == 0)
            {
                positionData.Add(CombatantType.Opponent_1, characterIds[i]);
            }
            else if (i == 1)
            {
                positionData.Add(CombatantType.Opponent_2, characterIds[i]);
            }
            else if (i == 2)
            {
                positionData.Add(CombatantType.Opponent_3, characterIds[i]);
            }
            else if (i == 3)
            {
                positionData.Add(CombatantType.Ally_1, characterIds[i]);
            }
            else if (i == 4)
            {
                positionData.Add(CombatantType.Ally_2, characterIds[i]);
            }
            else if (i == 5)
            {
                positionData.Add(CombatantType.Player, characterIds[i]);
            }
        }

        CombatSceneController.Instance.ArrangeSceneFromScenario(combatScenario.setId, characterIds);
    }

    private void InitializeCharacterCombatData()
    {
        baselineCombatantData = new List<BaselineCombatantData>();
        currentCombatantData = new List<CurrentCombatantData>();

        #region Player

        int characterId = PLAYER_CHARACTER_ID;

        #region Baseline Combatant Data

        float echelonDegreeNumber = MasterControl.Instance.GetPlayerTeamEchelonDegreeNumber();

        List<AttunedPathData> attunedPaths = new List<AttunedPathData>();
        int attunementSlots = (echelonDegreeNumber >= ECEHELON_DEGREE_NUMBER_REQUIREMENT_FOR_SECOND_ATTUNEMENT) ? 2 : 1;
        for (int i = 0; i < attunementSlots; i++)
        {
            Path path = MasterControl.Instance.GetPlayerPathFromPriority((Priority)i);
            PathData characterPathData = PathDataController.Instance.GetPathData(path);
            int pathAscensions = MasterControl.Instance.GetPlayerPathAscensions(path);
            PathEnergyBonusData pathEnergyBonusData = PathDataController.Instance.GetPathEnergyBonusData(path, pathAscensions);
            PathAscensionData pathAscensionData = PathDataController.Instance.GetPathAscensionData(pathAscensions);
            attunedPaths.Add(new AttunedPathData(path, characterPathData.styleFocus, characterPathData.energyFocus, pathAscensions, pathEnergyBonusData.energySegmentBonuses, pathAscensionData.energyAccumulationBonus));
        }

        Dictionary<CoreTrainingAttribute, int> coreTrainingAttributes = new Dictionary<CoreTrainingAttribute, int> { 
            { CoreTrainingAttribute.Stamina, MasterControl.Instance.GetPlayerCoreTrainingAttribute(CoreTrainingAttribute.Stamina) },
            { CoreTrainingAttribute.Power, MasterControl.Instance.GetPlayerCoreTrainingAttribute(CoreTrainingAttribute.Power) }, 
            { CoreTrainingAttribute.Speed, MasterControl.Instance.GetPlayerCoreTrainingAttribute(CoreTrainingAttribute.Speed) } 
        };

        Dictionary<StatModifier, float> modifiers = new Dictionary<StatModifier, float>();
        ReactionRankData blockData = ReactionDataController.Instance.GetReactionRankData(Reaction.Block, MasterControl.Instance.GetPlayerReactionRank(Reaction.Block));
        modifiers[StatModifier.Block_Success_Rate] = blockData.reactionModifiers[StatModifier.Block_Success_Rate];
        modifiers[StatModifier.Block_Success_Mitigation_Rate] = blockData.reactionModifiers[StatModifier.Block_Success_Mitigation_Rate];
        modifiers[StatModifier.Block_Fail_Mitigation_Rate] = blockData.reactionModifiers[StatModifier.Block_Fail_Mitigation_Rate];
        ReactionRankData dodgeData = ReactionDataController.Instance.GetReactionRankData(Reaction.Dodge, MasterControl.Instance.GetPlayerReactionRank(Reaction.Dodge));
        modifiers[StatModifier.Dodge_Success_Rate] = dodgeData.reactionModifiers[StatModifier.Dodge_Success_Rate];
        modifiers[StatModifier.Dodge_Success_Mitigation_Rate] = dodgeData.reactionModifiers[StatModifier.Dodge_Success_Mitigation_Rate];
        modifiers[StatModifier.Dodge_Fail_Mitigation_Rate] = dodgeData.reactionModifiers[StatModifier.Dodge_Fail_Mitigation_Rate];
        ReactionRankData parryData = ReactionDataController.Instance.GetReactionRankData(Reaction.Parry, MasterControl.Instance.GetPlayerReactionRank(Reaction.Parry));
        modifiers[StatModifier.Parry_Success_Rate] = parryData.reactionModifiers[StatModifier.Parry_Success_Rate];
        modifiers[StatModifier.Parry_Success_Mitigation_Rate] = parryData.reactionModifiers[StatModifier.Parry_Success_Mitigation_Rate];
        modifiers[StatModifier.Parry_Fail_Mitigation_Rate] = parryData.reactionModifiers[StatModifier.Parry_Fail_Mitigation_Rate];

        //TODO Figure out where to store data on how to calculate inital modifiers like Power
        modifiers[StatModifier.Stamina_Consumption_Rate] = 1f;
        modifiers[StatModifier.Evade_Regen_Rate] = 0.3f;
        modifiers[StatModifier.Blindside_Rate] = 0f;
        modifiers[StatModifier.Crushing_Blow_Rate] = 0f;
        modifiers[StatModifier.Attack_Cost] = 0.25f;
        modifiers[StatModifier.Special_Cost] = 0.25f;
        modifiers[StatModifier.Group_Cost] = 0.25f;

        float maxStamina = coreTrainingAttributes[CoreTrainingAttribute.Stamina] * staminaModifier;

        LogicParadigm logicParadigm;
        float logic = MasterControl.Instance.GetPlayerPersonalityValue(PersonalityCategory.Logic);

        if (logic <= .1f)
        {
            logicParadigm = LogicParadigm.Basic;
        }
        else if (logic <= .25f)
        {
            logicParadigm = LogicParadigm.Average;
        }
        else
        {
            logicParadigm = LogicParadigm.Advanced;
        }

        baselineCombatantData.Add(new BaselineCombatantData(characterId, echelonDegreeNumber, attunedPaths, coreTrainingAttributes, modifiers, maxStamina, logicParadigm));

        #endregion

        #region Current Combatant Data

        float currentStamina = maxStamina;
        AttunedPathData activePath = attunedPaths[0];
        float currentEnergySegments = PathDataController.Instance.GetPathAscensionData(activePath.ascensions).combatInitializationEnergySegments;
        float energySegmentBonus = PathDataController.Instance.GetPathAscensionData(activePath.ascensions).energyAccumulationBonus;

        currentCombatantData.Add(new CurrentCombatantData(characterId, currentStamina, activePath, currentEnergySegments, energySegmentBonus));

        #endregion

        #endregion

        #region Party Members

        int[] partyMemberCharacterIds = { MasterControl.Instance.GetPartyMemberCharacterId(Utility.PartyMemberPosition.Left), MasterControl.Instance.GetPartyMemberCharacterId(Utility.PartyMemberPosition.Right) };

        foreach (int partyMemberCharacterId in partyMemberCharacterIds)
        {
            characterId = partyMemberCharacterId;

            #region Baseline Combatant Data

            attunedPaths = new List<AttunedPathData>();
            attunementSlots = (echelonDegreeNumber >= ECEHELON_DEGREE_NUMBER_REQUIREMENT_FOR_SECOND_ATTUNEMENT) ? 2 : 1;
            for (int i = 0; i < attunementSlots; i++)
            {
                Path path = MasterControl.Instance.GetPartyMemberPathPriority(characterId, (Priority)i);
                PathData characterPathData = PathDataController.Instance.GetPathData(path);
                int pathAscensions = MasterControl.Instance.GetPartyMemberPathAscensions(characterId, path);
                PathEnergyBonusData pathEnergyBonusData = PathDataController.Instance.GetPathEnergyBonusData(path, pathAscensions);
                PathAscensionData pathAscensionData = PathDataController.Instance.GetPathAscensionData(pathAscensions);
                attunedPaths.Add(new AttunedPathData(path, characterPathData.styleFocus, characterPathData.energyFocus, pathAscensions, pathEnergyBonusData.energySegmentBonuses, pathAscensionData.energyAccumulationBonus));
            }

            coreTrainingAttributes = new Dictionary<CoreTrainingAttribute, int> {
                { CoreTrainingAttribute.Stamina, MasterControl.Instance.GetPartyMemberCoreTrainingAttribute(characterId, CoreTrainingAttribute.Stamina) },
                { CoreTrainingAttribute.Power, MasterControl.Instance.GetPartyMemberCoreTrainingAttribute(characterId, CoreTrainingAttribute.Power) },
                { CoreTrainingAttribute.Speed, MasterControl.Instance.GetPartyMemberCoreTrainingAttribute(characterId, CoreTrainingAttribute.Speed) }
            };

            modifiers = new Dictionary<StatModifier, float>();
            blockData = ReactionDataController.Instance.GetReactionRankData(Reaction.Block, MasterControl.Instance.GetPartyMemberReactionRank(characterId, Reaction.Block));
            modifiers[StatModifier.Block_Success_Rate] = blockData.reactionModifiers[StatModifier.Block_Success_Rate];
            modifiers[StatModifier.Block_Success_Mitigation_Rate] = blockData.reactionModifiers[StatModifier.Block_Success_Mitigation_Rate];
            modifiers[StatModifier.Block_Fail_Mitigation_Rate] = blockData.reactionModifiers[StatModifier.Block_Fail_Mitigation_Rate];
            dodgeData = ReactionDataController.Instance.GetReactionRankData(Reaction.Dodge, MasterControl.Instance.GetPartyMemberReactionRank(characterId, Reaction.Dodge));
            modifiers[StatModifier.Dodge_Success_Rate] = dodgeData.reactionModifiers[StatModifier.Dodge_Success_Rate];
            modifiers[StatModifier.Dodge_Success_Mitigation_Rate] = dodgeData.reactionModifiers[StatModifier.Dodge_Success_Mitigation_Rate];
            modifiers[StatModifier.Dodge_Fail_Mitigation_Rate] = dodgeData.reactionModifiers[StatModifier.Dodge_Fail_Mitigation_Rate];
            parryData = ReactionDataController.Instance.GetReactionRankData(Reaction.Parry, MasterControl.Instance.GetPartyMemberReactionRank(characterId, Reaction.Parry));
            modifiers[StatModifier.Parry_Success_Rate] = parryData.reactionModifiers[StatModifier.Parry_Success_Rate];
            modifiers[StatModifier.Parry_Success_Mitigation_Rate] = parryData.reactionModifiers[StatModifier.Parry_Success_Mitigation_Rate];
            modifiers[StatModifier.Parry_Fail_Mitigation_Rate] = parryData.reactionModifiers[StatModifier.Parry_Fail_Mitigation_Rate];

            //TODO Figure out where to store data on how to calculate inital modifiers like Power
            modifiers[StatModifier.Stamina_Consumption_Rate] = 1f;
            modifiers[StatModifier.Evade_Regen_Rate] = 0.3f;
            modifiers[StatModifier.Blindside_Rate] = 0f;
            modifiers[StatModifier.Crushing_Blow_Rate] = 0f;
            modifiers[StatModifier.Attack_Cost] = 0.25f;
            modifiers[StatModifier.Special_Cost] = 0.25f;
            modifiers[StatModifier.Group_Cost] = 0.25f;

            maxStamina = coreTrainingAttributes[CoreTrainingAttribute.Stamina] * staminaModifier;

            logicParadigm = CalculateLogicParadigm(characterId);

            baselineCombatantData.Add(new BaselineCombatantData(characterId, echelonDegreeNumber, attunedPaths, coreTrainingAttributes, modifiers, maxStamina, logicParadigm));

            #endregion

            #region Current Combatant Data

            currentStamina = maxStamina;
            activePath = attunedPaths[0];
            currentEnergySegments = PathDataController.Instance.GetPathAscensionData(activePath.ascensions).combatInitializationEnergySegments;
            energySegmentBonus = PathDataController.Instance.GetPathAscensionData(activePath.ascensions).energyAccumulationBonus;

            currentCombatantData.Add(new CurrentCombatantData(characterId, currentStamina, activePath, currentEnergySegments, energySegmentBonus));

            #endregion
        }

        #endregion

        #region Opponents

        TeamData opponentTeamData = TeamDataController.Instance.GetTeamData(combatScenario.teamId);
        opponentCohesionFactor = opponentTeamData.cohesionFactor;
        List<int> opponentCharacterIds = NPCCombatDataController.Instance.IdentifyOpponentCharacterIdsForCombatScene(combatScenario.teamId);

        foreach (int opponentCharacterId in opponentCharacterIds)
        {
            characterId = opponentCharacterId;
            NPCCombatData combatData = NPCCombatDataController.Instance.GetNPCCombatData(characterId);
            int totalCTP = RankDataController.Instance.GetTotalCTP(opponentTeamData.echelon, opponentTeamData.degree);
            echelonDegreeNumber = RankDataController.Instance.GetEchelonDegreeNumber(opponentTeamData.echelon, opponentTeamData.degree);
            logicParadigm = CalculateLogicParadigm(characterId);
            Dictionary<Reaction, int> opponentReactionRanks = DetermineOpponentReactionRank(logicParadigm, echelonDegreeNumber, combatData.reactionProgressionPrioitiesList);

            #region Baseline Combatant Data

            attunedPaths = new List<AttunedPathData>();
            attunementSlots = (echelonDegreeNumber >= ECEHELON_DEGREE_NUMBER_REQUIREMENT_FOR_SECOND_ATTUNEMENT) ? 2 : 1;
            int availableAscensions = Mathf.Clamp(Mathf.FloorToInt(echelonDegreeNumber - 1f), 0, 6);
            for (int i = 0; i < attunementSlots; i++)
            {
                Path path = combatData.pathAttunementPrioritiesList[(Priority)i];
                PathData characterPathData = PathDataController.Instance.GetPathData(path);
                int[] ascensionData = DetermineOpponentPathAscensions(availableAscensions, (Priority)i, logicParadigm);
                int pathAscensions = ascensionData[0];
                availableAscensions = ascensionData[1];
                PathEnergyBonusData pathEnergyBonusData = PathDataController.Instance.GetPathEnergyBonusData(path, pathAscensions);
                PathAscensionData pathAscensionData = PathDataController.Instance.GetPathAscensionData(pathAscensions);
                attunedPaths.Add(new AttunedPathData(path, characterPathData.styleFocus, characterPathData.energyFocus, pathAscensions, pathEnergyBonusData.energySegmentBonuses, pathAscensionData.energyAccumulationBonus));
            }

            coreTrainingAttributes = new Dictionary<CoreTrainingAttribute, int> {
                { CoreTrainingAttribute.Stamina, (int)(totalCTP * combatData.attributeEmphasisList[CoreTrainingAttribute.Stamina]) },
                { CoreTrainingAttribute.Power, (int)(totalCTP * combatData.attributeEmphasisList[CoreTrainingAttribute.Power]) },
                { CoreTrainingAttribute.Speed, (int)(totalCTP * combatData.attributeEmphasisList[CoreTrainingAttribute.Speed]) }
            };

            modifiers = new Dictionary<StatModifier, float>();
            blockData = ReactionDataController.Instance.GetReactionRankData(Reaction.Block, opponentReactionRanks[Reaction.Block]);
            modifiers[StatModifier.Block_Success_Rate] = blockData.reactionModifiers[StatModifier.Block_Success_Rate];
            modifiers[StatModifier.Block_Success_Mitigation_Rate] = blockData.reactionModifiers[StatModifier.Block_Success_Mitigation_Rate];
            modifiers[StatModifier.Block_Fail_Mitigation_Rate] = blockData.reactionModifiers[StatModifier.Block_Fail_Mitigation_Rate];
            dodgeData = ReactionDataController.Instance.GetReactionRankData(Reaction.Dodge, opponentReactionRanks[Reaction.Dodge]);
            modifiers[StatModifier.Dodge_Success_Rate] = dodgeData.reactionModifiers[StatModifier.Dodge_Success_Rate];
            modifiers[StatModifier.Dodge_Success_Mitigation_Rate] = dodgeData.reactionModifiers[StatModifier.Dodge_Success_Mitigation_Rate];
            modifiers[StatModifier.Dodge_Fail_Mitigation_Rate] = dodgeData.reactionModifiers[StatModifier.Dodge_Fail_Mitigation_Rate];
            parryData = ReactionDataController.Instance.GetReactionRankData(Reaction.Parry, opponentReactionRanks[Reaction.Parry]);
            modifiers[StatModifier.Parry_Success_Rate] = parryData.reactionModifiers[StatModifier.Parry_Success_Rate];
            modifiers[StatModifier.Parry_Success_Mitigation_Rate] = parryData.reactionModifiers[StatModifier.Parry_Success_Mitigation_Rate];
            modifiers[StatModifier.Parry_Fail_Mitigation_Rate] = parryData.reactionModifiers[StatModifier.Parry_Fail_Mitigation_Rate];

            //TODO Figure out where to store data on how to calculate inital modifiers like Power
            modifiers[StatModifier.Stamina_Consumption_Rate] = 1f;
            modifiers[StatModifier.Evade_Regen_Rate] = 0.3f;
            modifiers[StatModifier.Blindside_Rate] = 0f;
            modifiers[StatModifier.Crushing_Blow_Rate] = 0f;
            modifiers[StatModifier.Attack_Cost] = 0.25f;
            modifiers[StatModifier.Special_Cost] = 0.25f;
            modifiers[StatModifier.Group_Cost] = 0.25f;

            maxStamina = coreTrainingAttributes[CoreTrainingAttribute.Stamina] * staminaModifier;

            baselineCombatantData.Add(new BaselineCombatantData(characterId, echelonDegreeNumber, attunedPaths, coreTrainingAttributes, modifiers, maxStamina, logicParadigm));

            #endregion

            #region Current Combatant Data

            currentStamina = maxStamina;
            activePath = attunedPaths[0];
            currentEnergySegments = PathDataController.Instance.GetPathAscensionData(activePath.ascensions).combatInitializationEnergySegments;
            energySegmentBonus = PathDataController.Instance.GetPathAscensionData(activePath.ascensions).energyAccumulationBonus;

            currentCombatantData.Add(new CurrentCombatantData(characterId, currentStamina, activePath, currentEnergySegments, energySegmentBonus));

            #endregion
        }

        #endregion
    }

    private LogicParadigm CalculateLogicParadigm(int characterId)
    {
        LogicParadigm logicParadigm;
        float logic = CharacterDataController.Instance.FindPersonalityValue(characterId, PersonalityCategory.Logic);

        if (logic <= .1f)
        {
            logicParadigm = LogicParadigm.Basic;
        }
        else if (logic <= .25f)
        {
            logicParadigm = LogicParadigm.Average;
        }
        else
        {
            logicParadigm = LogicParadigm.Advanced;
        }

        return logicParadigm;
    }

    private Dictionary<Reaction, int> DetermineOpponentReactionRank(LogicParadigm logicParadigm, float rankNumber, Dictionary<Priority, Reaction> reactionProgressionPrioitiesList)
    {
        #region Initializing Opponent Reaction Ranks Dictionary

        Dictionary<Reaction, int> opponentReactionRanks = new Dictionary<Reaction, int>();
        int totalRanks = (int)(rankNumber * 10);
        float initialRank;
        int paradigmSeparation;
        int rank;

        if (logicParadigm == LogicParadigm.Basic)
        {
            initialRank = totalRanks * .3f;
            paradigmSeparation = 0;
            
        }
        else if (logicParadigm == LogicParadigm.Average)
        {
            initialRank = totalRanks * .4f;
            paradigmSeparation = 1;
        }
        else
        {
            initialRank = totalRanks * .6f;
            paradigmSeparation = 5;
        }

        rank = ReactionDataController.Instance.DetermineRankFromReactionAndRankNumber(reactionProgressionPrioitiesList[Priority.Primary], initialRank);
        opponentReactionRanks.Add(reactionProgressionPrioitiesList[Priority.Primary], rank);
        opponentReactionRanks.Add(reactionProgressionPrioitiesList[Priority.Secondary], Mathf.Clamp(rank - paradigmSeparation, 0, 10));
        opponentReactionRanks.Add(reactionProgressionPrioitiesList[Priority.Tertiary], Mathf.Clamp(rank - paradigmSeparation, 0, 10));

        #endregion

        #region Focus Opponent Reaction Ranks Dictionary

        float totalOpponentRankRequirement = ReactionDataController.Instance.RetrieveTotalOpponentRankRequirement(opponentReactionRanks);
        bool possibilities = true;
        int priority;

        //Testing
        int loopCount = 0;
        //

        if (totalRanks - totalOpponentRankRequirement > 0)
        {
            priority = 0;

            while (possibilities)
            {
                //Testing
                loopCount++;
                //

                Reaction reaction = opponentReactionRanks.ElementAt(priority).Key;

                if (opponentReactionRanks[reaction] < 11)
                {
                    opponentReactionRanks[reaction]++;
                    totalOpponentRankRequirement = ReactionDataController.Instance.RetrieveTotalOpponentRankRequirement(opponentReactionRanks);
                }
                else if (priority == Enum.GetNames(typeof(Priority)).Length - 1)
                {
                    possibilities = false;
                }

                if (totalRanks - totalOpponentRankRequirement < 0)
                {
                    opponentReactionRanks[reaction]--;
                    priority++;

                    if (priority == Enum.GetNames(typeof(Priority)).Length - 1)
                    {
                        possibilities = false;
                    }
                }
                else
                {
                    if (priority == Enum.GetNames(typeof(Priority)).Length - 1)
                    {
                        priority = 0;
                    }
                    else
                    {
                        priority++;
                    }
                }

                //Testing
                if (loopCount >= 30)
                {
                    possibilities = false;
                    print("looping issue");
                }
                //
            }
        }
        else if (totalRanks - totalOpponentRankRequirement < 0)
        {
            priority = Enum.GetNames(typeof(Priority)).Length - 1;

            while (possibilities)
            {
                //Testing
                loopCount++;
                //

                Reaction reaction = opponentReactionRanks.ElementAt(priority).Key;

                if (opponentReactionRanks[reaction] > 1)
                {
                    opponentReactionRanks[reaction]--;
                    totalOpponentRankRequirement = ReactionDataController.Instance.RetrieveTotalOpponentRankRequirement(opponentReactionRanks);
                }

                if (totalRanks - totalOpponentRankRequirement < 0)
                {
                    priority--;

                    if (priority == -1)
                    {
                        priority = Enum.GetNames(typeof(Priority)).Length - 1;
                    }
                }
                else
                {
                    possibilities = false;
                }

                //Testing
                if (loopCount >= 30)
                {
                    possibilities = false;
                    print("looping issue");
                }
                //
            }
        }
        else
        {
            //no loop
        }

        #endregion

        return opponentReactionRanks;
    }

    private int[] DetermineOpponentPathAscensions(int availableAscensions, Priority priority, LogicParadigm logicParadigm)
    {
        int[] ascensionData = new int[2];

        int ascensions = 0;

        if (priority == Priority.Primary)
        {
            if (logicParadigm == LogicParadigm.Basic)
            {
                ascensions = Mathf.CeilToInt(availableAscensions / 2);
            }
            else if (logicParadigm == LogicParadigm.Average)
            {
                if (availableAscensions < 4)
                {
                    ascensions = Mathf.Clamp(availableAscensions, 0, 2);
                }
                else
                {
                    ascensions = 3;
                }
            }
            else if (logicParadigm == LogicParadigm.Advanced)
            {
                ascensions = Mathf.Clamp(availableAscensions, 0, 3);
            }

            availableAscensions -= ascensions;
        }
        else
        {
            ascensions = availableAscensions;
        }

        ascensionData[0] = ascensions;
        ascensionData[1] = availableAscensions;

        return ascensionData;
    }
}

[Serializable]
public struct BaselineCombatantData
{
    public int characterId;
    public float echelonDegreeNumber;
    public List<AttunedPathData> attunedPaths;
    public Dictionary<CoreTrainingAttribute, int> coreTrainingAttributes;
    public Dictionary<StatModifier, float> modifiers;
    public float maxStamina;
    public LogicParadigm logicParadigm;
    

    public BaselineCombatantData(int characterId, float echelonDegreeNumber, List<AttunedPathData> attunedPaths, Dictionary<CoreTrainingAttribute, int> coreTrainingAttributes, 
        Dictionary<StatModifier, float> modifiers, float maxStamina, LogicParadigm logicParadigm)
    {
        this.characterId = characterId;
        this.echelonDegreeNumber = echelonDegreeNumber;
        this.attunedPaths = attunedPaths;
        this.coreTrainingAttributes = coreTrainingAttributes;
        this.maxStamina = maxStamina;
        this.modifiers = modifiers;
        this.logicParadigm = logicParadigm;
    }
}

[Serializable]
public struct CurrentCombatantData
{
    public int characterId;
    public float stamina;
    public AttunedPathData path;
    public float energySegments;
    public float enerySegmentBonus;
    //TODO restrictions...

    public CurrentCombatantData(int characterId, float stamina, AttunedPathData path, float energySegments, float enerySegmentBonus)
    {
        this.characterId = characterId;
        this.stamina = stamina;
        this.path = path;
        this.energySegments = energySegments;
        this.enerySegmentBonus = enerySegmentBonus;
    }
}

[Serializable]
public struct AttunedPathData
{
    public Path path;
    public CombatEventType styleFocus;
    public PathEnergyFocus energyFocus;
    public int ascensions;
    public Dictionary<EnergySegment, float> energySegmentBonuses;
    public float energyAccumulationBonus;

    public AttunedPathData(Path path, CombatEventType styleFocus, PathEnergyFocus energyFocus, int ascensions, Dictionary<EnergySegment, float> energySegmentBonuses, float energyAccumulationBonus)
    {
        this.path = path;
        this.styleFocus = styleFocus;
        this.energyFocus = energyFocus;
        this.ascensions = ascensions;
        this.energySegmentBonuses = energySegmentBonuses;
        this.energyAccumulationBonus = energyAccumulationBonus;
    }
}

[Serializable]
public struct CombatModifiers
{
    public int combatantCharacterId;
    //TODO source?
    public StatModifier type;
    public int roundStart;
    public int roundEnd;
    public float modification;

    public CombatModifiers(int combatantCharacterId, StatModifier type, int roundStart, int roundEnd, float modification)
    {
        this.combatantCharacterId = combatantCharacterId;
        this.type = type;
        this.roundStart = roundStart;
        this.roundEnd = roundEnd;
        this.modification = modification;
    }
}