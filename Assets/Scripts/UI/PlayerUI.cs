using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enduranceValueTextMesh;
    [SerializeField] private TextMeshProUGUI powerValueTextMesh;
    [SerializeField] private TextMeshProUGUI defenseValueTextMesh;
    [SerializeField] private TextMeshProUGUI criticalStrikeValueTextMesh;
    [SerializeField] private TextMeshProUGUI vitalityStrikeValueTextMesh;




    // -------------------------------------------------------------------------------- Start -------------------------------------------------------------------------------
    private void Start()
    {
        Player.Instance.OnAttributesChange += Player_OnAttributesChange;
        SetAttributes();
    }

    private void Player_OnAttributesChange(object sender, System.EventArgs e)
    {
        SetAttributes();
    }

    




    private void SetAttributes()
    {
        enduranceValueTextMesh.text = Player.Instance.Endurance.Value.ToString();
        powerValueTextMesh.text = Player.Instance.Power.Value.ToString();
        defenseValueTextMesh.text = Player.Instance.Defense.Value.ToString();
        criticalStrikeValueTextMesh.text = Player.Instance.CriticalStrike.Value.ToString();
        vitalityStrikeValueTextMesh.text = Player.Instance.Vitality.Value.ToString();
    }
}