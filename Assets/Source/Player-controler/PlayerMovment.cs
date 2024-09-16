using UnityEngine;

public class PlayerMovment
{
    public void Move(Player player, Vector2 direction)
    {
        player.Rb.MovePosition(player.Rb.position + direction * player.Speed * Time.fixedDeltaTime);
    }
}