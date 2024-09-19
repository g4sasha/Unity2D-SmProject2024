using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
	[field: SerializeField] public float Speed { get; private set; } = 5f;
    [field: SerializeField] public Rigidbody2D Rb { get; private set; }
	[field: SerializeField] public BulletType Bullet { get; private set; }
    [field: SerializeField] public HealthController Health { get; private set; }
	public RigidbodyMovement2D PlayerMovment { get; private set; }
	public BulletSpawner BulletSpawner { get; private set; }
    public ExpBank Expirience { get; private set; }
    [SerializeField] private ExpBar _expBar;
    [SerializeField] private LayerMask _expLayer;
    [SerializeField] private LayerMask _healLayer;

    private void OnValidate() => Rb = GetComponent<Rigidbody2D>();

    private void Awake()
    {
        PlayerMovment = new RigidbodyMovement2D();
		BulletSpawner = new BulletSpawner();
        Expirience = new ExpBank(level: 0, exp: 0);

        _expBar.Construct(Expirience);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((_expLayer & 1 << collision.gameObject.layer) != 0)
        {
            Expirience.AddExp(collision.gameObject.GetComponent<Expirience>().Weight); // TODO: not use GetComponent
            Destroy(collision.gameObject);
        }

        if ((_healLayer & 1 << collision.gameObject.layer) != 0)
        {
            Health.Heal(0.5f);
            Destroy(collision.gameObject);
        }
    }
}