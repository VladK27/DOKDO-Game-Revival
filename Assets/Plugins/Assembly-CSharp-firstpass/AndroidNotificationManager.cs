using System;
using System.Collections.Generic;
using System.Text;
using SA.Common.Pattern;
using UnityEngine;

public class AndroidNotificationManager : Singleton<AndroidNotificationManager>
{
	public const int LENGTH_SHORT = 0;

	public const int LENGTH_LONG = 1;

	public Action<int> OnNotificationIdLoaded = delegate
	{
	};

	private const string PP_KEY = "AndroidNotificationManagerKey";

	private const string PP_ID_KEY = "AndroidNotificationManagerKey_ID";

	private const string DATA_SPLITTER = "|";

	public int GetNextId
	{
		get
		{
			int num = 1;
			if (PlayerPrefs.HasKey("AndroidNotificationManagerKey_ID"))
			{
				num = PlayerPrefs.GetInt("AndroidNotificationManagerKey_ID");
				num++;
			}
			PlayerPrefs.SetInt("AndroidNotificationManagerKey_ID", num);
			return num;
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void LocadAppLaunchNotificationId()
	{
		AN_NotificationProxy.requestCurrentAppLaunchNotificationId();
	}

	public void HideAllNotifications()
	{
		AN_NotificationProxy.HideAllNotifications();
	}

	public void ShowToastNotification(string text)
	{
		ShowToastNotification(text, 0);
	}

	public void ShowToastNotification(string text, int duration)
	{
		AN_NotificationProxy.ShowToastNotification(text, duration);
	}

	public int ScheduleLocalNotification(string title, string message, int seconds)
	{
		AndroidNotificationBuilder builder = new AndroidNotificationBuilder(GetNextId, title, message, seconds);
		return ScheduleLocalNotification(builder);
	}

	public int ScheduleLocalNotification(AndroidNotificationBuilder builder)
	{
		AN_NotificationProxy.ScheduleLocalNotification(builder);
		LocalNotificationTemplate item = new LocalNotificationTemplate(builder.Id, builder.Title, builder.Message, DateTime.Now.AddSeconds(builder.Time));
		List<LocalNotificationTemplate> list = LoadPendingNotifications();
		list.Add(item);
		SaveNotifications(list);
		return builder.Id;
	}

	public void CancelLocalNotification(int id, bool clearFromPrefs = true)
	{
		AN_NotificationProxy.CanselLocalNotification(id);
		if (!clearFromPrefs)
		{
			return;
		}
		List<LocalNotificationTemplate> list = LoadPendingNotifications();
		List<LocalNotificationTemplate> list2 = new List<LocalNotificationTemplate>();
		foreach (LocalNotificationTemplate item in list)
		{
			if (item.id != id)
			{
				list2.Add(item);
			}
		}
		SaveNotifications(list2);
	}

	public void CancelAllLocalNotifications()
	{
		List<LocalNotificationTemplate> list = LoadPendingNotifications();
		foreach (LocalNotificationTemplate item in list)
		{
			CancelLocalNotification(item.id, false);
		}
		SaveNotifications(new List<LocalNotificationTemplate>());
	}

	private void OnNotificationIdLoadedEvent(string data)
	{
		int obj = Convert.ToInt32(data);
		OnNotificationIdLoaded(obj);
	}

	private void SaveNotifications(List<LocalNotificationTemplate> notifications)
	{
		if (notifications.Count == 0)
		{
			PlayerPrefs.DeleteKey("AndroidNotificationManagerKey");
			return;
		}
		string text = string.Empty;
		int count = notifications.Count;
		for (int i = 0; i < count; i++)
		{
			if (i != 0)
			{
				text += "|";
			}
			text += notifications[i].SerializedString;
		}
		PlayerPrefs.SetString("AndroidNotificationManagerKey", text);
	}

	public List<LocalNotificationTemplate> LoadPendingNotifications(bool includeAll = false)
	{
		string text = string.Empty;
		if (PlayerPrefs.HasKey("AndroidNotificationManagerKey"))
		{
			text = PlayerPrefs.GetString("AndroidNotificationManagerKey");
		}
		List<LocalNotificationTemplate> list = new List<LocalNotificationTemplate>();
		if (text != string.Empty)
		{
			string[] array = text.Split("|"[0]);
			string[] array2 = array;
			foreach (string s in array2)
			{
				string data = Encoding.UTF8.GetString(Convert.FromBase64String(s));
				try
				{
					LocalNotificationTemplate localNotificationTemplate = new LocalNotificationTemplate(data);
					if (!localNotificationTemplate.IsFired || includeAll)
					{
						list.Add(localNotificationTemplate);
					}
				}
				catch (Exception ex)
				{
					Debug.Log("AndroidNative. AndroidNotificationManager loading notification data failed: " + ex.Message);
				}
			}
		}
		return list;
	}
}
