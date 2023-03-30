public class CriticalStrikeChance : PlayerStat
{
    
    public override void SetValue(int playerAttributeValue)
    {
        value = playerAttributeValue * 0.0025f;
    }
}