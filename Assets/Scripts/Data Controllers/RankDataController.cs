using System;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class RankDataController : MonoBehaviour
{
    public static RankDataController Instance;
    private List<RankData> rankDatabase;
    private List<RankXPGainsData> rankXPGainsDatabase;

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
        #region Rank Data
        rankDatabase = new List<RankData>();
        rankDatabase.Add(new RankData(RankEchelon.Stone, RankDegree.First, 1.0f, 0, 0));
        rankDatabase.Add(new RankData(RankEchelon.Stone, RankDegree.Second, 1.1f, 2, 2));
        rankDatabase.Add(new RankData(RankEchelon.Stone, RankDegree.Third, 1.2f, 2, 4));
        rankDatabase.Add(new RankData(RankEchelon.Stone, RankDegree.Fourth, 1.3f, 2, 6));
        rankDatabase.Add(new RankData(RankEchelon.Stone, RankDegree.Fifth, 1.4f, 2, 8));
        rankDatabase.Add(new RankData(RankEchelon.Stone, RankDegree.Sixth, 1.5f, 2, 10));
        rankDatabase.Add(new RankData(RankEchelon.Stone, RankDegree.Seventh, 1.6f, 2, 12));
        rankDatabase.Add(new RankData(RankEchelon.Stone, RankDegree.Eighth, 1.7f, 3, 15));
        rankDatabase.Add(new RankData(RankEchelon.Stone, RankDegree.Nineth, 1.8f, 3, 18));
        rankDatabase.Add(new RankData(RankEchelon.Stone, RankDegree.Tenth, 1.9f, 3, 21));
        rankDatabase.Add(new RankData(RankEchelon.Iron, RankDegree.First, 2.0f, 3, 24));
        rankDatabase.Add(new RankData(RankEchelon.Iron, RankDegree.Second, 2.1f, 3, 27));
        rankDatabase.Add(new RankData(RankEchelon.Iron, RankDegree.Third, 2.2f, 3, 30));
        rankDatabase.Add(new RankData(RankEchelon.Iron, RankDegree.Fourth, 2.3f, 3, 33));
        rankDatabase.Add(new RankData(RankEchelon.Iron, RankDegree.Fifth, 2.4f, 4, 37));
        rankDatabase.Add(new RankData(RankEchelon.Iron, RankDegree.Sixth, 2.5f, 4, 41));
        rankDatabase.Add(new RankData(RankEchelon.Iron, RankDegree.Seventh, 2.6f, 4, 45));
        rankDatabase.Add(new RankData(RankEchelon.Iron, RankDegree.Eighth, 2.7f, 4, 49));
        rankDatabase.Add(new RankData(RankEchelon.Iron, RankDegree.Nineth, 2.8f, 4, 53));
        rankDatabase.Add(new RankData(RankEchelon.Iron, RankDegree.Tenth, 2.9f, 5, 58));
        rankDatabase.Add(new RankData(RankEchelon.Bronze, RankDegree.First, 3.0f, 5, 63));
        rankDatabase.Add(new RankData(RankEchelon.Bronze, RankDegree.Second, 3.1f, 5, 68));
        rankDatabase.Add(new RankData(RankEchelon.Bronze, RankDegree.Third, 3.2f, 5, 73));
        rankDatabase.Add(new RankData(RankEchelon.Bronze, RankDegree.Fourth, 3.3f, 6, 79));
        rankDatabase.Add(new RankData(RankEchelon.Bronze, RankDegree.Fifth, 3.4f, 6, 85));
        rankDatabase.Add(new RankData(RankEchelon.Bronze, RankDegree.Sixth, 3.5f, 6, 91));
        rankDatabase.Add(new RankData(RankEchelon.Bronze, RankDegree.Seventh, 3.6f, 7, 98));
        rankDatabase.Add(new RankData(RankEchelon.Bronze, RankDegree.Eighth, 3.7f, 7, 105));
        rankDatabase.Add(new RankData(RankEchelon.Bronze, RankDegree.Nineth, 3.8f, 7, 112));
        rankDatabase.Add(new RankData(RankEchelon.Bronze, RankDegree.Tenth, 3.9f, 8, 120));
        rankDatabase.Add(new RankData(RankEchelon.Silver, RankDegree.First, 4.0f, 8, 128));
        rankDatabase.Add(new RankData(RankEchelon.Silver, RankDegree.Second, 4.1f, 8, 136));
        rankDatabase.Add(new RankData(RankEchelon.Silver, RankDegree.Third, 4.2f, 9, 145));
        rankDatabase.Add(new RankData(RankEchelon.Silver, RankDegree.Fourth, 4.3f, 9, 154));
        rankDatabase.Add(new RankData(RankEchelon.Silver, RankDegree.Fifth, 4.4f, 10, 164));
        rankDatabase.Add(new RankData(RankEchelon.Silver, RankDegree.Sixth, 4.5f, 10, 174));
        rankDatabase.Add(new RankData(RankEchelon.Silver, RankDegree.Seventh, 4.6f, 11, 185));
        rankDatabase.Add(new RankData(RankEchelon.Silver, RankDegree.Eighth, 4.7f, 11, 196));
        rankDatabase.Add(new RankData(RankEchelon.Silver, RankDegree.Nineth, 4.8f, 12, 208));
        rankDatabase.Add(new RankData(RankEchelon.Silver, RankDegree.Tenth, 4.9f, 12, 220));
        rankDatabase.Add(new RankData(RankEchelon.Gold, RankDegree.First, 5.0f, 13, 233));
        rankDatabase.Add(new RankData(RankEchelon.Gold, RankDegree.Second, 5.1f, 14, 247));
        rankDatabase.Add(new RankData(RankEchelon.Gold, RankDegree.Third, 5.2f, 14, 261));
        rankDatabase.Add(new RankData(RankEchelon.Gold, RankDegree.Fourth, 5.3f, 15, 276));
        rankDatabase.Add(new RankData(RankEchelon.Gold, RankDegree.Fifth, 5.4f, 16, 292));
        rankDatabase.Add(new RankData(RankEchelon.Gold, RankDegree.Sixth, 5.5f, 17, 309));
        rankDatabase.Add(new RankData(RankEchelon.Gold, RankDegree.Seventh, 5.6f, 17, 326));
        rankDatabase.Add(new RankData(RankEchelon.Gold, RankDegree.Eighth, 5.7f, 18, 344));
        rankDatabase.Add(new RankData(RankEchelon.Gold, RankDegree.Nineth, 5.8f, 19, 363));
        rankDatabase.Add(new RankData(RankEchelon.Gold, RankDegree.Tenth, 5.9f, 20, 383));
        rankDatabase.Add(new RankData(RankEchelon.Platinum, RankDegree.First, 6.0f, 21, 404));
        rankDatabase.Add(new RankData(RankEchelon.Platinum, RankDegree.Second, 6.1f, 22, 426));
        rankDatabase.Add(new RankData(RankEchelon.Platinum, RankDegree.Third, 6.2f, 23, 449));
        rankDatabase.Add(new RankData(RankEchelon.Platinum, RankDegree.Fourth, 6.3f, 24, 473));
        rankDatabase.Add(new RankData(RankEchelon.Platinum, RankDegree.Fifth, 6.4f, 26, 499));
        rankDatabase.Add(new RankData(RankEchelon.Platinum, RankDegree.Sixth, 6.5f, 27, 526));
        rankDatabase.Add(new RankData(RankEchelon.Platinum, RankDegree.Seventh, 6.6f, 28, 554));
        rankDatabase.Add(new RankData(RankEchelon.Platinum, RankDegree.Eighth, 6.7f, 30, 584));
        rankDatabase.Add(new RankData(RankEchelon.Platinum, RankDegree.Nineth, 6.8f, 31, 615));
        rankDatabase.Add(new RankData(RankEchelon.Platinum, RankDegree.Tenth, 6.9f, 33, 648));
        rankDatabase.Add(new RankData(RankEchelon.Diamond, RankDegree.First, 7.0f, 34, 682));
        rankDatabase.Add(new RankData(RankEchelon.Diamond, RankDegree.Second, 7.1f, 36, 718));
        rankDatabase.Add(new RankData(RankEchelon.Diamond, RankDegree.Third, 7.2f, 38, 756));
        rankDatabase.Add(new RankData(RankEchelon.Diamond, RankDegree.Fourth, 7.3f, 40, 796));
        rankDatabase.Add(new RankData(RankEchelon.Diamond, RankDegree.Fifth, 7.4f, 42, 838));
        rankDatabase.Add(new RankData(RankEchelon.Diamond, RankDegree.Sixth, 7.5f, 44, 882));
        rankDatabase.Add(new RankData(RankEchelon.Diamond, RankDegree.Seventh, 7.6f, 46, 928));
        rankDatabase.Add(new RankData(RankEchelon.Diamond, RankDegree.Eighth, 7.7f, 48, 976));
        rankDatabase.Add(new RankData(RankEchelon.Diamond, RankDegree.Nineth, 7.8f, 51, 1027));
        rankDatabase.Add(new RankData(RankEchelon.Diamond, RankDegree.Tenth, 7.9f, 53, 1080));
        rankDatabase.Add(new RankData(RankEchelon.Master, RankDegree.First, 8.0f, 56, 1136));
        rankDatabase.Add(new RankData(RankEchelon.Master, RankDegree.Second, 8.1f, 59, 1195));
        rankDatabase.Add(new RankData(RankEchelon.Master, RankDegree.Third, 8.2f, 62, 1257));
        rankDatabase.Add(new RankData(RankEchelon.Master, RankDegree.Fourth, 8.3f, 65, 1322));
        rankDatabase.Add(new RankData(RankEchelon.Master, RankDegree.Fifth, 8.4f, 68, 1390));
        rankDatabase.Add(new RankData(RankEchelon.Master, RankDegree.Sixth, 8.5f, 71, 1461));
        rankDatabase.Add(new RankData(RankEchelon.Master, RankDegree.Seventh, 8.6f, 75, 1536));
        rankDatabase.Add(new RankData(RankEchelon.Master, RankDegree.Eighth, 8.7f, 79, 1615));
        rankDatabase.Add(new RankData(RankEchelon.Master, RankDegree.Nineth, 8.8f, 83, 1698));
        rankDatabase.Add(new RankData(RankEchelon.Master, RankDegree.Tenth, 8.9f, 87, 1785));
        rankDatabase.Add(new RankData(RankEchelon.Grand_Master, RankDegree.First, 9.0f, 91, 1876));
        rankDatabase.Add(new RankData(RankEchelon.Grand_Master, RankDegree.Second, 9.1f, 96, 1972));
        rankDatabase.Add(new RankData(RankEchelon.Grand_Master, RankDegree.Third, 9.2f, 101, 2073));
        rankDatabase.Add(new RankData(RankEchelon.Grand_Master, RankDegree.Fourth, 9.3f, 106, 2179));
        rankDatabase.Add(new RankData(RankEchelon.Grand_Master, RankDegree.Fifth, 9.4f, 111, 2290));
        rankDatabase.Add(new RankData(RankEchelon.Grand_Master, RankDegree.Sixth, 9.5f, 116, 2406));
        rankDatabase.Add(new RankData(RankEchelon.Grand_Master, RankDegree.Seventh, 9.6f, 122, 2528));
        rankDatabase.Add(new RankData(RankEchelon.Grand_Master, RankDegree.Eighth, 9.7f, 128, 2656));
        rankDatabase.Add(new RankData(RankEchelon.Grand_Master, RankDegree.Nineth, 9.8f, 135, 2791));
        rankDatabase.Add(new RankData(RankEchelon.Grand_Master, RankDegree.Tenth, 9.9f, 142, 2933));
        rankDatabase.Add(new RankData(RankEchelon.Legend, RankDegree.First, 10.0f, 297, 3230));
        rankDatabase.Add(new RankData(RankEchelon.Legend, RankDegree.Second, 10.1f, 326, 3556));
        rankDatabase.Add(new RankData(RankEchelon.Legend, RankDegree.Third, 10.2f, 359, 3915));
        rankDatabase.Add(new RankData(RankEchelon.Legend, RankDegree.Fourth, 10.3f, 395, 4310));
        rankDatabase.Add(new RankData(RankEchelon.Legend, RankDegree.Fifth, 10.4f, 434, 4744));
        rankDatabase.Add(new RankData(RankEchelon.Legend, RankDegree.Sixth, 10.5f, 478, 5222));
        rankDatabase.Add(new RankData(RankEchelon.Legend, RankDegree.Seventh, 10.6f, 526, 5748));
        rankDatabase.Add(new RankData(RankEchelon.Legend, RankDegree.Eighth, 10.7f, 578, 6326));
        rankDatabase.Add(new RankData(RankEchelon.Legend, RankDegree.Nineth, 10.8f, 636, 6962));
        rankDatabase.Add(new RankData(RankEchelon.Legend, RankDegree.Tenth, 10.9f, 700, 7662));
        #endregion

        #region Rank XP Gains Data
        rankXPGainsDatabase = new List<RankXPGainsData>();
        rankXPGainsDatabase.Add(new RankXPGainsData(-1f, 10));
        rankXPGainsDatabase.Add(new RankXPGainsData(-0.9f, 14));
        rankXPGainsDatabase.Add(new RankXPGainsData(-0.8f, 18));
        rankXPGainsDatabase.Add(new RankXPGainsData(-0.7f, 23));
        rankXPGainsDatabase.Add(new RankXPGainsData(-0.6f, 28));
        rankXPGainsDatabase.Add(new RankXPGainsData(-0.5f, 33));
        rankXPGainsDatabase.Add(new RankXPGainsData(-0.4f, 38));
        rankXPGainsDatabase.Add(new RankXPGainsData(-0.3f, 41));
        rankXPGainsDatabase.Add(new RankXPGainsData(-0.2f, 44));
        rankXPGainsDatabase.Add(new RankXPGainsData(-0.1f, 47));
        rankXPGainsDatabase.Add(new RankXPGainsData(0f, 50));
        rankXPGainsDatabase.Add(new RankXPGainsData(0.1f, 60));
        rankXPGainsDatabase.Add(new RankXPGainsData(0.2f, 70));
        rankXPGainsDatabase.Add(new RankXPGainsData(0.3f, 80));
        rankXPGainsDatabase.Add(new RankXPGainsData(0.4f, 90));
        rankXPGainsDatabase.Add(new RankXPGainsData(0.5f, 100));
        rankXPGainsDatabase.Add(new RankXPGainsData(0.6f, 140));
        rankXPGainsDatabase.Add(new RankXPGainsData(0.7f, 180));
        rankXPGainsDatabase.Add(new RankXPGainsData(0.8f, 220));
        rankXPGainsDatabase.Add(new RankXPGainsData(0.9f, 260));
        rankXPGainsDatabase.Add(new RankXPGainsData(1f, 300));
        #endregion
    }

    public float GetEchelonDegreeNumber(RankEchelon echelon, RankDegree degree)
    {
        return rankDatabase.Find(x => x.echelon == echelon && x.degree == degree).echelonDegreeNumber;
    }

    public int GetXPFromDegreeDelta(float degreeDelta)
    {
        return rankXPGainsDatabase.Find(x => x.degreeDelta == degreeDelta).xPGained;
    }

    public int GetTotalCTP(RankEchelon echelon, RankDegree degree)
    {
        return rankDatabase.Find(x => x.echelon == echelon && x.degree == degree).totalCTP;
    }
}

[Serializable]
public struct RankData
{
    public RankEchelon echelon;
    public RankDegree degree;
    public float echelonDegreeNumber;
    public int increaseInCTP;
    public int totalCTP;

    public RankData(RankEchelon echelon, RankDegree degree, float echelonDegreeNumber, int increaseInCTP, int totalCTP)
    {
        this.echelon = echelon;
        this.degree = degree;
        this.echelonDegreeNumber = echelonDegreeNumber;
        this.increaseInCTP = increaseInCTP;
        this.totalCTP = totalCTP;
    }
}

[Serializable]
public struct RankXPGainsData
{
    public float degreeDelta;
    public int xPGained;

    public RankXPGainsData(float degreeDelta, int xPGained)
    {
        this.degreeDelta = degreeDelta;
        this.xPGained = xPGained;
    }
}