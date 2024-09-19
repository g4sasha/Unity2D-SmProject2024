using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteRandomizer : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
	[SerializeField] private SpriteRenderer _spriteRenderer;

	private void OnValidate()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnEnable()
	{
		_spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];
	}
}