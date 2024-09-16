using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
	[field: SerializeField] public Rigidbody2D Rb { get; private set; }
	[SerializeField] private EnemyProperty _properties;
	[SerializeField] private LayerMask _playerLayer;
	[SerializeField] private LayerMask _bulletLayer;
	[SerializeField] private Player _player;
	private float _currentHealth;
	private float _cooldown;
	private bool _readyToAttack;
	private EnemyMovement _enemyMovement;

	private void OnValidate()
	{
		Rb = GetComponent<Rigidbody2D>();
	}

	private void Awake()
	{
		_currentHealth = _properties.MaxHealth;
		_enemyMovement = new EnemyMovement();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if ((_bulletLayer & 1 << other.gameObject.layer) != 0)
		{
			var damage = other.GetComponent<Bullet>().Damage; // TODO: not use GetComponent

            switch (_properties.DamageType)
            {
                case DamageType.DamageWithLogging:
                    Debug.Log($"{_properties.Name} was damaged! {_currentHealth} -> {_currentHealth - damage}"); // NOTE: do not move down!
                    TakeDamage(damage);
                    break;
                case DamageType.OnlyDamage:
					TakeDamage(damage);
                    break;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
	{
		if ((_playerLayer & 1 << other.gameObject.layer) != 0 && _readyToAttack)
		{
            switch (_properties.AttackType)
            {
                case AttackTypes.ConsoleLog:
					Debug.Log("Player damaged"); 
                    break;
            }
            
			_cooldown = _properties.AttackDelay;
        }
    }

    private void Update()
	{
		UpdateCooldown();
		Move();
	}

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage > 0f ? damage : 0f;
		CheckDeath();
    }

    public void Heal(float heal, bool logging = false) // NOTE: not in the TOR
    {
        _currentHealth += heal > 0f ? heal : 0f;

		if (logging)
		{
			Debug.Log($"{_properties.Name} healed! {_currentHealth} -> {_currentHealth + heal}");
		}
    }

	private void CheckDeath()
	{
		if (_currentHealth <= 0f)
		{
            switch (_properties.DeathType)
            {
                case DeathTypes.OnlyDestroy:
					Destroy(gameObject);
                    break;
            }
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

	private void Move()
	{
		Vector2 direction = (_player.transform.position - transform.position).normalized;
		_enemyMovement.Move(this, direction, _properties.MaxSpeed);
	}
}