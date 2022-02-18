using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Utility;

public class ReactionDataController : MonoBehaviour
{
    public static ReactionDataController Instance;

    private List<ReactionRankData> reactionRankDatabase;
    private Dictionary<int, int> reactionRankXP;  //Rank & Total XP Requirement
    private Dictionary<DecisionResult, int> reactionXP;

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
        #region Reaction XP
        reactionXP = new Dictionary<DecisionResult, int>();
        reactionXP.Add(DecisionResult.Success, 4);
        reactionXP.Add(DecisionResult.Failure, 2);
        #endregion

        #region Reaction Rank XP
        reactionRankXP = new Dictionary<int, int>();
        reactionRankXP.Add(1, 0);
        reactionRankXP.Add(2, 100);
        reactionRankXP.Add(3, 215);
        reactionRankXP.Add(4, 345);
        reactionRankXP.Add(5, 490);
        reactionRankXP.Add(6, 650);
        reactionRankXP.Add(7, 825);
        reactionRankXP.Add(8, 1015);
        reactionRankXP.Add(9, 1220);
        reactionRankXP.Add(10, 1440);
        reactionRankXP.Add(11, 1675);
        #endregion

        #region Reaction Rank Data
        reactionRankDatabase = new List<ReactionRankData>();
        reactionRankDatabase.Add(new ReactionRankData(0, 1, Reaction.Block, new Dictionary<StatModifier, float> { { StatModifier.Block_Success_Rate, 0.4f }, { StatModifier.Block_Success_Mitigation_Rate, 0.55f }, { StatModifier.Block_Fail_Mitigation_Rate, 0.4f } }, 0f));
        reactionRankDatabase.Add(new ReactionRankData(1, 2, Reaction.Block, new Dictionary<StatModifier, float> { { StatModifier.Block_Success_Rate, 0.45f }, { StatModifier.Block_Success_Mitigation_Rate, 0.575f }, { StatModifier.Block_Fail_Mitigation_Rate, 0.425f } }, 1.72f));
        reactionRankDatabase.Add(new ReactionRankData(2, 3, Reaction.Block, new Dictionary<StatModifier, float> { { StatModifier.Block_Success_Rate, 0.5f }, { StatModifier.Block_Success_Mitigation_Rate, 0.6f }, { StatModifier.Block_Fail_Mitigation_Rate, 0.45f } }, 3.64f));
        reactionRankDatabase.Add(new ReactionRankData(3, 4, Reaction.Block, new Dictionary<StatModifier, float> { { StatModifier.Block_Success_Rate, 0.55f }, { StatModifier.Block_Success_Mitigation_Rate, 0.625f }, { StatModifier.Block_Fail_Mitigation_Rate, 0.475f } }, 5.74f));
        reactionRankDatabase.Add(new ReactionRankData(4, 5, Reaction.Block, new Dictionary<StatModifier, float> { { StatModifier.Block_Success_Rate, 0.6f }, { StatModifier.Block_Success_Mitigation_Rate, 0.65f }, { StatModifier.Block_Fail_Mitigation_Rate, 0.5f } }, 8f));
        reactionRankDatabase.Add(new ReactionRankData(5, 6, Reaction.Block, new Dictionary<StatModifier, float> { { StatModifier.Block_Success_Rate, 0.65f }, { StatModifier.Block_Success_Mitigation_Rate, 0.675f }, { StatModifier.Block_Fail_Mitigation_Rate, 0.525f } }, 10.43f));
        reactionRankDatabase.Add(new ReactionRankData(6, 7, Reaction.Block, new Dictionary<StatModifier, float> { { StatModifier.Block_Success_Rate, 0.7f }, { StatModifier.Block_Success_Mitigation_Rate, 0.7f }, { StatModifier.Block_Fail_Mitigation_Rate, 0.55f } }, 13f));
        reactionRankDatabase.Add(new ReactionRankData(7, 8, Reaction.Block, new Dictionary<StatModifier, float> { { StatModifier.Block_Success_Rate, 0.75f }, { StatModifier.Block_Success_Mitigation_Rate, 0.725f }, { StatModifier.Block_Fail_Mitigation_Rate, 0.575f } }, 15.72f));
        reactionRankDatabase.Add(new ReactionRankData(8, 9, Reaction.Block, new Dictionary<StatModifier, float> { { StatModifier.Block_Success_Rate, 0.8f }, { StatModifier.Block_Success_Mitigation_Rate, 0.75f }, { StatModifier.Block_Fail_Mitigation_Rate, 0.6f } }, 18.56f));
        reactionRankDatabase.Add(new ReactionRankData(9, 10, Reaction.Block, new Dictionary<StatModifier, float> { { StatModifier.Block_Success_Rate, 0.85f }, { StatModifier.Block_Success_Mitigation_Rate, 0.775f }, { StatModifier.Block_Fail_Mitigation_Rate, 0.625f } }, 21.54f));
        reactionRankDatabase.Add(new ReactionRankData(10, 11, Reaction.Block, new Dictionary<StatModifier, float> { { StatModifier.Block_Success_Rate, 0.9f }, { StatModifier.Block_Success_Mitigation_Rate, 0.8f }, { StatModifier.Block_Fail_Mitigation_Rate, 0.65f } }, 24.63f));

