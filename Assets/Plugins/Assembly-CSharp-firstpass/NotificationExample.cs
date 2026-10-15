using System;
using System.Collections.Generic;
using SA.Common.Models;
using SA.Common.Pattern;
using SA.IOSNative.UserNotifications;
using UnityEngine;

public class NotificationExample : BaseIOSFeaturePreview
{
	private int lastNotificationId;

	private void Awake()
	{
		ISN_LocalNotificationsController.OnLocalNotificationReceived += HandleOnLocalNotificationReceived;
		if (Singleton<ISN_LocalNotificationsController>.Instance.LaunchNotification != null)
		{
			ISN_LocalNotification launchNotification = Singleton<ISN_LocalNotificationsController>.Instance.LaunchNotification;
			IOSMessage.Create("Launch Notification", "Messgae: " + launchNotification.Message + "\nNotification Data: " + launchNotification.Data);
		}
		if (Singleton<ISN_RemoteNotificationsController>.Instance.LaunchNotification != null)
		{
			ISN_RemoteNotification launchNotification2 = Singleton<ISN_RemoteNotificationsController>.Instance.LaunchNotification;
			IOSMessage.Create("Launch Remote Notification", "Body: " + launchNotification2.Body);
		}
	}

	private void OnGUI()
	{
		UpdateToStartPos();
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Local and Push Notifications", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Request Permissions"))
		{
			Singleton<ISN_LocalNotificationsController>.Instance.RequestNotificationPermissions();
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Print Notification Settings"))
		{
			CheckNotificationSettings();
		}
		StartY += YButtonStep;
		StartX = XStartPos;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Schedule Notification Silent"))
		{
			ISN_LocalNotificationsController.OnNotificationScheduleResult += OnNotificationScheduleResult;
			ISN_LocalNotification iSN_LocalNotification = new ISN_LocalNotification(DateTime.Now.AddSeconds(5.0), "Your Notification Text No Sound", false);
			iSN_LocalNotification.SetData("some_test_data");
			iSN_LocalNotification.Schedule();
			lastNotificationId = iSN_LocalNotification.Id;
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Schedule Notification"))
		{
			ISN_LocalNotificationsController.OnNotificationScheduleResult += OnNotificationScheduleResult;
			ISN_LocalNotification iSN_LocalNotification2 = new ISN_LocalNotification(DateTime.Now.AddSeconds(5.0), "Your Notification Text");
			iSN_LocalNotification2.SetData("some_test_data");
			iSN_LocalNotification2.SetSoundName("purchase_ok.wav");
			iSN_LocalNotification2.SetBadgesNumber(1);
			iSN_LocalNotification2.Schedule();
			lastNotificationId = iSN_LocalNotification2.Id;
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Cancel All Notifications"))
		{
			Singleton<ISN_LocalNotificationsController>.Instance.CancelAllLocalNotifications();
			IOSNativeUtility.SetApplicationBagesNumber(0);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Cansel Last Notification"))
		{
			Singleton<ISN_LocalNotificationsController>.Instance.CancelLocalNotificationById(lastNotificationId);
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		StartY += YLableStep;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Local and Push Notifications", style);
		StartY += YLableStep;
		StartX = XStartPos;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Reg Device For Push Notif. "))
		{
			Singleton<ISN_RemoteNotificationsController>.Instance.RegisterForRemoteNotifications((ISN_RemoteNotificationsRegistrationResult res) =>
			{
				Debug.Log("ISN_RemoteNotificationsRegistrationResult: " + res.IsSucceeded);
				if (!res.IsSucceeded)
				{
					Debug.Log(res.Error.Code + " / " + res.Error.Message);
				}
				else
				{
					Debug.Log(res.Token.DeviceId);
				}
			});
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Game Kit Notification"))
		{
			Singleton<ISN_LocalNotificationsController>.Instance.ShowGmaeKitNotification("Title", "Message");
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		StartY += YLableStep;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "User Notifications", style);
		StartY += YLableStep;
		StartX = XStartPos;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Reg Device For User Notif. "))
		{
			NotificationCenter.RequestPermissions((Result result) =>
			{
				ISN_Logger.Log("RequestPermissions callback" + result.ToString());
			});
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Schedule for 5 sec"))
		{
			NotificationContent notificationContent = new NotificationContent();
			notificationContent.Title = "Title_";
			notificationContent.Subtitle = "Subtitle_";
			notificationContent.Body = "Body_";
			notificationContent.Badge = 1;
			notificationContent.UserInfo["404"] = "test User Info";
			TimeIntervalTrigger trigger = new TimeIntervalTrigger(5);
			NotificationRequest request = new NotificationRequest("some0id0", notificationContent, trigger);
			ISN_Logger.Log("request Schedule for 5 sec");
			NotificationCenter.AddNotificationRequest(request, (Result result) =>
			{
				ISN_Logger.Log("request callback");
				ISN_Logger.Log(result.ToString());
			});
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Schedule by Calendar - Date Components"))
		{
			NotificationContent notificationContent2 = new NotificationContent();
			notificationContent2.Title = "Calendar - Date Components";
			notificationContent2.Subtitle = "Subtitle_";
			notificationContent2.Body = "Body_";
			notificationContent2.Badge = 1;
			notificationContent2.UserInfo["404"] = "test User Info";
			DateComponents dateComponents = new DateComponents();
			dateComponents.Second = 32;
			DateComponents dateComponents2 = dateComponents;
			CalendarTrigger calendarTrigger = new CalendarTrigger(dateComponents2);
			calendarTrigger.SetRepeat(true);
			NotificationRequest request2 = new NotificationRequest("some0id1", notificationContent2, calendarTrigger);
			NotificationCenter.AddNotificationRequest(request2, (Result result) =>
			{
				ISN_Logger.Log("request callback");
				ISN_Logger.Log(result.ToString());
			});
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Schedule by Calendar - Date"))
		{
			NotificationContent notificationContent3 = new NotificationContent();
			notificationContent3.Title = "Calendar - Date";
			notificationContent3.Subtitle = "Subtitle_";
			notificationContent3.Body = "Body_";
			notificationContent3.Badge = 1;
			notificationContent3.UserInfo["404"] = 1;
			DateComponents dateComponents = new DateComponents();
			dateComponents.Second = 32;
			dateComponents.Year = 2017;
			dateComponents.Month = 6;
			dateComponents.Day = 6;
			dateComponents.Hour = 13;
			dateComponents.Minute = 1;
			DateComponents dateComponents3 = dateComponents;
			CalendarTrigger trigger2 = new CalendarTrigger(dateComponents3);
			NotificationRequest request3 = new NotificationRequest("some0id2", notificationContent3, trigger2);
			NotificationCenter.AddNotificationRequest(request3, (Result result) =>
			{
				ISN_Logger.Log("request callback");
				ISN_Logger.Log(result.ToString());
			});
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Cancel All User Notifications"))
		{
			NotificationCenter.CancelAllNotifications();
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Cancel some0id2"))
		{
			NotificationCenter.CancelUserNotificationById("some0id2");
		}
		StartX += XButtonStep;
		if (!GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Get Pending UserNotifications"))
		{
			return;
		}
		NotificationCenter.GetPendingNotificationRequests((List<NotificationRequest> requests) =>
		{
			for (int i = 0; i < requests.Count; i++)
			{
				ISN_Logger.Log(requests[i].Content.Title);
			}
		});
	}

	public void CheckNotificationSettings()
	{
		int allowedNotificationsType = ISN_LocalNotificationsController.AllowedNotificationsType;
		Debug.Log("AllowedNotificationsType: " + allowedNotificationsType);
		if ((allowedNotificationsType & 2) != 0)
		{
			Debug.Log("Sound avaliable");
		}
		if ((allowedNotificationsType & 1) != 0)
		{
			Debug.Log("Badge avaliable");
		}
		if ((allowedNotificationsType & 4) != 0)
		{
			Debug.Log("Alert avaliable");
		}
	}

	private void HandleOnLocalNotificationReceived(ISN_LocalNotification notification)
	{
		IOSMessage.Create("Notification Received", "Messgae: " + notification.Message + "\nNotification Data: " + notification.Data);
	}

	private void OnNotificationScheduleResult(Result res)
	{
		ISN_LocalNotificationsController.OnNotificationScheduleResult -= OnNotificationScheduleResult;
		string empty = string.Empty;
		empty = ((!res.IsSucceeded) ? (empty + "Notification scheduling failed") : (empty + "Notification was successfully scheduled\n allowed notifications types: \n"));
		IOSMessage.Create("On Notification Schedule Result", empty);
	}
}
