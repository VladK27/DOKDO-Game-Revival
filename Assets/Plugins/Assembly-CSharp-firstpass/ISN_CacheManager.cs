using System;
using UnityEngine;

public class ISN_CacheManager : MonoBehaviour
{
	private const string DATA_SPLITTER = "|";

	private const string ACHIEVEMENT_SPLITTER = "&";

	private const string GA_DATA_CACHE_KEY = "ISN_Cache";

	public static string SavedData
	{
		get
		{
			if (PlayerPrefs.HasKey("ISN_Cache"))
			{
				return PlayerPrefs.GetString("ISN_Cache");
			}
			return string.Empty;
		}
		set
		{
			PlayerPrefs.SetString("ISN_Cache", value);
		}
	}

	public static void SaveAchievementRequest(string achievementId, float percent)
	{
		if (IOSNativeSettings.Instance.UseGCRequestCaching)
		{
			string savedData = SavedData;
			string text = achievementId + "&" + percent;
			savedData = ((!(savedData != string.Empty)) ? text : (savedData + "|" + text));
			SavedData = savedData;
		}
	}

	public static void SendAchievementCachedRequest()
	{
		string savedData = SavedData;
		if (savedData != string.Empty)
		{
			string[] array = savedData.Split("|"[0]);
			string[] array2 = array;
			foreach (string text in array2)
			{
				string[] array3 = text.Split("&"[0]);
				GameCenterManager.SubmitAchievementNoCache(Convert.ToSingle(array3[1]), array3[0]);
			}
		}
		Clear();
	}

	public static void Clear()
	{
		PlayerPrefs.DeleteKey("ISN_Cache");
	}
}
