using UnityEngine;

[CreateAssetMenu()]
public class Weapon : EquipableItem
{
    // Damage
    [SerializeField] private int damage;
    public int Damage { get { return damage; } }

    // Critical Strike
    [SerializeField] private int criticalStrike;
    public int CriticalStrike { get { return criticalStrike; } }


    // Critical Strike Damage
    [SerializeField] private int criticalStrikeDamage;
    public int CriticalStrikeDamage { get { return criticalStrikeDamage; } }

    public Weapon()
    {
        EquipType = EquipType.Weapon;
    }
}