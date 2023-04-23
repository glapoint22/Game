using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class MissionObjective
{
    // Mission Type
    [SerializeField] private MissionObjectiveType missionObjectiveType;
    public MissionObjectiveType MissionObjectiveType { get { return missionObjectiveType; } }

    // NPCs
    [SerializeField] private NpcCollection[] npcs;
    public NpcCollection[] NPCs { get { return npcs; } }

    // Collection Items
    [SerializeField] private ItemCollection[] collectionItems;

    // Delivery Items
    [SerializeField] private Item[] deliveryItems;

    // Exploration Areas
    [SerializeField] private GameObject[] explorationAreas;

    // Items to Use
    [SerializeField] private Item[] itemsToUse;

    // Items to Interact
    [SerializeField] private Item[] itemsToInteract;

    // NPCs to Escort
    [SerializeField] private NPC[] npcsToEscort;

    public void AddKill(NPC npc)
    {
        string untagged = UnityEditorInternal.InternalEditorUtility.tags[0];

        List<NpcCollection> collections = npcs
            .Where(x => x.NPC.Id == npc.Id && (x.Tag == untagged || npc.CompareTag(x.Tag)))
            .ToList();

        collections.ForEach(x => x.AddCount());
    }
}