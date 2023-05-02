using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ListGroup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTextMesh;
    [SerializeField] private Transform itemsContainer;
    [SerializeField] private Transform listItemPrefab;

    public event EventHandler OnListItemSelected;

    public void Set(string title, List<string> items)
    {
        titleTextMesh.text = title;

        foreach (var item in items)
        {
            Transform listItemTransform = Instantiate(listItemPrefab, itemsContainer);
            ListItem listItem = listItemTransform.GetComponent<ListItem>();
            listItem.Set(item);

            listItem.OnSelected += ListItem_OnSelected;
        }
    }

    private void ListItem_OnSelected(object sender, EventArgs e)
    {
        OnListItemSelected?.Invoke(sender, e);
    }
}