using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBackdrop : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    private Image image;
    private Slot currentSlot;

    public static UIBackdrop Instance { get; private set; }

    // -------------------------------------------------------------------------------- Awake -------------------------------------------------------------------------------
    private void Awake()
    {
        image = GetComponent<Image>();
        Instance = this;
    }










    // -------------------------------------------------------------------------------- Start -------------------------------------------------------------------------------
    private void Start()
    {
        Slot.OnMouseEndDrag += Slot_OnMouseEndDrag;
        Slot.OnMouseClick += Slot_OnMouseClick;
    }










    // ------------------------------------------------------------------------------- Enable -------------------------------------------------------------------------------
    public void Enable()
    {
        image.enabled = true;
    }










    // ------------------------------------------------------------------------------ Disable -------------------------------------------------------------------------------
    private void Disable()
    {
        image.enabled = false;
    }










    // ----------------------------------------------------------------------- Slot: On Mouse End Drag ----------------------------------------------------------------------
    private void Slot_OnMouseEndDrag(object sender, System.EventArgs e)
    {
        Disable();
    }










    // ------------------------------------------------------------------------------- On Drop ------------------------------------------------------------------------------
    public void OnDrop(PointerEventData eventData)
    {
        Slot slot = eventData.pointerDrag.GetComponent<Slot>();
        slot.RemoveItem();
    }










    // ------------------------------------------------------------------------ Slot: On Mouse Click ------------------------------------------------------------------------
    private void Slot_OnMouseClick(object sender, System.EventArgs e)
    {
        Slot slot = (Slot)sender;

        if (slot.Item != null && !UICursor.Instance.Item)
        {
            Enable();
            currentSlot = slot;
        }
        else if (UICursor.Instance.Item)
        {
            Disable();
        }
    }










    // -------------------------------------------------------------------------- On Pointer Click --------------------------------------------------------------------------
    public void OnPointerClick(PointerEventData eventData)
    {
        Disable();
        UICursor.Instance.RemoveItem();

        if (currentSlot != null)
        {
            currentSlot.Enable();
            currentSlot.RemoveItem();
            currentSlot = null;
        }
    }
}