        reactionRankDatabase.Add(new ReactionRankData(11, 1, Reaction.Dodge, new Dictionary<StatModifier, float> { { StatModifier.Dodge_Success_Rate, 0.3f }, { StatModifier.Dodge_Success_Mitigation_Rate, 0.8f }, { StatModifier.Dodge_Fail_Mitigation_Rate, 0.1f } }, 0f));
        reactionRankDatabase.Add(new ReactionRankData(12, 2, Reaction.Dodge, new Dictionary<StatModifier, float> { { StatModifier.Dodge_Success_Rate, 0.325f }, { StatModifier.Dodge_Success_Mitigation_Rate, 0.81f }, { StatModifier.Dodge_Fail_Mitigation_Rate, 0.125f } }, 1.89f));
        reactionRankDatabase.Add(new ReactionRankData(13, 3, Reaction.Dodge, new Dictionary<StatModifier, float> { { StatModifier.Dodge_Success_Rate, 0.35f }, { StatModifier.Dodge_Success_Mitigation_Rate, 0.82f }, { StatModifier.Dodge_Fail_Mitigation_Rate, 0.15f } }, 4.02f));
        reactionRankDatabase.Add(new ReactionRankData(14, 4, Reaction.Dodge, new Dictionary<StatModifier, float> { { StatModifier.Dodge_Success_Rate, 0.375f }, { StatModifier.Dodge_Success_Mitigation_Rate, 0.83f }, { StatModifier.Dodge_Fail_Mitigation_Rate, 0.175f } }, 6.38f));
        reactionRankDatabase.Add(new ReactionRankData(15, 5, Reaction.Dodge, new Dictionary<StatModifier, float> { { StatModifier.Dodge_Success_Rate, 0.4f }, { StatModifier.Dodge_Success_Mitigation_Rate, 0.84f }, { StatModifier.Dodge_Fail_Mitigation_Rate, 0.2f } }, 8.97f));
        reactionRankDatabase.Add(new ReactionRankData(16, 6, Reaction.Dodge, new Dictionary<StatModifier, float> { { StatModifier.Dodge_Success_Rate, 0.425f }, { StatModifier.Dodge_Success_Mitigation_Rate, 0.85f }, { StatModifier.Dodge_Fail_Mitigation_Rate, 0.225f } }, 11.78f));
        reactionRankDatabase.Add(new ReactionRankData(17, 7, Reaction.Dodge, new Dictionary<StatModifier, float> { { StatModifier.Dodge_Success_Rate, 0.45f }, { StatModifier.Dodge_Success_Mitigation_Rate, 0.86f }, { StatModifier.Dodge_Fail_Mitigation_Rate, 0.25f } }, 14.79f));
        reactionRankDatabase.Add(new ReactionRankData(18, 8, Reaction.Dodge, new Dictionary<StatModifier, float> { { StatModifier.Dodge_Success_Rate, 0.475f }, { StatModifier.Dodge_Success_Mitigation_Rate, 0.87f }, { StatModifier.Dodge_Fail_Mitigation_Rate, 0.275f } }, 18.01f));
        reactionRankDatabase.Add(new ReactionRankData(19, 9, Reaction.Dodge, new Dictionary<StatModifier, float> { { StatModifier.Dodge_Success_Rate, 0.5f }, { StatModifier.Dodge_Success_Mitigation_Rate, 0.88f }, { StatModifier.Dodge_Fail_Mitigation_Rate, 0.3f } }, 21.43f));
        reactionRankDatabase.Add(new ReactionRankData(20, 10, Reaction.Dodge, new Dictionary<StatModifier, float> { { StatModifier.Dodge_Success_Rate, 0.525f }, { StatModifier.Dodge_Success_Mitigation_Rate, 0.89f }, { StatModifier.Dodge_Fail_Mitigation_Rate, 0.325f } }, 25.04f));
        reactionRankDatabase.Add(new ReactionRankData(21, 11, Reaction.Dodge, new Dictionary<StatModifier, float> { { StatModifier.Dodge_Success_Rate, 0.55f }, { StatModifier.Dodge_Success_Mitigation_Rate, 0.9f }, { StatModifier.Dodge_Fail_Mitigation_Rate, 0.35f } }, 28.83f));

