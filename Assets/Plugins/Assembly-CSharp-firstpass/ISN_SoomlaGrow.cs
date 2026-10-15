using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Models;
using SA.Common.Pattern;
using SA.IOSNative.StoreKit;
using UnityEngine;

public class ISN_SoomlaGrow : Singleton<ISN_SoomlaGrow>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action ActionInitialized__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action ActionConnected__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action ActionDisconnected__BackingField = () =>
	{
	};

	private static bool _IsInitialized = false;

	public static bool IsInitialized
	{
		get
		{
			return _IsInitialized;
		}
	}

	public static event Action ActionInitialized
	{
		add
		{
			Action action = ActionInitialized__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionInitialized__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ActionInitialized__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionInitialized__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action ActionConnected
	{
		add
		{
			Action action = ActionConnected__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionConnected__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ActionConnected__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionConnected__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action ActionDisconnected
	{
		add
		{
			Action action = ActionDisconnected__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionDisconnected__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ActionDisconnected__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionDisconnected__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public void CreateObject()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public static void Init()
	{
	}

	private static void HandleOnVerificationComplete(VerificationResponse res)
	{
		if (res.Status != 0)
		{
			VerificationFailed();
		}
	}

	private static void HandleOnRestoreComplete(RestoreResult res)
	{
		RestoreFinished(res.IsSucceeded);
	}

	private static void HandleOnRestoreStarted()
	{
		RestoreStarted();
	}

	private static void HandleOnTransactionStarted(string prodcutId)
	{
		PurchaseStarted(prodcutId);
	}

	private static void HandleOnTransactionComplete(PurchaseResult res)
	{
		switch (res.State)
		{
		case PurchaseState.Purchased:
		{
			Product productById = Singleton<PaymentManager>.Instance.GetProductById(res.ProductIdentifier);
			if (productById != null)
			{
				PurchaseFinished(productById.Id, productById.PriceInMicros.ToString(), productById.CurrencyCode);
			}
			break;
		}
		case PurchaseState.Failed:
			if (res.Error.Code == 2)
			{
				PurchaseCanceled(res.ProductIdentifier);
			}
			else
			{
				PurchaseError();
			}
			break;
		}
	}

	public static void SocialAction(ISN_SoomlaEvent soomlaEvent, ISN_SoomlaAction action, ISN_SoomlaProvider provider)
	{
	}

	private static void PurchaseStarted(string prodcutId)
	{
	}

	private static void PurchaseFinished(string prodcutId, string priceInMicros, string currency)
	{
	}

	private static void PurchaseCanceled(string prodcutId)
	{
	}

	public static void SetPurchasesSupportedState(bool isSupported)
	{
	}

	private static void PurchaseError()
	{
	}

	private static void VerificationFailed()
	{
	}

	private static void RestoreStarted()
	{
	}

	private static void RestoreFinished(bool state)
	{
	}

	private void OnHighWayInitialized()
	{
		ActionInitialized__BackingField();
	}

	private void OnHihgWayConnected()
	{
		ActionConnected__BackingField();
	}

	private void OnHihgWayDisconnected()
	{
		ActionDisconnected__BackingField();
	}

	private static void HandleOnInstagramPostResult(Result res)
	{
		if (res.IsSucceeded)
		{
			SocialAction(ISN_SoomlaEvent.FINISHED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.INSTAGRAM);
		}
		else
		{
			SocialAction(ISN_SoomlaEvent.FAILED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.INSTAGRAM);
		}
	}

	private static void HandleOnTwitterPostResult(Result res)
	{
		if (res.IsSucceeded)
		{
			SocialAction(ISN_SoomlaEvent.FINISHED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.TWITTER);
		}
		else
		{
			SocialAction(ISN_SoomlaEvent.FAILED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.TWITTER);
		}
	}

	private static void HandleOnInstagramPostStart()
	{
		SocialAction(ISN_SoomlaEvent.STARTED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.INSTAGRAM);
	}

	private static void HandleOnTwitterPostStart()
	{
		SocialAction(ISN_SoomlaEvent.STARTED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.TWITTER);
	}

	private static void HandleOnFacebookPostStart()
	{
		SocialAction(ISN_SoomlaEvent.STARTED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.FACEBOOK);
	}

	private static void HandleOnFacebookPostResult(Result res)
	{
		if (res.IsSucceeded)
		{
			SocialAction(ISN_SoomlaEvent.FINISHED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.FACEBOOK);
		}
		else
		{
			SocialAction(ISN_SoomlaEvent.CANCELLED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.FACEBOOK);
		}
	}
}
