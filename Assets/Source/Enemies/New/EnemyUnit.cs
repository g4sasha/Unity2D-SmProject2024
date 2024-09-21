using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

namespace New
{
    public class EnemyUnit : Unit
    {
        public UnitHealth UnitHealth { get; private set; }
		public UnitDamageable UnitDamageable { get; private set; }
		public UnitMovable UnitMovable { get; private set; }
        [SerializeField] protected new Rigidbody2D rigidbody;

		[Header("-ATTACK-"), Space]
		[SerializeField] protected float attackDuration;
		[SerializeField] private WeaponConfig startWeapon;

		protected PlayerUnit _target { get; private set; }
		protected Weapon _equippedWeapon;

		private bool _isAttacking;

        public void Initialize()
		{
			UnitHealth = new UnitHealth(this);
			UnitDamageable = new UnitDamageable(UnitHealth);
			UnitMovable = new UnitRigidbodyMovable(rigidbody);
			UnitHealth.OnDeath += Die;
		}

        private void OnDestroy() => UnitHealth.OnDeath -= Die;

        private void Start()
        {
            Initialize();
            _target = PlayerUnit.Instance;
			_equippedWeapon = new Knife(startWeapon);
        }

        private void FixedUpdate()
        {
            Move();
			Flip();
        }

        protected virtual void Move()
        {
			var direction = GetDirection();

            if (Vector2.Distance(transform.position, _target.transform.position) > 2.1f)
            {
                UnitMovable.Move(direction * Config.Speed * Time.fixedDeltaTime);
            }
            else if (Vector2.Distance(transform.position, _target.transform.position) < 1.9f)
            {
                UnitMovable.Move(direction * -Config.Speed * Time.fixedDeltaTime);
            }
			else
			{
				Attack().Forget();
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

		protected virtual async UniTask Attack()
		{
			if (_isAttacking)
			{
				return;
			}
			
			var cansellationToken = gameObject.GetCancellationTokenOnDestroy();
			AnimateAttack(cansellationToken).Forget();
			_isAttacking = true;
			await _equippedWeapon.Attack(_target.UnitDamageable);
			_isAttacking = false;
		}

        protected async UniTaskVoid AnimateAttack(CancellationToken cansellationToken)
        {
			var startPosition = transform.position;
			var targetPosition = _target.transform.position;
            await transform.DOMove(targetPosition, attackDuration).WithCancellation(cansellationToken);
			await transform.DOMove(startPosition, attackDuration).WithCancellation(cansellationToken);
        }

        protected Vector2 GetDirection()
		{
			return (_target.transform.position - transform.position).normalized;
		}
    }
}