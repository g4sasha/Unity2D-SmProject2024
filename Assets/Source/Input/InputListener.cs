using System.Collections;
using UnityEngine;

public class InputListener : MonoBehaviour
{
    public bool Enabled { get; set; } = true;
    [SerializeField] private Player _player;
    [SerializeField] private VignetteEffect _vignette;
    [SerializeField] private PauseManager _pauseManager;
    private bool _readyToShoot = true;
    private bool _canMove = true;

    private void Update()
    {
        if (!Enabled)
        {
            return;
        }

        Movement();
        Shooting();
        PauseHandler();
    }

    private void PauseHandler()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pauseManager.isActiveAndEnabled)
            {
                _pauseManager.Hide();
            }
            else
            {
                _pauseManager.Show();
            }
        }
    }

    public void FreezeMovement(float duration) => StartCoroutine(FreezeRoutine(duration));

    private void Movement()
    {
        if (!_canMove)
        {
            return;
        }

        _player.PlayerMovment.Move(_player.Rb, new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), _player.Speed);
    }

    private void Shooting()
    {
        if (!_readyToShoot) return;

        if (Input.GetButton("Fire1"))
        {
            _player.BulletSpawner.Spawn(_player.transform.position, _player.Bullet.Prefab.GetComponent<Bullet>(), Input.mousePosition); // TODO: not use GetComponent
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
	{
		_readyToShoot = false;
		yield return new WaitForSeconds(_player.Bullet.Cooldown);
		_readyToShoot = true;
	}

    private IEnumerator FreezeRoutine(float duration)
    {
        _canMove = false;
        _vignette.Activate();
        yield return new WaitForSeconds(duration);
        _canMove = true;
        _vignette.Deactivate();
    }
}