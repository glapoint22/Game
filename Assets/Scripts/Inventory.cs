using UnityEngine;

public sealed class Inventory : MonoBehaviour
{
    [SerializeField] private Slot[] slots;

    public static Inventory Instance;


    // -------------------------------------------------------------------------------- Awake -------------------------------------------------------------------------------
    private void Awake()
    {
        Instance = this;
    }
    
    
    
    
    
    
    
    
    
    
    // ------------------------------------------------------------------------------ Add Item ------------------------------------------------------------------------------
    public void AddItem(Item item)
    {
        Slot emptySlot = null;

        for (int i = 0; i < slots.Length; i++)
        {
            Slot slot = slots[i];

            // If the slot does not have an item
            if (!slot.Item)
            {
                // Non stackable items
                if (item.IsStackable == false)
                {
                    // Add item to slot
                    slot.AddItem(item);

                    GameManager.Instance.DispatchUIChangeEvent();

                    return;
                }

                if (emptySlot == null) emptySlot = slot;
            }

            // Slot does have an item
            else
            {
                // Stackable items
                if (item.IsStackable && slot.ItemCount != item.MaxItemCount && slot.Item.Equals(item))
                {
                    slot.AddItem(item);
                    GameManager.Instance.DispatchUIChangeEvent();
                    return;
                }
            }
        }

        if (emptySlot != null)
        {
            emptySlot.AddItem(item);
            GameManager.Instance.DispatchUIChangeEvent();
        }
        else
        {
            // Every slot is occupied
            Debug.Log("Container Full");
        }
    }










    // --------------------------------------------------------------------------- Get Item Count ---------------------------------------------------------------------------
    public int GetItemCount(Item item)
    {
        int count = 0;

        foreach (Slot slot in slots)
        {
            if (slot.Item && slot.Item.Equals(item))
            {
                count += slot.ItemCount;
            }
        }

        return count;
    }










    // ---------------------------------------------------------------------------- Remove Item -----------------------------------------------------------------------------
    public void RemoveItem(Item item, int itemCount)
    {
        foreach (Slot slot in slots)
        {
            if (slot.Item && slot.Item.Equals(item))
            {
                int slotItemCount = slot.ItemCount - itemCount;
                itemCount -= slot.ItemCount;

                if (slotItemCount <= 0)
                {
                    slot.RemoveItem();
                }
                else
                {
                    slot.SetItemCount(slotItemCount);
                }

                if (itemCount <= 0) return;
            }
        }
    }
}