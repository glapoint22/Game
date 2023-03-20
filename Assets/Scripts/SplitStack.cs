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
    private ContainerSlot slot;





    // -------------------------------------------------------------------------------- Start -------------------------------------------------------------------------------
    private void Start()
    {
        ContainerSlot.OnShiftClick += Slot_OnShiftClick;
    }










    // ------------------------------------------------------------------------ Slot: On Shift Click ------------------------------------------------------------------------
    private void Slot_OnShiftClick(object sender, EventArgs e)
    {
        slot = (ContainerSlot)sender;

        // If the cursor slot does not have an item and the item to split is stackable
        if (!Cursor.Instance.Item && slot.Item is StackableItem)
        {
            image.sprite = slot.Item.Sprite;

            // Set the slider
            slider.minValue = 1;
            slider.maxValue = slot.StackSize;
            slider.value = Mathf.Ceil(slot.StackSize / 2f);

            // Show the split stack form
            uiBase.gameObject.SetActive(true);
        }
    }










    // ----------------------------------------------------------------------- On Slider Value Change -----------------------------------------------------------------------
    public void OnSliderValueChange()
    {
        stackText.text = slider.value + "/" + slot.StackSize;
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

        int remainder = (int)(slot.StackSize - slider.value);

        if (remainder == 0)
        {
            slot.RemoveItem();
        }
        else
        {
            slot.SetStackSize(remainder);
        }

        uiBase.gameObject.SetActive(false);

        GameManager.Instance.DispatchUIChangeEvent();
    }
}