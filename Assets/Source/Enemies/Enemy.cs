using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private EnemyProperty _properties;
	[SerializeField] private LayerMask _playerLayer;
	[SerializeField] private LayerMask _bulletLayer;
	private float _currentHealth;
	private float _cooldown;
	private bool _readyToAttack;

	private void Awake()
	{
		_currentHealth = _properties.MaxHealth;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if ((_bulletLayer & 1 << other.gameObject.layer) != 0)
		{
			var damage = other.GetComponent<Bullet>().Damage; // TODO: not use GetComponent

            switch (_properties.DamageType)
            {
                case DamageType.DefaultWithLogging:
                    Debug.Log($"{_properties.Name} was damaged! {_currentHealth} -> {_currentHealth - damage}"); // TODO: damage animation
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
                case AttackTypes.ConsoleWrite:
					Debug.Log("Player hit"); 
                    break;
            }
            
			_cooldown = _properties.AttackDelay;
        }
    }

    private void Update()
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

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage > 0f ? damage : 0f;
		CheckDeath();
    }

    public void Heal(float heal)
    {
        _currentHealth += heal > 0f ? heal : 0f;
    }

	private void CheckDeath()
	{
		if (_currentHealth <= 0f)
		{
            switch (_properties.DeathType)
            {
                case DeathTypes.Default:
					Destroy(gameObject);
                    break;
            }
        }
    }
}