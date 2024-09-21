using Cysharp.Threading.Tasks;
using UnityEngine;

namespace New
{
    public class EnemyUnit : Unit
    {
        public UnitHealth UnitHealth { get; private set; }
		public UnitDamageable UnitDamageable { get; private set; }
		public UnitAttack UnitAttack { get; private set; }
		public UnitMovable UnitMovable { get; private set; }
        [SerializeField] protected Rigidbody2D _rigidbody;

		[Header("-ATTACK-"), Space]
		[SerializeField] private int _attackDelayMs;
		[SerializeField] private float _attackSpeedCoef = 1f;

		private PlayerUnit _target;

        public void Initialize()
		{
			UnitHealth = new UnitHealth(this);
			UnitDamageable = new UnitDamageable(UnitHealth);
			UnitAttack = new UnitAttack(this);
			UnitMovable = new UnitRigidbodyMovable(_rigidbody);
			UnitHealth.OnDeath += Die;
		}

        private void OnDestroy() => UnitHealth.OnDeath -= Die;

        private void Start()
        {
            Initialize();
            _target = PlayerUnit.Instance;
        }

        private void FixedUpdate()
        {
            Move();
			Flip();
			Attack().Forget();
        }

        protected virtual void Move()
        {
			var direction = GetDirection();

            if (Vector2.Distance(transform.position, _target.transform.position) > 2.1f)
            {
                UnitMovable.Move(direction * Config.Speed * Time.fixedDeltaTime);
				UnitAttack.CanAttack = false;
            }
            else if (Vector2.Distance(transform.position, _target.transform.position) < 1.9f)
            {
                UnitMovable.Move(direction * -Config.Speed * Time.fixedDeltaTime);
				UnitAttack.CanAttack = false;
            }
			else
			{
				UnitAttack.CanAttack = true;
			}
        }

		protected virtual async UniTaskVoid Attack()
        {
			if (UnitAttack.CanAttack && !UnitAttack.IsAttacking)
			{
				UnitAttack.Attack(_target.UnitDamageable, _attackDelayMs).Forget();

				Vector2 originalPosition = transform.position;
				Vector2 targetPosition = _target.transform.position;

				float percent = 0f;

				while (percent < 1f)
				{
					percent += Time.deltaTime * _attackSpeedCoef;
					float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4f;
					transform.position = Vector2.Lerp(originalPosition, targetPosition, interpolation);
					await UniTask.Yield();
				}
			}
        }

		protected virtual void Flip()
		{
			var direction = GetDirection();

			if (direction.x > 0f)
			{
				transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			}
			else if (direction.x < 0f)
			{
				transform.rotation = Quaternion.Euler(0f, 180f, 0f);
			}
		}

		protected Vector2 GetDirection()
		{
			return (_target.transform.position - transform.position).normalized;
		}
    }
}