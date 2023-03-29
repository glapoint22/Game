using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot
{
    [SerializeField] private EquipmentType equipType;
    public EquipmentType EquipType { get { return equipType; } }


    // On Item Dropped
    public static event EventHandler<OnEquipItemDroppedEventArgs> OnEquipItemDropped;
    public class OnEquipItemDroppedEventArgs : EventArgs
    {
        public Slot otherSlot;
    }

    // ------------------------------------------------------------------------------ Drop Item -----------------------------------------------------------------------------
    public override void DropItem(Slot otherSlot)
    {
        if (otherSlot.Item is not EquipableItem || otherSlot is EquipmentSlot) return;

        OnEquipItemDropped?.Invoke(this, new OnEquipItemDroppedEventArgs
        {
            otherSlot = otherSlot
        });
    }










    // --------------------------------------------------------------------------- On Pointer Click -------------------------------------------------------------------------
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (Cursor.Instance.Item != null && Cursor.Instance.Item is not EquipableItem) return;

        base.OnPointerClick(eventData);
    }
}