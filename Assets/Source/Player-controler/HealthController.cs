using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float _currentHealth;

    [SerializeField]
    private float _maximumHealth;

    public float RemaningHealthPercentage
    {
        get
        {
            return _currentHealth / _maximumHealth;
        }
    }

    public bool IsInvicible { get; set; }

    public UnityEvent OnDied;

    public UnityEvent OnDamage;

    public UnityEvent OnHealthChanged;

    public void TakeDamage(float damageAmount)
    {
        if (_currentHealth ==0)
        {
            return;
        }

        _currentHealth -= damageAmount;

        OnHealthChanged.Invoke();

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        if(_currentHealth ==0)
        {
            OnDied.Invoke();
        }
    }
}
