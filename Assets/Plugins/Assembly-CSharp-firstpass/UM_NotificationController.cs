using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class UM_NotificationController : Singleton<UM_NotificationController>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<UM_PushRegistrationResult> OnPushIdLoadResult__BackingField = delegate
	{
	};

	private bool IsPushListnersRegistred;

	public static event Action<UM_PushRegistrationResult> OnPushIdLoadResult
	{
		add
		{
			Action<UM_PushRegistrationResult> action = OnPushIdLoadResult__BackingField;
			Action<UM_PushRegistrationResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPushIdLoadResult__BackingField, (Action<UM_PushRegistrationResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_PushRegistrationResult> action = OnPushIdLoadResult__BackingField;
			Action<UM_PushRegistrationResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPushIdLoadResult__BackingField, (Action<UM_PushRegistrationResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void RetrieveDevicePushId()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			if (!IsPushListnersRegistred)
			{
				GoogleCloudMessageService.ActionCMDRegistrationResult += HandleActionCMDRegistrationResult;
			}
			Singleton<GoogleCloudMessageService>.Instance.RgisterDevice();
			break;
		}
		IsPushListnersRegistred = true;
	}

	public void ShowNotificationPoup(string title, string messgae)
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			Singleton<AndroidNotificationManager>.Instance.ShowToastNotification(messgae);
			break;
		case RuntimePlatform.IPhonePlayer:
			Singleton<ISN_LocalNotificationsController>.Instance.ShowGmaeKitNotification(title, messgae);
			break;
		}
	}

	public int ScheduleLocalNotification(string title, string message, int seconds)
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			return Singleton<AndroidNotificationManager>.Instance.ScheduleLocalNotification(title, message, seconds);
		case RuntimePlatform.IPhonePlayer:
		{
			ISN_LocalNotification iSN_LocalNotification = new ISN_LocalNotification(DateTime.Now.AddSeconds(seconds), message);
			iSN_LocalNotification.Schedule();
			return iSN_LocalNotification.Id;
		}
		default:
			return 0;
		}
	}

	public void CancelLocalNotification(int id)
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			Singleton<AndroidNotificationManager>.Instance.CancelLocalNotification(id);
			break;
		case RuntimePlatform.IPhonePlayer:
			Singleton<ISN_LocalNotificationsController>.Instance.CancelLocalNotificationById(id);
			break;
		}
	}

	public void CancelAllLocalNotifications()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			Singleton<AndroidNotificationManager>.Instance.CancelAllLocalNotifications();
			break;
		case RuntimePlatform.IPhonePlayer:
			Singleton<ISN_LocalNotificationsController>.Instance.CancelAllLocalNotifications();
			break;
		}
	}

	private void HandleActionCMDRegistrationResult(GP_GCM_RegistrationResult res)
	{
		if (res.IsSucceeded)
		{
			OnRegstred();
		}
		else
		{
			OnRegFailed();
		}
	}

	private void OnRegFailed()
	{
		UM_PushRegistrationResult obj = new UM_PushRegistrationResult(string.Empty, false);
		OnPushIdLoadResult__BackingField(obj);
	}

	private void OnRegstred()
	{
		UM_PushRegistrationResult obj = new UM_PushRegistrationResult(Singleton<GoogleCloudMessageService>.Instance.registrationId, true);
		OnPushIdLoadResult__BackingField(obj);
	}

	private void IOSPushTokenReceived(ISN_RemoteNotificationsRegistrationResult res)
	{
		UM_PushRegistrationResult obj = new UM_PushRegistrationResult(res.Token.DeviceId, true);
		OnPushIdLoadResult__BackingField(obj);
	}
}
