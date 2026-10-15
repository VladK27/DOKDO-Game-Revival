using UnityEngine;

public class GameDataExample
{
	public static int coins
	{
		get
		{
			if (PlayerPrefs.HasKey("coins"))
			{
				return PlayerPrefs.GetInt("coins");
			}
			return 0;
		}
	}

	public static bool IsBoostPurchased
	{
		get
		{
			if (PlayerPrefs.HasKey("coins_boost"))
			{
				return true;
			}
			return false;
		}
	}

	public static void AddCoins(int amount)
	{
		if (IsBoostPurchased)
		{
			amount += Mathf.FloorToInt((float)amount * 0.2f);
		}
		PlayerPrefs.SetInt("coins", coins + amount);
	}

	public static void EnableCoinsBoost()
	{
		PlayerPrefs.SetInt("coins_boost", 1);
	}
}
