using UnityEngine;

[CreateAssetMenu()]
public class StackableItem : Item
{
    [SerializeField] private int maxStackSize;
    public int MaxStackSize { get { return maxStackSize; } }
}