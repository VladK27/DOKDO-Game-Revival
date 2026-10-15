using System;
using SA.Common.Pattern;
using UnityEngine;

public class UM_Score
{
	private UM_Player player;

	private GK_Score _GK_Score;

	private GPScore _GP_Score;

	private GC_Score _GC_Score;

	public bool IsValid
	{
		get
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
				{
					return _GC_Score != null;
				}
				return _GP_Score != null;
			case RuntimePlatform.IPhonePlayer:
				return _GK_Score != null;
			default:
				return true;
			}
		}
	}

	public int Rank
	{
		get
		{
			if (IsValid)
			{
				switch (Application.platform)
				{
				case RuntimePlatform.Android:
					if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
					{
						return _GC_Score.Rank;
					}
					return _GP_Score.Rank;
				case RuntimePlatform.IPhonePlayer:
					return _GK_Score.Rank;
				}
			}
			return -1;
		}
	}

	public long LongScore
	{
		get
		{
			if (IsValid)
			{
				switch (Application.platform)
				{
				case RuntimePlatform.Android:
					if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
					{
						return _GC_Score.Score;
					}
					return _GP_Score.LongScore;
				case RuntimePlatform.IPhonePlayer:
					return _GK_Score.LongScore;
				}
			}
			return 0L;
		}
	}

	public float CurrencyScore
	{
		get
		{
			if (IsValid)
			{
				switch (Application.platform)
				{
				case RuntimePlatform.Android:
					if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
					{
						return _GC_Score.CurrencyScore;
					}
					return _GP_Score.CurrencyScore;
				case RuntimePlatform.IPhonePlayer:
					return _GK_Score.CurrencyScore;
				}
			}
			return 0f;
		}
	}

	public TimeSpan TimeScore
	{
		get
		{
			if (IsValid)
			{
				switch (Application.platform)
				{
				case RuntimePlatform.Android:
					if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
					{
						return _GC_Score.TimeScore;
					}
					return _GP_Score.TimeScore;
				case RuntimePlatform.IPhonePlayer:
					return _GK_Score.Milliseconds;
				}
			}
			return System.TimeSpan.FromMilliseconds(0.0);
		}
	}

	public string LeaderboardId
	{
		get
		{
			if (IsValid)
			{
				switch (Application.platform)
				{
				case RuntimePlatform.Android:
					if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
					{
						return _GC_Score.LeaderboardId;
					}
					return _GP_Score.LeaderboardId;
				case RuntimePlatform.IPhonePlayer:
					return _GK_Score.LeaderboardId;
				}
			}
			return string.Empty;
		}
	}

	public UM_TimeSpan TimeSpan
	{
		get
		{
			if (IsValid)
			{
				switch (Application.platform)
				{
				case RuntimePlatform.Android:
					if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
					{
						return _GC_Score.TimeSpan.GetUMScore();
					}
					return _GP_Score.TimeSpan.Get_UM_TimeSpan();
				case RuntimePlatform.IPhonePlayer:
					return _GK_Score.TimeSpan.Get_UM_TimeSpan();
				}
			}
			return UM_TimeSpan.ALL_TIME;
		}
	}

	public UM_CollectionType Collection
	{
		get
		{
			if (IsValid)
			{
				switch (Application.platform)
				{
				case RuntimePlatform.Android:
					if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
					{
						return UM_CollectionType.GLOBAL;
					}
					return _GP_Score.Collection.Get_UM_Collection();
				case RuntimePlatform.IPhonePlayer:
					return _GK_Score.Collection.Get_UM_Collection();
				}
			}
			return UM_CollectionType.GLOBAL;
		}
	}

	public UM_Player Player
	{
		get
		{
			return player;
		}
	}

	public GK_Score GameCenterScore
	{
		get
		{
			return _GK_Score;
		}
	}

	public GPScore GooglePlayScore
	{
		get
		{
			return _GP_Score;
		}
	}

	public GC_Score GameCircleScore
	{
		get
		{
			return _GC_Score;
		}
	}

	public UM_Score(GK_Score gkScore, GPScore gpScore, GC_Score gcScore)
	{
		_GK_Score = gkScore;
		_GP_Score = gpScore;
		_GC_Score = gcScore;
		if (!IsValid)
		{
			return;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
			{
				GC_Player playerById2 = AMN_Singleton<SA_AmazonGameCircleManager>.Instance.GetPlayerById(_GC_Score.PlayerId);
				player = new UM_Player(null, null, playerById2);
			}
			else
			{
				GooglePlayerTemplate playerById3 = Singleton<GooglePlayManager>.Instance.GetPlayerById(_GP_Score.PlayerId);
				player = new UM_Player(null, playerById3, null);
			}
			break;
		case RuntimePlatform.IPhonePlayer:
		{
			GK_Player playerById = GameCenterManager.GetPlayerById(_GK_Score.PlayerId);
			player = new UM_Player(playerById, null, null);
			break;
		}
		}
	}
}
