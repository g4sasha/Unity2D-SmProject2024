using System;

public class ExpBank
{
	public Action<int, float> OnExpChanged { get; set; }
	public float MaxExp { get; private set; }
	public int Level { get; private set; }
	public float Exp { get; private set; }

	public ExpBank(int level, float exp)
	{
		Level = level;
		Exp = exp;
		UpdateProgression(); // Initial only!
	}

    public void AddExp(float value)
    {
        Exp += value;

		if (Exp >= MaxExp)
		{
			Exp -= MaxExp;
			AddLevel(1);
		}

		if (Exp < 0f)
		{
			Exp = 0f;
			AddLevel(-1);
		}

		OnExpChanged?.Invoke(Level, Exp);
		UpdateProgression();
    }

    public void AddLevel(int level)
    {
        Level += level;
		UpdateProgression();
    }

	// Initial only!
	private void UpdateProgression()
	{
		MaxExp = Level / 2f + 1f; // Progression
	}

    internal void AddExp(object weight)
    {
        throw new NotImplementedException();
    }
}