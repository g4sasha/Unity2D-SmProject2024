using UnityEngine;

public class InputListener : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void Update()
    {
        Movement();
        Shooting();
    }

    private void Movement() => _player.PlayerMovment.Move(_player, new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));

    private void Shooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _player.PlayerCombat.Shoot(_player, _player.Bullet, Input.mousePosition);
        }
    }
}