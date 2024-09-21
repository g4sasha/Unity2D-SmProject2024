using UnityEngine;

namespace New
{
	public class PlayerUnit : Unit
	{
		public UnitHealth UnitHealth { get; private set; }
		public UnitDamageable UnitDamageable { get; private set; }
		public UnitAttack UnitAttack { get; private set; }
		public UnitMovable UnitMovable { get; private set; }
		public UnitInputListener UnitInput { get; private set; }
		[SerializeField] private Rigidbody2D _rigidbody;
		[SerializeField] private PlayerHealthBar _healthBar;
		[SerializeField] private LayerMask _enemyLayer;
		[SerializeField] private GameOverManager _gameOverManager;

		public void Initialize()
		{
			UnitHealth = new UnitHealth(this);
			UnitDamageable = new UnitDamageable(UnitHealth);
			UnitAttack = new UnitAttack(this);
			UnitMovable = new UnitRigidbodyMovable(_rigidbody);
			UnitInput = new UnitInputListener();
			_healthBar.Connect(UnitHealth);
			UnitHealth.OnDeath += Die;
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			if ((_enemyLayer & 1 << other.gameObject.layer) != 0)
			{
				UnitDamageable.ApplyDamage(other.gameObject.GetComponent<Unit>().Config.Damage);
			}
		}

		private void FixedUpdate()
		{
			UnitMovable.Move(UnitInput.GetMoveAxes().normalized * Config.Speed * Time.fixedDeltaTime);
		}

		protected override void Die()
		{
			UnitHealth.OnDeath -= Die;
			_gameOverManager.ShowGameOver();
			UnitInput.Locked = true;
		}
	}
}