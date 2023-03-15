using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI itemCountText;
    [SerializeField] private Image image;
    public Item Item { get; private set; }

    public int ItemCount { get; private set; }


    // On Mouse Begin Drag Event
    public static event EventHandler OnMouseBeginDrag;



    // On Mouse End Drag Event
    public static event EventHandler OnMouseEndDrag;




    // On Shift Click Event
    public static event EventHandler OnShiftClick;




    // On Mouse Click Event
    public static event EventHandler OnMouseClick;





    // ------------------------------------------------------------------------------ Add Item ------------------------------------------------------------------------------
    public int AddItem(Item item, int itemCount = 1)
    {
        int remainder = 0;
        int count = ItemCount + itemCount;


        if (Item == null)
        {
            // Set the item
            Item = item;

            // Set the image
            image.gameObject.SetActive(true);
            image.sprite = Item.Sprite;
        }


        if (count > item.MaxItemCount)
        {
            remainder = count - item.MaxItemCount;
            count = item.MaxItemCount;
        }


        // Set the item count
        SetItemCount(count);

        return remainder;
    }
    
    
    
    
    
    
    
    
    
    
    // ----------------------------------------------------------------------------- Remove Item ----------------------------------------------------------------------------
    public void RemoveItem()
    {
        // Set the item
        Item = null;

        // Set the image
        image.gameObject.SetActive(false);
        image.sprite = null;

        // Set the item count
        ItemCount = 0;
        itemCountText.text = string.Empty;
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
    public void DropItem(Slot otherSlot)
    {
        // If the other slot is not this one
        if (otherSlot != this)
        {
            // If this slot's item is different from the other slot's item or the sum of two stacks is greater than the max item count
            if (Item && (!Item.Equals(otherSlot.Item) || ItemCount + otherSlot.ItemCount > Item.MaxItemCount))
            {
                SwapItems(otherSlot);
                GameManager.Instance.DispatchUIChangeEvent();
                return;
            }

            // Add the item(s) to this slot
            int remainder = AddItem(otherSlot.Item, otherSlot.ItemCount);

            if (remainder == 0)
            {
                // Remove the item from the other slot
                otherSlot.RemoveItem();
            }
            else
            {
                otherSlot.SetItemCount(remainder);
            }

            GameManager.Instance.DispatchUIChangeEvent();
        }
    }
    
    
    
    
    
    
    
    
    
    
    // ------------------------------------------------------------------------------ Swap Items ----------------------------------------------------------------------------
    private void SwapItems(Slot otherSlot)
    {
        // This slot's item
        Item item = Item;
        int itemCount = ItemCount;

        // Other slot's item
        int otherSlotItemCount = otherSlot.ItemCount;
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
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Item && ItemCount > 1)
        {
            // Shift Click
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                OnShiftClick?.Invoke(this, new EventArgs());
                return;
            }
        }


        if (!eventData.dragging && !Input.anyKey)
        {
            OnMouseClick?.Invoke(this, new EventArgs());
        }
    }










    // ------------------------------------------------------------------------------ Disable -------------------------------------------------------------------------------
    public void Disable()
    {
        // Fade the alpha on the image and the text
        itemCountText.color = image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
    }










    // -------------------------------------------------------------------------------- Enable ------------------------------------------------------------------------------
    public void Enable()
    {
        itemCountText.color = image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
    }
    
    
    
    
    
    
    
    
    
    
    // --------------------------------------------------------------------------- Set Item Count ---------------------------------------------------------------------------
    public void SetItemCount(int itemCount)
    {
        ItemCount = itemCount;

        if (Item.IsStackable)
        {
            itemCountText.text = ItemCount + "/" + Item.MaxItemCount;
        }
        else
        {
            itemCountText.text = string.Empty;
        }
    }
}