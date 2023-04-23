using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class MissionsManager : MonoBehaviour
{
    [SerializeField] private Mission[] missions;
    private readonly Dictionary<string, List<string>> missionAssetLookup = new();
    private readonly Dictionary<string, Mission> missionLookup = new();

    private void Awake()
    {
        for (int i = 0; i < missions.Length; i++)
        {
            Mission mission = missions[i];

            missionLookup.Add(mission.id, mission);

            foreach (MissionObjective missionObjective in mission.MissionObjectives)
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
        }
    }



    private void SetKillObjective(string missionId, NpcCollection[] npcs)
    {
        foreach (NpcCollection npcCollection in npcs)
        {
            if (missionAssetLookup.TryGetValue(npcCollection.NPC.Id, out List<string> missionAssets))
            {
                if (!missionAssets.Any(x => x == missionId))
                {
                    missionAssets.Add(missionId);
                }
            }
            else
            {
                missionAssetLookup.Add(npcCollection.NPC.Id, new List<string>() { missionId });
            }
        }
    }



    private void Start()
    {
        NPC.OnKilled += NPC_OnKilled;
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

                if (mission.Active) mission.SetKillObjective(npc);
            }
        }
    }




    private void OnValidate()
    {
        // Create an Id for each mission
        for (int i = 0; i < missions.Length; i++)
        {
            Mission mission = missions[i];

            if (string.IsNullOrEmpty(mission.id) || (i > 0 && mission.id == missions[i - 1].id))
            {
                mission.id = Guid.NewGuid().ToString();
            }
        }
    }
}
