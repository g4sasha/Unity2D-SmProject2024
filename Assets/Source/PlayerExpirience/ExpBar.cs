using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
	public int Level
    {
        get => _level;
        private set
        {
            _level = value;
			UpdateUI();
        }
    }

	public float Exp
	{
		get => _exp;
		private set
		{
			_exp = value;
			_foreground.fillAmount = _exp;
		}
	}

	[SerializeField] private Image _foreground;
	[SerializeField] private TMP_Text _levelField;
	[SerializeField] private string _levelFormat = "Уровень: {0}";

	private ExpBank _expBank;

    private int _level;
	private float _exp;

	private void OnDestroy()
	{
		_expBank.OnExpChanged -= ExpChange;
	}
	
	public void Construct(ExpBank expBank)
	{
		_expBank = expBank;
		_expBank.OnExpChanged += ExpChange;
		UpdateUI();
	}

	public void SetExp(float value)
	{
		_exp = value;
		UpdateUI();
	}

	public void SetLevel(int value)
	{
		Level = value;
	}

	private void ExpChange(int level, float exp)
	{
		Level = level;
		Exp = exp;
		UpdateUI();
	}

	private void UpdateUI()
	{
		_foreground.fillAmount = _exp;
		_levelField.text = string.Format(_levelFormat, _level);
	}
}