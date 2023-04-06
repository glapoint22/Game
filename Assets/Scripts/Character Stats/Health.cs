public class Health : CharacterStat
{
    public override void SetValue(int playerAttributeValue)
    {
        value = playerAttributeValue * 2;
    }
}