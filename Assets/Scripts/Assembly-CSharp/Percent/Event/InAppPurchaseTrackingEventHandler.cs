using System;
using Percent.Http;

namespace Percent.Event
{
	public class InAppPurchaseTrackingEventHandler : HttpsClient, ITrackingEventListener
	{
		private string code;

		public void onTriggerTrackingEvent<T>(T arg)
		{
			code = (string)Convert.ChangeType(arg, Type.GetType("System.String"));
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
				Logger.error("Can NOT send in-app purchase event. UUID is NOT exist.");
				return string.Empty;
			}
			string gET_REQ_LOG = PercentHttpConfig.GET_REQ_LOG;
			gET_REQ_LOG = HttpsClient.resolve(gET_REQ_LOG);
			HttpsClient.addResource(ref gET_REQ_LOG, Config.GAME_ID);
			HttpsClient.addResource(ref gET_REQ_LOG, UUIDLoader.uuid);
			HttpsClient.addResource(ref gET_REQ_LOG, PercentHttpConfig.REQ_RESOURCE_PAYMENT);
			HttpsClient.addResource(ref gET_REQ_LOG, code);
			return gET_REQ_LOG;
		}
	}
}
