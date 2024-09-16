using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
	[field: SerializeField] public float Speed { get; private set; } = 5f;
    [field: SerializeField] public Rigidbody2D Rb { get; private set; }
	[field: SerializeField] public Bullet Bullet { get; private set; }
	public PlayerMovment PlayerMovment { get; private set; }
	public PlayerCombat PlayerCombat { get; private set; }

    private void Awake()
    {
        PlayerMovment = new PlayerMovment();
		PlayerCombat = new PlayerCombat();
    }

    private void OnValidate() => Rb = GetComponent<Rigidbody2D>();
}