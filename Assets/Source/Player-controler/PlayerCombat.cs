using UnityEngine;

public class PlayerCombat
{
	public void Shoot(Player player, Bullet bullet, Vector3 mousePosition)
	{
		GameObject.Instantiate(bullet, player.transform.position, Quaternion.identity).SetDirection((mousePosition - Camera.main.WorldToScreenPoint(player.transform.position)).normalized);
	}
}