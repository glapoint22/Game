public class CriticalStrikeChance : CharacterStat
{
    
    public override void SetValue(int playerAttributeValue)
    {
        value = playerAttributeValue * 0.0025f;
    }
}