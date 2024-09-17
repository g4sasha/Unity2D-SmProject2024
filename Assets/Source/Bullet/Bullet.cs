using UnityEngine;

public class Bullet : MonoBehaviour
{
    [field: SerializeField] public BulletType BulletType { get; private set; }
    private Vector3 _direction;

    private void OnEnable() => Destroy(gameObject, BulletType.Lifetime);

    private void Start() => SoundManager.Instance.PlaySound("Shot");

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((BulletType.DestroyLayer & 1 << other.gameObject.layer) != 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update() => transform.position += BulletType.Speed * Time.deltaTime * _direction;

    public void SetDirection(Vector3 direction) => _direction = direction;
}