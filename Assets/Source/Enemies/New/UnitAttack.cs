using System;
using Cysharp.Threading.Tasks;

namespace New
{
	public class UnitAttack
	{
		public event Action<float> OnDamageChanged;
		public bool CanAttack { get; set; } = true;
		public bool IsAttacking { get; private set; }

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

		public async UniTaskVoid Attack(UnitDamageable target, int attackDelayMs)
		{
			if (IsAttacking || !CanAttack)
			{
				return;
			}

			target.ApplyDamage(Damage);
			IsAttacking = true;
			await UniTask.Delay(attackDelayMs);
			IsAttacking = false;
		}
	}
}