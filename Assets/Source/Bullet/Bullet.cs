using UnityEngine;

public class Bullet : MonoBehaviour
{
    [field: SerializeField] public float Damage { get; private set; } = 1f;
    [field: SerializeField] public float Cooldown { get; private set; } = 1f;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifetime = 5f;
    private Vector3 _direction;

    private void OnEnable() => Destroy(gameObject, _lifetime);

    private void Update() => transform.position += _speed * Time.deltaTime * _direction;

    public void SetDirection(Vector3 direction) => _direction = direction;
}