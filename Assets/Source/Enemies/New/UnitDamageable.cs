using System;

namespace New
{
	public class UnitDamageable
	{
		private UnitHealth _unitHealth;

		public UnitDamageable(UnitHealth health)
		{
			_unitHealth = health;
		}

		public void ApplyDamage(float damage)
		{
			if (damage < 0f)
			{
				throw new ArgumentOutOfRangeException(nameof(damage));
			}

			var totalDamage = ProcessDamage(damage);

			if (totalDamage < 0f)
			{
				throw new ArgumentOutOfRangeException(nameof(totalDamage));
			}
			
			_unitHealth.Health -= totalDamage;
		}

		protected virtual float ProcessDamage(float damage)
		{
			return damage;
		}
	}
}