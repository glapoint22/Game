using System;
using TMPro;
using UnityEngine;

public class UIBlueprint : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI blueprintText;
    [SerializeField] private Transform selection;
    public Transform Selection { get { return selection; } }


    public Blueprint Blueprint { get; private set; }


    // On UI Blueprint Selected Event
    public static event EventHandler<OnUIBlueprintSelectedEventArgs> OnUIBlueprintSelected;
    public sealed class OnUIBlueprintSelectedEventArgs : EventArgs
    {
        public UIBlueprint UIBlueprint;
    }



    // --------------------------------------------------------------------------- Set Blueprint ----------------------------------------------------------------------------
    public void Set(Blueprint blueprint)
    {
        Blueprint = blueprint;

        Item item = blueprint.Item;
        blueprintText.text = item.name;
    }










    // -------------------------------------------------------------------------- Select Blueprint --------------------------------------------------------------------------
    public void Select()
    {
        OnUIBlueprintSelected?.Invoke(this, new OnUIBlueprintSelectedEventArgs
        {
            UIBlueprint = this
        });
    }
}