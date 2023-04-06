using System;
using UnityEngine;

[Serializable]
public class CharacterAttribute
{
    // Attribute Type
    public AttributeType AttributeType { get; private set; }


    // Character Stat
    public CharacterStat CharacterStat { get; private set; }




    // Value
    [SerializeField] private int value;
    public int Value { get; private set; }




    public CharacterAttribute(AttributeType attributeType, CharacterStat characterStat)
    {
        AttributeType = attributeType;
        CharacterStat = characterStat;
    }
    
    
    
    
    
    
    
    
    
    
    // ------------------------------------------------------------------------------ Set Value -----------------------------------------------------------------------------
    public void SetValue(int value)
    {
        Value += value;
        CharacterStat.SetValue(Value);
    }










    // -------------------------------------------------------------------------- Initialize Value --------------------------------------------------------------------------
    public void InitializeValue()
    {
        Value = value;
        CharacterStat.SetValue(Value);
    }
}