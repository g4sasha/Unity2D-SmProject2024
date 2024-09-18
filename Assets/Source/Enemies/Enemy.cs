using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(RedImpulse))]
public class Enemy : MonoBehaviour
{
	public RigidbodyMovement2D EnemyMovement { get; private set; }
	[field: SerializeField] public Rigidbody2D Rb { get; private set; }
	[field: SerializeField] public float CurrentHealth { get; private set; }
	[SerializeField] private EnemyProperty _property;
	[SerializeField] private LayerMask _playerLayer;
	[SerializeField] private LayerMask _bulletLayer;
	[SerializeField] private GameObject _expPrefab;
	[SerializeField] private RedImpulse _redImpulse;
	private float _cooldown;
	private bool _readyToAttack;

	private void OnValidate()
	{
		Rb = GetComponent<Rigidbody2D>();
		_redImpulse = GetComponent<RedImpulse>();
	}

	private void Awake()
	{
		CurrentHealth = _property.MaxHealth;
		EnemyMovement = new RigidbodyMovement2D();
	}

	private void Update()
	{
		UpdateCooldown();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if ((_bulletLayer & 1 << other.gameObject.layer) != 0)
		{
			var damage = other.GetComponent<Bullet>().BulletType.Damage; // TODO: not use GetComponent
			var activityHandler = new ActivityHandler<DamageTypes>(_property.DamageType, this, _property, damage);
			activityHandler.Execute();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
	{
		if ((_playerLayer & 1 << collision.gameObject.layer) != 0 && _readyToAttack)
		{
            var activityHandler = new ActivityHandler<AttackTypes>(_property.AttackType, this, _property);
			activityHandler.Execute();
            _cooldown = _property.AttackDelay; // Do update coldown
        }
    }

	public void RedImpulse()
	{
		_redImpulse.Impulse().Forget();
	}

	public void SetHealth(float health)
	{
		CurrentHealth = health;
		CheckDeath();
	}

	public void TakeDamage(float damage)
    {
		CurrentHealth -= damage > 0f ? damage : 0f;
		CheckDeath();
    }

	public void FreezeAndAttack()
    {
        EnemyNavigator.Instance.PlayerHealth.TakeDamage(_property.MaxDamage / Random.Range(0.5f, 1f));
		EnemyNavigator.Instance.Il.FreezeMovement(1.5f);
    }

    public void Heal(float heal) // NOTE: not in the TOR
    {
        new ActivityHandler<HealTypes>(_property.HealType, this, _property, heal).Execute();
    }

    public void Move(Transform target)
	{
		Vector2 direction = (target.position - transform.position).normalized;
        var activityHandler = new ActivityHandler<MoveTypes>(_property.MoveType, this, _property, direction);
		activityHandler.Execute();
    }

    public void MoveWithOffense(Transform target, Vector2 direction)
    {
		float speed;

		if (Vector2.Distance(target.position, transform.position) > 3f)
		{
			speed = _property.MaxSpeed / 4;
		}
		else
		{
			speed = _property.MaxSpeed;
		}

		EnemyMovement.Move(Rb, direction, speed);
    }

    private void CheckDeath()
	{
		if (CurrentHealth <= 0f)
		{
            var activityHandler = new ActivityHandler<DeathTypes>(_property.DeathType, this, _property);
			activityHandler.Execute();
			Instantiate(_expPrefab, transform.position, Quaternion.identity).GetComponent<Expirience>().SetWeight(_property.Expirience); // TODO: not use Instantiate
			EnemyNavigator.Instance.RemoveEnemy(this);
        }
    }

	private void UpdateCooldown()
	{
		if (_cooldown <= 0f)
		{
			_cooldown = 0f; // NOTE: always updated
			_readyToAttack = true;
		}
		else
		{
			_cooldown -= Time.deltaTime;
			_readyToAttack = false;
		}
	}
}