using UnityEngine;

[CreateAssetMenu(menuName = "Apparel/Waist")]
public class Waist : EquipableItem
{
    // Sockets - Used for items you collect which are in different colors ROYGBIV that enhance or augment characteristics


    public Waist()
    {
        EquipType = EquipmentType.Waist;
    }
}