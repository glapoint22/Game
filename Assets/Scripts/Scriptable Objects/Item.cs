using UnityEngine;

[CreateAssetMenu()]
public class Item : ScriptableObject
{
    [SerializeField] private new string name;
    public string Name { get { return name; } }


    [SerializeField] private string description;
    public string Description { get { return description; } }


    [SerializeField] private Transform prefab;
    public Transform Prefab { get { return prefab; } }


    [SerializeField] private Sprite sprite;
    public Sprite Sprite { get { return sprite; } }
    

    [SerializeField] private Texture2D cursor;
    public Texture2D Cursor { get { return cursor;} }
}