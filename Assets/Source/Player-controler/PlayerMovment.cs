
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public float speed= 5f;
    public Rigidbody2D rb;
    UnityEngine.Vector2 move;

    void FixedUpdate()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        rb.MovePosition(rb.position+move*speed*Time.fixedDeltaTime);
    }
}