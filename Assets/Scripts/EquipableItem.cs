using UnityEngine;

public abstract class EquipableItem : Item
{
    // Durability
    [SerializeField] private int durability;
    public int Durability { get { return durability; } }

    public EquipType EquipType { get; protected set; }
}