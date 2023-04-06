using UnityEngine;

public abstract class Character : MonoBehaviour
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
}