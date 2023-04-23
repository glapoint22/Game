using UnityEngine;

[CreateAssetMenu(menuName = "Apparel/Chest")]
public class Chest : EquipableItem
{
    public Chest()
    {
        EquipType = EquipmentType.Chest;
    }
}