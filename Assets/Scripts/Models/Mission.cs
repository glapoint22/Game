using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Mission
{
    [SerializeField] private string title;
    public string Title { get { return title; } }

    public string id;

    [SerializeField] private MissionObjective[] objectives;
    public MissionObjective[] Objectives { get { return objectives; } }

    // Prerequisite
    [SerializeField] private bool prerequisite;
    public bool Prerequisite { get { return prerequisite; } }

    // Prerequisite Mission Id
    [SerializeField] private string prerequisiteMissionId;
    public string PrerequisiteMissionId { get { return prerequisiteMissionId; } }

    // Timed
    [SerializeField] private bool timed;
    [SerializeField] private int timeInMinutes;

    // Short Description
    [SerializeField][TextArea(5, 5)] private string objectivesText;
    public string ObjectivesText { get { return objectivesText; } }

    // Long Description
    [SerializeField][TextArea(20, 20)] private string description;
    public string Description { get { return description; } }

    // Mission Completed Text
    [SerializeField][TextArea(5, 5)] private string missionCompletedText;

    // Mission Giver
    [SerializeField] private NPC missionGiver;
    public NPC MissionGiver { get { return missionGiver; } }

    // Mission Receiver
    [SerializeField] private NPC missionReceiver;
    public NPC MissionReceiver { get { return missionReceiver; } }

    // Rewards
    [SerializeField] private Item[] rewards;

    // Reputations
    [SerializeField] private Reputation[] reputations;


    public MissionStatus status;
    public MissionStatus Status { get { return status; } }


    public List<MissionObjective> GetKillObjectives(NPC killedNPC)
    {
        return objectives
           .Where(missionObjective => missionObjective.MissionObjectiveType == MissionObjectiveType.Kill && missionObjective.NPCs
               .Any(z => z.NPC.Id == killedNPC.Id))
           .ToList();
    }
}