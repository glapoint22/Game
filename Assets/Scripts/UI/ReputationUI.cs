using System.Linq;
using UnityEngine;

public class ReputationUI : MonoBehaviour
{
    [SerializeField] private ReputationDisplay[] reputations;



    // -------------------------------------------------------------------------------- Start -------------------------------------------------------------------------------
    private void Start()
    {
        Player.Instance.OnReputationChange += Player_OnReputationChange;
        SetReputations();
    }

    private void Player_OnReputationChange(object sender, System.EventArgs e)
    {
        SetReputations();
    }


    private void SetReputations()
    {
        foreach (PlayerReputation playerReputation in Player.Instance.Reputations)
        {
            Color reputationColor = GameManager.Instance.GetReputationColor(playerReputation.Level);
            string reputationLevel = playerReputation.Level.ToString();
            int maxReputationPoints = GameManager.Instance.GetMaxReputationPoints(playerReputation.Level);
            int reputationPoints = playerReputation.Points;


            ReputationDisplay reputationDisplay = reputations
                .Single(x => x.Faction == playerReputation.Faction);

            reputationDisplay.Update(reputationColor, reputationLevel, maxReputationPoints, reputationPoints);
        }
    }
}