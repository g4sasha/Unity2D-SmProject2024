using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LayerMove : MonoBehaviour
{
	[SerializeField] private bool _autoFind = true;
	[SerializeField] private Transform _target;
	[SerializeField] private SpriteRenderer _spriteRenderer;
	[SerializeField] private int _min = -100;
	[SerializeField] private int _max = 100;

	private void OnValidate()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		
		if (_autoFind)
		{
			_target = GameObject.Find("Player").transform;
		}
	}

	private void OnEnable()
	{
		_target = GameObject.Find("Player").transform;
	}

	private void Update()
	{
		if (!_target)
		{
			return;
		}

		if (transform.position.y > _target.position.y - 1.5f)
		{
			_spriteRenderer.sortingOrder = _min;
		}
		else
		{
			_spriteRenderer.sortingOrder = _max;
		}
	}
}