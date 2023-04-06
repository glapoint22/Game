public class BaseDamage : CharacterStat
{
    
    public override void SetValue(int playerAttributeValue)
    {
        value = playerAttributeValue * 5;
    }
}