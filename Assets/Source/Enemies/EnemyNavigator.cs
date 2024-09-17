using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigator : MonoBehaviour
{
	public static EnemyNavigator Instance { get; private set; }
	[field: SerializeField] public HealthController PlayerHealth { get; private set; }
	[field: SerializeField] public Player Player { get; private set; }
	[field: SerializeField] public InputListener Il { get; private set; }
	[SerializeField] private List<Enemy> _enemies;

    private void Awake() => Instance = this; // NOTE: Only gameplay scene

    private void Update()
	{
		foreach (var enemy in _enemies)
		{
			enemy.Move(Player.transform);
		}
	}

    public void AddEnemy(Enemy enemy) => _enemies.Add(enemy);

	public void RemoveEnemy(Enemy enemy) => _enemies.Remove(enemy);
}