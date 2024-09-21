using System;
using UnityEngine;
using UnityEngine.UI;

namespace New
{
	public class ProgressBar : MonoBehaviour
	{
		public event Action<float> OnValueChanged;

		[SerializeField] private Image _foreground;

		public float Value
		{
			get => _value;
			set
			{
				_value = Mathf.Clamp(value, 0f, 1f);
				_foreground.fillAmount = _value;
				OnValueChanged?.Invoke(_value);
			}
		}

		private float _value;

		private void OnValidate()
		{
			if (!_foreground)
			{
				return;
			}

			_foreground.fillAmount = _value;
		}
    }
}