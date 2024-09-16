using UnityEngine;

public class InputListener : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void Update()
    {
        Movement();
        Shooting();
    }

    private void Movement() => _player.PlayerMovment.Move(_player.Rb, new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), _player.Speed);

    private void Shooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _player.BulletSpawner.Spawn(_player.transform.position, _player.Bullet, Input.mousePosition);
        }
    }
}