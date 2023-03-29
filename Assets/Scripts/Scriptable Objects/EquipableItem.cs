using UnityEngine;

public abstract class EquipableItem : Item
{
    // Durability
    [SerializeField] private int durability;

    // Attributes
    [SerializeField] private Attribute[] attributes;
    public Attribute[] Attributes { get { return attributes; } }

    // Equipment Type
    public EquipmentType EquipType { get; protected set; }
}