using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class VignetteEffect : MonoBehaviour
{
	[SerializeField] private Image _vignette;

	private void OnValidate()
	{
		_vignette = GetComponent<Image>();
	}

	private void Awake()
	{
		_vignette.enabled = false;
	}

	public void Activate()
	{
		_vignette.enabled = true;
	}

	public void Deactivate()
	{
		_vignette.enabled = false;
	}
}