using System;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class PathDataController : MonoBehaviour
{
    public static PathDataController Instance;
    private List<PathData> pathDatabase;
    private List<PathEnergyBonusData> pathEnergyBonusDatabase;
    private List<PathAscensionData> pathAscensionDatabase;
    private List<EnergyGainsData> energyGainsDatabase;

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

    void Start()
    {
        #region Path Database
        pathDatabase = new List<PathData>();
        pathDatabase.Add(new PathData(0, Path.Fire, CombatEventType.Reaction, PathEnergyFocus.Parry_Success_Rate, "Focuses inward, improving the chance to successfully parry an attack."));
        pathDatabase.Add(new PathData(1, Path.Water, CombatEventType.Action, PathEnergyFocus.Dodge_Success_Rate, "Focuses outward, reducing the chance to successfully dodge an attack."));
        pathDatabase.Add(new PathData(2, Path.Air, CombatEventType.Reaction, PathEnergyFocus.Dodge_Success_Rate, "Focuses inward, improving the chance to successfully dodge an attack."));
        pathDatabase.Add(new PathData(3, Path.Earth, CombatEventType.Action, PathEnergyFocus.Block_Success_Rate, "Focuses outward, reducing the chance to successfully block an attack."));
        pathDatabase.Add(new PathData(4, Path.Shadow, CombatEventType.Action, PathEnergyFocus.Parry_Success_Rate, "Focuses outward, reducing the chance to successfully parry an attack."));
        pathDatabase.Add(new PathData(5, Path.Light, CombatEventType.Reaction, PathEnergyFocus.Block_Success_Rate, "Focuses inward, improving the chance to successfully block an attack."));
        #endregion

        #region Path Energy Bonus Database
        pathEnergyBonusDatabase = new List<PathEnergyBonusData>();
        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(0, Path.Fire, 0, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, 0.04f }, { EnergySegment.Second_Segment, 0.05f }, { EnergySegment.Third_Segment, 0.06f }, { EnergySegment.Fourth_Segment, 0.07f }, { EnergySegment.Fifth_Segment, 0.08f }, { EnergySegment.Sixth_Segment, 0.09f } }));
        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(1, Path.Water, 0, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, -0.06f }, { EnergySegment.Second_Segment, -0.07f }, { EnergySegment.Third_Segment, -0.08f }, { EnergySegment.Fourth_Segment, -0.09f }, { EnergySegment.Fifth_Segment, -0.1f }, { EnergySegment.Sixth_Segment, -0.11f } }));
        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(2, Path.Air, 0, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, 0.06f }, { EnergySegment.Second_Segment, 0.07f }, { EnergySegment.Third_Segment, 0.08f }, { EnergySegment.Fourth_Segment, 0.09f }, { EnergySegment.Fifth_Segment, 0.1f }, { EnergySegment.Sixth_Segment, 0.11f } }));
        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(3, Path.Earth, 0, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, -0.08f }, { EnergySegment.Second_Segment, -0.09f }, { EnergySegment.Third_Segment, -0.1f }, { EnergySegment.Fourth_Segment, -0.11f }, { EnergySegment.Fifth_Segment, -0.12f }, { EnergySegment.Sixth_Segment, -0.13f } }));
        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(4, Path.Shadow, 0, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, -0.04f }, { EnergySegment.Second_Segment, -0.05f }, { EnergySegment.Third_Segment, -0.06f }, { EnergySegment.Fourth_Segment, -0.07f }, { EnergySegment.Fifth_Segment, -0.08f }, { EnergySegment.Sixth_Segment, -0.09f } }));
        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(5, Path.Light, 0, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, 0.08f }, { EnergySegment.Second_Segment, 0.09f }, { EnergySegment.Third_Segment, 0.1f }, { EnergySegment.Fourth_Segment, 0.11f }, { EnergySegment.Fifth_Segment, 0.12f }, { EnergySegment.Sixth_Segment, 0.13f } }));

        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(6, Path.Fire, 1, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, 0.05f }, { EnergySegment.Second_Segment, 0.06f }, { EnergySegment.Third_Segment, 0.07f }, { EnergySegment.Fourth_Segment, 0.08f }, { EnergySegment.Fifth_Segment, 0.09f }, { EnergySegment.Sixth_Segment, 0.1f } }));
        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(7, Path.Water, 1, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, -0.08f }, { EnergySegment.Second_Segment, -0.09f }, { EnergySegment.Third_Segment, -0.1f }, { EnergySegment.Fourth_Segment, -0.11f }, { EnergySegment.Fifth_Segment, -0.12f }, { EnergySegment.Sixth_Segment, -0.13f } }));
        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(8, Path.Air, 1, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, 0.08f }, { EnergySegment.Second_Segment, 0.09f }, { EnergySegment.Third_Segment, 0.1f }, { EnergySegment.Fourth_Segment, 0.11f }, { EnergySegment.Fifth_Segment, 0.12f }, { EnergySegment.Sixth_Segment, 0.13f } }));
        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(9, Path.Earth, 1, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, -0.11f }, { EnergySegment.Second_Segment, -0.12f }, { EnergySegment.Third_Segment, -0.13f }, { EnergySegment.Fourth_Segment, -0.14f }, { EnergySegment.Fifth_Segment, -0.15f }, { EnergySegment.Sixth_Segment, -0.16f } }));
        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(10, Path.Shadow, 1, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, -0.05f }, { EnergySegment.Second_Segment, -0.06f }, { EnergySegment.Third_Segment, -0.07f }, { EnergySegment.Fourth_Segment, -0.08f }, { EnergySegment.Fifth_Segment, -0.09f }, { EnergySegment.Sixth_Segment, -0.1f } }));
        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(11, Path.Light, 1, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, 0.11f }, { EnergySegment.Second_Segment, 0.12f }, { EnergySegment.Third_Segment, 0.13f }, { EnergySegment.Fourth_Segment, 0.14f }, { EnergySegment.Fifth_Segment, 0.15f }, { EnergySegment.Sixth_Segment, 0.16f } }));

        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(12, Path.Fire, 2, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, 0.06f }, { EnergySegment.Second_Segment, 0.07f }, { EnergySegment.Third_Segment, 0.08f }, { EnergySegment.Fourth_Segment, 0.09f }, { EnergySegment.Fifth_Segment, 0.1f }, { EnergySegment.Sixth_Segment, 0.11f } }));
        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(13, Path.Water, 2, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, -0.1f }, { EnergySegment.Second_Segment, -0.11f }, { EnergySegment.Third_Segment, -0.12f }, { EnergySegment.Fourth_Segment, -0.13f }, { EnergySegment.Fifth_Segment, -0.14f }, { EnergySegment.Sixth_Segment, -0.15f } }));
        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(14, Path.Air, 2, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, 0.1f }, { EnergySegment.Second_Segment, 0.11f }, { EnergySegment.Third_Segment, 0.12f }, { EnergySegment.Fourth_Segment, 0.13f }, { EnergySegment.Fifth_Segment, 0.14f }, { EnergySegment.Sixth_Segment, 0.15f } }));
        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(15, Path.Earth, 2, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, -0.14f }, { EnergySegment.Second_Segment, -0.15f }, { EnergySegment.Third_Segment, -0.16f }, { EnergySegment.Fourth_Segment, -0.17f }, { EnergySegment.Fifth_Segment, -0.18f }, { EnergySegment.Sixth_Segment, -0.19f } }));
        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(16, Path.Shadow, 2, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, -0.06f }, { EnergySegment.Second_Segment, -0.07f }, { EnergySegment.Third_Segment, -0.08f }, { EnergySegment.Fourth_Segment, -0.09f }, { EnergySegment.Fifth_Segment, -0.1f }, { EnergySegment.Sixth_Segment, -0.11f } }));
        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(17, Path.Light, 2, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, 0.14f }, { EnergySegment.Second_Segment, 0.15f }, { EnergySegment.Third_Segment, 0.16f }, { EnergySegment.Fourth_Segment, 0.17f }, { EnergySegment.Fifth_Segment, 0.18f }, { EnergySegment.Sixth_Segment, 0.19f } }));

        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(18, Path.Fire, 3, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, 0.07f }, { EnergySegment.Second_Segment, 0.08f }, { EnergySegment.Third_Segment, 0.09f }, { EnergySegment.Fourth_Segment, 0.1f }, { EnergySegment.Fifth_Segment, 0.11f }, { EnergySegment.Sixth_Segment, 0.12f } }));
        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(19, Path.Water, 3, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, -0.13f }, { EnergySegment.Second_Segment, -0.14f }, { EnergySegment.Third_Segment, -0.15f }, { EnergySegment.Fourth_Segment, -0.16f }, { EnergySegment.Fifth_Segment, -0.17f }, { EnergySegment.Sixth_Segment, -0.18f } }));
        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(20, Path.Air, 3, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, 0.13f }, { EnergySegment.Second_Segment, 0.14f }, { EnergySegment.Third_Segment, 0.15f }, { EnergySegment.Fourth_Segment, 0.16f }, { EnergySegment.Fifth_Segment, 0.17f }, { EnergySegment.Sixth_Segment, 0.18f } }));
        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(21, Path.Earth, 3, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, -0.17f }, { EnergySegment.Second_Segment, -0.18f }, { EnergySegment.Third_Segment, -0.19f }, { EnergySegment.Fourth_Segment, -0.2f }, { EnergySegment.Fifth_Segment, -0.21f }, { EnergySegment.Sixth_Segment, -0.22f } }));
        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(22, Path.Shadow, 3, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, -0.07f }, { EnergySegment.Second_Segment, -0.08f }, { EnergySegment.Third_Segment, -0.09f }, { EnergySegment.Fourth_Segment, -0.1f }, { EnergySegment.Fifth_Segment, -0.11f }, { EnergySegment.Sixth_Segment, -0.12f } }));
        pathEnergyBonusDatabase.Add(new PathEnergyBonusData(23, Path.Light, 3, new Dictionary<EnergySegment, float>() { { EnergySegment.First_Segment, 0.17f }, { EnergySegment.Second_Segment, 0.18f }, { EnergySegment.Third_Segment, 0.19f }, { EnergySegment.Fourth_Segment, 0.2f }, { EnergySegment.Fifth_Segment, 0.21f }, { EnergySegment.Sixth_Segment, 0.22f } }));
        #endregion

        #region Path Ascension Database
        pathAscensionDatabase = new List<PathAscensionData>();
        pathAscensionDatabase.Add(new PathAscensionData(0, 0, 0, 0f, 0));
        pathAscensionDatabase.Add(new PathAscensionData(1, 1, 35, 0.25f, 0));
        pathAscensionDatabase.Add(new PathAscensionData(2, 2, 95, 0.25f, 1));
        pathAscensionDatabase.Add(new PathAscensionData(3, 3, 180, 0.5f, 2));
        #endregion

        #region Energy Gains Database
        energyGainsDatabase = new List<EnergyGainsData>();
        energyGainsDatabase.Add(new EnergyGainsData(0, CombatEventType.Action, DecisionResult.Success, 1f));
        energyGainsDatabase.Add(new EnergyGainsData(1, CombatEventType.Action, DecisionResult.Failure, 0.5f));
        energyGainsDatabase.Add(new EnergyGainsData(2, CombatEventType.Reaction, DecisionResult.Success, 0.5f));
        energyGainsDatabase.Add(new EnergyGainsData(3, CombatEventType.Reaction, DecisionResult.Failure, 0.25f));
        #endregion

    }

    public PathData GetPathData(Path path)
    {
        return pathDatabase.Find(x => x.path == path);
    }

    public PathEnergyBonusData GetPathEnergyBonusData(Path path, int ascensionGems)
    {
        return pathEnergyBonusDatabase.Find(x => x.path == path && x.ascension == ascensionGems);
    }

    public PathAscensionData GetPathAscensionData(int ascensionGems)
    {
        return pathAscensionDatabase.Find(x => x.ascension == ascensionGems);
    }
}

