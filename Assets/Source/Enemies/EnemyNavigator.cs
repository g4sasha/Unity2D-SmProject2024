using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigator : MonoBehaviour
{
	[SerializeField] private List<Enemy> _enemies;
	[SerializeField] private Transform _target;

	private void Update()
	{
		foreach (var enemy in _enemies)
		{
			enemy.Move(_target);
		}
	}
}