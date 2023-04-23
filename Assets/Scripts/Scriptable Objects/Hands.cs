using UnityEngine;

[CreateAssetMenu(menuName = "Apparel/Hands")]
public class Hands : EquipableItem
{
    public Hands()
    {
        EquipType = EquipmentType.Hands;
    }
}