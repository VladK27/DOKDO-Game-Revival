using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ANMiniJSON;
using SA.Common.Pattern;
using UnityEngine;

public class GoogleCloudMessageService : Singleton<GoogleCloudMessageService>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string> ActionCouldMessageLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GP_GCM_RegistrationResult> ActionCMDRegistrationResult__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string, Dictionary<string, object>> ActionGCMPushLaunched__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string, Dictionary<string, object>> ActionGCMPushReceived__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string, Dictionary<string, object>> ActionParsePushReceived__BackingField = delegate
	{
	};

	private string _lastMessage = string.Empty;

	private string _registrationId = string.Empty;

	public string registrationId
	{
		get
		{
			return _registrationId;
		}
	}

	public string lastMessage
	{
		get
		{
			return _lastMessage;
		}
	}

	public static event Action<string> ActionCouldMessageLoaded
	{
		add
		{
			Action<string> action = ActionCouldMessageLoaded__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionCouldMessageLoaded__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = ActionCouldMessageLoaded__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionCouldMessageLoaded__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GP_GCM_RegistrationResult> ActionCMDRegistrationResult
	{
		add
		{
			Action<GP_GCM_RegistrationResult> action = ActionCMDRegistrationResult__BackingField;
			Action<GP_GCM_RegistrationResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionCMDRegistrationResult__BackingField, (Action<GP_GCM_RegistrationResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GP_GCM_RegistrationResult> action = ActionCMDRegistrationResult__BackingField;
			Action<GP_GCM_RegistrationResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionCMDRegistrationResult__BackingField, (Action<GP_GCM_RegistrationResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string, Dictionary<string, object>> ActionGCMPushLaunched
	{
		add
		{
			Action<string, Dictionary<string, object>> action = ActionGCMPushLaunched__BackingField;
			Action<string, Dictionary<string, object>> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionGCMPushLaunched__BackingField, (Action<string, Dictionary<string, object>>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string, Dictionary<string, object>> action = ActionGCMPushLaunched__BackingField;
			Action<string, Dictionary<string, object>> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionGCMPushLaunched__BackingField, (Action<string, Dictionary<string, object>>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string, Dictionary<string, object>> ActionGCMPushReceived
	{
		add
		{
			Action<string, Dictionary<string, object>> action = ActionGCMPushReceived__BackingField;
			Action<string, Dictionary<string, object>> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionGCMPushReceived__BackingField, (Action<string, Dictionary<string, object>>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string, Dictionary<string, object>> action = ActionGCMPushReceived__BackingField;
			Action<string, Dictionary<string, object>> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionGCMPushReceived__BackingField, (Action<string, Dictionary<string, object>>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string, Dictionary<string, object>> ActionParsePushReceived
	{
		add
		{
			Action<string, Dictionary<string, object>> action = ActionParsePushReceived__BackingField;
			Action<string, Dictionary<string, object>> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionParsePushReceived__BackingField, (Action<string, Dictionary<string, object>>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string, Dictionary<string, object>> action = ActionParsePushReceived__BackingField;
			Action<string, Dictionary<string, object>> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionParsePushReceived__BackingField, (Action<string, Dictionary<string, object>>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void Init()
	{
		switch (AndroidNativeSettings.Instance.PushService)
		{
		case AN_PushNotificationService.Google:
			InitPushNotifications();
			break;
		case AN_PushNotificationService.OneSignal:
			InitOneSignalNotifications();
			break;
		case AN_PushNotificationService.Parse:
			InitParsePushNotifications();
			break;
		}
	}

	public void InitOneSignalNotifications()
	{
	}

	public void InitPushNotifications()
	{
		AN_CloudMessagingProxy.InitPushNotifications((!(AndroidNativeSettings.Instance.PushNotificationSmallIcon == null)) ? AndroidNativeSettings.Instance.PushNotificationSmallIcon.name.ToLower() : string.Empty, (!(AndroidNativeSettings.Instance.PushNotificationLargeIcon == null)) ? AndroidNativeSettings.Instance.PushNotificationLargeIcon.name.ToLower() : string.Empty, (!(AndroidNativeSettings.Instance.PushNotificationSound == null)) ? AndroidNativeSettings.Instance.PushNotificationSound.name : string.Empty, AndroidNativeSettings.Instance.EnableVibrationPush, AndroidNativeSettings.Instance.ShowPushWhenAppIsForeground, AndroidNativeSettings.Instance.ReplaceOldNotificationWithNew, string.Format("{0}|{1}|{2}|{3}", 255f * AndroidNativeSettings.Instance.PushNotificationColor.a, 255f * AndroidNativeSettings.Instance.PushNotificationColor.r, 255f * AndroidNativeSettings.Instance.PushNotificationColor.g, 255f * AndroidNativeSettings.Instance.PushNotificationColor.b));
	}

	public void InitPushNotifications(string smallIcon, string largeIcon, string sound, bool enableVibrationPush, bool showWhenAppForeground, bool replaceOldNotificationWithNew, string color)
	{
		AN_CloudMessagingProxy.InitPushNotifications(smallIcon, largeIcon, sound, enableVibrationPush, showWhenAppForeground, replaceOldNotificationWithNew, color);
	}

	public void InitParsePushNotifications()
	{
		ParsePushesStub.InitParse();
		ParsePushesStub.OnPushReceived += HandleOnPushReceived;
	}

	public void RgisterDevice()
	{
		AN_CloudMessagingProxy.GCMRgisterDevice(AndroidNativeSettings.Instance.GCM_SenderId);
	}

	public void LoadLastMessage()
	{
		AN_CloudMessagingProxy.GCMLoadLastMessage();
	}

	public void RemoveLastMessageInfo()
	{
		AN_CloudMessagingProxy.GCMRemoveLastMessageInfo();
	}

	public void HideAll()
	{
		AN_CloudMessagingProxy.HideAllNotifications();
	}

	private void HandleOnPushReceived(string stringPayload, Dictionary<string, object> payload)
	{
		ActionParsePushReceived__BackingField(stringPayload, payload);
	}

	private void GCMNotificationCallback(string data)
	{
		UnityEngine.Debug.Log("[GCMNotificationCallback] JSON Data: " + data);
		string[] array = data.Split(new string[1] { "|" }, StringSplitOptions.None);
		string arg = array[0];
		Dictionary<string, object> arg2 = Json.Deserialize(array[1]) as Dictionary<string, object>;
		ActionGCMPushReceived__BackingField(arg, arg2);
	}

	private void GCMNotificationLaunchedCallback(string data)
	{
		UnityEngine.Debug.Log("[GCMNotificationLaunchedCallback] JSON Data: " + data);
		string[] array = data.Split(new string[1] { "|" }, StringSplitOptions.None);
		string arg = array[0];
		Dictionary<string, object> arg2 = Json.Deserialize(array[1]) as Dictionary<string, object>;
		ActionGCMPushLaunched__BackingField(arg, arg2);
	}

	private void OnLastMessageLoaded(string data)
	{
		_lastMessage = data;
		ActionCouldMessageLoaded__BackingField(lastMessage);
	}

	private void OnRegistrationReviced(string regId)
	{
		_registrationId = regId;
		ActionCMDRegistrationResult__BackingField(new GP_GCM_RegistrationResult(_registrationId));
	}

	private void OnRegistrationFailed()
	{
		ActionCMDRegistrationResult__BackingField(new GP_GCM_RegistrationResult());
	}
}
