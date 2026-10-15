using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public static class AndroidInAppPurchaseManager
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<BillingResult> ActionProductPurchased__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<BillingResult> ActionProductConsumed__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<BillingResult> ActionBillingSetupFinished__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<BillingResult> ActionRetrieveProducsFinished__BackingField = delegate
	{
	};

	public static AN_InAppClient _Client = null;

	public static AN_InAppClient Client
	{
		get
		{
			if (_Client == null)
			{
				GameObject gameObject = new GameObject("AndroidInAppPurchaseManager");
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
				if (Application.isEditor && AndroidNativeSettings.Instance.Is_InApps_EditorTestingEnabled)
				{
					_Client = gameObject.AddComponent<AN_InApp_EditorClient>();
				}
				if (_Client == null)
				{
					_Client = gameObject.AddComponent<AN_InAppAndroidClient>();
				}
				_Client.ActionBillingSetupFinished += HandleActionBillingSetupFinished;
				_Client.ActionProductConsumed += HandleActionProductConsumed;
				_Client.ActionProductPurchased += HandleActionProductPurchased;
				_Client.ActionRetrieveProducsFinished += HandleActionRetrieveProducsFinished;
			}
			return _Client;
		}
	}

	[Obsolete("Instance is deprectaed, please use Client instead")]
	public static AN_InAppClient Instance
	{
		get
		{
			return Client;
		}
	}

	public static event Action<BillingResult> ActionProductPurchased
	{
		add
		{
			Action<BillingResult> action = ActionProductPurchased__BackingField;
			Action<BillingResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionProductPurchased__BackingField, (Action<BillingResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<BillingResult> action = ActionProductPurchased__BackingField;
			Action<BillingResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionProductPurchased__BackingField, (Action<BillingResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<BillingResult> ActionProductConsumed
	{
		add
		{
			Action<BillingResult> action = ActionProductConsumed__BackingField;
			Action<BillingResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionProductConsumed__BackingField, (Action<BillingResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<BillingResult> action = ActionProductConsumed__BackingField;
			Action<BillingResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionProductConsumed__BackingField, (Action<BillingResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<BillingResult> ActionBillingSetupFinished
	{
		add
		{
			Action<BillingResult> action = ActionBillingSetupFinished__BackingField;
			Action<BillingResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionBillingSetupFinished__BackingField, (Action<BillingResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<BillingResult> action = ActionBillingSetupFinished__BackingField;
			Action<BillingResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionBillingSetupFinished__BackingField, (Action<BillingResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<BillingResult> ActionRetrieveProducsFinished
	{
		add
		{
			Action<BillingResult> action = ActionRetrieveProducsFinished__BackingField;
			Action<BillingResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRetrieveProducsFinished__BackingField, (Action<BillingResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<BillingResult> action = ActionRetrieveProducsFinished__BackingField;
			Action<BillingResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRetrieveProducsFinished__BackingField, (Action<BillingResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private static void HandleActionRetrieveProducsFinished(BillingResult res)
	{
		ActionRetrieveProducsFinished__BackingField(res);
	}

	private static void HandleActionProductPurchased(BillingResult res)
	{
		ActionProductPurchased__BackingField(res);
	}

	private static void HandleActionProductConsumed(BillingResult res)
	{
		ActionProductConsumed__BackingField(res);
	}

	private static void HandleActionBillingSetupFinished(BillingResult res)
	{
		ActionBillingSetupFinished__BackingField(res);
	}
}
