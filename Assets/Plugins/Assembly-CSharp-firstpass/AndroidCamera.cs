using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using SA.Common.Util;
using UnityEngine;

public class AndroidCamera : Singleton<AndroidCamera>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AndroidImagePickResult> OnImagePicked__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AndroidImagesPickResult> OnImagesPicked__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<GallerySaveResult> OnImageSaved__BackingField = delegate
	{
	};

	private static string _lastImageName = string.Empty;

	public event Action<AndroidImagePickResult> OnImagePicked
	{
		add
		{
			Action<AndroidImagePickResult> action = OnImagePicked__BackingField;
			Action<AndroidImagePickResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnImagePicked__BackingField, (Action<AndroidImagePickResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AndroidImagePickResult> action = OnImagePicked__BackingField;
			Action<AndroidImagePickResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnImagePicked__BackingField, (Action<AndroidImagePickResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<AndroidImagesPickResult> OnImagesPicked
	{
		add
		{
			Action<AndroidImagesPickResult> action = OnImagesPicked__BackingField;
			Action<AndroidImagesPickResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnImagesPicked__BackingField, (Action<AndroidImagesPickResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AndroidImagesPickResult> action = OnImagesPicked__BackingField;
			Action<AndroidImagesPickResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnImagesPicked__BackingField, (Action<AndroidImagesPickResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<GallerySaveResult> OnImageSaved
	{
		add
		{
			Action<GallerySaveResult> action = OnImageSaved__BackingField;
			Action<GallerySaveResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnImageSaved__BackingField, (Action<GallerySaveResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GallerySaveResult> action = OnImageSaved__BackingField;
			Action<GallerySaveResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnImageSaved__BackingField, (Action<GallerySaveResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		AndroidNative.InitCameraAPI(AndroidNativeSettings.Instance.GalleryFolderName, AndroidNativeSettings.Instance.MaxImageLoadSize, (int)AndroidNativeSettings.Instance.CameraCaptureMode, (int)AndroidNativeSettings.Instance.ImageFormat);
	}

	[Obsolete("SaveImageToGalalry is deprecated, please use SaveImageToGallery instead.")]
	public void SaveImageToGalalry(Texture2D image, string name = "Screenshot")
	{
		SaveImageToGallery(image, name);
	}

	public void SaveImageToGallery(Texture2D image, string name = "Screenshot")
	{
		if (image != null)
		{
			byte[] inArray = image.EncodeToPNG();
			string imageData = Convert.ToBase64String(inArray);
			AndroidNative.SaveToGalalry(imageData, name);
		}
		else
		{
			UnityEngine.Debug.LogWarning("AndroidCamera::SaveToGalalry:  image is null");
		}
	}

	public void SaveScreenshotToGallery(string name = "Screenshot")
	{
		_lastImageName = name;
		SA.Common.Util.Screen.TakeScreenshot(OnScreenshotReady);
	}

	public void GetImageFromGallery()
	{
		AndroidNative.GetImageFromGallery();
	}

	public void GetImagesFromGallery()
	{
		AndroidNative.GetImagesFromGallery();
	}

	public void GetImageFromCamera()
	{
		AndroidNative.GetImageFromCamera(AndroidNativeSettings.Instance.SaveCameraImageToGallery);
	}

	private void OnImagePickedEvent(string data)
	{
		UnityEngine.Debug.Log("OnImagePickedEvent");
		string[] array = data.Split("|"[0]);
		string codeString = array[0];
		string imagePathInfo = array[1];
		string imageData = array[2];
		AndroidImagePickResult obj = new AndroidImagePickResult(codeString, imageData, imagePathInfo);
		OnImagePicked__BackingField(obj);
	}

	private void ImagesPickedCallback(string data)
	{
		UnityEngine.Debug.Log("[OnImagesPickedEvent]");
		string[] array = data.Split(new string[1] { "|%|" }, StringSplitOptions.None);
		string resultCode = array[0];
		string imagesData = array[1];
		AndroidImagesPickResult obj = new AndroidImagesPickResult(resultCode, imagesData);
		OnImagesPicked__BackingField(obj);
	}

	private void OnImageSavedEvent(string data)
	{
		GallerySaveResult obj = new GallerySaveResult(data);
		OnImageSaved__BackingField(obj);
	}

	private void OnImageSaveFailedEvent(string data)
	{
		GallerySaveResult obj = new GallerySaveResult();
		OnImageSaved__BackingField(obj);
	}

	private void OnScreenshotReady(Texture2D tex)
	{
		SaveImageToGallery(tex, _lastImageName);
	}

	public static string GetRandomString()
	{
		string text = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
		text = text.Replace("=", string.Empty);
		text = text.Replace("+", string.Empty);
		return text.Replace("/", string.Empty);
	}
}
