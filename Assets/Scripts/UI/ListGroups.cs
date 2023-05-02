using System;
using System.Collections.Generic;
using UnityEngine;

public class ListGroups : MonoBehaviour
{
    [SerializeField] private Transform listGroupPrefab;
    [SerializeField] private Transform listItemPrefab;

    private ListItem selectedListItem;

    public event EventHandler OnListItemSelected;

    public void Add(string title, List<string> items)
    {
        Transform listGroupTransform = Instantiate(listGroupPrefab, transform);
        ListGroup listGroup = listGroupTransform.GetComponent<ListGroup>();

        listGroup.Set(title, items);

        listGroup.OnListItemSelected += ListGroup_OnListItemSelected;
    }

    private void ListGroup_OnListItemSelected(object sender, EventArgs e)
    {
        if (selectedListItem != null)
        {
            selectedListItem.SetSelection(false);
        }

        selectedListItem = (ListItem)sender;

        OnListItemSelected?.Invoke(selectedListItem, e);
    }
}