using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBlueprintCategory : MonoBehaviour
{
    [SerializeField] private Transform blueprintPrefab;
    [SerializeField] private TextMeshProUGUI categoryName;
    [SerializeField] private TextMeshProUGUI expandText;

    private readonly List<UIBlueprint> uiBlueprints = new();


    // -------------------------------------------------------------------------------- Set ---------------------------------------------------------------------------------
    public void Set(BlueprintCategory blueprintCategory)
    {
        categoryName.text = blueprintCategory.Name;

        foreach (Blueprint blueprint in blueprintCategory.Blueprints)
        {
            Transform blueprintTransform = Instantiate(blueprintPrefab, transform);
            UIBlueprint uiBlueprint = blueprintTransform.GetComponent<UIBlueprint>();

            uiBlueprints.Add(uiBlueprint);

            uiBlueprint.Set(blueprint);
        }
    }
    
    
    
    
    
    
    
    
    
    
    // ----------------------------------------------------------------------------- On Click -------------------------------------------------------------------------------
    public void OnClick()
    {
        foreach (UIBlueprint uiBlueprint in uiBlueprints)
        {
            uiBlueprint.gameObject.SetActive(!uiBlueprint.isActiveAndEnabled);
            expandText.text = uiBlueprint.isActiveAndEnabled ? "-" : "+";
        }
    }
    
    
    
    
    
    
    
    
    
    
    // ---------------------------------------------------------------------- Select First Blueprint ------------------------------------------------------------------------
    public void SelectFirstBlueprint()
    {
        uiBlueprints[0].Select();
    }
}