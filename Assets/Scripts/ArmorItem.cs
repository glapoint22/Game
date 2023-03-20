using UnityEngine;

public abstract class ArmorItem : EquipableItem
{
    // Armor
    [SerializeField] private int armor;
    public int Armor { get { return armor; } }

    // Health
    [SerializeField] private int health;
    public int Health { get { return health; } }
}