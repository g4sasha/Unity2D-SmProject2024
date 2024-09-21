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

		public void Initialize()
		{
			UnitHealth = new UnitHealth(this);
			UnitDamageable = new UnitDamageable(UnitHealth);
			UnitAttack = new UnitAttack(this);
			UnitMovable = new UnitRigidbodyMovable(_rigidbody);
			UnitInput = new UnitInputListener();
		}

		private void FixedUpdate()
		{
			UnitMovable.Move(UnitInput.GetMoveAxes().normalized * Config.Speed * Time.fixedDeltaTime);
		}
	}
}