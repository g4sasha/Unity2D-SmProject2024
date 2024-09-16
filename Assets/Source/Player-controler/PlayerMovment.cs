using UnityEngine;

public class PlayerMovment
{
    public void Move(Player player, Vector2 direction) => player.Rb.MovePosition(player.Rb.position + player.Speed * Time.fixedDeltaTime * direction);
}