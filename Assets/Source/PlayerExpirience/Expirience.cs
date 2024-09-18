using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Expirience : MonoBehaviour
{
	public float Weight { get; private set; }
	[SerializeField] private Rigidbody2D _rb;
	[SerializeField] private float _speed;
	private Transform _target;
	private Vector2 _direction;

	private void OnValidate()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		_target = EnemyNavigator.Instance.Player.transform;
	}

	private void Update()
	{
		Move();
	}

	public void SetWeight(float value) => Weight = value; // NOTE: Use with Instantiate

	private void Move()
	{
		if (!_target)
		{
			return;
		}

		_direction = (_target.position - transform.position).normalized;
		var distance = Vector2.Distance(_target.position, transform.position);
		_rb.velocity = _direction * _speed / (distance * 2f + 1f);
	}
}