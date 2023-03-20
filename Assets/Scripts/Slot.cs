using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    [SerializeField] protected Image image;
    public Item Item { get; protected set; }

    public int StackSize { get; protected set; }


    // On Mouse Begin Drag Event
    public static event EventHandler OnMouseBeginDrag;



    // On Mouse End Drag Event
    public static event EventHandler OnMouseEndDrag;




    // On Shift Click Event
    public static event EventHandler OnShiftClick;




    // On Mouse Click Event
    public static event EventHandler OnMouseClick;





    // ------------------------------------------------------------------------------ Add Item ------------------------------------------------------------------------------
    public int AddItem(Item item, int stackSize = 1)
    {
        int remainder = 0;
        int count = StackSize + stackSize;


        if (Item == null)
        {
            // Set the item
            Item = item;

            // Set the image
            image.gameObject.SetActive(true);
            image.sprite = Item.Sprite;
        }


        if (item is StackableItem stackableItem && count > stackableItem.MaxStackSize)
        {
            remainder = count - stackableItem.MaxStackSize;
            count = stackableItem.MaxStackSize;
        }


        // Set the item count
        SetStackSize(count);

        return remainder;
    }










    // ----------------------------------------------------------------------------- Remove Item ----------------------------------------------------------------------------
    public virtual void RemoveItem()
    {
        // Set the item
        Item = null;

        // Set the image
        image.gameObject.SetActive(false);
        image.sprite = null;

        StackSize = 0;
    }










    // --------------------------------------------------------------------------- On Begin Drag ----------------------------------------------------------------------------
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!Item)
        {
            eventData.pointerDrag = null;
            return;
        }

        OnMouseBeginDrag?.Invoke(this, new EventArgs());

        Disable();
    }










    // ------------------------------------------------------------------------------ On Drag -------------------------------------------------------------------------------
    public void OnDrag(PointerEventData eventData) { }










    // ----------------------------------------------------------------------------- On End Drag ----------------------------------------------------------------------------
    public void OnEndDrag(PointerEventData eventData)
    {
        // Restore the alpha on the image and the text
        Enable();

        OnMouseEndDrag?.Invoke(this, EventArgs.Empty);
    }










    // ------------------------------------------------------------------------------- On Drop ------------------------------------------------------------------------------
    public void OnDrop(PointerEventData eventData)
    {
        // Get the other slot from where the item was dragged from
        Slot otherSlot = eventData.pointerDrag.GetComponent<Slot>();

        DropItem(otherSlot);
    }










    // ------------------------------------------------------------------------------ Drop Item -----------------------------------------------------------------------------
    public virtual void DropItem(Slot otherSlot)
    {
        // If the other slot is not this one
        if (otherSlot != this)
        {
            // If this slot's item is different from the other slot's item
            // Or the item is not stackable
            // Or the sum of two stacks is greater than the max stack size
            if (Item && (!Item.Equals(otherSlot.Item) ||
                Item is not StackableItem ||
                Item is StackableItem stackableItem && StackSize + otherSlot.StackSize > stackableItem.MaxStackSize))
            {
                SwapItems(otherSlot);
                GameManager.Instance.DispatchUIChangeEvent();
                return;
            }

            // Add the item(s) to this slot
            int remainder = AddItem(otherSlot.Item, otherSlot.StackSize);

            if (remainder == 0)
            {
                // Remove the item from the other slot
                otherSlot.RemoveItem();
            }
            else
            {
                otherSlot.SetStackSize(remainder);
            }

            GameManager.Instance.DispatchUIChangeEvent();
        }
    }










    // ------------------------------------------------------------------------------ Swap Items ----------------------------------------------------------------------------
    public void SwapItems(Slot otherSlot)
    {
        // This slot's item
        Item item = Item;
        int itemCount = StackSize;

        // Other slot's item
        int otherSlotItemCount = otherSlot.StackSize;
        Item otherSlotItem = otherSlot.Item;


        // Remove the items from both slots
        RemoveItem();
        otherSlot.RemoveItem();


        // Add the other slot's item to this slot
        AddItem(otherSlotItem, otherSlotItemCount);

        // Add the item that used to be in this slot to the other slot
        otherSlot.AddItem(item, itemCount);
    }










    // --------------------------------------------------------------------------- On Pointer Click -------------------------------------------------------------------------
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (!Item && !Cursor.Instance.Item) return;

        if (Item && Item is StackableItem && StackSize > 1)
        {
            // Shift Click
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                OnShiftClick?.Invoke(this, new EventArgs());
                return;
            }
        }


        if (!Input.anyKey)
        {
            OnMouseClick?.Invoke(this, new EventArgs());
        }
    }










    // ------------------------------------------------------------------------------ Disable -------------------------------------------------------------------------------
    public virtual void Disable()
    {
        // Fade the alpha on the image
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
    }










    // -------------------------------------------------------------------------------- Enable ------------------------------------------------------------------------------
    public virtual void Enable()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
    }






    // --------------------------------------------------------------------------- Set Stack Size ---------------------------------------------------------------------------
    public virtual void SetStackSize(int stackSize)
    {
        StackSize = stackSize;
    }
}