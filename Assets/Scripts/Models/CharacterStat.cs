public abstract class CharacterStat
{
    // Value
    protected float value;
    public float Value { get { return value; } }



    // ------------------------------------------------------------------------------ Set Value -----------------------------------------------------------------------------
    public abstract void SetValue(int playerAttributeValue);
}