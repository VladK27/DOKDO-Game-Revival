using System.Collections.Generic;
using ANMiniJSON;
using SA.Common.Pattern;
using SA.Common.Util;
using UnityEngine;

public sealed class NotificationsTab : FeatureTab
{
	[SerializeField]
	private Texture2D bigPicture;

	private int LastNotificationId;

	private void Awake()
	{
		GoogleCloudMessageService.ActionCMDRegistrationResult += HandleActionCMDRegistrationResult;
		GoogleCloudMessageService.ActionCouldMessageLoaded += OnMessageLoaded;
		GoogleCloudMessageService.ActionGCMPushLaunched += HandleActionGCMPushLaunched;
		GoogleCloudMessageService.ActionGCMPushReceived += HandleActionGCMPushReceived;
		Singleton<GoogleCloudMessageService>.Instance.Init();
	}

	public void OnShowToastButton()
	{
		AndroidToast.ShowToastNotification("Hello Toast", 1);
	}

	public void OnScheduleLocal()
	{
		AndroidNotificationBuilder androidNotificationBuilder = new AndroidNotificationBuilder(IdFactory.NextId, "Local Notification Title", "This is local notification", 10);
		androidNotificationBuilder.SetBigPicture(bigPicture);
		Singleton<AndroidNotificationManager>.Instance.ScheduleLocalNotification(androidNotificationBuilder);
	}

	public void OnCancelLastLocal()
	{
		Singleton<AndroidNotificationManager>.Instance.CancelLocalNotification(LastNotificationId);
	}

	public void OnCancelAll()
	{
		Singleton<AndroidNotificationManager>.Instance.CancelAllLocalNotifications();
	}

	public void OnRegisterDevice()
	{
		Singleton<GoogleCloudMessageService>.Instance.RgisterDevice();
	}

	public void OnLoadLastGcmMessage()
	{
		Singleton<GoogleCloudMessageService>.Instance.LoadLastMessage();
	}

	private void HandleActionGCMPushReceived(string message, Dictionary<string, object> data)
	{
		Debug.Log("[HandleActionGCMPushReceived]");
		Debug.Log("Message: " + message);
		foreach (KeyValuePair<string, object> datum in data)
		{
			Debug.Log("Data Entity: " + datum.Key + " " + datum.Value.ToString());
		}
		AN_PoupsProxy.showMessage(message, Json.Serialize(data));
	}

	private void HandleActionGCMPushLaunched(string message, Dictionary<string, object> data)
	{
		Debug.Log("[HandleActionGCMPushLaunched]");
		Debug.Log("Message: " + message);
		foreach (KeyValuePair<string, object> datum in data)
		{
			Debug.Log("Data Entity: " + datum.Key + " " + datum.Value.ToString());
		}
		AN_PoupsProxy.showMessage(message, Json.Serialize(data));
	}

	private void HandleActionCMDRegistrationResult(GP_GCM_RegistrationResult res)
	{
		if (res.IsSucceeded)
		{
			AN_PoupsProxy.showMessage("Regstred", "GCM REG ID: " + Singleton<GoogleCloudMessageService>.Instance.registrationId);
		}
		else
		{
			AN_PoupsProxy.showMessage("Reg Failed", "GCM Registration failed :(");
		}
	}

	private void OnNotificationIdLoaded(int notificationid)
	{
		AN_PoupsProxy.showMessage("Loaded", "App was laucnhed with notification id: " + notificationid);
	}

	private void OnMessageLoaded(string msg)
	{
		AN_PoupsProxy.showMessage("Message Loaded", "Last GCM Message: " + Singleton<GoogleCloudMessageService>.Instance.lastMessage);
	}
}
