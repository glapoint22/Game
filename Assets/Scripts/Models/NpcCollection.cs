using System;
using UnityEngine;

[Serializable]
public class NpcCollection
{
    // NPC Instances
    [SerializeField] private NPC npc;
    public NPC NPC { get { return npc; } }

    // Count
    [SerializeField] private int count;
    public int Count { get { return count; } }

    // Tag
    [SerializeField] private string tag;
    public string Tag { get { return tag; } }

    private int currentCount;
    public int CurrentCount { get { return currentCount; } }

    public void AddCount()
    {
        currentCount++;
        Debug.Log(currentCount);
    }
}