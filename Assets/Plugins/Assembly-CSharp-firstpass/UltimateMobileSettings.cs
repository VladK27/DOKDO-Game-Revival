using System.Collections.Generic;
using UnityEngine;

public class UltimateMobileSettings : ScriptableObject
{
	public const string VERSION_NUMBER = "10.1/21";

	private const string UMSettingsAssetName = "UltimateMobileSettings";

	private const string UMSettingsAssetExtension = ".asset";

	public int ToolbarSelectedIndex;

	[SerializeField]
	public List<UM_InAppProduct> InAppProducts = new List<UM_InAppProduct>();

	public UM_TransactionsHandlingMode TransactionsHandlingMode;

	public int InApps_EditorFillRateIndex = 4;

	public int InApps_EditorFillRate = 100;

	public bool Is_InApps_EditorTestingEnabled = true;

	public bool IsInAppSettingsProductsOpen = true;

	[SerializeField]
	public List<UM_Leaderboard> Leaderboards = new List<UM_Leaderboard>();

	[SerializeField]
	public List<UM_Achievement> Achievements = new List<UM_Achievement>();

	public bool AutoLoadUsersSmallImages = true;

	public bool AutoLoadUsersBigImages;

	public bool AutoLoadLeaderboardsInfo = true;

	public bool AutoLoadAchievementsInfo = true;

	public bool IsAchievementsOpen = true;

	public bool IsLeaderBoardsOpen = true;

	public UM_IOSAdEngineOprions IOSAdEdngine;

	public UM_WP8AdEngineOprions WP8AdEdngine;

	public bool IsCameraAndGallerySettingsOpen = true;

	public bool IsCameraAndGalleryAndroidSettingsOpen;

	public bool IsCameraAndGalleryIOSSettingsOpen;

	public bool IsLP_Android_SettingsOpen;

	public bool IsLP_IOS_SettingsOpen;

	public bool TP_Android_SettingsOpen;

	public bool TP_IOS_SettingsOpen;

	public UM_PlatformDependencies PlatformEngine;

	private static UltimateMobileSettings instance;

	public static UltimateMobileSettings Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Resources.Load("UltimateMobileSettings") as UltimateMobileSettings;
				if (instance == null)
				{
					instance = ScriptableObject.CreateInstance<UltimateMobileSettings>();
				}
			}
			return instance;
		}
	}

	public void AddProduct(UM_InAppProduct p)
	{
		InAppProducts.Add(p);
	}

	public void RemoveProduct(UM_InAppProduct p)
	{
		InAppProducts.Remove(p);
	}

	public UM_InAppProduct GetProductById(string id)
	{
		foreach (UM_InAppProduct inAppProduct in InAppProducts)
		{
			if (inAppProduct.id.Equals(id))
			{
				return inAppProduct;
			}
		}
		return null;
	}

	public UM_InAppProduct GetProductByIOSId(string id)
	{
		foreach (UM_InAppProduct inAppProduct in InAppProducts)
		{
			if (inAppProduct.IOSId.Equals(id))
			{
				return inAppProduct;
			}
		}
		return null;
	}

	public UM_InAppProduct GetProductByAndroidId(string id)
	{
		foreach (UM_InAppProduct inAppProduct in InAppProducts)
		{
			if (inAppProduct.AndroidId.Equals(id))
			{
				return inAppProduct;
			}
		}
		return null;
	}

	public UM_InAppProduct GetProductByAmazonId(string id)
	{
		foreach (UM_InAppProduct inAppProduct in InAppProducts)
		{
			if (inAppProduct.AmazonId.Equals(id))
			{
				return inAppProduct;
			}
		}
		return null;
	}

	public UM_InAppProduct GetProductByWp8Id(string id)
	{
		foreach (UM_InAppProduct inAppProduct in InAppProducts)
		{
			if (inAppProduct.WP8Id.Equals(id))
			{
				return inAppProduct;
			}
		}
		return null;
	}

	public void AddAchievement(UM_Achievement a)
	{
		Achievements.Add(a);
	}

	public void RemoveAchievement(UM_Achievement a)
	{
		Achievements.Remove(a);
	}

	public UM_Achievement GetAchievementById(string id)
	{
		foreach (UM_Achievement achievement in Achievements)
		{
			if (achievement.id.Equals(id))
			{
				return achievement;
			}
		}
		return null;
	}

	public UM_Achievement GetAchievementByIOSId(string id)
	{
		foreach (UM_Achievement achievement in Achievements)
		{
			if (achievement.IOSId.Equals(id))
			{
				return achievement;
			}
		}
		return null;
	}

	public UM_Achievement GetAchievementByAndroidId(string id)
	{
		foreach (UM_Achievement achievement in Achievements)
		{
			if (achievement.AndroidId.Equals(id))
			{
				return achievement;
			}
		}
		return null;
	}

	public void AddLeaderboard(UM_Leaderboard l)
	{
		Leaderboards.Add(l);
	}

	public void RemoveLeaderboard(UM_Leaderboard l)
	{
		Leaderboards.Remove(l);
	}

	public UM_Leaderboard GetLeaderboardById(string id)
	{
		foreach (UM_Leaderboard leaderboard in Leaderboards)
		{
			if (leaderboard.id.Equals(id))
			{
				return leaderboard;
			}
		}
		return null;
	}

	public UM_Leaderboard GetLeaderboardByIOSId(string id)
	{
		foreach (UM_Leaderboard leaderboard in Leaderboards)
		{
			if (leaderboard.IOSId.Equals(id))
			{
				return leaderboard;
			}
		}
		return null;
	}

	public UM_Leaderboard GetLeaderboardByAndroidId(string id)
	{
		foreach (UM_Leaderboard leaderboard in Leaderboards)
		{
			if (leaderboard.AndroidId.Equals(id))
			{
				return leaderboard;
			}
		}
		return null;
	}

	public UM_Leaderboard GetLeaderboardByAmazonId(string id)
	{
		foreach (UM_Leaderboard leaderboard in Leaderboards)
		{
			if (leaderboard.AmazonId.Equals(id))
			{
				return leaderboard;
			}
		}
		return null;
	}
}
