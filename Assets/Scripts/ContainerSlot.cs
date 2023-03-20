using TMPro;
using UnityEngine;

public class ContainerSlot : Slot
{
    [SerializeField] private TextMeshProUGUI stackSizeTextMesh;



    // ----------------------------------------------------------------------------- Remove Item ----------------------------------------------------------------------------
    public override void RemoveItem()
    {
        base.RemoveItem();

        // Set the stack size text
        stackSizeTextMesh.text = string.Empty;
    }









    // --------------------------------------------------------------------------- Set Stack Size ---------------------------------------------------------------------------
    public override void SetStackSize(int stackSize)
    {
        base.SetStackSize(stackSize);

        if (Item is StackableItem stackableItem) stackSizeTextMesh.text = StackSize + "/" + stackableItem.MaxStackSize;

    }
    
    
    
    
    
    
    
    
    
    
    // ------------------------------------------------------------------------------ Disable -------------------------------------------------------------------------------
    public override void Disable()
    {
        base.Disable();

        // Fade the alpha on the text
        stackSizeTextMesh.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
    }
    
    
    
    
    
    
    
    
    
    
    // -------------------------------------------------------------------------------- Enable ------------------------------------------------------------------------------
    public override void Enable()
    {
        base.Enable();

        stackSizeTextMesh.color = new Color(image.color.r, image.color.g, image.color.b, 1);
    }
}