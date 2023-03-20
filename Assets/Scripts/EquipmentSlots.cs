using UnityEngine;
using static EquipSlot;

public class EquipmentSlots : MonoBehaviour
{
    [SerializeField] private EquipSlot[] slots;

    // -------------------------------------------------------------------------------- Start -------------------------------------------------------------------------------
    private void Start()
    {
        OnEquipItemDropped += EquipSlot_OnItemDropped;
    }

    private void EquipSlot_OnItemDropped(object sender, OnEquipItemDroppedEventArgs e)
    {
        Slot otherSlot = e.otherSlot;
        EquipableItem equipableItem = otherSlot.Item as EquipableItem;

        foreach (EquipSlot equipSlot in slots)
        {
            if (equipSlot.EquipType == equipableItem.EquipType)
            {
                if (equipSlot.Item)
                {
                    equipSlot.SwapItems(otherSlot);
                }
                else
                {
                    equipSlot.AddItem(equipableItem);
                    otherSlot.RemoveItem();
                }
                break;
            }
        }
    }
}