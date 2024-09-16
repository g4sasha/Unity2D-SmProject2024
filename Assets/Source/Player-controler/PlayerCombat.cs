using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	[SerializeField] private Transform _player;
	[SerializeField] private Bullet _bulletPrefab;

	private void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Instantiate(_bulletPrefab, transform.position, Quaternion.identity).SetDirection((Input.mousePosition - Camera.main.WorldToScreenPoint(_player.position)).normalized);
		}
	}
}