using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class Crafting : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Transform materialsContainer;
    [SerializeField] private Transform uiBlueprintMaterialPrefab;
    [SerializeField] private Button craftButton;
    [SerializeField] private Button craftAllButton;
    [SerializeField] private TextMeshProUGUI craftAllButtonText;
    [SerializeField] private Counter counter;
    [SerializeField] private BlueprintCategory[] blueprintCategories;
    [SerializeField] private Transform blueprintCategoryPrefab;
    [SerializeField] private Transform blueprintsContainer;

    private readonly List<UIBlueprintMaterial> uiMaterials = new();
    private UIBlueprint currentSelectedUIBlueprint;
    private Blueprint currentBlueprint;
    private int maxCraftingItems;




    // -------------------------------------------------------------------------------- Start -------------------------------------------------------------------------------
    private void Start()
    {
        UIBlueprint.OnUIBlueprintSelected += OnUIBlueprintSelected;
        Inventory.Instance.OnChange += Inventory_OnChange;

        counter.IsEnabled = false;

        CreateBlueprints();
    }
    
    
    
    
    
    
    
    
    
    
    // ------------------------------------------------------------------------- Inventory: On Change -----------------------------------------------------------------------
    private void Inventory_OnChange(object sender, System.EventArgs e)
    {
        UpdateUIMaterials();

        SetMaxCraftingItems();

        SetCraftButtons();

        SetCounter(GetCounterCount());
    }
    
    
    
    
    
    
    
    
    
    
    // --------------------------------------------------------------------------- Get Counter Count ------------------------------------------------------------------------
    private int GetCounterCount()
    {
        int count;

        if (maxCraftingItems > 0 && counter.Count == 0)
        {
            count = 1;
        }
        else if (maxCraftingItems == 0)
        {
            count = 0;
        }
        else
        {
            count = counter.Count;
        }

        return count;
    }








    // --------------------------------------------------------------------------- Create Blueprints ------------------------------------------------------------------------
    private void CreateBlueprints()
    {
        for (int i = 0; i < blueprintCategories.Length; i++)
        {
            BlueprintCategory blueprintCategory = blueprintCategories[i];
            Transform blueprintCategoryTransform = Instantiate(blueprintCategoryPrefab, blueprintsContainer);
            UIBlueprintCategory uiBlueprintCategory = blueprintCategoryTransform.GetComponent<UIBlueprintCategory>();

            uiBlueprintCategory.Set(blueprintCategory);

            if (i == 0)
            {
                uiBlueprintCategory.SelectFirstBlueprint();
            }
        }
    }










    // ----------------------------------------------------------------------- On UI Blueprint Selected ---------------------------------------------------------------------
    private void OnUIBlueprintSelected(object sender, UIBlueprint.OnUIBlueprintSelectedEventArgs e)
    {
        if (e.UIBlueprint.Blueprint == currentBlueprint) return;

        currentBlueprint = e.UIBlueprint.Blueprint;

        SetCraftingItem();

        RemoveUIMaterials();

        GetUIMaterials();

        SetMaxCraftingItems();

        SetCraftButtons();

        SetSelectedBlueprint(e.UIBlueprint);

        SetCounter(maxCraftingItems > 0 ? 1 : 0);
    }










    // ------------------------------------------------------------------------ Set Selected Blueprint ----------------------------------------------------------------------
    private void SetSelectedBlueprint(UIBlueprint uiBlueprint)
    {
        if (currentSelectedUIBlueprint != null)
        {
            currentSelectedUIBlueprint.Selection.gameObject.SetActive(false);
        }

        currentSelectedUIBlueprint = uiBlueprint;
        currentSelectedUIBlueprint.Selection.gameObject.SetActive(true);
    }






    // ------------------------------------------------------------------------- Set Crafting Item --------------------------------------------------------------------------
    private void SetCraftingItem()
    {
        Item item = currentBlueprint.Item;

        itemImage.sprite = item.Sprite;
        itemNameText.text = item.Name;
    }










    // ---------------------------------------------------------------------- Set Max Crafting Items ------------------------------------------------------------------------
    private void SetMaxCraftingItems()
    {
        int[] avgCountArray = new int[uiMaterials.Count];

        for (int i = 0; i < avgCountArray.Length; i++)
        {
            UIBlueprintMaterial uiMaterial = uiMaterials[i];

            avgCountArray[i] = uiMaterial.InventoryItemCount / uiMaterial.Material.itemCount;
        }


        maxCraftingItems = Mathf.Min(avgCountArray);
    }










    // ------------------------------------------------------------------------ Remove UI Materials -------------------------------------------------------------------------
    private void RemoveUIMaterials()
    {
        foreach (Transform child in materialsContainer)
        {
            Destroy(child.gameObject);
        }

        uiMaterials.Clear();
    }










    // -------------------------------------------------------------------------- Get UI Materials --------------------------------------------------------------------------
    private void GetUIMaterials()
    {
        foreach (BlueprintMaterial material in currentBlueprint.Materials)
        {
            Transform uiBlueprintMaterialTransform = Instantiate(uiBlueprintMaterialPrefab, materialsContainer);
            UIBlueprintMaterial uiMaterial = uiBlueprintMaterialTransform.GetComponent<UIBlueprintMaterial>();

            uiMaterials.Add(uiMaterial);

            uiMaterial.SetMaterial(material);
        }
    }







    // ------------------------------------------------------------------------ On Craft Button Click -----------------------------------------------------------------------
    public void OnCraftButtonClick()
    {
        CraftItems(counter.Count);
    }










    // ---------------------------------------------------------------------- On Craft All Button Click ---------------------------------------------------------------------
    public void OnCraftAllButtonClick()
    {
        CraftItems(maxCraftingItems);
    }










    // ----------------------------------------------------------------------------- Craft Items ----------------------------------------------------------------------------
    private void CraftItems(int itemCount)
    {
        for (int i = 0; i < itemCount; i++)
        {
            Craft();
        }
    }








    // -------------------------------------------------------------------------------- Craft -------------------------------------------------------------------------------
    private void Craft()
    {
        foreach (BlueprintMaterial material in currentBlueprint.Materials)
        {
            Inventory.Instance.RemoveItem(material.item, material.itemCount);
        }

        Inventory.Instance.AddItem(currentBlueprint.Item);
    }










    // ------------------------------------------------------------------------- Update UI Materials ------------------------------------------------------------------------
    private void UpdateUIMaterials()
    {
        foreach (UIBlueprintMaterial uiMaterial in uiMaterials)
        {
            uiMaterial.UpdateItemCount();
        }
    }









    // ------------------------------------------------------------------------- Set Craft Buttons --------------------------------------------------------------------------
    private void SetCraftButtons()
    {
        counter.IsEnabled = craftAllButton.interactable = craftButton.interactable = maxCraftingItems > 0;
        craftAllButtonText.text = "Craft all (" + maxCraftingItems + ")";
    }










    // ---------------------------------------------------------------------------- Set Counter -----------------------------------------------------------------------------
    private void SetCounter(int count)
    {
        counter.MaxCount = maxCraftingItems;
        counter.Count = count;
    }
}