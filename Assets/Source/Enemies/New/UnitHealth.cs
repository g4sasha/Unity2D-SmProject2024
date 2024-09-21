using System;
using UnityEngine;

namespace New
{
	public class UnitHealth
	{
		public event Action<float> OnHealthChanged;
		public event Action OnDeath;
		public float MaxHealth { get; private set; }

		public float Health
		{
			get => _health;
			set
			{
				_health = Mathf.Clamp(value, 0f, MaxHealth);
				OnHealthChanged?.Invoke(_health);

				if (_health == 0f)
				{
					OnDeath?.Invoke();
				}
			}
		}

		private float _health;

		private Unit _unit;

		public UnitHealth(Unit unit)
		{
			_unit = unit;
			MaxHealth = _unit.Config.Health;
			Health = MaxHealth;
		}
	}
}