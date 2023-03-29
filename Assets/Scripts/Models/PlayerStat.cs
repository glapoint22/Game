public abstract class PlayerStat
{
    // Attribute Type
    protected AttributeType attributeType;
    public AttributeType AttributeType { get { return attributeType; } }

    // Value
    protected float value;
    public float Value { get { return value; } }



    // ------------------------------------------------------------------------------ Set Value -----------------------------------------------------------------------------
    public abstract void SetValue(int playerAttributeValue);
}