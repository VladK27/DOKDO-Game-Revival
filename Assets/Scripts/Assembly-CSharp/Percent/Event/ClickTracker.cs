using Percent.Http;

namespace Percent.Event
{
	public class ClickTracker : InHouseTracker
	{
		protected override string getUrl(PromotionType type, int clickedGameId)
		{
			if (!UUIDLoader.hasUUID())
			{
				Logger.error("Can NOT send click event. UUID is NOT exist.");
				return string.Empty;
			}
			string result = HttpsClient.resolve(PercentHttpConfig.GET_REQ_LOG);
			HttpsClient.addResource(ref result, Config.GAME_ID);
			HttpsClient.addResource(ref result, UUIDLoader.uuid);
			HttpsClient.addResource(ref result, PercentHttpConfig.REQ_RESOURCE_CLICK);
			HttpsClient.addResource(ref result, getResourceOf(type));
			HttpsClient.addResource(ref result, clickedGameId);
			return result;
		}

		protected override string addParamater(string url)
		{
			if (string.IsNullOrEmpty(url))
			{
				return string.Empty;
			}
			Parameter parameter = new Parameter(url);
			parameter.addParamater(PercentHttpConfig.REQ_RESOURCE_ID, PromotionData.crossPromotionData.rId);
			return parameter.getUrl();
		}
	}
}
