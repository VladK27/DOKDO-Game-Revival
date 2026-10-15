using Percent.Http;

namespace Percent.Event
{
	public class InHouseTracker : HttpsClient
	{
		internal virtual void trigger(PromotionType type, int gameId)
		{
			string value = ((!type.Equals(PromotionType.BADGE)) ? addParamater(getUrl(type, gameId)) : getUrl(type, gameId));
			if (!string.IsNullOrEmpty(value))
			{
				sendGETRequest(value);
			}
		}

		protected virtual string getUrl(PromotionType type, int gameId)
		{
			return string.Empty;
		}

		protected virtual string addParamater(string url)
		{
			return url;
		}

		protected string getResourceOf(PromotionType type)
		{
			string result = string.Empty;
			switch (type)
			{
			case PromotionType.IMAGE:
				result = PercentHttpConfig.REQ_RESOURCE_PROMOTION_TYPE_IMAGE;
				break;
			case PromotionType.SLIDE:
				result = PercentHttpConfig.REQ_RESOURCE_PROMOTION_TYPE_SLIDE;
				break;
			case PromotionType.VIDEO:
				result = PercentHttpConfig.REQ_RESOURCE_PROMOTION_TYPE_VIDEO;
				break;
			case PromotionType.PLAYABLE:
				result = PercentHttpConfig.REQ_RESOURCE_PROMOTION_TYPE_PLAYABLE;
				break;
			case PromotionType.BADGE:
				result = PercentHttpConfig.REQ_RESOURCE_PROMOTION_TYPE_BADGE;
				break;
			}
			return result;
		}
	}
}
