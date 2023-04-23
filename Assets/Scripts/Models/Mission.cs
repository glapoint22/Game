using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Mission
{
    [SerializeField] private string missionName;
    public string MissionName { get { return missionName; } }

    public string id;

    [SerializeField] private MissionObjective[] missionObjectives;
    public MissionObjective[] MissionObjectives { get { return missionObjectives; } }

    // Chain
    [SerializeField] private bool chain;
    [SerializeField] private int missionIndex;

    // Timed
    [SerializeField] private bool timed;
    [SerializeField] private int timeInMinutes;

    // Short Description
    [SerializeField][TextArea(5, 5)] private string shortDescription;

    // Long Description
    [SerializeField][TextArea(20, 20)] private string longDescription;

    // Mission Completed Text
    [SerializeField][TextArea(5, 5)] private string missionCompletedText;

    // Mission Giver
    [SerializeField] private NPC missionGiver;

    // Mission Receiver
    [SerializeField] private NPC missionReceiver;

    // Rewards
    [SerializeField] private Item[] rewards;

    // Reputations
    [SerializeField] private Reputation[] reputations;

    // Active
    private bool active = true;
    public bool Active { get { return active; } }


    public void SetKillObjective(NPC killedNPC)
    {
        string untagged = UnityEditorInternal.InternalEditorUtility.tags[0];

        // This line of code is used to get a list of mission objectives based on the following criteria:
        // 1. The mission objective type must be "Kill"
        // 2. The objective must have at least one NPC with the same Id as the NPC that has been killed,
        // AND either the NPC set in the objective must be tagged as "Untagged"
        // OR
        // the objective NPC must have the same tag as the NPC that has been killed.
        List<MissionObjective> killObjectives = missionObjectives
            .Where(missionObjective => missionObjective.MissionObjectiveType == MissionObjectiveType.Kill &&
            (missionObjective.NPCs.Any(npcCollection => npcCollection.Tag == untagged && npcCollection.NPC.Id == killedNPC.Id) ||
            missionObjective.NPCs
                .Where(npcCollection => npcCollection.NPC.Id == killedNPC.Id)
                .Select(npcCollection => npcCollection.Tag)
                .ToList()
                .Contains(killedNPC.tag)))
            .ToList();

        // Mark this NPC as kililed for each objective it was a part of
        killObjectives.ForEach(killObjective => killObjective.AddKill(killedNPC));
    }
}