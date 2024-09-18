using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
	[field: SerializeField] public Rigidbody2D Rb { get; private set; }
	[SerializeField] private EnemyProperty _properties;
	[SerializeField] private LayerMask _playerLayer;
	[SerializeField] private LayerMask _bulletLayer;
	[SerializeField] private GameObject _expPrefab;
	private float _currentHealth;
	private float _cooldown;
	private bool _readyToAttack;
	private RigidbodyMovement2D _enemyMovement;

	private void OnValidate()
	{
		Rb = GetComponent<Rigidbody2D>();
	}

	private void Awake()
	{
		_currentHealth = _properties.MaxHealth;
		_enemyMovement = new RigidbodyMovement2D();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if ((_bulletLayer & 1 << other.gameObject.layer) != 0)
		{
			var damage = other.GetComponent<Bullet>().BulletType.Damage; // TODO: not use GetComponent
			TakeDamage(damage);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
	{
		if ((_playerLayer & 1 << collision.gameObject.layer) != 0 && _readyToAttack)
		{
            switch (_properties.AttackType) // Attack type switch
            {
                case AttackTypes.ConsoleLog:
                    Debug.Log("Player damaged");
                    break;
                case AttackTypes.OnlyMaxDamage:
                    EnemyNavigator.Instance.PlayerHealth.TakeDamage(_properties.MaxDamage);
                    break;
                case AttackTypes.FreezeAndDamage:
					FreezeAndAttack();
                    break;
            }

            _cooldown = _properties.AttackDelay; // Do update coldown
        }
    }

    private void FreezeAndAttack()
    {
        EnemyNavigator.Instance.PlayerHealth.TakeDamage(_properties.MaxDamage / Random.Range(0.5f, 1f));
		EnemyNavigator.Instance.Il.FreezeMovement(1.5f);
    }

    private void Update()
	{
		UpdateCooldown();
	}

    public void TakeDamage(float damage)
    {
        switch (_properties.DamageType) // Damage type switch
        {
            case DamageTypes.DamageWithLogging:
				Debug.Log($"{_properties.Name} took damage! {_currentHealth} -> {_currentHealth - damage}");
				_currentHealth -= damage > 0f ? damage : 0f;
                break;
            case DamageTypes.OnlyDamage:
				_currentHealth -= damage > 0f ? damage : 0f;
                break;
        }

        CheckDeath();
    }

    public void Heal(float heal, bool logging = false) // NOTE: not in the TOR
    {
        switch (_properties.HealType) // Heal type switch
        {
            case HealTypes.OnlyHeal:
				_currentHealth += heal > 0f ? heal : 0f;
                break;
        }

		if (logging)
		{
			Debug.Log($"{_properties.Name} healed! {_currentHealth} -> {_currentHealth + heal}");
		}
    }

    public void Move(Transform target)
	{
		Vector2 direction = (target.position - transform.position).normalized;

        switch (_properties.MoveType) // Move type switch
        {
            case MoveTypes.OnlyMaxSpeed:
                _enemyMovement.Move(Rb, direction, _properties.MaxSpeed);
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
			speed = _properties.MaxSpeed / 4;
		}
		else
		{
			speed = _properties.MaxSpeed;
		}

		_enemyMovement.Move(Rb, direction, speed);
    }

    private void CheckDeath()
	{
		if (_currentHealth <= 0f)
		{
            switch (_properties.DeathType) // Death type switch
            {
                case DeathTypes.OnlyDestroy:
					Destroy(gameObject);
                    break;
            }

			Instantiate(_expPrefab, transform.position, Quaternion.identity).GetComponent<Expirience>().SetWeight(_properties.Expirience); // TODO: not use Instantiate
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