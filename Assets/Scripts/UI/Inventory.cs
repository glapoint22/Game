using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class Inventory : MonoBehaviour
{
    [SerializeField] private InventorySlot[] slots;
    private readonly List<Tuple<Item, int>> snapshot = new();

    public static Inventory Instance;
    public event EventHandler OnChange;


    // -------------------------------------------------------------------------------- Awake -------------------------------------------------------------------------------
    private void Awake()
    {
        Instance = this;
    }










    // -------------------------------------------------------------------------------- Start -------------------------------------------------------------------------------
    private void Start()
    {
        Cursor.Instance.OnItemDropped += Cursor_OnItemDropped;
    }










    // ----------------------------------------------------------------------- Cursor: On Item Dropped ----------------------------------------------------------------------
    private void Cursor_OnItemDropped(object sender, EventArgs e)
    {
        // Check to see if there are any differences between the current state of the inventory and the snapshot
        for (int i = 0; i < slots.Length; i++)
        {
            if ((slots[i].Item && snapshot[i].Item1 &&
                slots[i].Item.Equals(snapshot[i].Item1) &&
                slots[i].StackSize == snapshot[i].Item2) ||
                slots[i].Item == null && snapshot[i].Item1 == null) continue;

            // There is a difference
            OnChange?.Invoke(this, EventArgs.Empty);
            SetSnapshot();
            break;
        }
    }









    // ------------------------------------------------------------------------------ Add Item ------------------------------------------------------------------------------
    public void AddItem(Item item)
    {
        InventorySlot emptySlot = null;
        bool itemAdded = false;

        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlot slot = slots[i];

            // If the slot does not have an item
            if (!slot.Item)
            {
                // Non stackable items
                if (item is not StackableItem)
                {
                    // Add item to slot
                    slot.AddItem(item);
                    itemAdded = true;
                    break;
                }

                if (emptySlot == null) emptySlot = slot;
            }

            // Slot does have an item
            else
            {
                // Stackable items
                if (item is StackableItem stackableItem && slot.StackSize != stackableItem.MaxStackSize && slot.Item.Equals(item))
                {
                    slot.AddItem(item);
                    itemAdded = true;
                    emptySlot = null;
                    break;
                }
            }
        }

        if (emptySlot != null)
        {
            emptySlot.AddItem(item);
            itemAdded = true;
        }

        if (itemAdded)
        {
            OnChange?.Invoke(this, EventArgs.Empty);
            SetSnapshot();
        }
        else
        {
            // Every slot is occupied
            Debug.Log("Container Full");
        }
    }










    // ---------------------------------------------------------------------------- Set Snapshot ----------------------------------------------------------------------------
    private void SetSnapshot()
    {
        snapshot.Clear();
        foreach (InventorySlot slot in slots)
        {
            snapshot.Add(new Tuple<Item, int>(slot.Item, slot.StackSize));
        }
    }







    // --------------------------------------------------------------------------- Get Item Count ---------------------------------------------------------------------------
    public int GetItemCount(Item item)
    {
        int count = 0;

        foreach (InventorySlot slot in slots)
        {
            if (slot.Item && slot.Item.Equals(item))
            {
                count += slot.StackSize;
            }
        }

        return count;
    }










    // ---------------------------------------------------------------------------- Remove Item -----------------------------------------------------------------------------
    public void RemoveItem(Item item, int itemCount)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.Item && slot.Item.Equals(item))
            {
                int slotItemCount = slot.StackSize - itemCount;
                itemCount -= slot.StackSize;

                if (slotItemCount <= 0)
                {
                    slot.RemoveItem();
                }
                else
                {
                    slot.SetStackSize(slotItemCount);
                }

                if (itemCount <= 0) return;
            }
        }
    }
}