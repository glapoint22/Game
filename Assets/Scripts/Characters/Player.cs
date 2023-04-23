using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player: MonoBehaviour
{
    // Endurance
    [SerializeField] protected CharacterAttribute endurance = new(AttributeType.Endurance, new Health());
    public CharacterAttribute Endurance { get { return endurance; } }

    // Power
    [SerializeField] protected CharacterAttribute power = new(AttributeType.Power, new BaseDamage());
    public CharacterAttribute Power { get { return power; } }

    // Defense
    [SerializeField] protected CharacterAttribute defense = new(AttributeType.Defense, new DamageReduction());
    public CharacterAttribute Defense { get { return defense; } }

    // Critical Strike
    [SerializeField] protected CharacterAttribute criticalStrike = new(AttributeType.CriticalStrike, new CriticalStrikeChance());
    public CharacterAttribute CriticalStrike { get { return criticalStrike; } }

    // Vitality
    [SerializeField] protected CharacterAttribute vitality = new(AttributeType.Vitality, new HealthRegenerationRate());
    public CharacterAttribute Vitality { get { return vitality; } }


    // Reputations
    [SerializeField] private PlayerReputation[] reputations;
    public PlayerReputation[] Reputations { get { return reputations; } }

    private List<CharacterAttribute> characterAttributes;
    public static Player Instance { get; private set; }


    public event EventHandler OnAttributesChange;
    public event EventHandler OnReputationChange;


    // -------------------------------------------------------------------------------- Awake -------------------------------------------------------------------------------
    protected void Awake()
    {
        Instance = this;

        Equipment.Instance.OnEquipmentChange += Equipment_OnEquipmentChange;


        characterAttributes = new List<CharacterAttribute>()
        {
            endurance,
            power,
            defense,
            criticalStrike,
            vitality
        };

        InitializeCharacterAttributeValues();
    }





    private void InitializeCharacterAttributeValues()
    {
        // Reset each character attribute
        foreach (CharacterAttribute playerAttribute in characterAttributes)
        {
            playerAttribute.InitializeValue();
        }
    }




    // ------------------------------------------------------------------- Equipment: On Equipment Change -------------------------------------------------------------------
    private void Equipment_OnEquipmentChange(object sender, EventArgs e)
    {
        InitializeCharacterAttributeValues();

        // Loop through each equipment slot and update the player attributes
        foreach (EquipmentSlot equipmentSlot in Equipment.Instance.Slots)
        {
            if (equipmentSlot.Item == null) continue;

            EquipableItem item = equipmentSlot.Item as EquipableItem;

            foreach (Attribute attribute in item.Attributes)
            {
                //Get the player attribute that is the same type as the current attribute
                CharacterAttribute playerAttribute = characterAttributes
                    .Single(x => x.AttributeType == attribute.attributeType);

                // Set the value for the player attribute
                playerAttribute.SetValue(attribute.value);
            }
        }

        OnAttributesChange?.Invoke(this, new EventArgs());
    }





    public void SetReputation(Reputation[] reputations)
    {
        foreach (Reputation reputation in reputations)
        {
            PlayerReputation playerReputation = this.reputations
                .Single(x => x.Faction == reputation.Faction);

            int maxPoints = GameManager.Instance.GetMaxReputationPoints(playerReputation.Level);
            int points = playerReputation.Points + reputation.Points;
            ReputationLevel level = playerReputation.Level;

            if (points > maxPoints)
            {
                level = GameManager.Instance.GetNextReputationLevel(level);

                if (level == ReputationLevel.None)
                {
                    points = maxPoints;
                    level = playerReputation.Level;
                }
                else
                {
                    points -= maxPoints;
                }
            }
            else if (points < 0)
            {
                level = GameManager.Instance.GetPreviousReputationLevel(level);

                if (level == ReputationLevel.None)
                {
                    points = 0;
                    level = playerReputation.Level;
                }
                else
                {
                    maxPoints = GameManager.Instance.GetMaxReputationPoints(level);
                    points += maxPoints;
                }
            }

            playerReputation.Update(level, points);
        }

        OnReputationChange?.Invoke(this, new EventArgs());
    }
}