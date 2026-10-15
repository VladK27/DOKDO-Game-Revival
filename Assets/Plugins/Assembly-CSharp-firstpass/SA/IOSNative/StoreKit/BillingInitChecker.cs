using SA.Common.Models;
using SA.Common.Pattern;

namespace SA.IOSNative.StoreKit
{
	public class BillingInitChecker
	{
		public delegate void BillingInitListener();

		private BillingInitListener _listener;

		public BillingInitChecker(BillingInitListener listener)
		{
			_listener = listener;
			if (Singleton<PaymentManager>.Instance.IsStoreLoaded)
			{
				_listener();
				return;
			}
			PaymentManager.OnStoreKitInitComplete += HandleOnStoreKitInitComplete;
			if (!Singleton<PaymentManager>.Instance.IsWaitingLoadResult)
			{
				Singleton<PaymentManager>.Instance.LoadStore();
			}
		}

		private void HandleOnStoreKitInitComplete(Result obj)
		{
			PaymentManager.OnStoreKitInitComplete -= HandleOnStoreKitInitComplete;
			_listener();
		}
	}
}
