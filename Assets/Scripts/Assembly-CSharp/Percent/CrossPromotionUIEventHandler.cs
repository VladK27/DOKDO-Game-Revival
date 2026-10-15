using Percent.Event;
using UnityEngine;

namespace Percent
{
	public class CrossPromotionUIEventHandler : MonoBehaviour
	{
		public CrossPromotion crossPromotion;

		public ClickTracker clickTracker;

		public void onHide()
		{
			crossPromotion.hide();
		}

		public void onImageTypeClick()
		{
			Util.goMarket(PromotionData.crossPromotionData.storeUrl);
			int gameId = PromotionData.crossPromotionData.gameId;
			clickTracker.trigger(PromotionType.IMAGE, gameId);
		}

		public void onSlideTypeClick()
		{
			Util.goMarket(PromotionData.crossPromotionData.storeUrl);
			int gameId = PromotionData.crossPromotionData.gameId;
			clickTracker.trigger(PromotionType.SLIDE, gameId);
		}
	}
}
