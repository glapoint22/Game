using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBackdrop : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    private Image image;
    private Slot currentSlot;

    // -------------------------------------------------------------------------------- Awake -------------------------------------------------------------------------------
    private void Awake()
    {
        image = GetComponent<Image>();
    }










    // -------------------------------------------------------------------------------- Start -------------------------------------------------------------------------------
    private void Start()
    {
        Slot.OnMouseEndDrag += Slot_OnMouseEndDrag;
        Slot.OnMouseClick += Slot_OnMouseClick;
        Cursor.Instance.OnItemAdded += Cursor_OnItemAdded;
    }
    
    
    
    
    
    
    
    
    
    
    // ------------------------------------------------------------------------ Cursor: On Item Added -----------------------------------------------------------------------
    private void Cursor_OnItemAdded(object sender, System.EventArgs e)
    {
        image.enabled = true;
    }
    
    
    
    
    
    
    
    
    
    
    // ----------------------------------------------------------------------- Slot: On Mouse End Drag ----------------------------------------------------------------------
    private void Slot_OnMouseEndDrag(object sender, System.EventArgs e)
    {
        image.enabled = false;
    }










    // ------------------------------------------------------------------------------- On Drop ------------------------------------------------------------------------------
    public void OnDrop(PointerEventData eventData)
    {
        Slot slot = eventData.pointerDrag.GetComponent<Slot>();
        slot.RemoveItem();

        GameManager.Instance.DispatchUIChangeEvent();
    }










    // ------------------------------------------------------------------------ Slot: On Mouse Click ------------------------------------------------------------------------
    private void Slot_OnMouseClick(object sender, System.EventArgs e)
    {
        Slot slot = (Slot)sender;

        if (slot.Item != null && !Cursor.Instance.Item)
        {
            image.enabled = true;
            currentSlot = slot;
        }
        else if (Cursor.Instance.Item)
        {
            image.enabled = false;
        }
    }










    // -------------------------------------------------------------------------- On Pointer Click --------------------------------------------------------------------------
    public void OnPointerClick(PointerEventData eventData)
    {
        image.enabled = false;
        Cursor.Instance.RemoveItem();

        if (currentSlot != null)
        {
            currentSlot.Enable();
            currentSlot.RemoveItem();
            currentSlot = null;
        }

        GameManager.Instance.DispatchUIChangeEvent();
    }
}