using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using IronSourceJSON;
using UnityEngine;

public class IronSourceEvents : MonoBehaviour
{
	private const string ERROR_CODE = "error_code";

	private const string ERROR_DESCRIPTION = "error_description";

	private const string INSTANCE_ID_KEY = "instanceId";

	private const string PLACEMENT_KEY = "placement";

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<IronSourceError> _onRewardedVideoAdShowFailedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action _onRewardedVideoAdOpenedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action _onRewardedVideoAdClosedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action _onRewardedVideoAdStartedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action _onRewardedVideoAdEndedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<IronSourcePlacement> _onRewardedVideoAdRewardedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<IronSourcePlacement> _onRewardedVideoAdClickedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<bool> _onRewardedVideoAvailabilityChangedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string, bool> _onRewardedVideoAvailabilityChangedDemandOnlyEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string> _onRewardedVideoAdOpenedDemandOnlyEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string> _onRewardedVideoAdClosedDemandOnlyEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string, IronSourcePlacement> _onRewardedVideoAdRewardedDemandOnlyEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string, IronSourceError> _onRewardedVideoAdShowFailedDemandOnlyEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string, IronSourcePlacement> _onRewardedVideoAdClickedDemandOnlyEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action _onInterstitialAdReadyEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<IronSourceError> _onInterstitialAdLoadFailedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action _onInterstitialAdOpenedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action _onInterstitialAdClosedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action _onInterstitialAdShowSucceededEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<IronSourceError> _onInterstitialAdShowFailedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action _onInterstitialAdClickedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string> _onInterstitialAdReadyDemandOnlyEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string, IronSourceError> _onInterstitialAdLoadFailedDemandOnlyEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string> _onInterstitialAdOpenedDemandOnlyEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string> _onInterstitialAdClosedDemandOnlyEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string> _onInterstitialAdShowSucceededDemandOnlyEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string, IronSourceError> _onInterstitialAdShowFailedDemandOnlyEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string> _onInterstitialAdClickedDemandOnlyEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action _onInterstitialAdRewardedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action _onOfferwallOpenedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<IronSourceError> _onOfferwallShowFailedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action _onOfferwallClosedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<IronSourceError> _onGetOfferwallCreditsFailedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<Dictionary<string, object>> _onOfferwallAdCreditedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<bool> _onOfferwallAvailableEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action _onBannerAdLoadedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<IronSourceError> _onBannerAdLoadFailedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action _onBannerAdClickedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action _onBannerAdScreenPresentedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action _onBannerAdScreenDismissedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action _onBannerAdLeftApplicationEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string> _onSegmentReceivedEvent__BackingField;

