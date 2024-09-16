using UnityEngine;

public class EnemyMovement
{
	public void Move(Enemy target, Vector2 direction, float speed)
	{
		target.Rb.velocity = direction * speed;
	}
}