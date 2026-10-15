using Boomlagoon.JSON;
using Percent.Http;
using UnityEngine.Events;

namespace Percent
{
	public class AccessDataLoader : HttpsClient
	{
		private UnityAction<bool> onReceiveAccess;

		internal void request(UnityAction<bool> onReceiveAccess)
		{
			this.onReceiveAccess = onReceiveAccess;
			sendGETRequest(getUrl(), true);
		}

		private string getUrl()
		{
			string result = HttpsClient.resolve(PercentHttpConfig.GET_REQ_LOG);
			HttpsClient.addResource(ref result, Config.GAME_ID);
			HttpsClient.addResource(ref result, UUIDLoader.uuid);
			HttpsClient.addResource(ref result, PercentHttpConfig.REQ_RESOURCE_ACCESS);
			return result;
		}

		internal override void onGETResponseSuccess(JSONObject json)
		{
			PromotionData.setData(json);
			base.onGETResponseSuccess(json);
			if (!PromotionData.isPromotionDataNull())
			{
				if (onReceiveAccess != null)
				{
					onReceiveAccess(true);
				}
			}
			else if (onReceiveAccess != null)
			{
				onReceiveAccess(false);
			}
		}

		internal override void onGETResponseFail()
		{
			base.onGETResponseFail();
			if (onReceiveAccess != null)
			{
				onReceiveAccess(false);
			}
		}
	}
}
