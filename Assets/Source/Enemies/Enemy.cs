using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(RedImpulse))]
public class Enemy : MonoBehaviour
{
	[field: SerializeField] public Rigidbody2D Rb { get; private set; }
	[SerializeField] private EnemyProperty _property;
	[SerializeField] private LayerMask _playerLayer;
	[SerializeField] private LayerMask _bulletLayer;
	[SerializeField] private GameObject _expPrefab;
	[SerializeField] private RedImpulse _redImpulse;
	private float _currentHealth;
	private float _cooldown;
	private bool _readyToAttack;
	private RigidbodyMovement2D _enemyMovement;

	private void OnValidate()
	{
		Rb = GetComponent<Rigidbody2D>();
		_redImpulse = GetComponent<RedImpulse>();
	}

	private void Awake()
	{
		_currentHealth = _property.MaxHealth;
		_enemyMovement = new RigidbodyMovement2D();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if ((_bulletLayer & 1 << other.gameObject.layer) != 0)
		{
			var damage = other.GetComponent<Bullet>().BulletType.Damage; // TODO: not use GetComponent
			var ActivityHandler = new ActivityHandler<DamageTypes>(_property.DamageType, this, _property, damage);
			ActivityHandler.Execute();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
	{
		if ((_playerLayer & 1 << collision.gameObject.layer) != 0 && _readyToAttack)
		{
            switch (_property.AttackType) // Attack type switch
            {
                case AttackTypes.ConsoleLog:
                    Debug.Log("Player damaged");
                    break;
                case AttackTypes.OnlyMaxDamage:
                    EnemyNavigator.Instance.PlayerHealth.TakeDamage(_property.MaxDamage);
                    break;
                case AttackTypes.FreezeAndDamage:
					FreezeAndAttack();
                    break;
            }

            _cooldown = _property.AttackDelay; // Do update coldown
        }
    }

    private void Update()
	{
		UpdateCooldown();
	}

	public void RedImpulse()
	{
		_redImpulse.Impulse().Forget();
	}

	private void FreezeAndAttack()
    {
        EnemyNavigator.Instance.PlayerHealth.TakeDamage(_property.MaxDamage / Random.Range(0.5f, 1f));
		EnemyNavigator.Instance.Il.FreezeMovement(1.5f);
    }

    public void TakeDamage(float damage)
    {
		_currentHealth -= damage > 0f ? damage : 0f;
		CheckDeath();
    }

    public void Heal(float heal, bool logging = false) // NOTE: not in the TOR
    {
        switch (_property.HealType) // Heal type switch
        {
            case HealTypes.OnlyHeal:
				_currentHealth += heal > 0f ? heal : 0f;
                break;
        }

		if (logging)
		{
			Debug.Log($"{_property.Name} healed! {_currentHealth} -> {_currentHealth + heal}");
		}
    }

    public void Move(Transform target)
	{
		Vector2 direction = (target.position - transform.position).normalized;

        switch (_property.MoveType) // Move type switch
        {
            case MoveTypes.OnlyMaxSpeed:
                _enemyMovement.Move(Rb, direction, _property.MaxSpeed);
                break;
            case MoveTypes.Offense:
				MoveWithOffense(target, direction);
                break;
        }
    }

    private void MoveWithOffense(Transform target, Vector2 direction)
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

		_enemyMovement.Move(Rb, direction, speed);
    }

    private void CheckDeath()
	{
		if (_currentHealth <= 0f)
		{
            switch (_property.DeathType) // Death type switch
            {
                case DeathTypes.OnlyDestroy:
					Destroy(gameObject);
                    break;
            }

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