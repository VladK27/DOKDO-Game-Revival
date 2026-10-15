using UnityEngine;

namespace Percent.Badge
{
	public class BadgePoolCacher
	{
		private static string PREF_POOL_LENGTH = "percentBadgePoolLength";

		private static string PREF_BADGE_GAME_ID = "percentBadgeGameId";

		private static string PREF_BADGE_ICON_URL = "percentBadgeIconUrl";

		private static string PREF_BADGE_STORE_URL = "percentBadgeStoreUrl";

		internal static void save()
		{
			int num = PromotionData.badgePool.Length;
			PlayerPrefs.SetInt(PREF_POOL_LENGTH, num);
			for (int i = 0; i < num; i++)
			{
				PlayerPrefs.SetInt(PREF_BADGE_GAME_ID, PromotionData.badgePool[i].gameId);
				PlayerPrefs.SetString(PREF_BADGE_ICON_URL, PromotionData.badgePool[i].iconUrl);
				PlayerPrefs.SetString(PREF_BADGE_STORE_URL, PromotionData.badgePool[i].storeUrl);
			}
		}

		internal static BadgeData[] load()
		{
			int num = PlayerPrefs.GetInt(PREF_POOL_LENGTH);
			BadgeData[] array = new BadgeData[num];
			for (int i = 0; i < num; i++)
			{
				array[i].gameId = PlayerPrefs.GetInt(PREF_BADGE_GAME_ID);
				array[i].iconUrl = PlayerPrefs.GetString(PREF_BADGE_ICON_URL);
				array[i].storeUrl = PlayerPrefs.GetString(PREF_BADGE_STORE_URL);
			}
			return array;
		}
	}
}
