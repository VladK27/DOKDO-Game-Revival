using UnityEngine;

namespace Percent.Event
{
	public class PercentTracker : MonoBehaviour
	{
		public TrackerEventType type;

		public string inappCode;

		public void trigger()
		{
			if (UUIDLoader.hasUUID())
			{
				TrackingEventBridge.triggerTrackingEvent(this);
			}
			else
			{
				Logger.warning("Can NOT trigger Event. UUID is NOT exist");
			}
		}

		public void triggerInAppPurchase(string inappCode)
		{
			this.inappCode = inappCode;
			type = TrackerEventType.IN_APP_PURCHASE;
			trigger();
		}

		public void triggerSeeAdsInterstitial()
		{
			type = TrackerEventType.SEE_ADS_INTERSTITIAL;
			trigger();
		}

		public void triggerSeeAdsReward()
		{
			type = TrackerEventType.SEE_ADS_REWARD;
			trigger();
		}
	}
}
