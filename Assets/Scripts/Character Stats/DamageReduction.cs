public class DamageReduction : CharacterStat
{
    public override void SetValue(int playerAttributeValue)
    {
        value = playerAttributeValue / (playerAttributeValue + 400);
    }
}