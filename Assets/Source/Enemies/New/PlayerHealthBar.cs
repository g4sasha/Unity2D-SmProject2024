namespace New
{
	public class PlayerHealthBar : ProgressBar
	{
		private UnitHealth _unitHealth;

		public void Connect(UnitHealth unitHealth)
		{
			_unitHealth = unitHealth;
			_unitHealth.OnHealthChanged += UpdateValue;
		}

		private void Start()
		{
			UpdateValue(_unitHealth.Health);
		}

		private void OnDestroy()
		{
			_unitHealth.OnHealthChanged -= UpdateValue;
		}

        private void UpdateValue(float value)
        {
            Value = value / _unitHealth.MaxHealth;
        }
    }
}