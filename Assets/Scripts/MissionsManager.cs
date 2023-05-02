using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionsManager : MonoBehaviour
{
    [SerializeField] private Mission[] missions;
    public Mission[] Missions { get { return missions; } }

    private readonly Dictionary<string, List<string>> missionAssetLookup = new();
    private readonly Dictionary<string, Mission> missionLookup = new();

    public static MissionsManager Instance { get; private set; }


    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < missions.Length; i++)
        {
            Mission mission = missions[i];

            missionLookup.Add(mission.id, mission);



            foreach (MissionObjective missionObjective in mission.Objectives)
            {
                switch (missionObjective.MissionObjectiveType)
                {
                    case MissionObjectiveType.Kill:
                        SetKillObjective(mission.id, missionObjective.NPCs);
                        break;

                    case MissionObjectiveType.Collect:
                        break;
                    case MissionObjectiveType.Deliver:
                        break;
                    case MissionObjectiveType.Explore:
                        break;
                    case MissionObjectiveType.TalkTo:
                        break;
                    case MissionObjectiveType.UseItem:
                        break;
                    case MissionObjectiveType.InteractWithItem:
                        break;
                    case MissionObjectiveType.Escort:
                        break;
                }
            }

            SetMissionAssetLookup(mission.MissionGiver.Id, mission.id);
        }
    }



    private void SetKillObjective(string missionId, NpcCollection[] npcs)
    {
        foreach (NpcCollection npcCollection in npcs)
        {
            SetMissionAssetLookup(npcCollection.NPC.Id, missionId);
        }
    }



    private void SetMissionAssetLookup(string assetId, string missionId)
    {
        if (missionAssetLookup.TryGetValue(assetId, out List<string> missionAssets))
        {
            if (!missionAssets.Any(x => x == missionId))
            {
                missionAssets.Add(missionId);
            }
        }
        else
        {
            missionAssetLookup.Add(assetId, new List<string>() { missionId });
        }
    }


    private void Start()
    {
        NPC.OnKilled += NPC_OnKilled;
    }

    public List<Mission> GetMissions(NPC npc)
    {
        List<Mission> missions = new();

        // Get all the mission ids associated with this npc
        missionAssetLookup.TryGetValue(npc.Id, out List<string> missionIds);

        if (missionIds != null)
        {
            foreach (string missionId in missionIds)
            {
                Mission mission = missionLookup[missionId];

                // This if statement checks if the retrieved mission satisfies a set of conditions before adding it to a List of missions.

                // The mission's status must not be Complete.
                // The mission's status must be Available and the mission giver's Id must match the npc's Id, OR
                // The mission's status must be Active or ReadyForTurnIn and the mission receiver's Id must match the npc's Id.
                // The mission must not have a prerequisite OR the prerequisite mission must be Complete.
                if (mission.Status != MissionStatus.Complete &&
                    (mission.Status == MissionStatus.Available && mission.MissionGiver.Id == npc.Id ||
                    (mission.Status == MissionStatus.Active || mission.Status == MissionStatus.ReadyForTurnIn) && mission.MissionReceiver.Id == npc.Id) &&
                    (!mission.Prerequisite || mission.Prerequisite && missionLookup[mission.PrerequisiteMissionId].Status == MissionStatus.Complete))
                {
                    missions.Add(mission);
                }
            }
        }

        return missions;
    }

    private void NPC_OnKilled(object sender, EventArgs e)
    {
        NPC npc = (NPC)sender;

        missionAssetLookup.TryGetValue(npc.Id, out List<string> missionIds);

        if (missionIds != null)
        {
            foreach (string missionId in missionIds)
            {
                Mission mission = missionLookup[missionId];

                if (mission.Status == MissionStatus.Active)
                {
                    // Get all the kill objectives in this mission related to this NPC
                    List<MissionObjective> killObjectives = mission.GetKillObjectives(npc);

                    killObjectives.ForEach(killObjective => killObjective.UpdateKillObjective(npc));
                }
            }
        }
    }




    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            // Create an Id for each mission
            for (int i = 0; i < missions.Length; i++)
            {
                Mission mission = missions[i];

                if (string.IsNullOrEmpty(mission.id) || missions.Any(x => x != mission && x.id == mission.id))
                {
                    mission.id = Guid.NewGuid().ToString();
                }
            }
        }
    }
}
