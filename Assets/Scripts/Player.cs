using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Endurance
    [SerializeField] private Endurance endurance;
    public Endurance Endurance { get { return endurance; } }

    // Power
    [SerializeField] private Power power;
    public Power Power { get { return power; } }

    // Defense
    [SerializeField] private Defense defense;
    public Defense Defense { get { return defense; } }

    // Critical Strike
    [SerializeField] private CriticalStrike criticalStrike;
    public CriticalStrike CriticalStrike { get { return criticalStrike; } }

    // Vitality
    [SerializeField] private Vitality vitality;
    public Vitality Vitality { get { return vitality; } }

    

    // Health
    private readonly Health health = new();

    // Base Damage
    private readonly BaseDamage baseDamage = new();

    // Damage Reduction
    private readonly DamageReduction damageReduction = new();

    // Critical Strike Chance
    private readonly CriticalStrikeChance criticalStrikeChance = new();

    // Health Regeneration Rate
    private readonly HealthRegenerationRate healthRegenerationRate = new();


    private List<PlayerAttribute> playerAttributes;
    public static Player Instance { get; private set; }
    public event EventHandler OnChange;


    // -------------------------------------------------------------------------------- Awake -------------------------------------------------------------------------------
    private void Awake()
    {
        Instance = this;

        Equipment.Instance.OnEquipmentChange += Equipment_OnEquipmentChange; ;

        endurance.Initialize(health);
        power.Initialize(baseDamage);
        defense.Initialize(damageReduction);
        criticalStrike.Initialize(criticalStrikeChance);
        vitality.Initialize(healthRegenerationRate);

        playerAttributes = new List<PlayerAttribute>()
        {
            endurance,
            power,
            defense,
            criticalStrike,
            vitality
        };
    }










    // ------------------------------------------------------------------- Equipment: On Equipment Change -------------------------------------------------------------------
    private void Equipment_OnEquipmentChange(object sender, EventArgs e)
    {
        foreach (PlayerAttribute playerAttribute in playerAttributes)
        {
            playerAttribute.ResetValue();
        }

        foreach (EquipmentSlot equipmentSlot in Equipment.Instance.Slots)
        {
            if (equipmentSlot.Item == null) continue;

            EquipableItem item = equipmentSlot.Item as EquipableItem;

            foreach (Attribute attribute in item.Attributes)
            {
                //Get the player attribute that is the same type as the current attribute
                PlayerAttribute playerAttribute = playerAttributes
                    .Single(x => x.AttributeType == attribute.attributeType);

                // Set the value for the player attribute
                playerAttribute.SetValue(attribute.value);
            }
        }

        OnChange?.Invoke(this, new EventArgs());
    }
}