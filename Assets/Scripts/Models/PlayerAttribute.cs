using System;
using UnityEngine;

[Serializable]
public class PlayerAttribute
{
    // Attribute Type
    [SerializeField] private AttributeType attributeType;
    public AttributeType AttributeType { get { return attributeType; } }


    // Description
    [SerializeField] private string description;
    public string Description { get { return description; } }



    // Base Value
    [SerializeField] private int baseValue;

    // Value
    public int Value { get; private set; }



    public void Initialize()
    {
        Value = baseValue;
    }



    public void SetValue(int value)
    {
        Value += value;
    }
}