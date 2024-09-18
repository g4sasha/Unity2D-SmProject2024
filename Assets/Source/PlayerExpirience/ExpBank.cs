using System;

public class ExpBank
{
	public Action<int, float> OnExpChanged { get; set; }
	public int Level { get; private set; }
	public float Exp { get; private set; }

	public ExpBank(int level, float exp)
	{
		Level = level;
		Exp = exp;
	}

    public void AddExp(float value)
    {
        Exp += value;

		if (Exp >= 1f)
		{
			Exp -= 1f;
			AddLevel(1);
		}

		if (Exp < 0f)
		{
			Exp = 0f;
			AddLevel(-1);
		}

		OnExpChanged?.Invoke(Level, Exp);
    }

    public void AddLevel(int level) => Level += level;
}