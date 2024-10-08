using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemies/Enemy")]
public class EnemyProperty : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float MaxSpeed { get; private set; }
    [field: SerializeField] public float MinDamage { get; private set; }
    [field: SerializeField] public float MaxDamage { get; private set; }
    [field: SerializeField] public float AttackDelay { get; private set; }
    [field: SerializeField] public float Expirience { get; private set; }
    [field: SerializeField] public DeathTypes DeathType { get; private set; }
    [field: SerializeField] public AttackTypes AttackType { get; private set; }
    [field: SerializeField] public DamageTypes DamageType { get; private set; }
    [field: SerializeField] public MoveTypes MoveType { get; private set; }
    [field: SerializeField] public HealTypes HealType { get; private set; }
    [field: SerializeField] public GameObject Prefab { get; private set; }
}