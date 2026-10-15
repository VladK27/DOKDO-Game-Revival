using System;
using Percent.Http;

namespace Percent.Event
{
	public class WatchAdsTrackingEventHandler : HttpsClient, ITrackingEventListener
	{
		private string adType;

		public void onTriggerTrackingEvent<T>(T arg)
		{
			adType = (string)Convert.ChangeType(arg, Type.GetType("System.String"));
			string value = getUrl();
			if (!string.IsNullOrEmpty(value))
			{
				sendGETRequest(value);
			}
		}

		internal string getUrl()
		{
			if (!UUIDLoader.hasUUID())
			{
				Logger.error("Can NOT send watchAds event. UUID is NOT exist.");
				return string.Empty;
			}
			string gET_REQ_LOG = PercentHttpConfig.GET_REQ_LOG;
			gET_REQ_LOG = HttpsClient.resolve(gET_REQ_LOG);
			HttpsClient.addResource(ref gET_REQ_LOG, Config.GAME_ID);
			HttpsClient.addResource(ref gET_REQ_LOG, UUIDLoader.uuid);
			HttpsClient.addResource(ref gET_REQ_LOG, PercentHttpConfig.REQ_RESOURCE_AD);
			HttpsClient.addResource(ref gET_REQ_LOG, adType);
			return gET_REQ_LOG;
		}
	}
}