        reactionRankDatabase.Add(new ReactionRankData(22, 1, Reaction.Parry, new Dictionary<StatModifier, float> { { StatModifier.Parry_Success_Rate, 0.1f }, { StatModifier.Parry_Success_Mitigation_Rate, 0.9f }, { StatModifier.Parry_Fail_Mitigation_Rate, 0f } }, 0f));
        reactionRankDatabase.Add(new ReactionRankData(23, 2, Reaction.Parry, new Dictionary<StatModifier, float> { { StatModifier.Parry_Success_Rate, 0.125f }, { StatModifier.Parry_Success_Mitigation_Rate, 0.91f }, { StatModifier.Parry_Fail_Mitigation_Rate, 0.025f } }, 2.22f));
        reactionRankDatabase.Add(new ReactionRankData(24, 3, Reaction.Parry, new Dictionary<StatModifier, float> { { StatModifier.Parry_Success_Rate, 0.15f }, { StatModifier.Parry_Success_Mitigation_Rate, 0.92f }, { StatModifier.Parry_Fail_Mitigation_Rate, 0.05f } }, 4.72f));
        reactionRankDatabase.Add(new ReactionRankData(25, 4, Reaction.Parry, new Dictionary<StatModifier, float> { { StatModifier.Parry_Success_Rate, 0.175f }, { StatModifier.Parry_Success_Mitigation_Rate, 0.93f }, { StatModifier.Parry_Fail_Mitigation_Rate, 0.075f } }, 7.49f));
        reactionRankDatabase.Add(new ReactionRankData(26, 5, Reaction.Parry, new Dictionary<StatModifier, float> { { StatModifier.Parry_Success_Rate, 0.2f }, { StatModifier.Parry_Success_Mitigation_Rate, 0.94f }, { StatModifier.Parry_Fail_Mitigation_Rate, 0.1f } }, 10.51f));
        reactionRankDatabase.Add(new ReactionRankData(27, 6, Reaction.Parry, new Dictionary<StatModifier, float> { { StatModifier.Parry_Success_Rate, 0.225f }, { StatModifier.Parry_Success_Mitigation_Rate, 0.95f }, { StatModifier.Parry_Fail_Mitigation_Rate, 0.125f } }, 13.77f));
        reactionRankDatabase.Add(new ReactionRankData(28, 7, Reaction.Parry, new Dictionary<StatModifier, float> { { StatModifier.Parry_Success_Rate, 0.25f }, { StatModifier.Parry_Success_Mitigation_Rate, 0.96f }, { StatModifier.Parry_Fail_Mitigation_Rate, 0.15f } }, 17.27f));
        reactionRankDatabase.Add(new ReactionRankData(29, 8, Reaction.Parry, new Dictionary<StatModifier, float> { { StatModifier.Parry_Success_Rate, 0.275f }, { StatModifier.Parry_Success_Mitigation_Rate, 0.97f }, { StatModifier.Parry_Fail_Mitigation_Rate, 0.175f } }, 21f));
        reactionRankDatabase.Add(new ReactionRankData(30, 9, Reaction.Parry, new Dictionary<StatModifier, float> { { StatModifier.Parry_Success_Rate, 0.3f }, { StatModifier.Parry_Success_Mitigation_Rate, 0.98f }, { StatModifier.Parry_Fail_Mitigation_Rate, 0.2f } }, 24.94f));
        reactionRankDatabase.Add(new ReactionRankData(31, 10, Reaction.Parry, new Dictionary<StatModifier, float> { { StatModifier.Parry_Success_Rate, 0.325f }, { StatModifier.Parry_Success_Mitigation_Rate, 0.99f }, { StatModifier.Parry_Fail_Mitigation_Rate, 0.225f } }, 29.09f));
        reactionRankDatabase.Add(new ReactionRankData(32, 11, Reaction.Parry, new Dictionary<StatModifier, float> { { StatModifier.Parry_Success_Rate, 0.35f }, { StatModifier.Parry_Success_Mitigation_Rate, 1f }, { StatModifier.Parry_Fail_Mitigation_Rate, 0.25f } }, 33.44f));

        #endregion
    }

    public ReactionRankData GetReactionRankData(Reaction reaction, int rank)
    {
        return reactionRankDatabase.Find(x => x.reaction == reaction && x.rank == rank);
    }

    public int DetermineRankFromReactionAndRankNumber(Reaction reaction, float rankNumber)
    {
        int rank = 0;
        float currentDifference = 100f;

        foreach (ReactionRankData item in reactionRankDatabase.Where(x => x.reaction == reaction))
        {
            float difference = rankNumber - item.rankRequirementGuideline;

            if (Math.Abs(difference) < currentDifference)
            {
                currentDifference = difference;
                rank = item.rank;
            }
        }

        return rank;
    }

    public float RetrieveTotalOpponentRankRequirement(Dictionary<Reaction, int> opponentReactionRanks)
    {
        float rankRequirement = 0f;

        for (int i = 0; i < opponentReactionRanks.Count; i++)
        {
            rankRequirement += reactionRankDatabase.Find(x => x.reaction == opponentReactionRanks.ElementAt(i).Key && x.rank == opponentReactionRanks.ElementAt(i).Value).rankRequirementGuideline;
        }

        return rankRequirement;
    }
}

[Serializable]
public struct ReactionRankData
{
    public int id;
    public int rank;
    public Reaction reaction;
    public Dictionary<StatModifier, float> reactionModifiers;
    public float rankRequirementGuideline;

    public ReactionRankData(int id, int rank, Reaction reaction, Dictionary<StatModifier, float> reactionModifiers, float rankRequirementGuideline)
    {
        this.id = id;
        this.rank = rank;
        this.reaction = reaction;
        this.reactionModifiers = reactionModifiers;
        this.rankRequirementGuideline = rankRequirementGuideline;
    }
}