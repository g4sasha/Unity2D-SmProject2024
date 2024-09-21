using UnityEngine;

namespace New
{
    [CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon/Weapon")]
	public class WeaponConfig : ScriptableObject
	{
		[field: SerializeField] public string Name { get; private set; }
		[field: SerializeField] public float Damage { get; private set; }
		[field: SerializeField] public float Cooldown { get; private set; }
	}
}