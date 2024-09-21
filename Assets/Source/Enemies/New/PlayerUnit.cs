using UnityEngine;

namespace New
{
	public sealed class PlayerUnit : Unit
	{
		public static PlayerUnit Instance { get; private set; }
		public UnitHealth UnitHealth { get; private set; }
		public UnitDamageable UnitDamageable { get; private set; }
		public UnitAttack UnitAttack { get; private set; }
		public UnitMovable UnitMovable { get; private set; }
		public UnitInputListener UnitInput { get; private set; }
		[SerializeField] private Rigidbody2D _rigidbody;
		[SerializeField] private PlayerHealthBar _healthBar;
		[SerializeField] private GameOverManager _gameOverManager;

		public void Initialize()
		{
			if (Instance)
			{
				Destroy(gameObject);
				return;
			}
			else
			{
				Instance = this;
			}

			UnitHealth = new UnitHealth(this);
			UnitDamageable = new UnitDamageable(UnitHealth);
			UnitAttack = new UnitAttack(this);
			UnitMovable = new UnitRigidbodyMovable(_rigidbody);
			UnitInput = new UnitInputListener();
			_healthBar.Connect(UnitHealth);
			UnitHealth.OnDeath += Die;
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