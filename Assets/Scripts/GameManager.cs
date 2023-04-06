using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ReputationSetting[] reputationSettings;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public int GetMaxReputationPoints(ReputationLevel level)
    {
        int points = 0;

        foreach (ReputationSetting reputationSetting in reputationSettings)
        {
            if (reputationSetting.Level == level)
            {
                points = reputationSetting.Points;
                break;
            }
        }

        return points;
    }



    public ReputationLevel GetNextReputationLevel(ReputationLevel currentLevel)
    {
        ReputationSetting reputation = reputationSettings
            .SkipWhile(x => x.Level != currentLevel)
            .Skip(1)
            .FirstOrDefault();

        if (reputation == null) return ReputationLevel.None;
        return reputation.Level;
    }



    public ReputationLevel GetPreviousReputationLevel(ReputationLevel currentLevel)
    {
        ReputationSetting reputation = reputationSettings
            .Reverse()
            .SkipWhile(x => x.Level != currentLevel)
            .Skip(1)
            .FirstOrDefault();

        if (reputation == null) return ReputationLevel.None;
        return reputation.Level;
    }



    public Color GetReputationColor(ReputationLevel currentLevel)
    {
        return reputationSettings.Single(x => x.Level == currentLevel).Color;
    }
}