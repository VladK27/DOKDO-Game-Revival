using Boomlagoon.JSON;
using Percent.Http;
using UnityEngine;
using UnityEngine.Events;

namespace Percent
{
	public class UUIDLoader : HttpsClient
	{
		internal static string uuid = string.Empty;

		private static readonly string PREF_UUID = "percentUUID";

		private readonly string VALUE_DEFAULT_ADVERTISING_ID = "00000000-0000-0000-0000-000000000000";

		public static bool isTrackingEnabled;

		public static string advertisingId;

		internal void setUUID()
		{
			if (hasUUID())
			{
				uuid = PlayerPrefs.GetString(PREF_UUID);
			}
		}

		public static bool hasUUID()
		{
			if (!PlayerPrefs.HasKey(PREF_UUID))
			{
				return false;
			}
			return true;
		}

		public static string tryGetUUID()
		{
			if (hasUUID())
			{
				return uuid;
			}
			return string.Empty;
		}

		public void request(UnityAction<bool> onReceiveUUID)
		{
			onGETResponse = onReceiveUUID;
			sendGETRequestAsync();
		}

		public void sendGETRequestAsync()
		{
			string text = HttpsClient.resolve(PercentHttpConfig.GET_REQ_LOG);
			HttpsClient.addResource(ref text, Config.GAME_ID);
			HttpsClient.addResource(ref text, PercentHttpConfig.REQ_RESOURCE_INSTALL);
			HttpsClient.addResource(ref text, Util.getPlatform());
			HttpsClient.addResource(ref text, Util.getRegion());
			if (isTrackingEnabled)
			{
				HttpsClient.addResource(ref text, advertisingId);
				onGetUrl(text);
			}
			else
			{
				HttpsClient.addResource(ref text, VALUE_DEFAULT_ADVERTISING_ID);
				onGetUrl(text);
			}
		}

		private void onGetUrl(string url)
		{
			sendGETRequest(url, true);
		}

		internal override void onGETResponseSuccess(JSONObject json)
		{
			uuid = json.GetString("uuid");
			saveUUID();
			base.onGETResponseSuccess();
		}

		private void saveUUID()
		{
			PlayerPrefs.SetString(PREF_UUID, uuid);
		}
	}
}
