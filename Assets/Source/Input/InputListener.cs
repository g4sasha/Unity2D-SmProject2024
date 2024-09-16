using System.Collections;
using UnityEngine;

public class InputListener : MonoBehaviour
{
    [SerializeField] private Player _player;
    private bool _readyToShoot = true;

    private void Update()
    {
        Movement();
        Shooting();
    }

    private void Movement() => _player.PlayerMovment.Move(_player.Rb, new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), _player.Speed);

    private void Shooting()
    {
        if (!_readyToShoot) return;

        if (Input.GetButton("Fire1"))
        {
            _player.BulletSpawner.Spawn(_player.transform.position, _player.Bullet, Input.mousePosition);
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
	{
		_readyToShoot = false;
		yield return new WaitForSeconds(_player.Bullet.Cooldown);
		_readyToShoot = true;
	}
}