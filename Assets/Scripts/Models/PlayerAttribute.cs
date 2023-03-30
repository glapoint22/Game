using UnityEngine;

public abstract class PlayerAttribute
{
    // Attribute Type
    protected AttributeType attributeType;
    public AttributeType AttributeType { get { return attributeType; } }


    // Description
    [SerializeField] private string description;



    // Base Value
    [SerializeField] private int baseValue;


    private PlayerStat playerStat;

    // Value
    public int Value { get; private set; }







    public void Initialize(PlayerStat playerStat)
    {
        ResetValue();
        this.playerStat = playerStat;
    }



    public void SetValue(int value)
    {
        Value += value;
        playerStat.SetValue(Value);
    }


    public void ResetValue()
    {
        Value = baseValue;
    }
}