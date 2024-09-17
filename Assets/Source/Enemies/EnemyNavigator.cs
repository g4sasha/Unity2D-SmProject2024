using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigator : MonoBehaviour
{
	public static EnemyNavigator Instance { get; private set; }
	[field: SerializeField] public HealthController PlayerHealth { get; private set; }
	[SerializeField] private List<Enemy> _enemies;
	[SerializeField] private Transform _target;

    private void Awake() => Instance = this; // NOTE: Only gameplay scene

    private void Update()
	{
		foreach (var enemy in _enemies)
		{
			enemy.Move(_target);
		}
	}

    public void AddEnemy(Enemy enemy) => _enemies.Add(enemy);

	public void RemoveEnemy(Enemy enemy) => _enemies.Remove(enemy);
}