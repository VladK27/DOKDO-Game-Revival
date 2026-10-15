using Percent.Http;
using UnityEngine.Events;

namespace Percent
{
	public class AndroidReferrerRequester : HttpsClient
	{
		public UnityEvent onMessageReceive;

		private string message;

		public void onNativeMessageReceive(string message)
		{
			this.message = message;
			onMessageReceive.Invoke();
		}

		internal void request()
		{
			sendGETRequest(getUrl());
		}

		private string getUrl()
		{
			string prefix = HttpsClient.resolve(PercentHttpConfig.GET_REQ_LOG);
			HttpsClient.addResource(ref prefix, Config.GAME_ID);
			HttpsClient.addResource(ref prefix, UUIDLoader.uuid);
			HttpsClient.addResource(ref prefix, PercentHttpConfig.REQ_RESOURCE_REFERRER);
			Parameter parameter = new Parameter(prefix);
			parameter.addParamater("referrer", message);
			return parameter.getUrl();
		}
	}
}
