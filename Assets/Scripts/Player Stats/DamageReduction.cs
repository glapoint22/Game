public class DamageReduction : PlayerStat
{
    public DamageReduction()
    {
        attributeType = AttributeType.Defense;
    }

    public override void SetValue(int playerAttributeValue)
    {
        value = playerAttributeValue / (playerAttributeValue + 400);
    }
}