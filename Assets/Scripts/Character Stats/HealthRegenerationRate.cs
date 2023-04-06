public class HealthRegenerationRate : CharacterStat
{
    public override void SetValue(int playerAttributeValue)
    {
        value = (playerAttributeValue / 5);
    }
}