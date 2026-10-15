using System;
using SA.Common.Models;
using SA.Common.Pattern;
using SA.IOSNative.Contacts;
using SA.IOSNative.Gestures;
using SA.IOSNative.Privacy;
using SA.IOSNative.System;
using SA.IOSNative.UIKit;
using UnityEngine;

public class NativeIOSActionsExample : BaseIOSFeaturePreview
{
	public Texture2D hello_texture;

	public Texture2D drawTexture;

	private DateTime time;

	private void Awake()
	{
		time = new DateTime(1997, 5, 11);
		Debug.Log("Subscribed");
		Singleton<ISN_GestureRecognizer>.Instance.OnSwipe += (ISN_SwipeDirection direction) =>
		{
			Debug.Log("Swipe: " + direction);
		};
		Singleton<ForceTouch>.Instance.Setup(0.5f, 1f, 2.5f);
		Singleton<ForceTouch>.Instance.OnForceTouchStarted += () =>
		{
			Debug.Log("OnForceTouchStarted");
		};
		Singleton<ForceTouch>.Instance.OnForceChanged += (ForceInfo info) =>
		{
			Debug.Log("OnForceChanged: " + info.Force + " / " + info.MaxForce);
		};
		Singleton<ForceTouch>.Instance.OnForceTouchFinished += () =>
		{
			Debug.Log("OnForceTouchFinished");
		};
		Debug.Log(ForceTouch.AppOpenshortcutItem);
		Singleton<ForceTouch>.Instance.OnAppShortcutClick += (string action) =>
		{
			Debug.Log("Menu Item With action: " + action + " was clicked");
		};
	}

