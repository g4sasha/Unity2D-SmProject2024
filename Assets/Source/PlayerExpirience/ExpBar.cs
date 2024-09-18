using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
	[SerializeField] private Image _foreground;
	[SerializeField] private TMP_Text _levelField;
	[SerializeField] private string _levelFormat = "Уровень: {0}";

	private ExpBank _expBank;

    private int _level;
	private float _exp;

    private void Start() => UpdateUI();

    private void OnDestroy()
    {
        _expBank.OnExpChanged -= ExpChange;
		_expBank = null;
    }

    public void Construct(ExpBank expBank)
    {
        _expBank = expBank;
		_expBank.OnExpChanged += ExpChange;
    }

    public void SetExp(float value)
	{
		_exp = value;
		UpdateUI();
	}

	public void SetLevel(int value)
	{
		_level = value;
		UpdateUI();
	}

	private void ExpChange(int level, float exp)
	{
		_level = level;
		_exp = exp;
		UpdateUI();
	}

	private void UpdateUI()
	{
		_foreground.fillAmount = _exp / _expBank.MaxExp;
		_levelField.text = string.Format(_levelFormat, _level);
	}
}