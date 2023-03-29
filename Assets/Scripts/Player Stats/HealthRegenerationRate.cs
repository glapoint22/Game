public class HealthRegenerationRate : PlayerStat
{
    public HealthRegenerationRate()
    {
        attributeType = AttributeType.Vitality;
    }

    public override void SetValue(int playerAttributeValue)
    {
        value = (playerAttributeValue / 5);
    }
}