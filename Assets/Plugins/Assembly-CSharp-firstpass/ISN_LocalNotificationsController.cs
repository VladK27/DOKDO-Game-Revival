using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class ISN_LocalNotificationsController : Singleton<ISN_LocalNotificationsController>
{
	private const string PP_KEY = "IOSNotificationControllerKey";

	private const string PP_ID_KEY = "IOSNotificationControllerrKey_ID";

	private ISN_LocalNotification _LaunchNotification;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<Result> OnNotificationScheduleResult__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<ISN_LocalNotification> OnLocalNotificationReceived__BackingField = delegate
	{
	};

	public static int AllowedNotificationsType
	{
		get
		{
			return 0;
		}
	}

	public ISN_LocalNotification LaunchNotification
	{
		get
		{
			return _LaunchNotification;
		}
	}

	public static event Action<Result> OnNotificationScheduleResult
	{
		add
		{
			Action<Result> action = OnNotificationScheduleResult__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnNotificationScheduleResult__BackingField, (Action<Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Result> action = OnNotificationScheduleResult__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnNotificationScheduleResult__BackingField, (Action<Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<ISN_LocalNotification> OnLocalNotificationReceived
	{
		add
		{
			Action<ISN_LocalNotification> action = OnLocalNotificationReceived__BackingField;
			Action<ISN_LocalNotification> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLocalNotificationReceived__BackingField, (Action<ISN_LocalNotification>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<ISN_LocalNotification> action = OnLocalNotificationReceived__BackingField;
			Action<ISN_LocalNotification> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLocalNotificationReceived__BackingField, (Action<ISN_LocalNotification>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void RequestNotificationPermissions()
	{
		if (ISN_Device.CurrentDevice.MajorSystemVersion >= 8)
		{
		}
	}

	public void ShowGmaeKitNotification(string title, string message)
	{
		GameCenterManager.ShowGmaeKitNotification(title, message);
	}

	public void CancelAllLocalNotifications()
	{
		SaveNotifications(new List<ISN_LocalNotification>());
	}

	public void CancelLocalNotification(ISN_LocalNotification notification)
	{
		CancelLocalNotificationById(notification.Id);
	}

	public void CancelLocalNotificationById(int notificationId)
	{
	}

	public void ScheduleNotification(ISN_LocalNotification notification)
	{
	}

	public List<ISN_LocalNotification> LoadPendingNotifications(bool includeAll = false)
	{
		return null;
	}

	public void ApplicationIconBadgeNumber(int badges)
	{
	}

	private void OnNotificationScheduleResultAction(string array)
	{
		string[] array2 = array.Split("|"[0]);
		Result result = null;
		result = ((!array2[0].Equals("0")) ? new Result() : new Result(new Error()));
		OnNotificationScheduleResult__BackingField(result);
	}

	private void OnLocalNotificationReceived_Event(string array)
	{
		string[] array2 = array.Split("|"[0]);
		string message = array2[0];
		int id = Convert.ToInt32(array2[1]);
		string data = array2[2];
		int badgesNumber = Convert.ToInt32(array2[3]);
		ISN_LocalNotification iSN_LocalNotification = new ISN_LocalNotification(DateTime.Now, message);
		iSN_LocalNotification.SetData(data);
		iSN_LocalNotification.SetBadgesNumber(badgesNumber);
		iSN_LocalNotification.SetId(id);
		OnLocalNotificationReceived__BackingField(iSN_LocalNotification);
	}

	private void SaveNotifications(List<ISN_LocalNotification> notifications)
	{
		if (notifications.Count == 0)
		{
			PlayerPrefs.DeleteKey("IOSNotificationControllerKey");
			return;
		}
		string text = string.Empty;
		int count = notifications.Count;
		for (int i = 0; i < count; i++)
		{
			if (i != 0)
			{
				text += '|';
			}
			text += notifications[i].SerializedString;
		}
		PlayerPrefs.SetString("IOSNotificationControllerKey", text);
	}
}
