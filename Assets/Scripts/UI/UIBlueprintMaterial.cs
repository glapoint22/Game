using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBlueprintMaterial : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemCountText;
    [SerializeField] private TextMeshProUGUI itemNameText;

    public BlueprintMaterial Material { get; private set; }
    public int InventoryItemCount { get; private set; }



    // ---------------------------------------------------------------------------- Set Material ----------------------------------------------------------------------------
    public void SetMaterial(BlueprintMaterial material)
    {
        Material = material;

        itemImage.sprite = material.item.Sprite;
        itemNameText.text = material.item.name;

        UpdateItemCount();
    }










    // ------------------------------------------------------------------------- Update Item Count --------------------------------------------------------------------------
    public void UpdateItemCount()
    {
        InventoryItemCount = Inventory.Instance.GetItemCount(Material.item);
        itemCountText.text = InventoryItemCount + "/" + Material.itemCount;
    }
}