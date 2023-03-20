using UnityEngine;

[CreateAssetMenu()]
public class RangedWeapon : Weapon
{
    // Range
    [SerializeField] private int range;
    public int Range { get { return range; } }
}