[Serializable]
public struct PathData
{
    public int id;
    public Path path;
    public CombatEventType styleFocus;
    public PathEnergyFocus energyFocus;
    public string focusStatement;

    public PathData(int id, Path path, CombatEventType styleFocus, PathEnergyFocus energyFocus, string focusStatement)
    {
        this.id = id;
        this.path = path;
        this.styleFocus = styleFocus;
        this.energyFocus = energyFocus;
        this.focusStatement = focusStatement;
    }
}

[Serializable]
public struct PathEnergyBonusData
{
    public int id;
    public Path path;
    public int ascension;
    public Dictionary<EnergySegment, float> energySegmentBonuses;

    public PathEnergyBonusData(int id, Path path, int ascension, Dictionary<EnergySegment, float> energySegmentBonuses)
    {
        this.id = id;
        this.path = path;
        this.ascension = ascension;
        this.energySegmentBonuses = energySegmentBonuses;
    }
}

[Serializable]
public struct PathAscensionData
{
    public int id;
    public int ascension;
    public int energySegmentAccumulationRequirement;
    public float energyAccumulationBonus;
    public int combatInitializationEnergySegments;

    public PathAscensionData(int id, int ascension, int energySegmentAccumulationRequirement, float energyAccumulationBonus, int combatInitializationEnergySegments)
    {
        this.id = id;
        this.ascension = ascension;
        this.energySegmentAccumulationRequirement = energySegmentAccumulationRequirement;
        this.energyAccumulationBonus = energyAccumulationBonus;
        this.combatInitializationEnergySegments = combatInitializationEnergySegments;
    }
}

[Serializable]
public struct EnergyGainsData
{
    public int id;
    public CombatEventType eventType;
    public DecisionResult decisionResult;
    public float energyGainsBonus;

    public EnergyGainsData(int id, CombatEventType eventType, DecisionResult decisionResult, float energyGainsBonus)
    {
        this.id = id;
        this.eventType = eventType;
        this.decisionResult = decisionResult;
        this.energyGainsBonus = energyGainsBonus;
    }
}