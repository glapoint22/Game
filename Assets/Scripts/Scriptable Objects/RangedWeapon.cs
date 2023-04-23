using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Ranged Weapon")]
public class RangedWeapon : Weapon
{
    // Range
    [SerializeField] private int range;
}