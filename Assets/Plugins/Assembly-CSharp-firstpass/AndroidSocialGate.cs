using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using UnityEngine;

public class AndroidSocialGate : MonoBehaviour
{
	private static AndroidSocialGate _Instance = null;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<bool, string> OnShareIntentCallback__BackingField = delegate
	{
	};

	public static event Action<bool, string> OnShareIntentCallback
	{
		add
		{
			Action<bool, string> action = OnShareIntentCallback__BackingField;
			Action<bool, string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnShareIntentCallback__BackingField, (Action<bool, string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<bool, string> action = OnShareIntentCallback__BackingField;
			Action<bool, string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnShareIntentCallback__BackingField, (Action<bool, string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static void StartGooglePlusShare(string text, Texture2D texture = null)
	{
		CheckAndCreateInstance();
		AN_SocialSharingProxy.StartGooglePlusShareIntent(text, (!(texture == null)) ? Convert.ToBase64String(texture.EncodeToPNG()) : string.Empty);
	}

	public static void StartShareIntent(string caption, string message, string packageNamePattern = "")
	{
		CheckAndCreateInstance();
		StartShareIntentWithSubject(caption, message, string.Empty, packageNamePattern);
	}

	public static void StartShareIntent(string caption, string message, Texture2D texture, string packageNamePattern = "")
	{
		CheckAndCreateInstance();
		StartShareIntentWithSubject(caption, message, string.Empty, texture, packageNamePattern);
	}

	public static void StartShareIntentWithSubject(string caption, string message, string subject, string packageNamePattern = "")
	{
		CheckAndCreateInstance();
		AN_SocialSharingProxy.StartShareIntent(caption, message, subject, packageNamePattern);
	}

	public static void StartShareIntentWithSubject(string caption, string message, string subject, Texture2D texture, string packageNamePattern = "")
	{
		CheckAndCreateInstance();
		byte[] inArray = texture.EncodeToPNG();
		string media = Convert.ToBase64String(inArray);
		AN_SocialSharingProxy.StartShareIntent(caption, message, subject, media, packageNamePattern, (int)AndroidNativeSettings.Instance.ImageFormat, AndroidNativeSettings.Instance.SaveCameraImageToGallery);
	}

	public static void SendMail(string caption, string message, string subject, string recipients, Texture2D texture = null)
	{
		CheckAndCreateInstance();
		if (texture != null)
		{
			byte[] inArray = texture.EncodeToPNG();
			string media = Convert.ToBase64String(inArray);
			AN_SocialSharingProxy.SendMailWithImage(caption, message, subject, recipients, media, (int)AndroidNativeSettings.Instance.ImageFormat, AndroidNativeSettings.Instance.SaveCameraImageToGallery);
		}
		else
		{
			AN_SocialSharingProxy.SendMail(caption, message, subject, recipients);
		}
	}

	public static void ShareTwitterGif(string gifPath, string message)
	{
		AN_SocialSharingProxy.ShareTwitterGif(gifPath, message);
	}

	public static void SendTextMessage(string body, string recepient)
	{
		List<string> list = new List<string>();
		list.Add(recepient);
		SendTextMessage(body, list);
	}

	public static void SendTextMessage(string body, List<string> recipients)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (string recipient in recipients)
		{
			stringBuilder.Append(recipient);
			stringBuilder.Append("|");
		}
		AN_SocialSharingProxy.SendTextMessage(body, stringBuilder.ToString());
	}

	private static void CheckAndCreateInstance()
	{
		if (_Instance == null)
		{
			_Instance = UnityEngine.Object.FindObjectOfType(typeof(AndroidSocialGate)) as AndroidSocialGate;
			if (_Instance == null)
			{
				_Instance = new GameObject().AddComponent<AndroidSocialGate>();
				_Instance.gameObject.name = _Instance.GetType().Name;
			}
		}
	}

	private void ShareCallback(string data)
	{
		string[] array = data.Split(new string[1] { "|" }, StringSplitOptions.None);
		bool flag = int.Parse(array[1]) == -1;
		OnShareIntentCallback__BackingField(flag, array[0]);
		UnityEngine.Debug.Log("[AndroidSocialGate]ShareCallback Posted:" + flag + " Package:" + array[0]);
	}
}
