using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class SplitStack : MonoBehaviour
{
    [SerializeField] private Transform uiBase;
    [SerializeField] private Image image;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI stackText;
    private Slot slot;

    
    
    
    
    // -------------------------------------------------------------------------------- Start -------------------------------------------------------------------------------
    private void Start()
    {
        Slot.OnShiftClick += Slot_OnShiftClick;
    }










    // ------------------------------------------------------------------------ Slot: On Shift Click ------------------------------------------------------------------------
    private void Slot_OnShiftClick(object sender, EventArgs e)
    {
        slot = (Slot)sender;

        // If the cursor slot does not have an item and the item to split is stackable
        if (!Cursor.Instance.Item && slot.Item.IsStackable)
        {
            image.sprite = slot.Item.Sprite;

            // Set the slider
            slider.minValue = 1;
            slider.maxValue = slot.ItemCount;
            slider.value = Mathf.Ceil(slot.ItemCount / 2f);

            // Show the split stack form
            uiBase.gameObject.SetActive(true);
        }
    }
    
    
    
    
    
    
    
    
    
    
    // ----------------------------------------------------------------------- On Slider Value Change -----------------------------------------------------------------------
    public void OnSliderValueChange()
    {
        stackText.text = slider.value + "/" + slot.ItemCount;
    }
    
    
    
    
    
    
    
    
    
    
    // ----------------------------------------------------------------------- On Cancel Button Click -----------------------------------------------------------------------
    public void OnCancelButtonClick()
    {
        uiBase.gameObject.SetActive(false);
    }
    
    
    
    
    
    
    
    
    
    
    // ------------------------------------------------------------------------- On Ok Button Click -------------------------------------------------------------------------
    public void OnOkButtonClick()
    {
        Cursor.Instance.AddItem(slot.Item, (int)slider.value);

        int remainder = (int)(slot.ItemCount - slider.value);

        if (remainder == 0)
        {
            slot.RemoveItem();
        }
        else
        {
            slot.SetItemCount(remainder);
        }

        uiBase.gameObject.SetActive(false);

        GameManager.Instance.DispatchUIChangeEvent();
    }
}