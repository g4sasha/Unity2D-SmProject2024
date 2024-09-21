using UnityEngine;

namespace New
{
    public abstract class Unit : MonoBehaviour
    {
		[field: SerializeField] public UnitConfig Config;

		protected virtual void Die()
		{
			Destroy(gameObject);
		}
    }
}