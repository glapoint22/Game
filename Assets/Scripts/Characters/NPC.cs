using System;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Id
    [SerializeField] private string id = Guid.NewGuid().ToString();
    public string Id { get { return id; } }

    // Health
    [SerializeField] private float health;

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
}