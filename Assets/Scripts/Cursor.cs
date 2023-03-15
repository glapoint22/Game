using System;
using UnityEngine;

public sealed class Cursor : MonoBehaviour
{
    public Item Item { get; private set; }
    private int itemCount;
    private Slot currentSlot;

    public static Cursor Instance { get; private set; }

    public event EventHandler OnItemAdded;


    // -------------------------------------------------------------------------------- Awake -------------------------------------------------------------------------------
    private void Awake()
    {
        Instance = this;
    }










    // -------------------------------------------------------------------------------- Start -------------------------------------------------------------------------------
    private void Start()
    {
        Slot.OnMouseBeginDrag += Slot_OnMouseBeginDrag;
        Slot.OnMouseEndDrag += Slot_OnMouseEndDrag;
        Slot.OnMouseClick += Slot_OnMouseClick;
    }










    // ------------------------------------------------------------------------ Slot: On Mouse Click ------------------------------------------------------------------------
    private void Slot_OnMouseClick(object sender, EventArgs e)
    {
        Slot slot = (Slot)sender;

        if (currentSlot == null)
        {
            // If the cursor has an item on it
            if (Item)
            {
                // If there is no item in the clicked slot or the two items are the same type and the sum of two stacks is less than the max item count
                if (!slot.Item || (slot.Item.Equals(Item) && slot.ItemCount + itemCount <= Item.MaxItemCount))
                {
                    // Add the item to the slot
                    slot.AddItem(Item, itemCount);

                    // Remove the item from the cursor
                    RemoveItem();


                    GameManager.Instance.DispatchUIChangeEvent();
                }
            }

            // If the slot that was clicked has an item
            else if (slot.Item)
            {
                // Add the item to the cursor
                AddItem(slot.Item, slot.ItemCount);
                slot.Disable();
                currentSlot = slot;
            }
        }
        else
        {
            // Drop the item in the clicked slot
            slot.DropItem(currentSlot);
            currentSlot.Enable();
            RemoveItem();

            currentSlot = null;

            GameManager.Instance.DispatchUIChangeEvent();
        }
    }
    
    
    
    
    
    
    
    
    
    
    // ------------------------------------------------------------------------------ Add Item ------------------------------------------------------------------------------
    public void AddItem(Item item, int itemCount = 1)
    {
        Item = item;
        this.itemCount = itemCount;

        UnityEngine.Cursor.SetCursor(item.Cursor, new Vector2(16, 16), CursorMode.Auto);

        OnItemAdded?.Invoke(this, new EventArgs());
    }










    // ---------------------------------------------------------------------- Slot: On Mouse Begin Drag ---------------------------------------------------------------------
    private void Slot_OnMouseBeginDrag(object sender, EventArgs e)
    {
        Slot slot = (Slot)sender;

        AddItem(slot.Item);
    }










    // ----------------------------------------------------------------------- Slot: On Mouse End Drag ----------------------------------------------------------------------
    private void Slot_OnMouseEndDrag(object sender, EventArgs e)
    {
        RemoveItem();
    }










    // ----------------------------------------------------------------------------- Remove Item ----------------------------------------------------------------------------
    public void RemoveItem()
    {
        Item = null;
        currentSlot = null;
        UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}