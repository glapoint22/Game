public class BaseDamage : PlayerStat
{
    public BaseDamage()
    {
        attributeType = AttributeType.Power;
    }

    public override void SetValue(int playerAttributeValue)
    {
        value = (1 + playerAttributeValue / 100);
    }
}