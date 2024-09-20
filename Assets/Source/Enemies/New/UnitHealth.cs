using System;
using UnityEngine;

namespace New
{
	public class UnitHealth
	{
		public event Action<float> OnHealthChanged;
		public float MaxHealth { get; private set; }

		public float Health
		{
			get => _health;
			set
			{
				_health = Mathf.Clamp(value, 0f, MaxHealth);
				OnHealthChanged?.Invoke(_health);
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