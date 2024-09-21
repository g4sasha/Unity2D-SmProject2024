using New;
using UnityEngine;

namespace Global
{
	public class Bootstrapper : MonoBehaviour
	{
		[SerializeField] private PlayerUnit _playerUnit;

		private void Awake()
		{
			_playerUnit.Initialize();
			_playerUnit.UnitHealth.Health = 50f;
		}
	}
}