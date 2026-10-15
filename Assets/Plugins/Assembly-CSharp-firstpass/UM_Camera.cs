using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class UM_Camera : Singleton<UM_Camera>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<UM_ImagePickResult> OnImagePicked__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<UM_ImageSaveResult> OnImageSaved__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<UM_ImagesPickResult> OnImagesPicked__BackingField = delegate
	{
	};

	public event Action<UM_ImagePickResult> OnImagePicked
	{
		add
		{
			Action<UM_ImagePickResult> action = OnImagePicked__BackingField;
			Action<UM_ImagePickResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnImagePicked__BackingField, (Action<UM_ImagePickResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_ImagePickResult> action = OnImagePicked__BackingField;
			Action<UM_ImagePickResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnImagePicked__BackingField, (Action<UM_ImagePickResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<UM_ImageSaveResult> OnImageSaved
	{
		add
		{
			Action<UM_ImageSaveResult> action = OnImageSaved__BackingField;
			Action<UM_ImageSaveResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnImageSaved__BackingField, (Action<UM_ImageSaveResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_ImageSaveResult> action = OnImageSaved__BackingField;
			Action<UM_ImageSaveResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnImageSaved__BackingField, (Action<UM_ImageSaveResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<UM_ImagesPickResult> OnImagesPicked
	{
		add
		{
			Action<UM_ImagesPickResult> action = OnImagesPicked__BackingField;
			Action<UM_ImagesPickResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnImagesPicked__BackingField, (Action<UM_ImagesPickResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_ImagesPickResult> action = OnImagesPicked__BackingField;
			Action<UM_ImagesPickResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnImagesPicked__BackingField, (Action<UM_ImagesPickResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		Singleton<AndroidCamera>.Instance.OnImagePicked += OnAndroidImagePicked;
		IOSCamera.OnImagePicked += OnIOSImagePicked;
		Singleton<AndroidCamera>.Instance.OnImageSaved += OnAndroidImageSaved;
		IOSCamera.OnImageSaved += OnIOSImageSaved;
		Singleton<AndroidCamera>.Instance.OnImagesPicked += HandleOnImagesPicked;
	}

	public void SaveImageToGalalry(Texture2D image)
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			Singleton<AndroidCamera>.Instance.SaveImageToGallery(image);
			break;
		case RuntimePlatform.IPhonePlayer:
			Singleton<IOSCamera>.Instance.SaveTextureToCameraRoll(image);
			break;
		}
	}

	public void SaveScreenshotToGallery()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			Singleton<AndroidCamera>.Instance.SaveScreenshotToGallery();
			break;
		case RuntimePlatform.IPhonePlayer:
			Singleton<IOSCamera>.Instance.SaveScreenshotToCameraRoll();
			break;
		}
	}

	public void GetImageFromGallery()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			Singleton<AndroidCamera>.Instance.GetImageFromGallery();
			break;
		case RuntimePlatform.IPhonePlayer:
			Singleton<IOSCamera>.Instance.PickImage(ISN_ImageSource.Library);
			break;
		}
	}

	public void GetImagesFromGallery()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			Singleton<AndroidCamera>.Instance.GetImagesFromGallery();
			break;
		}
	}

	public void GetImageFromCamera()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			Singleton<AndroidCamera>.Instance.GetImageFromCamera();
			break;
		case RuntimePlatform.IPhonePlayer:
			Singleton<IOSCamera>.Instance.PickImage(ISN_ImageSource.Camera);
			break;
		}
	}

	private void HandleOnImagesPicked(AndroidImagesPickResult result)
	{
		OnImagesPicked__BackingField(new UM_ImagesPickResult(result.IsSucceeded, result.Images));
	}

	private void OnAndroidImagePicked(AndroidImagePickResult obj)
	{
		UM_ImagePickResult obj2 = new UM_ImagePickResult(obj.Image);
		OnImagePicked__BackingField(obj2);
	}

	private void OnIOSImagePicked(IOSImagePickResult obj)
	{
		UM_ImagePickResult obj2 = new UM_ImagePickResult(obj.Image);
		OnImagePicked__BackingField(obj2);
	}

	private void OnAndroidImageSaved(GallerySaveResult res)
	{
		UM_ImageSaveResult obj = new UM_ImageSaveResult(res.imagePath, res.IsSucceeded);
		OnImageSaved__BackingField(obj);
	}

	private void OnIOSImageSaved(Result res)
	{
		UM_ImageSaveResult obj = new UM_ImageSaveResult(string.Empty, res.IsSucceeded);
		OnImageSaved__BackingField(obj);
	}
}
