using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
	[field: SerializeField] public float Speed { get; private set; } = 5f;
    [field: SerializeField] public Rigidbody2D Rb { get; private set; }
	private PlayerMovment _playerMovment;

	private void OnValidate()
	{
		Rb = GetComponent<Rigidbody2D>();
	}
}