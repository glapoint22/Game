using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Weapon")]
public class Weapon : EquipableItem
{
    // Damage
    [SerializeField] private int damage;

    // Damage over Time
    [SerializeField] private int damageOverTime;

    // Time
    [SerializeField] private int time;

    public Weapon()
    {
        EquipType = EquipmentType.Weapon;
    }
}