public class DamageReduction : PlayerStat
{
    public override void SetValue(int playerAttributeValue)
    {
        value = playerAttributeValue / (playerAttributeValue + 400);
    }
}