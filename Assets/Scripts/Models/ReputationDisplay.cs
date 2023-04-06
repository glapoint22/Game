using System;
using TMPro;
using UnityEngine;

[Serializable]
public class ReputationDisplay
{
    // Faction
    [SerializeField] private FactionType faction;
    public FactionType Faction { get { return faction; } }

    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private TextMeshProUGUI reputationLevelTextMesh;

    public void Update(Color reputationColor, string reputationLevel, int maxReputationPoints, int reputationPoints)
    {
        reputationLevelTextMesh.text = reputationLevel;
        progressBar.SetColor(reputationColor);
        progressBar.SetMaxValue(maxReputationPoints);
        progressBar.SetValue(reputationPoints);
    }
}