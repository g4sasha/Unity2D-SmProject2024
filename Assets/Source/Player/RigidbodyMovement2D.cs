using UnityEngine;

public class RigidbodyMovement2D
{
    public void Move(Rigidbody2D target, Vector2 direction, float speed)
        => target.MovePosition(target.position + speed * Time.fixedDeltaTime * direction);
}