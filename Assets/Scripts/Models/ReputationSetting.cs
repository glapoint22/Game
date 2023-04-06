using System;
using UnityEngine;

[Serializable]
public class ReputationSetting
{
    // Level
    [SerializeField] private ReputationLevel level;
    public ReputationLevel Level { get { return level; } }

    // Points
    [SerializeField] int points;
    public int Points { get { return points; } }

    // Color
    [SerializeField] private Color color;
    public Color Color { get { return color; } }
}