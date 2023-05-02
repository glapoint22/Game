using System;
using TMPro;
using UnityEngine;

public class ListItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTextMesh;
    [SerializeField] private GameObject selectionGameObject;

    public event EventHandler OnSelected;

    public void Set(string title)
    {
        titleTextMesh.text = title;
    }


    public void OnClick()
    {
        if (selectionGameObject.activeSelf) return;

        SetSelection(true);
        OnSelected?.Invoke(this, new EventArgs());
    }

    public void SetSelection(bool selected)
    {
        selectionGameObject.SetActive(selected);
    }
}