using UnityEngine;
using System;
using static Utility;
using System.Collections.Generic;

public class Utility
{
    public static int PLAYER_CHARACTER_ID = 0;
    public static int PLAYER_CHARACTER_MODEL_ID = 0;
    public static int INITIAL_ALLY_LEFT_CHARACTER_ID = 712;
    public static int INITIAL_ALLY_RIGHT_CHARACTER_ID = 713;
    public static float PERSONALITY_BONUS_THRESHOLD = .7f;
    public static float ECEHELON_DEGREE_NUMBER_REQUIREMENT_FOR_SECOND_ATTUNEMENT = 4.0f;

    public enum RankEchelon
    {
        Stone,
        Iron,
        Bronze,
        Silver,
        Gold,
        Platinum,
        Diamond,
        Master,
        Grand_Master,
        Legend
    }

    public enum RankDegree
    {
        First,
        Second,
        Third,
        Fourth,
        Fifth,
        Sixth,
        Seventh,
        Eighth,
        Nineth,
        Tenth
    }

    public enum Location
    {
        Hub_Town,
        Fields,
        Test_1
    }

    public enum CombatState
    {
        Initializing,
        Progressing,
        Acting,
        Reacting,
        Dialogue,
        Cutscene,
        Closing
    }

    public enum CombatantAnimationState
    {
        Idle,
        Attack,
        Defend,
        Evade,
        Special,
        Flinch,
        Block,
        Dodge,
        Parry
    }

    public enum SaveSlot
    {
        Aplha,
        Beta,
        Omega
    }

    public enum Path
    {
        Fire,
        Water,
        Air,
        Earth,
        Shadow,
        Light
    }

    public enum EnergySegment
    {
        First_Segment,
        Second_Segment,
        Third_Segment,
        Fourth_Segment,
        Fifth_Segment,
        Sixth_Segment
    }

    public enum PathEnergyFocus
    {
        Block_Success_Rate,
        Dodge_Success_Rate,
        Parry_Success_Rate
    }

    public enum StatusEffect
    {
        None,
        Haste,
        Slow
    }

    public enum EnemyTargeting
    {
        None,
        Single,
        All
    }

    public enum PartyTargeting
    {
        None,
        Self,
        All
    }

    public enum CombatantType
    {
        Ally_1,
        Player,
        Ally_2,
        Opponent_1,
        Opponent_2,
        Opponent_3
    }

    public enum PersonalityCategory
    {
        Patience,
        Empathy,
        Cunning,
        Logic,
        Kindness,
        Charisma
    }

    public enum InputStates
    {
        Null,
        None,
        Overworld,
        Menus,
        Dialogue,
        Combat
    }

    public enum ButtonPresses
    {
        None,
        Interact,
        Jump,
        Cast,
        Select,
        Advance
    }

    public enum SoundEffects
    {
        Null,
        Company_Logo,
        Player_Death
    }

    public enum MusicTracks
    {
        Null,
        Level_Zero_Normal,
        Level_One_Normal,
        Player_Death
    }

    public enum DecisionResult
    {
        Success,
        Failure
    }

    public enum Reaction
    {
        Block,
        Dodge,
        Parry
    }

    public enum Action
    {
        Attack,
        Defend,
        Evade,
        Special
    }

    public enum StatModifier
    {
        Stamina_Consumption_Rate,
        Power,
        Speed,
        Block_Success_Rate,
        Block_Success_Mitigation_Rate,
        Block_Fail_Mitigation_Rate,
        Dodge_Success_Rate,
        Dodge_Success_Mitigation_Rate,
        Dodge_Fail_Mitigation_Rate,
        Parry_Success_Rate,
        Parry_Success_Mitigation_Rate,
        Parry_Fail_Mitigation_Rate,
        Evade_Regen_Rate,
        Blindside_Rate,
        Crushing_Blow_Rate,
        Attack_Cost,
        Special_Cost,
        Group_Cost
    }

    public enum CoreTrainingAttribute
    {
        Stamina,
        Power,
        Speed
    }

    public enum TeamName
    {
        Master_Blaster,
        Crowd_Control,
        Triple_J,
        The_Guardians
    }
    public enum Priority
    {
        Primary,
        Secondary,
        Tertiary
    }

    public enum PartyMemberPosition
    {
        Left,
        Right,
        Bench
    }

    public enum LogicParadigm
    {
        Basic,
        Average,
        Advanced
    }

    #region Events
    public enum EventTriggerType
    {
        Combatant_Health_Change,
        Time_Limit
    }

    public enum EventName
    {
        Tutorial_1,
        Tutorial_2
    }

    public enum EventCaller
    {
        Combat_Control
    }

    public enum CombatEventType
    {
        Action,
        Reaction
    }
    #endregion
}

public struct OpponentCombatData
{

}

[Serializable]
public struct PlayerCharactersReactionData
{
    public Reaction reaction;
    public int xp;
    public int rank;

    public PlayerCharactersReactionData(Reaction reaction, int xp, int rank)
    {
        this.reaction = reaction;
        this.xp = xp;
        this.rank = rank;
    }
}

[Serializable]
public struct PlayerCharactersPathData
{
    public Path path;
    public int xp;
    public int ascensionGems;

    public PlayerCharactersPathData(Path path, int xp, int attunementGems)
    {
        this.path = path;
        this.xp = xp;
        this.ascensionGems = attunementGems;
    }
}

[Serializable]
public struct PlayerCharactersPersonalityData
{
    public PersonalityCategory personality;
    public float value;

    public PlayerCharactersPersonalityData(PersonalityCategory personality, float value)
    {
        this.personality = personality;
        this.value = value;
    }
}

[Serializable]
public struct CharacterCoreTrainingAttributeData
{
    public CoreTrainingAttribute attribute;
    public int value;

    public CharacterCoreTrainingAttributeData(CoreTrainingAttribute attribute, int value)
    {
        this.attribute = attribute;
        this.value = value;
    }
}

[Serializable]
public struct PartyMemberPosition
{
    public Utility.PartyMemberPosition position;
    public int characterId;

    public PartyMemberPosition(Utility.PartyMemberPosition position, int characterId)
    {
        this.position = position;
        this.characterId = characterId;
    }
}

[Serializable]
public struct PartyMemberCombatData
{
    public int characterId;
    public List<PlayerCharactersReactionData> reactionData;
    public List<PlayerCharactersPathData> pathData;
    public List<CharacterCoreTrainingAttributeData> coreTrainingAttributeData;
    public Dictionary<Priority, Path> pathPriorities;

    public PartyMemberCombatData(int characterId, List<PlayerCharactersReactionData> reactionData, List<PlayerCharactersPathData> pathData, List<CharacterCoreTrainingAttributeData> coreTrainingAttributeData, Dictionary<Priority, Path> pathPriorities)
    {
        this.characterId = characterId;
        this.reactionData = reactionData;
        this.pathData = pathData;
        this.coreTrainingAttributeData = coreTrainingAttributeData;
        this.pathPriorities = pathPriorities;
    }
}

[Serializable]
public struct CombatantPathData
{
    public Path path;
    public Priority priority;
    public int pathEnergyBonusDataId;
    public CombatEventType pathEventType;
    public PathEnergyFocus pathEnergyFocus;
    public float pathEnergyAccumulationBonus;
}