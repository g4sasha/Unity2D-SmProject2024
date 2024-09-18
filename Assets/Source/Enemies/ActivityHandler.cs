using System;
using UnityEngine;

public class ActivityHandler<T> where T : Enum
{
    private T _activityType;
    private Enemy _target;
    private EnemyProperty _property;
    private float _value; // universal value

    public ActivityHandler(T activityType, Enemy target, EnemyProperty property, float value = 0f)
    {
        _activityType = activityType;
        _target = target;
        _property = property;
        _value = value;
    }

    public void Execute()
    {
        switch (typeof(T))
        {
            case Type t when t == typeof(DamageTypes):
                HandleDamage();
                break;
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