public class HealthRegenerationRate : PlayerStat
{
    public override void SetValue(int playerAttributeValue)
    {
        value = (playerAttributeValue / 5);
    }
}