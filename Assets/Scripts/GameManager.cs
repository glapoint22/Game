using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ReputationSetting[] reputationSettings;
    //[SerializeField] private MissionInspector[] missions;
    //private readonly Dictionary<string, List<string>> missionAssetLookup = new();

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        //for (int i = 0; i < missions.Length; i++)
        //{
        //    MissionInspector mission = missions[i];

        //    foreach (NpcGroup npcGroup in mission.NPCGroups)
        //    {
        //        if (npcGroup.NPC == null) continue;
        //        if (missionAssetLookup.TryGetValue(npcGroup.NPC.Id, out List<string> missionAssets))
        //        {
        //            missionAssets.Add(mission.id);
        //        }
        //        else
        //        {
        //            missionAssetLookup.Add(npcGroup.NPC.Id, new List<string>() { mission.id });
        //        }
        //    }
        //}
    }



    //private void OnValidate()
    //{
    //    // Create an Id for each mission
    //    for (int i = 0; i < missions.Length; i++)
    //    {
    //        MissionInspector mission = missions[i];

    //        if (string.IsNullOrEmpty(mission.id) || (i > 0 && mission.id == missions[i - 1].id))
    //        {
    //            mission.id = Guid.NewGuid().ToString();
    //        }
    //    }
    //}



    //private void Start()
    //{
    //    NPC.OnKilled += NPC_OnKilled;
    //}

    //private void NPC_OnKilled(object sender, EventArgs e)
    //{
    //    NPC npc = (NPC)sender;

    //    List<string> missions = missionAssetLookup[npc.Id];
    //}

    public int GetMaxReputationPoints(ReputationLevel level)
    {
        int points = 0;

        foreach (ReputationSetting reputationSetting in reputationSettings)
        {
            if (reputationSetting.Level == level)
            {
                points = reputationSetting.Points;
                break;
            }
        }

        return points;
    }



    public ReputationLevel GetNextReputationLevel(ReputationLevel currentLevel)
    {
        ReputationSetting reputation = reputationSettings
            .SkipWhile(x => x.Level != currentLevel)
            .Skip(1)
            .FirstOrDefault();

        if (reputation == null) return ReputationLevel.None;
        return reputation.Level;
    }



    public ReputationLevel GetPreviousReputationLevel(ReputationLevel currentLevel)
    {
        ReputationSetting reputation = reputationSettings
            .Reverse()
            .SkipWhile(x => x.Level != currentLevel)
            .Skip(1)
            .FirstOrDefault();

        if (reputation == null) return ReputationLevel.None;
        return reputation.Level;
    }



    public Color GetReputationColor(ReputationLevel currentLevel)
    {
        return reputationSettings.Single(x => x.Level == currentLevel).Color;
    }




}