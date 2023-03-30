public class BaseDamage : PlayerStat
{
    
    public override void SetValue(int playerAttributeValue)
    {
        value = (1 + playerAttributeValue / 100);
    }
}