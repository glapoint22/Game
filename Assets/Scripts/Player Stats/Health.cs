public class Health : PlayerStat
{
    const int Modifier = 2;

    public Health()
    {
        attributeType = AttributeType.Endurance;
    }

    public override void SetValue(int playerAttributeValue)
    {
        value = playerAttributeValue * Modifier;
    }
}