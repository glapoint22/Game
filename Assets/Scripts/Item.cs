using UnityEngine;

[CreateAssetMenu()]
public sealed class Item : ScriptableObject
{
    [SerializeField] private string itemName;
    public string ItemName { get { return itemName; } }


    [SerializeField] private string description;
    public string Description { get { return description; } }


    [SerializeField] private Transform prefab;
    public Transform Prefab { get { return prefab; } }


    [SerializeField] private Sprite sprite;
    public Sprite Sprite { get { return sprite; } }


    [SerializeField] private int maxItemCount;
    public int MaxItemCount { get { return maxItemCount; } }


    [SerializeField] private bool isStackable;
    public bool IsStackable { get { return isStackable; } }

    [SerializeField] private Texture2D cursor;
    public Texture2D Cursor { get { return cursor;} }
}