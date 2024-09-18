using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
	[field: SerializeField] public float Speed { get; private set; } = 5f;
    [field: SerializeField] public Rigidbody2D Rb { get; private set; }
	[field: SerializeField] public BulletType Bullet { get; private set; }
	public RigidbodyMovement2D PlayerMovment { get; private set; }
	public BulletSpawner BulletSpawner { get; private set; }
    public ExpBank Expirience { get; private set; }
    [SerializeField] private ExpBar _expBar;

    private void Awake()
    {
        PlayerMovment = new RigidbodyMovement2D();
		BulletSpawner = new BulletSpawner();
        Expirience = new ExpBank(level: 0, exp: 0);

        _expBar.Construct(Expirience);
    }

    private void OnValidate() => Rb = GetComponent<Rigidbody2D>();
}