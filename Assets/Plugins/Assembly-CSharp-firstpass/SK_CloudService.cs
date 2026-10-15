using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using SA.IOSNative.StoreKit;
using UnityEngine;

public class SK_CloudService : Singleton<SK_CloudService>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<SK_AuthorizationResult> OnAuthorizationFinished__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<SK_RequestCapabilitieResult> OnCapabilitiesRequestFinished__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<SK_RequestStorefrontIdentifierResult> OnStorefrontIdentifierRequestFinished__BackingField = delegate
	{
	};

	public static int AuthorizationStatus
	{
		get
		{
			return BillingNativeBridge.CloudService_AuthorizationStatus();
		}
	}

	public static event Action<SK_AuthorizationResult> OnAuthorizationFinished
	{
		add
		{
			Action<SK_AuthorizationResult> action = OnAuthorizationFinished__BackingField;
			Action<SK_AuthorizationResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAuthorizationFinished__BackingField, (Action<SK_AuthorizationResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<SK_AuthorizationResult> action = OnAuthorizationFinished__BackingField;
			Action<SK_AuthorizationResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAuthorizationFinished__BackingField, (Action<SK_AuthorizationResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<SK_RequestCapabilitieResult> OnCapabilitiesRequestFinished
	{
		add
		{
			Action<SK_RequestCapabilitieResult> action = OnCapabilitiesRequestFinished__BackingField;
			Action<SK_RequestCapabilitieResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnCapabilitiesRequestFinished__BackingField, (Action<SK_RequestCapabilitieResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<SK_RequestCapabilitieResult> action = OnCapabilitiesRequestFinished__BackingField;
			Action<SK_RequestCapabilitieResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnCapabilitiesRequestFinished__BackingField, (Action<SK_RequestCapabilitieResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<SK_RequestStorefrontIdentifierResult> OnStorefrontIdentifierRequestFinished
	{
		add
		{
			Action<SK_RequestStorefrontIdentifierResult> action = OnStorefrontIdentifierRequestFinished__BackingField;
			Action<SK_RequestStorefrontIdentifierResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnStorefrontIdentifierRequestFinished__BackingField, (Action<SK_RequestStorefrontIdentifierResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<SK_RequestStorefrontIdentifierResult> action = OnStorefrontIdentifierRequestFinished__BackingField;
			Action<SK_RequestStorefrontIdentifierResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnStorefrontIdentifierRequestFinished__BackingField, (Action<SK_RequestStorefrontIdentifierResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void RequestAuthorization()
	{
		BillingNativeBridge.CloudService_RequestAuthorization();
	}

	public void RequestCapabilities()
	{
		BillingNativeBridge.CloudService_RequestCapabilities();
	}

	public void RequestStorefrontIdentifier()
	{
		BillingNativeBridge.CloudService_RequestStorefrontIdentifier();
	}

	private void Event_AuthorizationFinished(string data)
	{
		int status = Convert.ToInt32(data);
		SK_AuthorizationResult obj = new SK_AuthorizationResult((SK_CloudServiceAuthorizationStatus)status);
		OnAuthorizationFinished__BackingField(obj);
	}

	private void Event_RequestCapabilitieSsuccess(string data)
	{
		int capability = Convert.ToInt32(data);
		SK_RequestCapabilitieResult obj = new SK_RequestCapabilitieResult((SK_CloudServiceCapability)capability);
		OnCapabilitiesRequestFinished__BackingField(obj);
	}

	private void Event_RequestCapabilitiesFailed(string errorData)
	{
		SK_RequestCapabilitieResult obj = new SK_RequestCapabilitieResult(errorData);
		OnCapabilitiesRequestFinished__BackingField(obj);
	}

	private void Event_RequestStorefrontIdentifierSsuccess(string storefrontIdentifier)
	{
		SK_RequestStorefrontIdentifierResult sK_RequestStorefrontIdentifierResult = new SK_RequestStorefrontIdentifierResult();
		sK_RequestStorefrontIdentifierResult.StorefrontIdentifier = storefrontIdentifier;
		OnStorefrontIdentifierRequestFinished__BackingField(sK_RequestStorefrontIdentifierResult);
	}

	private void Event_RequestStorefrontIdentifierFailed(string errorData)
	{
		SK_RequestStorefrontIdentifierResult obj = new SK_RequestStorefrontIdentifierResult(errorData);
		OnStorefrontIdentifierRequestFinished__BackingField(obj);
	}
}
