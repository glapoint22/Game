using System;
using UnityEngine;

[Serializable]
public class Reputation
{
    // Faction
    [SerializeField] private FactionType faction;
    public FactionType Faction { get { return faction; } }

    // Points
    [SerializeField] private int points;
    public int Points { get { return points; } }
}