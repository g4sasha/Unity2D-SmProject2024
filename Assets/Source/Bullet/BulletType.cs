using UnityEngine;

[CreateAssetMenu(fileName = "NewBullet", menuName = "Bullets/Bullet")]
public class BulletType : ScriptableObject
{
	[field: SerializeField] public float Damage { get; private set; } = 1f;
    [field: SerializeField] public float Cooldown { get; private set; } = 1f;
    [field: SerializeField] public float Speed = 10f;
    [field: SerializeField] public float Lifetime = 5f;
	[field: SerializeField] public string SoundName = "Shot";
	[field: SerializeField] public GameObject Prefab { get; private set; }
	[field: SerializeField] public LayerMask DestroyLayer { get; private set; }
}