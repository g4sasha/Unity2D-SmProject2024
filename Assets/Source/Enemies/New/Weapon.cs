using Cysharp.Threading.Tasks;

namespace New
{
    public abstract class Weapon
	{
		protected WeaponConfig config { get; private set; }

		public Weapon(WeaponConfig config)
		{
			this.config = config;
		}

		public virtual async UniTask Attack(UnitDamageable target)
		{
			target.ApplyDamage(config.Damage);
			await UniTask.Delay((int)(config.Cooldown * 1000));
		}
	}
}