using Boomlagoon.JSON;
using Percent.Badge;
using Percent.Http;
using UnityEngine;

namespace Percent
{
	public class PromotionData
	{
		internal static BadgeData[] badgePool = new BadgeData[0];

		internal static CrossPromotionData crossPromotionData;

		internal static JSONObject extraData;

		internal static void setData(JSONObject json)
		{
			setBadgePool(json);
			setCrossPromotionData(json);
			setInterstitialInterval(json);
			setExtraData(json);
		}

		private static void setBadgePool(JSONObject json)
		{
			if (!json.ContainsKey(PercentHttpConfig.RES_PARAM_BADGE_POOL))
			{
				return;
			}
			JSONArray array = json.GetArray(PercentHttpConfig.RES_PARAM_BADGE_POOL);
			if (!array.Length.Equals(0))
			{
				badgePool = new BadgeData[array.Length];
				for (int i = 0; i < badgePool.Length; i++)
				{
					badgePool[i].gameId = (int)array[i].Obj.GetNumber(PercentHttpConfig.RES_PARAM_GAME_ID);
					badgePool[i].iconUrl = array[i].Obj.GetString(PercentHttpConfig.RES_PARAM_ICON_URL);
					badgePool[i].storeUrl = array[i].Obj.GetString(PercentHttpConfig.RES_PARAM_STORE_URL);
				}
			}
		}

		private static void setCrossPromotionData(JSONObject json)
		{
			initCrossPromotionData();
			if (json.ContainsKey(PercentHttpConfig.RES_PARAM_CROSSPROMOTION_DATA))
			{
				JSONObject jSONObject = json.GetObject(PercentHttpConfig.RES_PARAM_CROSSPROMOTION_DATA);
				crossPromotionData.gameId = (int)jSONObject.GetNumber(PercentHttpConfig.RES_PARAM_GAME_ID);
				crossPromotionData.type = getType(jSONObject.GetString(PercentHttpConfig.RES_PARAM_CROSSPROMOTION_TYPE));
				JSONArray array = jSONObject.GetArray(PercentHttpConfig.RES_PARAM_RESOURCE_URL);
				crossPromotionData.resourceUrl = new string[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					crossPromotionData.resourceUrl[i] = array[i].Str;
				}
				crossPromotionData.storeUrl = jSONObject.GetString(PercentHttpConfig.RES_PARAM_STORE_URL);
				crossPromotionData.rId = (int)jSONObject.GetNumber(PercentHttpConfig.RES_PARAM_RID);
			}
		}

		private static void initCrossPromotionData()
		{
			crossPromotionData = default(CrossPromotionData);
			crossPromotionData.gameId = Constants.VALUE_INT_NULL;
		}

		private static PromotionType getType(string typeStr)
		{
			PromotionType result = PromotionType.IMAGE;
			switch (typeStr)
			{
			case "image":
				result = PromotionType.IMAGE;
				break;
			case "slide":
				result = PromotionType.SLIDE;
				break;
			}
			return result;
		}

		private static void setInterstitialInterval(JSONObject json)
		{
			if (json.ContainsKey(PercentHttpConfig.RES_PARAM_INTERSTITIAL_INTERVAL))
			{
				double number = json.GetNumber(PercentHttpConfig.RES_PARAM_INTERSTITIAL_INTERVAL);
				GameObject.Find("Interstitial").GetComponent<Interstitial>().Interval = (int)number;
			}
		}

		private static void setExtraData(JSONObject json)
		{
			if (json.ContainsKey(PercentHttpConfig.RES_PARAM_EXTRA_DATA))
			{
				extraData = json.GetObject(PercentHttpConfig.RES_PARAM_EXTRA_DATA);
			}
		}

		internal static bool isPromotionDataNull()
		{
			return crossPromotionData.gameId.Equals(Constants.VALUE_INT_NULL) ? true : false;
		}

		internal static bool isBadgePoolNull()
		{
			return badgePool.Length.Equals(0) ? true : false;
		}
	}
}
