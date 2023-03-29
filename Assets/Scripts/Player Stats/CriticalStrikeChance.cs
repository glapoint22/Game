public class CriticalStrikeChance : PlayerStat
{
    public CriticalStrikeChance()
    {
        attributeType = AttributeType.CriticalStrike;
    }

    public override void SetValue(int playerAttributeValue)
    {
        value = playerAttributeValue * 0.0025f;
    }
}