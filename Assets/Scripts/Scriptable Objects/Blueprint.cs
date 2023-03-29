using UnityEngine;

[CreateAssetMenu()]
public sealed class Blueprint : ScriptableObject
{
    [SerializeField] private Item item;
    public Item Item { get { return item; } }


    [SerializeField] private BlueprintMaterial[] materials;
    public BlueprintMaterial[] Materials { get { return materials; } }
}