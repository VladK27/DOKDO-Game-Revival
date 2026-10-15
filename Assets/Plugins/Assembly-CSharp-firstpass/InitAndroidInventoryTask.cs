using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class InitAndroidInventoryTask : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action ActionComplete__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action ActionFailed__BackingField = () =>
	{
	};

	public event Action ActionComplete
	{
		add
		{
			Action action = ActionComplete__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionComplete__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ActionComplete__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionComplete__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action ActionFailed
	{
		add
		{
			Action action = ActionFailed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionFailed__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ActionFailed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionFailed__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static InitAndroidInventoryTask Create()
	{
		return new GameObject("InitAndroidInventoryTask").AddComponent<InitAndroidInventoryTask>();
	}

	public void Run()
	{
		UnityEngine.Debug.Log("InitAndroidInventoryTask task started");
		if (AndroidInAppPurchaseManager.Client.IsConnected)
		{
			OnBillingConnected(null);
			return;
		}
		AndroidInAppPurchaseManager.ActionBillingSetupFinished += OnBillingConnected;
		if (!AndroidInAppPurchaseManager.Client.IsConnectingToServiceInProcess)
		{
			AndroidInAppPurchaseManager.Client.Connect();
		}
	}

	private void OnBillingConnected(BillingResult result)
	{
		UnityEngine.Debug.Log("OnBillingConnected");
		if (result == null)
		{
			OnBillingConnectFinished();
			return;
		}
		AndroidInAppPurchaseManager.ActionBillingSetupFinished -= OnBillingConnected;
		if (result.IsSuccess)
		{
			OnBillingConnectFinished();
			return;
		}
		UnityEngine.Debug.Log("OnBillingConnected Failed");
		ActionFailed__BackingField();
	}

	private void OnBillingConnectFinished()
	{
		UnityEngine.Debug.Log("OnBillingConnected COMPLETE");
		if (AndroidInAppPurchaseManager.Client.IsInventoryLoaded)
		{
			UnityEngine.Debug.Log("IsInventoryLoaded COMPLETE");
			ActionComplete__BackingField();
			return;
		}
		AndroidInAppPurchaseManager.ActionRetrieveProducsFinished += OnRetrieveProductsFinised;
		if (!AndroidInAppPurchaseManager.Client.IsProductRetrievingInProcess)
		{
			AndroidInAppPurchaseManager.Client.RetrieveProducDetails();
		}
	}

	private void OnRetrieveProductsFinised(BillingResult result)
	{
		UnityEngine.Debug.Log("OnRetrieveProductsFinised");
		AndroidInAppPurchaseManager.ActionRetrieveProducsFinished -= OnRetrieveProductsFinised;
		if (result.IsSuccess)
		{
			UnityEngine.Debug.Log("OnRetrieveProductsFinised COMPLETE");
			ActionComplete__BackingField();
		}
		else
		{
			UnityEngine.Debug.Log("OnRetrieveProductsFinised FAILED");
			ActionFailed__BackingField();
		}
	}
}