	private static event Action<IronSourceError> _onRewardedVideoAdShowFailedEvent
	{
		add
		{
			Action<IronSourceError> action = _onRewardedVideoAdShowFailedEvent__BackingField;
			Action<IronSourceError> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdShowFailedEvent__BackingField, (Action<IronSourceError>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<IronSourceError> action = _onRewardedVideoAdShowFailedEvent__BackingField;
			Action<IronSourceError> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdShowFailedEvent__BackingField, (Action<IronSourceError>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<IronSourceError> onRewardedVideoAdShowFailedEvent
	{
		add
		{
			if (_onRewardedVideoAdShowFailedEvent__BackingField == null || !_onRewardedVideoAdShowFailedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdShowFailedEvent += value;
			}
		}
		remove
		{
			if (_onRewardedVideoAdShowFailedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdShowFailedEvent -= value;
			}
		}
	}

	private static event Action _onRewardedVideoAdOpenedEvent
	{
		add
		{
			Action action = _onRewardedVideoAdOpenedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdOpenedEvent__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = _onRewardedVideoAdOpenedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdOpenedEvent__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action onRewardedVideoAdOpenedEvent
	{
		add
		{
			if (_onRewardedVideoAdOpenedEvent__BackingField == null || !_onRewardedVideoAdOpenedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdOpenedEvent += value;
			}
		}
		remove
		{
			if (_onRewardedVideoAdOpenedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdOpenedEvent -= value;
			}
		}
	}

	private static event Action _onRewardedVideoAdClosedEvent
	{
		add
		{
			Action action = _onRewardedVideoAdClosedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdClosedEvent__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = _onRewardedVideoAdClosedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdClosedEvent__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action onRewardedVideoAdClosedEvent
	{
		add
		{
			if (_onRewardedVideoAdClosedEvent__BackingField == null || !_onRewardedVideoAdClosedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdClosedEvent += value;
			}
		}
		remove
		{
			if (_onRewardedVideoAdClosedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdClosedEvent -= value;
			}
		}
	}

	private static event Action _onRewardedVideoAdStartedEvent
	{
		add
		{
			Action action = _onRewardedVideoAdStartedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdStartedEvent__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = _onRewardedVideoAdStartedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdStartedEvent__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action onRewardedVideoAdStartedEvent
	{
		add
		{
			if (_onRewardedVideoAdStartedEvent__BackingField == null || !_onRewardedVideoAdStartedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdStartedEvent += value;
			}
		}
		remove
		{
			if (_onRewardedVideoAdStartedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdStartedEvent -= value;
			}
		}
	}

	private static event Action _onRewardedVideoAdEndedEvent
	{
		add
		{
			Action action = _onRewardedVideoAdEndedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdEndedEvent__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = _onRewardedVideoAdEndedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdEndedEvent__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action onRewardedVideoAdEndedEvent
	{
		add
		{
			if (_onRewardedVideoAdEndedEvent__BackingField == null || !_onRewardedVideoAdEndedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdEndedEvent += value;
			}
		}
		remove
		{
			if (_onRewardedVideoAdEndedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdEndedEvent -= value;
			}
		}
	}

	private static event Action<IronSourcePlacement> _onRewardedVideoAdRewardedEvent
	{
		add
		{
			Action<IronSourcePlacement> action = _onRewardedVideoAdRewardedEvent__BackingField;
			Action<IronSourcePlacement> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdRewardedEvent__BackingField, (Action<IronSourcePlacement>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<IronSourcePlacement> action = _onRewardedVideoAdRewardedEvent__BackingField;
			Action<IronSourcePlacement> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdRewardedEvent__BackingField, (Action<IronSourcePlacement>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<IronSourcePlacement> onRewardedVideoAdRewardedEvent
	{
		add
		{
			if (_onRewardedVideoAdRewardedEvent__BackingField == null || !_onRewardedVideoAdRewardedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdRewardedEvent += value;
			}
		}
		remove
		{
			if (_onRewardedVideoAdRewardedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdRewardedEvent -= value;
			}
		}
	}

	private static event Action<IronSourcePlacement> _onRewardedVideoAdClickedEvent
	{
		add
		{
			Action<IronSourcePlacement> action = _onRewardedVideoAdClickedEvent__BackingField;
			Action<IronSourcePlacement> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdClickedEvent__BackingField, (Action<IronSourcePlacement>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<IronSourcePlacement> action = _onRewardedVideoAdClickedEvent__BackingField;
			Action<IronSourcePlacement> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdClickedEvent__BackingField, (Action<IronSourcePlacement>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<IronSourcePlacement> onRewardedVideoAdClickedEvent
	{
		add
		{
			if (_onRewardedVideoAdClickedEvent__BackingField == null || !_onRewardedVideoAdClickedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdClickedEvent += value;
			}
		}
		remove
		{
			if (_onRewardedVideoAdClickedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdClickedEvent -= value;
			}
		}
	}

	private static event Action<bool> _onRewardedVideoAvailabilityChangedEvent
	{
		add
		{
			Action<bool> action = _onRewardedVideoAvailabilityChangedEvent__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAvailabilityChangedEvent__BackingField, (Action<bool>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<bool> action = _onRewardedVideoAvailabilityChangedEvent__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAvailabilityChangedEvent__BackingField, (Action<bool>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<bool> onRewardedVideoAvailabilityChangedEvent
	{
		add
		{
			if (_onRewardedVideoAvailabilityChangedEvent__BackingField == null || !_onRewardedVideoAvailabilityChangedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAvailabilityChangedEvent += value;
			}
		}
		remove
		{
			if (_onRewardedVideoAvailabilityChangedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAvailabilityChangedEvent -= value;
			}
		}
	}

	private static event Action<string, bool> _onRewardedVideoAvailabilityChangedDemandOnlyEvent
	{
		add
		{
			Action<string, bool> action = _onRewardedVideoAvailabilityChangedDemandOnlyEvent__BackingField;
			Action<string, bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAvailabilityChangedDemandOnlyEvent__BackingField, (Action<string, bool>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string, bool> action = _onRewardedVideoAvailabilityChangedDemandOnlyEvent__BackingField;
			Action<string, bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAvailabilityChangedDemandOnlyEvent__BackingField, (Action<string, bool>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string, bool> onRewardedVideoAvailabilityChangedDemandOnlyEvent
	{
		add
		{
			if (_onRewardedVideoAvailabilityChangedDemandOnlyEvent__BackingField == null || !_onRewardedVideoAvailabilityChangedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAvailabilityChangedDemandOnlyEvent += value;
			}
		}
		remove
		{
			if (_onRewardedVideoAvailabilityChangedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAvailabilityChangedDemandOnlyEvent -= value;
			}
		}
	}

	private static event Action<string> _onRewardedVideoAdOpenedDemandOnlyEvent
	{
		add
		{
			Action<string> action = _onRewardedVideoAdOpenedDemandOnlyEvent__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdOpenedDemandOnlyEvent__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = _onRewardedVideoAdOpenedDemandOnlyEvent__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdOpenedDemandOnlyEvent__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string> onRewardedVideoAdOpenedDemandOnlyEvent
	{
		add
		{
			if (_onRewardedVideoAdOpenedDemandOnlyEvent__BackingField == null || !_onRewardedVideoAdOpenedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdOpenedDemandOnlyEvent += value;
			}
		}
		remove
		{
			if (_onRewardedVideoAdOpenedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdOpenedDemandOnlyEvent -= value;
			}
		}
	}

	private static event Action<string> _onRewardedVideoAdClosedDemandOnlyEvent
	{
		add
		{
			Action<string> action = _onRewardedVideoAdClosedDemandOnlyEvent__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdClosedDemandOnlyEvent__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = _onRewardedVideoAdClosedDemandOnlyEvent__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdClosedDemandOnlyEvent__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string> onRewardedVideoAdClosedDemandOnlyEvent
	{
		add
		{
			if (_onRewardedVideoAdClosedDemandOnlyEvent__BackingField == null || !_onRewardedVideoAdClosedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdClosedDemandOnlyEvent += value;
			}
		}
		remove
		{
			if (_onRewardedVideoAdClosedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdClosedDemandOnlyEvent -= value;
			}
		}
	}

	private static event Action<string, IronSourcePlacement> _onRewardedVideoAdRewardedDemandOnlyEvent
	{
		add
		{
			Action<string, IronSourcePlacement> action = _onRewardedVideoAdRewardedDemandOnlyEvent__BackingField;
			Action<string, IronSourcePlacement> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdRewardedDemandOnlyEvent__BackingField, (Action<string, IronSourcePlacement>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string, IronSourcePlacement> action = _onRewardedVideoAdRewardedDemandOnlyEvent__BackingField;
			Action<string, IronSourcePlacement> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdRewardedDemandOnlyEvent__BackingField, (Action<string, IronSourcePlacement>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string, IronSourcePlacement> onRewardedVideoAdRewardedDemandOnlyEvent
	{
		add
		{
			if (_onRewardedVideoAdRewardedDemandOnlyEvent__BackingField == null || !_onRewardedVideoAdRewardedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdRewardedDemandOnlyEvent += value;
			}
		}
		remove
		{
			if (_onRewardedVideoAdRewardedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdRewardedDemandOnlyEvent -= value;
			}
		}
	}

	private static event Action<string, IronSourceError> _onRewardedVideoAdShowFailedDemandOnlyEvent
	{
		add
		{
			Action<string, IronSourceError> action = _onRewardedVideoAdShowFailedDemandOnlyEvent__BackingField;
			Action<string, IronSourceError> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdShowFailedDemandOnlyEvent__BackingField, (Action<string, IronSourceError>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string, IronSourceError> action = _onRewardedVideoAdShowFailedDemandOnlyEvent__BackingField;
			Action<string, IronSourceError> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdShowFailedDemandOnlyEvent__BackingField, (Action<string, IronSourceError>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string, IronSourceError> onRewardedVideoAdShowFailedDemandOnlyEvent
	{
		add
		{
			if (_onRewardedVideoAdShowFailedDemandOnlyEvent__BackingField == null || !_onRewardedVideoAdShowFailedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdShowFailedDemandOnlyEvent += value;
			}
		}
		remove
		{
			if (_onRewardedVideoAdShowFailedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdShowFailedDemandOnlyEvent -= value;
			}
		}
	}

	private static event Action<string, IronSourcePlacement> _onRewardedVideoAdClickedDemandOnlyEvent
	{
		add
		{
			Action<string, IronSourcePlacement> action = _onRewardedVideoAdClickedDemandOnlyEvent__BackingField;
			Action<string, IronSourcePlacement> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdClickedDemandOnlyEvent__BackingField, (Action<string, IronSourcePlacement>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string, IronSourcePlacement> action = _onRewardedVideoAdClickedDemandOnlyEvent__BackingField;
			Action<string, IronSourcePlacement> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onRewardedVideoAdClickedDemandOnlyEvent__BackingField, (Action<string, IronSourcePlacement>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string, IronSourcePlacement> onRewardedVideoAdClickedDemandOnlyEvent
	{
		add
		{
			if (_onRewardedVideoAdClickedDemandOnlyEvent__BackingField == null || !_onRewardedVideoAdClickedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdClickedDemandOnlyEvent += value;
			}
		}
		remove
		{
			if (_onRewardedVideoAdClickedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onRewardedVideoAdClickedDemandOnlyEvent -= value;
			}
		}
	}

	private static event Action _onInterstitialAdReadyEvent
	{
		add
		{
			Action action = _onInterstitialAdReadyEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdReadyEvent__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = _onInterstitialAdReadyEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdReadyEvent__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action onInterstitialAdReadyEvent
	{
		add
		{
			if (_onInterstitialAdReadyEvent__BackingField == null || !_onInterstitialAdReadyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdReadyEvent += value;
			}
		}
		remove
		{
			if (_onInterstitialAdReadyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdReadyEvent -= value;
			}
		}
	}

	private static event Action<IronSourceError> _onInterstitialAdLoadFailedEvent
	{
		add
		{
			Action<IronSourceError> action = _onInterstitialAdLoadFailedEvent__BackingField;
			Action<IronSourceError> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdLoadFailedEvent__BackingField, (Action<IronSourceError>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<IronSourceError> action = _onInterstitialAdLoadFailedEvent__BackingField;
			Action<IronSourceError> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdLoadFailedEvent__BackingField, (Action<IronSourceError>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<IronSourceError> onInterstitialAdLoadFailedEvent
	{
		add
		{
			if (_onInterstitialAdLoadFailedEvent__BackingField == null || !_onInterstitialAdLoadFailedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdLoadFailedEvent += value;
			}
		}
		remove
		{
			if (_onInterstitialAdLoadFailedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdLoadFailedEvent -= value;
			}
		}
	}

	private static event Action _onInterstitialAdOpenedEvent
	{
		add
		{
			Action action = _onInterstitialAdOpenedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdOpenedEvent__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = _onInterstitialAdOpenedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdOpenedEvent__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action onInterstitialAdOpenedEvent
	{
		add
		{
			if (_onInterstitialAdOpenedEvent__BackingField == null || !_onInterstitialAdOpenedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdOpenedEvent += value;
			}
		}
		remove
		{
			if (_onInterstitialAdOpenedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdOpenedEvent -= value;
			}
		}
	}

	private static event Action _onInterstitialAdClosedEvent
	{
		add
		{
			Action action = _onInterstitialAdClosedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdClosedEvent__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = _onInterstitialAdClosedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdClosedEvent__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action onInterstitialAdClosedEvent
	{
		add
		{
			if (_onInterstitialAdClosedEvent__BackingField == null || !_onInterstitialAdClosedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdClosedEvent += value;
			}
		}
		remove
		{
			if (_onInterstitialAdClosedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdClosedEvent -= value;
			}
		}
	}

	private static event Action _onInterstitialAdShowSucceededEvent
	{
		add
		{
			Action action = _onInterstitialAdShowSucceededEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdShowSucceededEvent__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = _onInterstitialAdShowSucceededEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdShowSucceededEvent__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action onInterstitialAdShowSucceededEvent
	{
		add
		{
			if (_onInterstitialAdShowSucceededEvent__BackingField == null || !_onInterstitialAdShowSucceededEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdShowSucceededEvent += value;
			}
		}
		remove
		{
			if (_onInterstitialAdShowSucceededEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdShowSucceededEvent -= value;
			}
		}
	}

	private static event Action<IronSourceError> _onInterstitialAdShowFailedEvent
	{
		add
		{
			Action<IronSourceError> action = _onInterstitialAdShowFailedEvent__BackingField;
			Action<IronSourceError> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdShowFailedEvent__BackingField, (Action<IronSourceError>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<IronSourceError> action = _onInterstitialAdShowFailedEvent__BackingField;
			Action<IronSourceError> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdShowFailedEvent__BackingField, (Action<IronSourceError>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<IronSourceError> onInterstitialAdShowFailedEvent
	{
		add
		{
			if (_onInterstitialAdShowFailedEvent__BackingField == null || !_onInterstitialAdShowFailedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdShowFailedEvent += value;
			}
		}
		remove
		{
			if (_onInterstitialAdShowFailedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdShowFailedEvent -= value;
			}
		}
	}

	private static event Action _onInterstitialAdClickedEvent
	{
		add
		{
			Action action = _onInterstitialAdClickedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdClickedEvent__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = _onInterstitialAdClickedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdClickedEvent__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action onInterstitialAdClickedEvent
	{
		add
		{
			if (_onInterstitialAdClickedEvent__BackingField == null || !_onInterstitialAdClickedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdClickedEvent += value;
			}
		}
		remove
		{
			if (_onInterstitialAdClickedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdClickedEvent -= value;
			}
		}
	}

	private static event Action<string> _onInterstitialAdReadyDemandOnlyEvent
	{
		add
		{
			Action<string> action = _onInterstitialAdReadyDemandOnlyEvent__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdReadyDemandOnlyEvent__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = _onInterstitialAdReadyDemandOnlyEvent__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdReadyDemandOnlyEvent__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string> onInterstitialAdReadyDemandOnlyEvent
	{
		add
		{
			if (_onInterstitialAdReadyDemandOnlyEvent__BackingField == null || !_onInterstitialAdReadyDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdReadyDemandOnlyEvent += value;
			}
		}
		remove
		{
			if (_onInterstitialAdReadyDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdReadyDemandOnlyEvent -= value;
			}
		}
	}

	private static event Action<string, IronSourceError> _onInterstitialAdLoadFailedDemandOnlyEvent
	{
		add
		{
			Action<string, IronSourceError> action = _onInterstitialAdLoadFailedDemandOnlyEvent__BackingField;
			Action<string, IronSourceError> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdLoadFailedDemandOnlyEvent__BackingField, (Action<string, IronSourceError>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string, IronSourceError> action = _onInterstitialAdLoadFailedDemandOnlyEvent__BackingField;
			Action<string, IronSourceError> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdLoadFailedDemandOnlyEvent__BackingField, (Action<string, IronSourceError>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string, IronSourceError> onInterstitialAdLoadFailedDemandOnlyEvent
	{
		add
		{
			if (_onInterstitialAdLoadFailedDemandOnlyEvent__BackingField == null || !_onInterstitialAdLoadFailedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdLoadFailedDemandOnlyEvent += value;
			}
		}
		remove
		{
			if (_onInterstitialAdLoadFailedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdLoadFailedDemandOnlyEvent -= value;
			}
		}
	}

	private static event Action<string> _onInterstitialAdOpenedDemandOnlyEvent
	{
		add
		{
			Action<string> action = _onInterstitialAdOpenedDemandOnlyEvent__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdOpenedDemandOnlyEvent__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = _onInterstitialAdOpenedDemandOnlyEvent__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdOpenedDemandOnlyEvent__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string> onInterstitialAdOpenedDemandOnlyEvent
	{
		add
		{
			if (_onInterstitialAdOpenedDemandOnlyEvent__BackingField == null || !_onInterstitialAdOpenedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdOpenedDemandOnlyEvent += value;
			}
		}
		remove
		{
			if (_onInterstitialAdOpenedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdOpenedDemandOnlyEvent -= value;
			}
		}
	}

	private static event Action<string> _onInterstitialAdClosedDemandOnlyEvent
	{
		add
		{
			Action<string> action = _onInterstitialAdClosedDemandOnlyEvent__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdClosedDemandOnlyEvent__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = _onInterstitialAdClosedDemandOnlyEvent__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdClosedDemandOnlyEvent__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string> onInterstitialAdClosedDemandOnlyEvent
	{
		add
		{
			if (_onInterstitialAdClosedDemandOnlyEvent__BackingField == null || !_onInterstitialAdClosedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdClosedDemandOnlyEvent += value;
			}
		}
		remove
		{
			if (_onInterstitialAdClosedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdClosedDemandOnlyEvent -= value;
			}
		}
	}

	private static event Action<string> _onInterstitialAdShowSucceededDemandOnlyEvent
	{
		add
		{
			Action<string> action = _onInterstitialAdShowSucceededDemandOnlyEvent__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdShowSucceededDemandOnlyEvent__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = _onInterstitialAdShowSucceededDemandOnlyEvent__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdShowSucceededDemandOnlyEvent__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string> onInterstitialAdShowSucceededDemandOnlyEvent
	{
		add
		{
			if (_onInterstitialAdShowSucceededDemandOnlyEvent__BackingField == null || !_onInterstitialAdShowSucceededDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdShowSucceededDemandOnlyEvent += value;
			}
		}
		remove
		{
			if (_onInterstitialAdShowSucceededDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdShowSucceededDemandOnlyEvent -= value;
			}
		}
	}

	private static event Action<string, IronSourceError> _onInterstitialAdShowFailedDemandOnlyEvent
	{
		add
		{
			Action<string, IronSourceError> action = _onInterstitialAdShowFailedDemandOnlyEvent__BackingField;
			Action<string, IronSourceError> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdShowFailedDemandOnlyEvent__BackingField, (Action<string, IronSourceError>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string, IronSourceError> action = _onInterstitialAdShowFailedDemandOnlyEvent__BackingField;
			Action<string, IronSourceError> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdShowFailedDemandOnlyEvent__BackingField, (Action<string, IronSourceError>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string, IronSourceError> onInterstitialAdShowFailedDemandOnlyEvent
	{
		add
		{
			if (_onInterstitialAdShowFailedDemandOnlyEvent__BackingField == null || !_onInterstitialAdShowFailedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdShowFailedDemandOnlyEvent += value;
			}
		}
		remove
		{
			if (_onInterstitialAdShowFailedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdShowFailedDemandOnlyEvent -= value;
			}
		}
	}

	private static event Action<string> _onInterstitialAdClickedDemandOnlyEvent
	{
		add
		{
			Action<string> action = _onInterstitialAdClickedDemandOnlyEvent__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdClickedDemandOnlyEvent__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = _onInterstitialAdClickedDemandOnlyEvent__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdClickedDemandOnlyEvent__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string> onInterstitialAdClickedDemandOnlyEvent
	{
		add
		{
			if (_onInterstitialAdClickedDemandOnlyEvent__BackingField == null || !_onInterstitialAdClickedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdClickedDemandOnlyEvent += value;
			}
		}
		remove
		{
			if (_onInterstitialAdClickedDemandOnlyEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdClickedDemandOnlyEvent -= value;
			}
		}
	}

	private static event Action _onInterstitialAdRewardedEvent
	{
		add
		{
			Action action = _onInterstitialAdRewardedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdRewardedEvent__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = _onInterstitialAdRewardedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onInterstitialAdRewardedEvent__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action onInterstitialAdRewardedEvent
	{
		add
		{
			if (_onInterstitialAdRewardedEvent__BackingField == null || !_onInterstitialAdRewardedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdRewardedEvent += value;
			}
		}
		remove
		{
			if (_onInterstitialAdRewardedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onInterstitialAdRewardedEvent -= value;
			}
		}
	}

	private static event Action _onOfferwallOpenedEvent
	{
		add
		{
			Action action = _onOfferwallOpenedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onOfferwallOpenedEvent__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = _onOfferwallOpenedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onOfferwallOpenedEvent__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action onOfferwallOpenedEvent
	{
		add
		{
			if (_onOfferwallOpenedEvent__BackingField == null || !_onOfferwallOpenedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onOfferwallOpenedEvent += value;
			}
		}
		remove
		{
			if (_onOfferwallOpenedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onOfferwallOpenedEvent -= value;
			}
		}
	}

	private static event Action<IronSourceError> _onOfferwallShowFailedEvent
	{
		add
		{
			Action<IronSourceError> action = _onOfferwallShowFailedEvent__BackingField;
			Action<IronSourceError> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onOfferwallShowFailedEvent__BackingField, (Action<IronSourceError>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<IronSourceError> action = _onOfferwallShowFailedEvent__BackingField;
			Action<IronSourceError> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onOfferwallShowFailedEvent__BackingField, (Action<IronSourceError>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<IronSourceError> onOfferwallShowFailedEvent
	{
		add
		{
			if (_onOfferwallShowFailedEvent__BackingField == null || !_onOfferwallShowFailedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onOfferwallShowFailedEvent += value;
			}
		}
		remove
		{
			if (_onOfferwallShowFailedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onOfferwallShowFailedEvent -= value;
			}
		}
	}

	private static event Action _onOfferwallClosedEvent
	{
		add
		{
			Action action = _onOfferwallClosedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onOfferwallClosedEvent__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = _onOfferwallClosedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onOfferwallClosedEvent__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action onOfferwallClosedEvent
	{
		add
		{
			if (_onOfferwallClosedEvent__BackingField == null || !_onOfferwallClosedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onOfferwallClosedEvent += value;
			}
		}
		remove
		{
			if (_onOfferwallClosedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onOfferwallClosedEvent -= value;
			}
		}
	}

	private static event Action<IronSourceError> _onGetOfferwallCreditsFailedEvent
	{
		add
		{
			Action<IronSourceError> action = _onGetOfferwallCreditsFailedEvent__BackingField;
			Action<IronSourceError> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onGetOfferwallCreditsFailedEvent__BackingField, (Action<IronSourceError>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<IronSourceError> action = _onGetOfferwallCreditsFailedEvent__BackingField;
			Action<IronSourceError> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onGetOfferwallCreditsFailedEvent__BackingField, (Action<IronSourceError>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<IronSourceError> onGetOfferwallCreditsFailedEvent
	{
		add
		{
			if (_onGetOfferwallCreditsFailedEvent__BackingField == null || !_onGetOfferwallCreditsFailedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onGetOfferwallCreditsFailedEvent += value;
			}
		}
		remove
		{
			if (_onGetOfferwallCreditsFailedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onGetOfferwallCreditsFailedEvent -= value;
			}
		}
	}

	private static event Action<Dictionary<string, object>> _onOfferwallAdCreditedEvent
	{
		add
		{
			Action<Dictionary<string, object>> action = _onOfferwallAdCreditedEvent__BackingField;
			Action<Dictionary<string, object>> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onOfferwallAdCreditedEvent__BackingField, (Action<Dictionary<string, object>>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Dictionary<string, object>> action = _onOfferwallAdCreditedEvent__BackingField;
			Action<Dictionary<string, object>> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onOfferwallAdCreditedEvent__BackingField, (Action<Dictionary<string, object>>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<Dictionary<string, object>> onOfferwallAdCreditedEvent
	{
		add
		{
			if (_onOfferwallAdCreditedEvent__BackingField == null || !_onOfferwallAdCreditedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onOfferwallAdCreditedEvent += value;
			}
		}
		remove
		{
			if (_onOfferwallAdCreditedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onOfferwallAdCreditedEvent -= value;
			}
		}
	}

	private static event Action<bool> _onOfferwallAvailableEvent
	{
		add
		{
			Action<bool> action = _onOfferwallAvailableEvent__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onOfferwallAvailableEvent__BackingField, (Action<bool>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<bool> action = _onOfferwallAvailableEvent__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onOfferwallAvailableEvent__BackingField, (Action<bool>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<bool> onOfferwallAvailableEvent
	{
		add
		{
			if (_onOfferwallAvailableEvent__BackingField == null || !_onOfferwallAvailableEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onOfferwallAvailableEvent += value;
			}
		}
		remove
		{
			if (_onOfferwallAvailableEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onOfferwallAvailableEvent -= value;
			}
		}
	}

	private static event Action _onBannerAdLoadedEvent
	{
		add
		{
			Action action = _onBannerAdLoadedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onBannerAdLoadedEvent__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = _onBannerAdLoadedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onBannerAdLoadedEvent__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action onBannerAdLoadedEvent
	{
		add
		{
			if (_onBannerAdLoadedEvent__BackingField == null || !_onBannerAdLoadedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onBannerAdLoadedEvent += value;
			}
		}
		remove
		{
			if (_onBannerAdLoadedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onBannerAdLoadedEvent -= value;
			}
		}
	}

	private static event Action<IronSourceError> _onBannerAdLoadFailedEvent
	{
		add
		{
			Action<IronSourceError> action = _onBannerAdLoadFailedEvent__BackingField;
			Action<IronSourceError> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onBannerAdLoadFailedEvent__BackingField, (Action<IronSourceError>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<IronSourceError> action = _onBannerAdLoadFailedEvent__BackingField;
			Action<IronSourceError> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onBannerAdLoadFailedEvent__BackingField, (Action<IronSourceError>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<IronSourceError> onBannerAdLoadFailedEvent
	{
		add
		{
			if (_onBannerAdLoadFailedEvent__BackingField == null || !_onBannerAdLoadFailedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onBannerAdLoadFailedEvent += value;
			}
		}
		remove
		{
			if (_onBannerAdLoadFailedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onBannerAdLoadFailedEvent -= value;
			}
		}
	}

	private static event Action _onBannerAdClickedEvent
	{
		add
		{
			Action action = _onBannerAdClickedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onBannerAdClickedEvent__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = _onBannerAdClickedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onBannerAdClickedEvent__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action onBannerAdClickedEvent
	{
		add
		{
			if (_onBannerAdClickedEvent__BackingField == null || !_onBannerAdClickedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onBannerAdClickedEvent += value;
			}
		}
		remove
		{
			if (_onBannerAdClickedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onBannerAdClickedEvent -= value;
			}
		}
	}

	private static event Action _onBannerAdScreenPresentedEvent
	{
		add
		{
			Action action = _onBannerAdScreenPresentedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onBannerAdScreenPresentedEvent__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = _onBannerAdScreenPresentedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onBannerAdScreenPresentedEvent__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action onBannerAdScreenPresentedEvent
	{
		add
		{
			if (_onBannerAdScreenPresentedEvent__BackingField == null || !_onBannerAdScreenPresentedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onBannerAdScreenPresentedEvent += value;
			}
		}
		remove
		{
			if (_onBannerAdScreenPresentedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onBannerAdScreenPresentedEvent -= value;
			}
		}
	}

	private static event Action _onBannerAdScreenDismissedEvent
	{
		add
		{
			Action action = _onBannerAdScreenDismissedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onBannerAdScreenDismissedEvent__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = _onBannerAdScreenDismissedEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onBannerAdScreenDismissedEvent__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action onBannerAdScreenDismissedEvent
	{
		add
		{
			if (_onBannerAdScreenDismissedEvent__BackingField == null || !_onBannerAdScreenDismissedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onBannerAdScreenDismissedEvent += value;
			}
		}
		remove
		{
			if (_onBannerAdScreenDismissedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onBannerAdScreenDismissedEvent -= value;
			}
		}
	}

	private static event Action _onBannerAdLeftApplicationEvent
	{
		add
		{
			Action action = _onBannerAdLeftApplicationEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onBannerAdLeftApplicationEvent__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = _onBannerAdLeftApplicationEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onBannerAdLeftApplicationEvent__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action onBannerAdLeftApplicationEvent
	{
		add
		{
			if (_onBannerAdLeftApplicationEvent__BackingField == null || !_onBannerAdLeftApplicationEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onBannerAdLeftApplicationEvent += value;
			}
		}
		remove
		{
			if (_onBannerAdLeftApplicationEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onBannerAdLeftApplicationEvent -= value;
			}
		}
	}

	private static event Action<string> _onSegmentReceivedEvent
	{
		add
		{
			Action<string> action = _onSegmentReceivedEvent__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onSegmentReceivedEvent__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = _onSegmentReceivedEvent__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref _onSegmentReceivedEvent__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string> onSegmentReceivedEvent
	{
		add
		{
			if (_onSegmentReceivedEvent__BackingField == null || !_onSegmentReceivedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onSegmentReceivedEvent += value;
			}
		}
		remove
		{
			if (_onSegmentReceivedEvent__BackingField.GetInvocationList().Contains(value))
			{
				_onSegmentReceivedEvent -= value;
			}
		}
	}

	private void Awake()
	{
		base.gameObject.name = "IronSourceEvents";
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void onRewardedVideoAdShowFailed(string description)
	{
		if (_onRewardedVideoAdShowFailedEvent__BackingField != null)
		{
			IronSourceError errorFromErrorObject = getErrorFromErrorObject(description);
			_onRewardedVideoAdShowFailedEvent__BackingField(errorFromErrorObject);
		}
	}

	public void onRewardedVideoAdOpened(string empty)
	{
		if (_onRewardedVideoAdOpenedEvent__BackingField != null)
		{
			_onRewardedVideoAdOpenedEvent__BackingField();
		}
	}

	public void onRewardedVideoAdClosed(string empty)
	{
		if (_onRewardedVideoAdClosedEvent__BackingField != null)
		{
			_onRewardedVideoAdClosedEvent__BackingField();
		}
	}

	public void onRewardedVideoAdStarted(string empty)
	{
		if (_onRewardedVideoAdStartedEvent__BackingField != null)
		{
			_onRewardedVideoAdStartedEvent__BackingField();
		}
	}

	public void onRewardedVideoAdEnded(string empty)
	{
		if (_onRewardedVideoAdEndedEvent__BackingField != null)
		{
			_onRewardedVideoAdEndedEvent__BackingField();
		}
	}

	public void onRewardedVideoAdRewarded(string description)
	{
		if (_onRewardedVideoAdRewardedEvent__BackingField != null)
		{
			IronSourcePlacement placementFromObject = getPlacementFromObject(description);
			_onRewardedVideoAdRewardedEvent__BackingField(placementFromObject);
		}
	}

	public void onRewardedVideoAdClicked(string description)
	{
		if (_onRewardedVideoAdClickedEvent__BackingField != null)
		{
			IronSourcePlacement placementFromObject = getPlacementFromObject(description);
			_onRewardedVideoAdClickedEvent__BackingField(placementFromObject);
		}
	}

	public void onRewardedVideoAvailabilityChanged(string stringAvailable)
	{
		bool obj = stringAvailable == "true";
		if (_onRewardedVideoAvailabilityChangedEvent__BackingField != null)
		{
			_onRewardedVideoAvailabilityChangedEvent__BackingField(obj);
		}
	}

	public void onRewardedVideoAvailabilityChangedDemandOnly(string args)
	{
		if (_onRewardedVideoAvailabilityChangedDemandOnlyEvent__BackingField != null && !string.IsNullOrEmpty(args))
		{
			List<object> list = Json.Deserialize(args) as List<object>;
			bool arg = list[1].ToString().ToLower() == "true";
			string arg2 = list[0].ToString();
			_onRewardedVideoAvailabilityChangedDemandOnlyEvent__BackingField(arg2, arg);
		}
	}

	public void onRewardedVideoAdOpenedDemandOnly(string instanceId)
	{
		if (_onRewardedVideoAdOpenedDemandOnlyEvent__BackingField != null)
		{
			_onRewardedVideoAdOpenedDemandOnlyEvent__BackingField(instanceId);
		}
	}

	public void onRewardedVideoAdClosedDemandOnly(string instanceId)
	{
		if (_onRewardedVideoAdClosedDemandOnlyEvent__BackingField != null)
		{
			_onRewardedVideoAdClosedDemandOnlyEvent__BackingField(instanceId);
		}
	}

	public void onRewardedVideoAdRewardedDemandOnly(string args)
	{
		if (_onRewardedVideoAdRewardedDemandOnlyEvent__BackingField != null && !string.IsNullOrEmpty(args))
		{
			List<object> list = Json.Deserialize(args) as List<object>;
			string arg = list[0].ToString();
			IronSourcePlacement placementFromObject = getPlacementFromObject(list[1]);
			_onRewardedVideoAdRewardedDemandOnlyEvent__BackingField(arg, placementFromObject);
		}
	}

	public void onRewardedVideoAdShowFailedDemandOnly(string args)
	{
		if (_onRewardedVideoAdShowFailedDemandOnlyEvent__BackingField != null && !string.IsNullOrEmpty(args))
		{
			List<object> list = Json.Deserialize(args) as List<object>;
			IronSourceError errorFromErrorObject = getErrorFromErrorObject(list[1]);
			string arg = list[0].ToString();
			_onRewardedVideoAdShowFailedDemandOnlyEvent__BackingField(arg, errorFromErrorObject);
		}
	}

	public void onRewardedVideoAdClickedDemandOnly(string args)
	{
		if (_onRewardedVideoAdClickedDemandOnlyEvent__BackingField != null && !string.IsNullOrEmpty(args))
		{
			List<object> list = Json.Deserialize(args) as List<object>;
			string arg = list[0].ToString();
			IronSourcePlacement placementFromObject = getPlacementFromObject(list[1]);
			_onRewardedVideoAdClickedDemandOnlyEvent__BackingField(arg, placementFromObject);
		}
	}

	public void onInterstitialAdReady()
	{
		if (_onInterstitialAdReadyEvent__BackingField != null)
		{
			_onInterstitialAdReadyEvent__BackingField();
		}
	}

	public void onInterstitialAdLoadFailed(string description)
	{
		if (_onInterstitialAdLoadFailedEvent__BackingField != null)
		{
			IronSourceError errorFromErrorObject = getErrorFromErrorObject(description);
			_onInterstitialAdLoadFailedEvent__BackingField(errorFromErrorObject);
		}
	}

	public void onInterstitialAdOpened(string empty)
	{
		if (_onInterstitialAdOpenedEvent__BackingField != null)
		{
			_onInterstitialAdOpenedEvent__BackingField();
		}
	}

	public void onInterstitialAdClosed(string empty)
	{
		if (_onInterstitialAdClosedEvent__BackingField != null)
		{
			_onInterstitialAdClosedEvent__BackingField();
		}
	}

	public void onInterstitialAdShowSucceeded(string empty)
	{
		if (_onInterstitialAdShowSucceededEvent__BackingField != null)
		{
			_onInterstitialAdShowSucceededEvent__BackingField();
		}
	}

	public void onInterstitialAdShowFailed(string description)
	{
		if (_onInterstitialAdShowFailedEvent__BackingField != null)
		{
			IronSourceError errorFromErrorObject = getErrorFromErrorObject(description);
			_onInterstitialAdShowFailedEvent__BackingField(errorFromErrorObject);
		}
	}

	public void onInterstitialAdClicked(string empty)
	{
		if (_onInterstitialAdClickedEvent__BackingField != null)
		{
			_onInterstitialAdClickedEvent__BackingField();
		}
	}

	public void onInterstitialAdReadyDemandOnly(string instanceId)
	{
		if (_onInterstitialAdReadyDemandOnlyEvent__BackingField != null)
		{
			_onInterstitialAdReadyDemandOnlyEvent__BackingField(instanceId);
		}
	}

	public void onInterstitialAdLoadFailedDemandOnly(string args)
	{
		if (_onInterstitialAdLoadFailedDemandOnlyEvent__BackingField != null && !string.IsNullOrEmpty(args))
		{
			List<object> list = Json.Deserialize(args) as List<object>;
			IronSourceError errorFromErrorObject = getErrorFromErrorObject(list[1]);
			string arg = list[0].ToString();
			_onInterstitialAdLoadFailedDemandOnlyEvent__BackingField(arg, errorFromErrorObject);
		}
	}

	public void onInterstitialAdOpenedDemandOnly(string instanceId)
	{
		if (_onInterstitialAdOpenedDemandOnlyEvent__BackingField != null)
		{
			_onInterstitialAdOpenedDemandOnlyEvent__BackingField(instanceId);
		}
	}

	public void onInterstitialAdClosedDemandOnly(string instanceId)
	{
		if (_onInterstitialAdClosedDemandOnlyEvent__BackingField != null)
		{
			_onInterstitialAdClosedDemandOnlyEvent__BackingField(instanceId);
		}
	}

	public void onInterstitialAdShowSucceededDemandOnly(string instanceId)
	{
		if (_onInterstitialAdShowSucceededDemandOnlyEvent__BackingField != null)
		{
			_onInterstitialAdShowSucceededDemandOnlyEvent__BackingField(instanceId);
		}
	}

	public void onInterstitialAdShowFailedDemandOnly(string args)
	{
		if (_onInterstitialAdLoadFailedDemandOnlyEvent__BackingField != null && !string.IsNullOrEmpty(args))
		{
			List<object> list = Json.Deserialize(args) as List<object>;
			IronSourceError errorFromErrorObject = getErrorFromErrorObject(list[1]);
			string arg = list[0].ToString();
			_onInterstitialAdShowFailedDemandOnlyEvent__BackingField(arg, errorFromErrorObject);
		}
	}

	public void onInterstitialAdClickedDemandOnly(string instanceId)
	{
		if (_onInterstitialAdClickedDemandOnlyEvent__BackingField != null)
		{
			_onInterstitialAdClickedDemandOnlyEvent__BackingField(instanceId);
		}
	}

	public void onInterstitialAdRewarded(string empty)
	{
		if (_onInterstitialAdRewardedEvent__BackingField != null)
		{
			_onInterstitialAdRewardedEvent__BackingField();
		}
	}

	public void onOfferwallOpened(string empty)
	{
		if (_onOfferwallOpenedEvent__BackingField != null)
		{
			_onOfferwallOpenedEvent__BackingField();
		}
	}

	public void onOfferwallShowFailed(string description)
	{
		if (_onOfferwallShowFailedEvent__BackingField != null)
		{
			IronSourceError errorFromErrorObject = getErrorFromErrorObject(description);
			_onOfferwallShowFailedEvent__BackingField(errorFromErrorObject);
		}
	}

	public void onOfferwallClosed(string empty)
	{
		if (_onOfferwallClosedEvent__BackingField != null)
		{
			_onOfferwallClosedEvent__BackingField();
		}
	}

	public void onGetOfferwallCreditsFailed(string description)
	{
		if (_onGetOfferwallCreditsFailedEvent__BackingField != null)
		{
			IronSourceError errorFromErrorObject = getErrorFromErrorObject(description);
			_onGetOfferwallCreditsFailedEvent__BackingField(errorFromErrorObject);
		}
	}

	public void onOfferwallAdCredited(string json)
	{
		if (_onOfferwallAdCreditedEvent__BackingField != null)
		{
			_onOfferwallAdCreditedEvent__BackingField(Json.Deserialize(json) as Dictionary<string, object>);
		}
	}

	public void onOfferwallAvailable(string stringAvailable)
	{
		bool obj = stringAvailable == "true";
		if (_onOfferwallAvailableEvent__BackingField != null)
		{
			_onOfferwallAvailableEvent__BackingField(obj);
		}
	}

	public void onBannerAdLoaded()
	{
		if (_onBannerAdLoadedEvent__BackingField != null)
		{
			_onBannerAdLoadedEvent__BackingField();
		}
	}

	public void onBannerAdLoadFailed(string description)
	{
		if (_onBannerAdLoadFailedEvent__BackingField != null)
		{
			IronSourceError errorFromErrorObject = getErrorFromErrorObject(description);
			_onBannerAdLoadFailedEvent__BackingField(errorFromErrorObject);
		}
	}

	public void onBannerAdClicked()
	{
		if (_onBannerAdClickedEvent__BackingField != null)
		{
			_onBannerAdClickedEvent__BackingField();
		}
	}

	public void onBannerAdScreenPresented()
	{
		if (_onBannerAdScreenPresentedEvent__BackingField != null)
		{
			_onBannerAdScreenPresentedEvent__BackingField();
		}
	}

	public void onBannerAdScreenDismissed()
	{
		if (_onBannerAdScreenDismissedEvent__BackingField != null)
		{
			_onBannerAdScreenDismissedEvent__BackingField();
		}
	}

	public void onBannerAdLeftApplication()
	{
		if (_onBannerAdLeftApplicationEvent__BackingField != null)
		{
			_onBannerAdLeftApplicationEvent__BackingField();
		}
	}

	public void onSegmentReceived(string segmentName)
	{
		if (_onSegmentReceivedEvent__BackingField != null)
		{
			_onSegmentReceivedEvent__BackingField(segmentName);
		}
	}

	private IronSourceError getErrorFromErrorObject(object descriptionObject)
	{
		Dictionary<string, object> dictionary = null;
		if (descriptionObject is IDictionary)
		{
			dictionary = descriptionObject as Dictionary<string, object>;
		}
		else if (descriptionObject is string && !string.IsNullOrEmpty(descriptionObject.ToString()))
		{
			dictionary = Json.Deserialize(descriptionObject.ToString()) as Dictionary<string, object>;
		}
		IronSourceError result = new IronSourceError(-1, string.Empty);
		if (dictionary != null && dictionary.Count > 0)
		{
			int errorCode = Convert.ToInt32(dictionary["error_code"].ToString());
			string errorDescription = dictionary["error_description"].ToString();
			result = new IronSourceError(errorCode, errorDescription);
		}
		return result;
	}

	private IronSourcePlacement getPlacementFromObject(object placementObject)
	{
		Dictionary<string, object> dictionary = null;
		if (placementObject is IDictionary)
		{
			dictionary = placementObject as Dictionary<string, object>;
		}
		else if (placementObject is string)
		{
			dictionary = Json.Deserialize(placementObject.ToString()) as Dictionary<string, object>;
		}
		IronSourcePlacement result = null;
		if (dictionary != null && dictionary.Count > 0)
		{
			int rewardAmount = Convert.ToInt32(dictionary["placement_reward_amount"].ToString());
			string rewardName = dictionary["placement_reward_name"].ToString();
			string placementName = dictionary["placement_name"].ToString();
			result = new IronSourcePlacement(placementName, rewardName, rewardAmount);
		}
		return result;
	}
}
