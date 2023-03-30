public class Health : PlayerStat
{
    public override void SetValue(int playerAttributeValue)
    {
        value = playerAttributeValue * 2;
    }
}