using System;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] private EquipmentSlot[] slots;
    public EquipmentSlot[] Slots { get { return slots; } }
    private readonly List<Item> snapshot = new();
    public static Equipment Instance;

    public event EventHandler OnEquipmentChange;


    // -------------------------------------------------------------------------------- Awake -------------------------------------------------------------------------------
    private void Awake()
    {
        Instance = this;
    }










    // -------------------------------------------------------------------------------- Start -------------------------------------------------------------------------------
    private void Start()
    {
        EquipmentSlot.OnEquipItemDropped += EquipmentSlot_OnEquipItemDropped;
        Cursor.Instance.OnItemDropped += Cursor_OnItemDropped;

        SetSnapshot();
    }










    // ----------------------------------------------------------------------- Cursor: On Item Dropped ----------------------------------------------------------------------
    private void Cursor_OnItemDropped(object sender, EventArgs e)
    {
        // Check to see if there are any differences between the current state of the equipment and the snapshot
        for (int i = 0; i < slots.Length; i++)
        {
            if ((slots[i].Item && snapshot[i] &&
                slots[i].Item.Equals(snapshot[i])) ||
                slots[i].Item == null && snapshot[i] == null) continue;


            // There is a difference
            OnEquipmentChange?.Invoke(this, new EventArgs());
            SetSnapshot();
            break;
        }
    }










    // ---------------------------------------------------------------- EquipmentSlot: On Equip Item Dropped ----------------------------------------------------------------
    private void EquipmentSlot_OnEquipItemDropped(object sender, EquipmentSlot.OnEquipItemDroppedEventArgs e)
    {
        Slot otherSlot = e.otherSlot;
        EquipableItem equipableItem = otherSlot.Item as EquipableItem;

        foreach (EquipmentSlot equipmentSlot in slots)
        {
            if (equipmentSlot.EquipType == equipableItem.EquipType)
            {
                if (equipmentSlot.Item)
                {
                    equipmentSlot.SwapItems(otherSlot);
                }
                else
                {
                    equipmentSlot.AddItem(equipableItem);
                    otherSlot.RemoveItem();
                }
                break;
            }
        }
    }










    // ---------------------------------------------------------------------------- Set Snapshot ----------------------------------------------------------------------------
    private void SetSnapshot()
    {
        snapshot.Clear();
        foreach (EquipmentSlot slot in slots)
        {
            snapshot.Add(slot.Item);
        }
    }
    
    
    
    
    
    
    
    
    
    
    // ----------------------------------------------------------------------------- Get Weapon -----------------------------------------------------------------------------
    public Weapon GetWeapon()
    {
        foreach (EquipmentSlot equipmentSlot in slots)
        {
            if (equipmentSlot.EquipType == EquipmentType.Weapon && equipmentSlot.Item)
            {
                return equipmentSlot.Item as Weapon;
            }
        }

        return null;
    }
}