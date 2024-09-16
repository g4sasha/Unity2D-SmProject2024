using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnCooldown;
    [SerializeField] private int _enemiesCount;
    [SerializeField] private EnemyProperty _enemyType;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (Application.isPlaying)
        {
            yield return new WaitForSeconds(_spawnCooldown);

            for (int i = 0; i < _enemiesCount; i++)
            {
                EnemyNavigator.Instance.AddEnemy(
                    Instantiate(_enemyType.Prefab, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), Quaternion.identity)
                    .GetComponent<Enemy>()); // TODO: not use GetComponent
            }
        }
    }
}