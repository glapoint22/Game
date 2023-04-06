using System;
using UnityEngine;

[Serializable]
public class PlayerReputation
{
    // Faction
    [SerializeField] private FactionType faction;
    public FactionType Faction { get { return faction; } }

    // Level
    [SerializeField] private ReputationLevel level;
    public ReputationLevel Level { get { return level; } }

    // Points
    [SerializeField] private int points;
    public int Points { get { return points; } }


    public void Update(ReputationLevel level, int points)
    {
        this.level = level;
        this.points = points;
    }
}