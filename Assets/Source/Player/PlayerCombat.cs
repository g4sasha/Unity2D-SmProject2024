using UnityEngine;

public class BulletSpawner
{
    public void Spawn(Vector3 position, Bullet bullet, Vector3 mousePosition)
		=> GameObject.Instantiate(bullet, position, Quaternion.identity)
		.SetDirection((mousePosition - Camera.main.WorldToScreenPoint(position)).normalized);
}