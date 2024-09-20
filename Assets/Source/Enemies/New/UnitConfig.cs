using UnityEngine;

namespace New
{
	[CreateAssetMenu(fileName = "NewUnit", menuName = "Unit/UnitConfig")]
	public class UnitConfig : ScriptableObject
	{
		[field: SerializeField] public string Name { get; private set; }
		[field: SerializeField, Min(0)] public float Health { get; private set; }
		[field: SerializeField, Min(0)] public float Damage { get; private set; }
		[field: SerializeField, Min(0)] public float Speed { get; private set; }
		[field: SerializeField] public Unit Prefab { get; private set; }
	}
}