using System;

namespace New
{
	public class UnitAttack
	{
		public event Action<float> OnDamageChanged;

		public float Damage
		{
			get => _damage;
			set
			{
				_damage = value;
				OnDamageChanged?.Invoke(_damage);
			}
		}

		private float _damage;

		private Unit _unit;

		public UnitAttack(Unit unit)
		{
			_unit = unit;
			Damage = _unit.Config.Damage;
		}

		public void Attack(UnitDamageable target)
		{
			target.ApplyDamage(Damage);
		}
	}
}