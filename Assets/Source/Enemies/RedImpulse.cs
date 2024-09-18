using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RedImpulse : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _spriteRenderer;
	private Tween _colorTween;

    private void OnValidate() => _spriteRenderer = GetComponent<SpriteRenderer>();

    private void Awake() => _spriteRenderer.color = Color.white;

    private void OnDestroy() => _colorTween.Kill();

    public async UniTask Impulse()
    {
        // Проверяем, существует ли объект и компонент SpriteRenderer
        if (_spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer не найден или объект уничтожен.");
            return; // Завершаем выполнение метода, если объект был уничтожен
        }

        _spriteRenderer.color = Color.red;

        await UniTask.Delay(100);

        // Проверяем снова перед изменением цвета
        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = Color.white;
        }
    }
}