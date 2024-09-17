using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float _currentHealth;

    [SerializeField] private float _maximumHealth;

    [SerializeField] private HealthBar _healthBar;

    public float RemaningHealthPercentage => _currentHealth / _maximumHealth;
    public UnityEvent OnDied;

    private void OnEnable()
    {
        TakeDamage(0f);
    }

    public void TakeDamage(float damageAmount)
    {
        if (_currentHealth <= 0f)
        {
            _currentHealth = 0;
            return;
        }

        _currentHealth -= damageAmount;

        _healthBar.UpdateHealthBar(this);

        if (_currentHealth <= 0f)
        {
            OnDied?.Invoke();
        }
    }
}