	private void OnGUI()
	{
		UpdateToStartPos();
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "System Utils", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Check if FB App exists"))
		{
			if (SharedApplication.CheckUrl("fb://"))
			{
				IOSMessage.Create("Success", "Facebook App is installed on current device");
			}
			else
			{
				IOSMessage.Create("ERROR", "Facebook App is not installed on current device");
			}
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Open FB Profile"))
		{
			SharedApplication.OpenUrl("fb://profile");
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Open App Store"))
		{
			SharedApplication.OpenUrl("itms-apps://");
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Get IFA"))
		{
			IOSMessage.Create("Identifier Loaded", ISN_Device.CurrentDevice.AdvertisingIdentifier);
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Set App Bages Count"))
		{
			IOSNativeUtility.SetApplicationBagesNumber(10);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Clear Application Bages"))
		{
			IOSNativeUtility.SetApplicationBagesNumber(0);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Device Info"))
		{
			ShowDevoceInfo();
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Pick Contacts UI"))
		{
			Singleton<ContactStore>.Instance.ShowContactsPickerUI((ContactsResult result) =>
			{
				if (result.IsSucceeded)
				{
					Debug.Log("Picked " + result.Contacts.Count + " contacts");
					IOSMessage.Create("Success", "Picked " + result.Contacts.Count + " contacts");
					{
						foreach (Contact contact in result.Contacts)
						{
							Debug.Log("contact.GivenName: " + contact.GivenName);
							if (contact.PhoneNumbers.Count > 0)
							{
								Debug.Log("contact.PhoneNumber: " + contact.PhoneNumbers[0].Digits);
							}
							if (contact.Emails.Count > 0)
							{
								Debug.Log("contact.Email: " + contact.Emails[0]);
							}
						}
						return;
					}
				}
				IOSMessage.Create("Error", result.Error.Code + " / " + result.Error.Message);
			});
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Load Contacts"))
		{
			Singleton<ContactStore>.Instance.RetrievePhoneContacts((ContactsResult result) =>
			{
				if (result.IsSucceeded)
				{
					Debug.Log("Loaded " + result.Contacts.Count + " contacts");
					IOSMessage.Create("Success", "Loaded " + result.Contacts.Count + " contacts");
					{
						foreach (Contact contact2 in result.Contacts)
						{
							if (contact2.PhoneNumbers.Count > 0)
							{
								Debug.Log(contact2.GivenName + " / " + contact2.PhoneNumbers[0].Digits);
							}
						}
						return;
					}
				}
				IOSMessage.Create("Error", result.Error.Code + " / " + result.Error.Message);
			});
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		StartY += YLableStep;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Date Time Picker", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Time"))
		{
			DateTimePicker.Show(DateTimePickerMode.Time, (DateTime dateTime) =>
			{
			});
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Date"))
		{
			DateTimePicker.Show(DateTimePickerMode.Date, (DateTime dateTime) =>
			{
			});
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Date And Time"))
		{
			DateTimePicker.Show(DateTimePickerMode.DateAndTime, (DateTime dateTime) =>
			{
			});
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Countdown Timer"))
		{
			DateTimePicker.Show(DateTimePickerMode.CountdownTimer, (DateTime dateTime) =>
			{
			});
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Set Date Without UI"))
		{
			DateTimePicker.Show(DateTimePickerMode.Date, time, (DateTime dateTime) =>
			{
			});
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Calendar Picker"))
		{
			Calendar.PickDate((DateTime dateTime) =>
			{
				ISN_Logger.Log("OnDateChanged: " + dateTime.ToShortDateString());
			}, 1989);
			SA.IOSNative.Privacy.PermissionsManager.RequestPermission(Permission.NSPhotoLibrary, (PermissionStatus permissionStatus) =>
			{
				Debug.Log(string.Concat("PermissionsManager.RequestPermission: ", Permission.NSPhotoLibrary, permissionStatus.ToString()));
			});
			SA.IOSNative.Privacy.PermissionsManager.RequestPermission(Permission.NSLocationWhenInUse, (PermissionStatus permissionStatus) =>
			{
				Debug.Log(string.Concat("PermissionsManager.RequestPermission: ", Permission.NSLocationWhenInUse, permissionStatus.ToString()));
			});
			SA.IOSNative.Privacy.PermissionsManager.RequestPermission(Permission.NSMicrophone, (PermissionStatus permissionStatus) =>
			{
				Debug.Log(string.Concat("PermissionsManager.RequestPermission: ", Permission.NSMicrophone, permissionStatus.ToString()));
			});
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		StartY += YLableStep;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Video", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Player Streamed video"))
		{
			Singleton<IOSVideoManager>.Instance.PlayStreamingVideo("https://dl.dropboxusercontent.com/u/83133800/Important/Doosan/GT2100-Video.mov");
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Open YouTube Video"))
		{
			Singleton<IOSVideoManager>.Instance.OpenYouTubeVideo("xzCEdSKMkdU");
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		StartY += YLableStep;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Camera Roll", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth + 10, buttonHeight), "Save Screenshot To Camera Roll"))
		{
			IOSCamera.OnImageSaved += OnImageSaved;
			Singleton<IOSCamera>.Instance.SaveScreenshotToCameraRoll();
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Save Texture To Camera Roll"))
		{
			IOSCamera.OnImageSaved += OnImageSaved;
			Singleton<IOSCamera>.Instance.SaveTextureToCameraRoll(hello_texture);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Get Image From Camera"))
		{
			IOSCamera.OnImagePicked += OnImage;
			Singleton<IOSCamera>.Instance.PickImage(ISN_ImageSource.Camera);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Get Image From Album"))
		{
			IOSCamera.OnImagePicked += OnImage;
			Singleton<IOSCamera>.Instance.PickImage(ISN_ImageSource.Album);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Load Multiple Images"))
		{
			ISN_FilePicker.MediaPickFinished += (ISN_FilePickerResult res) =>
			{
				Debug.Log("Picked " + res.PickedImages.Count + " images");
				if (res.PickedImages.Count != 0)
				{
					UnityEngine.Object.Destroy(drawTexture);
					drawTexture = res.PickedImages[0];
				}
			};
			Singleton<ISN_FilePicker>.Instance.PickFromCameraRoll();
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		StartY += YLableStep;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "PickedImage", style);
		StartY += YLableStep;
		if (drawTexture != null)
		{
			GUI.DrawTexture(new Rect(StartX, StartY, buttonWidth, buttonWidth), drawTexture);
		}
	}

	private void ShowDevoceInfo()
	{
		ISN_Device currentDevice = ISN_Device.CurrentDevice;
		IOSMessage.Create("Device Info", string.Concat("Name: ", currentDevice.Name, "\nSystem Name: ", currentDevice.SystemName, "\nModel: ", currentDevice.Model, "\nLocalized Model: ", currentDevice.LocalizedModel, "\nSystem Version: ", currentDevice.SystemVersion, "\nMajor System Version: ", currentDevice.MajorSystemVersion, "\nPreferred Language Code: ", currentDevice.PreferredLanguageCode, "\nPreferred Language_ISO639_1: ", currentDevice.PreferredLanguage_ISO639_1, "\nUser Interface Idiom: ", currentDevice.InterfaceIdiom, "\nGUID in Base64: ", currentDevice.GUID.Base64String));
		Debug.Log("ISN_TimeZone.LocalTimeZone.Name: " + ISN_TimeZone.LocalTimeZone.Name);
		Debug.Log("ISN_TimeZone.LocalTimeZone.SecondsFromGMT: " + ISN_TimeZone.LocalTimeZone.SecondsFromGMT);
		Debug.Log("ISN_TimeZone.LocalTimeZone.Name: " + ISN_Build.Current.Version);
		Debug.Log("ISN_TimeZone.LocalTimeZone.Name: " + ISN_Build.Current.Number);
	}

	private void OnDateChanged(DateTime time)
	{
		ISN_Logger.Log("OnDateChanged: " + time.ToString());
	}

	private void OnPickerClosed(DateTime time)
	{
		ISN_Logger.Log("OnPickerClosed: " + time.ToString());
	}

	private void OnImage(IOSImagePickResult result)
	{
		if (result.IsSucceeded)
		{
			UnityEngine.Object.Destroy(drawTexture);
			drawTexture = result.Image;
			IOSMessage.Create("Success", "Image Successfully Loaded, Image size: " + result.Image.width + "x" + result.Image.height);
		}
		else
		{
			IOSMessage.Create("ERROR", "Image Load Failed");
		}
		IOSCamera.OnImagePicked -= OnImage;
	}

	private void OnImageSaved(Result result)
	{
		IOSCamera.OnImageSaved -= OnImageSaved;
		if (result.IsSucceeded)
		{
			IOSMessage.Create("Success", "Image Successfully saved to Camera Roll");
		}
		else
		{
			IOSMessage.Create("ERROR", "Image Save Failed");
		}
	}
}
