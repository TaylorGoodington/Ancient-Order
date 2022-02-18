using System;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class TeamDataController : MonoBehaviour
{
    public static TeamDataController Instance;
    private List<TeamData> teamDatabase;

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
        //I will probably save a copy of the Team Database because I will be making changes to it based on character progression.
        teamDatabase = new List<TeamData>();
        teamDatabase.Add(new TeamData(0, TeamName.Master_Blaster, RankDegree.First, RankEchelon.Grand_Master, 0.75f));
        teamDatabase.Add(new TeamData(1, TeamName.Triple_J, RankDegree.First, RankEchelon.Stone, 0.05f));
        teamDatabase.Add(new TeamData(2, TeamName.Triple_J, RankDegree.First, RankEchelon.Stone, 0.05f));
        teamDatabase.Add(new TeamData(3, TeamName.The_Guardians, RankDegree.Tenth, RankEchelon.Legend, 1f));
    }

    public TeamData GetTeamData(int teamId)
    {
        return teamDatabase.Find(x => x.id == teamId);
    }

    private void UpdateTeamDatabase()
    {
        //Gets list of defeated teams from somewhere
        //list probably comes from GameData and is a list of the team Id and their current rank.
        //updates defeated teams to a new rank when player team ranks up
        //should be within a few degrees of their current rank
        //updates team rank data in GameData
    }
}

[Serializable]
public struct TeamData
{
    public int id;
    public TeamName teamName;
    public RankDegree degree;
    public RankEchelon echelon;
    public float cohesionFactor; //Works to effect the way the team works together

    public TeamData(int id, TeamName teamName, RankDegree degree, RankEchelon echelon, float cohesionFactor)
    {
        this.id = id;
        this.teamName = teamName;
        this.degree = degree;
        this.echelon = echelon;
        this.cohesionFactor = cohesionFactor;
    }
}