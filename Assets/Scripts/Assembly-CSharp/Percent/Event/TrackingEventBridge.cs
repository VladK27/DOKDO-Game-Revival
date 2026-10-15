using UnityEngine;

namespace Percent.Event
{
	public class TrackingEventBridge : MonoBehaviour
	{
		private static ITrackingEventListener[] handlers;

		private void Awake()
		{
			handlers = new ITrackingEventListener[2];
			handlers[0] = base.gameObject.AddComponent<InAppPurchaseTrackingEventHandler>();
			handlers[1] = base.gameObject.AddComponent<WatchAdsTrackingEventHandler>();
		}

		public static void triggerTrackingEvent(PercentTracker tracker)
		{
			switch (tracker.type)
			{
			case TrackerEventType.IN_APP_PURCHASE:
			{
				string inappCode = tracker.inappCode;
				handlers[0].onTriggerTrackingEvent(inappCode);
				break;
			}
			case TrackerEventType.SEE_ADS_INTERSTITIAL:
				handlers[1].onTriggerTrackingEvent("i");
				break;
			case TrackerEventType.SEE_ADS_REWARD:
				handlers[1].onTriggerTrackingEvent("r");
				break;
			}
		}
	}
}
