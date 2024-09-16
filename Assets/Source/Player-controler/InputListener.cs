using UnityEngine;

public class InputListener : MonoBehaviour
{
    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        _playerMovment.Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }
}