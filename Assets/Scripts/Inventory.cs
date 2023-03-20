using UnityEngine;

public sealed class Inventory : MonoBehaviour
{
    [SerializeField] private ContainerSlot[] slots;

    public static Inventory Instance;


    // -------------------------------------------------------------------------------- Awake -------------------------------------------------------------------------------
    private void Awake()
    {
        Instance = this;
    }
    
    
    
    
    
    
    
    
    
    
    // ------------------------------------------------------------------------------ Add Item ------------------------------------------------------------------------------
    public void AddItem(Item item)
    {
        ContainerSlot emptySlot = null;

        for (int i = 0; i < slots.Length; i++)
        {
            ContainerSlot slot = slots[i];

            // If the slot does not have an item
            if (!slot.Item)
            {
                // Non stackable items
                if (item is not StackableItem)
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
                if (item is StackableItem stackableItem && slot.StackSize != stackableItem.MaxStackSize && slot.Item.Equals(item))
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

        foreach (ContainerSlot slot in slots)
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
        foreach (ContainerSlot slot in slots)
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