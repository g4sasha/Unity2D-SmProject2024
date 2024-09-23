using Cysharp.Threading.Tasks;

namespace New
{
    public abstract class Weapon
	{
		public bool IsReloading { get; protected set; }
		protected WeaponConfig config { get; private set; }

		public Weapon(WeaponConfig config)
		{
			this.config = config;
		}

		public virtual async UniTask Attack(UnitDamageable target)
		{
			if (IsReloading)
			{
				return;
			}
			
			target.ApplyDamage(config.Damage);
			IsReloading = true;
			await UniTask.Delay((int)(config.Cooldown * 1000));
			IsReloading = false;
		}
	}
}