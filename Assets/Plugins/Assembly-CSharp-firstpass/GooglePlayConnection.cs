using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class GooglePlayConnection : Singleton<GooglePlayConnection>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GooglePlayConnectionResult> ActionConnectionResultReceived__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GPConnectionState> ActionConnectionStateChanged__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action ActionPlayerConnected__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action ActionPlayerDisconnected__BackingField = () =>
	{
	};

	private bool _IsInitialized;

	private static GPConnectionState _State = GPConnectionState.STATE_UNCONFIGURED;

	public bool IsConnected
	{
		get
		{
			return State == GPConnectionState.STATE_CONNECTED;
		}
	}

	[Obsolete("state is deprecated, please use State instead.")]
	public static GPConnectionState state
	{
		get
		{
			return State;
		}
	}

	public static GPConnectionState State
	{
		get
		{
			return _State;
		}
	}

	[Obsolete("isInitialized is deprecated, please use IsInitialized instead.")]
	public bool isInitialized
	{
		get
		{
			return IsInitialized;
		}
	}

	public bool IsInitialized
	{
		get
		{
			return _IsInitialized;
		}
	}

	public static event Action<GooglePlayConnectionResult> ActionConnectionResultReceived
	{
		add
		{
			Action<GooglePlayConnectionResult> action = ActionConnectionResultReceived__BackingField;
			Action<GooglePlayConnectionResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionConnectionResultReceived__BackingField, (Action<GooglePlayConnectionResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GooglePlayConnectionResult> action = ActionConnectionResultReceived__BackingField;
			Action<GooglePlayConnectionResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionConnectionResultReceived__BackingField, (Action<GooglePlayConnectionResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GPConnectionState> ActionConnectionStateChanged
	{
		add
		{
			Action<GPConnectionState> action = ActionConnectionStateChanged__BackingField;
			Action<GPConnectionState> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionConnectionStateChanged__BackingField, (Action<GPConnectionState>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GPConnectionState> action = ActionConnectionStateChanged__BackingField;
			Action<GPConnectionState> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionConnectionStateChanged__BackingField, (Action<GPConnectionState>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action ActionPlayerConnected
	{
		add
		{
			Action action = ActionPlayerConnected__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPlayerConnected__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ActionPlayerConnected__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPlayerConnected__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action ActionPlayerDisconnected
	{
		add
		{
			Action action = ActionPlayerDisconnected__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPlayerDisconnected__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ActionPlayerDisconnected__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPlayerDisconnected__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		Singleton<GooglePlayManager>.Instance.Create();
		Init();
	}

	private void Init()
	{
		string text = string.Empty;
		if (AndroidNativeSettings.Instance.EnableGamesAPI)
		{
			text += "GamesAPI";
		}
		if (AndroidNativeSettings.Instance.EnablePlusAPI)
		{
			text += "PlusAPI";
		}
		if (AndroidNativeSettings.Instance.EnableDriveAPI)
		{
			text += "DriveAPI";
		}
		if (AndroidNativeSettings.Instance.EnableAppInviteAPI)
		{
			text += "AppInvite";
		}
		AN_GMSGeneralProxy.setConnectionParams(AndroidNativeSettings.Instance.ShowConnectingPopup);
		AN_GMSGeneralProxy.playServiceInit(text);
	}

	[Obsolete("connect is deprecated, please use Connect instead.")]
	public void connect()
	{
		Connect();
	}

	public void Connect()
	{
		Connect(null);
	}

	[Obsolete("connect is deprecated, please use Connect instead.")]
	public void connect(string accountName)
	{
		Connect(accountName);
	}

	public void Connect(string accountName)
	{
		if (_State != GPConnectionState.STATE_CONNECTED && _State != GPConnectionState.STATE_CONNECTING)
		{
			OnStateChange(GPConnectionState.STATE_CONNECTING);
			if (accountName != null)
			{
				AN_GMSGeneralProxy.playServiceConnect(accountName);
			}
			else
			{
				AN_GMSGeneralProxy.playServiceConnect();
			}
		}
	}

	[Obsolete("disconnect is deprecated, please use Disconnect instead.")]
	public void disconnect()
	{
		Disconnect();
	}

	public void Disconnect()
	{
		if (_State != GPConnectionState.STATE_DISCONNECTED && _State != GPConnectionState.STATE_CONNECTING)
		{
			OnStateChange(GPConnectionState.STATE_DISCONNECTED);
			AN_GMSGeneralProxy.playServiceDisconnect();
		}
	}

	public static bool CheckState()
	{
		GPConnectionState gPConnectionState = _State;
		if (gPConnectionState == GPConnectionState.STATE_CONNECTED)
		{
			return true;
		}
		return false;
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		AN_GMSGeneralProxy.OnApplicationPause(pauseStatus);
	}

	private void OnPlayServiceDisconnected(string data)
	{
		OnStateChange(GPConnectionState.STATE_DISCONNECTED);
	}

	private void OnConnectionResult(string resultCode)
	{
		UnityEngine.Debug.Log("[OnConnectionResult] resultCode " + resultCode);
		GooglePlayConnectionResult googlePlayConnectionResult = new GooglePlayConnectionResult();
		googlePlayConnectionResult.code = (GP_ConnectionResultCode)Convert.ToInt32(resultCode);
		if (googlePlayConnectionResult.IsSuccess)
		{
			OnStateChange(GPConnectionState.STATE_CONNECTED);
		}
		else
		{
			OnStateChange(GPConnectionState.STATE_DISCONNECTED);
		}
		ActionConnectionResultReceived__BackingField(googlePlayConnectionResult);
	}

	private void OnStateChange(GPConnectionState connectionState)
	{
		_State = connectionState;
		switch (_State)
		{
		case GPConnectionState.STATE_CONNECTED:
			ActionPlayerConnected__BackingField();
			break;
		case GPConnectionState.STATE_DISCONNECTED:
			ActionPlayerDisconnected__BackingField();
			break;
		}
		ActionConnectionStateChanged__BackingField(_State);
		UnityEngine.Debug.Log("Play Serice Connection State -> " + _State);
	}
}
