using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerAttribute[] playerAttributes;

    // Health
    private readonly Health health = new();

    // Base Damage
    private readonly BaseDamage baseDamage = new();

    // Critical Strike Chance
    private readonly CriticalStrikeChance criticalStrikeChance = new();

    // Damage Reduction
    private readonly DamageReduction damageReduction = new();

    // Health Regeneration Rate
    private readonly HealthRegenerationRate healthRegenerationRate = new();


    private List<PlayerStat> playerStats;


    // -------------------------------------------------------------------------------- Start -------------------------------------------------------------------------------
    private void Start()
    {
        Equipment.Instance.OnEquipmentChange += Equipment_OnEquipmentChange;
        playerAttributes.ToList().ForEach(x => x.Initialize());
        playerStats = new List<PlayerStat>()
        {
            health,
            baseDamage,
            criticalStrikeChance,
            damageReduction,
            healthRegenerationRate
        };
    }










    // ------------------------------------------------------------------- Equipment: On Equipment Change -------------------------------------------------------------------
    private void Equipment_OnEquipmentChange(object sender, OnEquipmentChangeEventArgs e)
    {
        // These are attributes that were on the piece of equipment that was either added or removed
        Attribute[] attributes = e.attributes;


        foreach (Attribute attribute in attributes)
        {
            // Get the player attribute that is the same type as the current attribute
            PlayerAttribute playerAttribute = playerAttributes
                .Single(x => x.AttributeType == attribute.attributeType);


            // Set the player attribute's value
            playerAttribute.SetValue(attribute.value);




            // Get the player stat that is associated with the current attribute
            PlayerStat playerStat = playerStats
                .Single(x => x.AttributeType == attribute.attributeType);

            // Set the value for the player stat
            playerStat.SetValue(playerAttribute.Value);
        }
    }
}