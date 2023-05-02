using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Id
    [SerializeField] private string id;
    public string Id { get { return id; } }

    // Health
    [SerializeField] private float health;

    private List<Mission> missions;

    // On Killed Event
    public static event EventHandler OnKilled;




    // -------------------------------------------------------------------------------- Hit ---------------------------------------------------------------------------------
    public void Hit(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            OnKilled?.Invoke(this, new EventArgs());
        }
    }



    public void Interact()
    {
        missions = MissionsManager.Instance.GetMissions(this);

        if (missions.Count == 0)
        {
            // Show npc dialogue
        }
        else if (missions.Count == 1)
        {
            Mission mission = missions[0];

            switch (mission.Status)
            {
                case MissionStatus.Available:
                    //Debug.Log("Kill 8 Marauders and 6 bears.");
                    Dialogue dialogue = new(mission.Description);

                    DialogueBox.Instance.Open();

                    break;

                case MissionStatus.Active:
                    Debug.Log("How is the mission coming along?");
                    break;

                case MissionStatus.ReadyForTurnIn:
                    Debug.Log("Looks like you completed the mission. Good job!");
                    break;
            }
        }
        else
        {
            List<IGrouping<MissionStatus, Mission>> missionsGroupedByStatus = missions.GroupBy(x => x.Status).ToList();
            Debug.Log(missionsGroupedByStatus);
        }
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = Guid.NewGuid().ToString();
            }
            else
            {
                NPC[] npcs = FindObjectsByType<NPC>(FindObjectsSortMode.None);

                if (npcs.Any(x => x != this && x.Id == id))
                {
                    id = Guid.NewGuid().ToString();
                }
            }
        }
    }
}