using System;
using UnityEngine;

public class ActivityHandler<T> where T : Enum
{
    private T _activityType;
    private Enemy _target;
    private EnemyProperty _property;
    private float _value; // universal value for damage
	private Vector2 _vector2Value; // universal vector for move

    public ActivityHandler(T activityType, Enemy target, EnemyProperty property)
    {
        _activityType = activityType;
        _target = target;
        _property = property;
    }

	public ActivityHandler(T activityType, Enemy target, EnemyProperty property, float value)
    {
        _activityType = activityType;
        _target = target;
        _property = property;
        _value = value;
    }

	public ActivityHandler(T activityType, Enemy target, EnemyProperty property, Vector2 value)
    {
        _activityType = activityType;
        _target = target;
        _property = property;
        _vector2Value = value;
    }

    public void Execute()
    {
        switch (typeof(T))
        {
            case Type t when t == typeof(DamageTypes):
                HandleDamage();
                break;
			case Type t when t == typeof(AttackTypes):
				HandleAttack();
				break;
			case Type t when t == typeof(HealTypes):
				HandleHeal();
				break;
			case Type t when t == typeof(MoveTypes):
				HandleMove();
				break;
			case Type t when t == typeof(DeathTypes):
				HandleDeath();
				break;
        }
    }

    private void HandleDeath()
    {
        switch (_property.DeathType) // Death type switch
		{
			case DeathTypes.OnlyDestroy:
				GameObject.Destroy(_target.gameObject);
				break;
		}
    }

    private void HandleMove()
    {
        switch (_property.MoveType) // Move type switch
        {
            case MoveTypes.OnlyMaxSpeed:
                _target.EnemyMovement.Move(_target.Rb, _vector2Value, _property.MaxSpeed);
                break;
            case MoveTypes.Offense:
				_target.MoveWithOffense(EnemyNavigator.Instance.Player.transform, _vector2Value);
                break;
        }
    }

    private void HandleHeal()
    {
        switch (_property.HealType) // Heal type switch
        {
            case HealTypes.OnlyHeal:
				_target.SetHealth(_target.CurrentHealth + _value);
                break;
        }
    }

    private void HandleAttack()
    {
        if (_activityType is AttackTypes attackType)
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
					_target.FreezeAndAttack();
                    break;
            }
		}
    }

    private void HandleDamage()
    {
        if (_activityType is DamageTypes damageType)
        {
            switch (damageType)
            {
                case DamageTypes.OnlyDamage:
					_target.TakeDamage(_value);
                    break;
                case DamageTypes.OnlyLogging:
					Debug.Log($"{_property.Name} get {_value} damage");
                    break;
                case DamageTypes.DamageWithRedImpulse:
					_target.TakeDamage(_value);
					_target?.RedImpulse();
                    break;
            }
        }
    }
}