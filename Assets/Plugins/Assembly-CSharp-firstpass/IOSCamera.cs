using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class IOSCamera : Singleton<IOSCamera>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<IOSImagePickResult> OnImagePicked__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<Result> OnImageSaved__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string> OnVideoPathPicked__BackingField = delegate
	{
	};

	private bool _IsWaitngForResponce;

	private bool _IsInitialized;

	public static event Action<IOSImagePickResult> OnImagePicked
	{
		add
		{
			Action<IOSImagePickResult> action = OnImagePicked__BackingField;
			Action<IOSImagePickResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnImagePicked__BackingField, (Action<IOSImagePickResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<IOSImagePickResult> action = OnImagePicked__BackingField;
			Action<IOSImagePickResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnImagePicked__BackingField, (Action<IOSImagePickResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<Result> OnImageSaved
	{
		add
		{
			Action<Result> action = OnImageSaved__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnImageSaved__BackingField, (Action<Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Result> action = OnImageSaved__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnImageSaved__BackingField, (Action<Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string> OnVideoPathPicked
	{
		add
		{
			Action<string> action = OnVideoPathPicked__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnVideoPathPicked__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = OnVideoPathPicked__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnVideoPathPicked__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		Init();
	}

	public void Init()
	{
		if (!_IsInitialized)
		{
			_IsInitialized = true;
		}
	}

	public void SaveTextureToCameraRoll(Texture2D texture)
	{
	}

	public void SaveScreenshotToCameraRoll()
	{
		StartCoroutine(SaveScreenshot());
	}

	public void GetVideoPathFromAlbum()
	{
	}

	[Obsolete("GetImageFromAlbum is deprecated, please use PickImage(ISN_ImageSource.Album) ")]
	public void GetImageFromAlbum()
	{
		PickImage(ISN_ImageSource.Album);
	}

	[Obsolete("GetImageFromCamera is deprecated, please use PickImage(ISN_ImageSource.Camera) ")]
	public void GetImageFromCamera()
	{
		PickImage(ISN_ImageSource.Camera);
	}

	public void PickImage(ISN_ImageSource source)
	{
		if (!_IsWaitngForResponce)
		{
			_IsWaitngForResponce = true;
		}
	}

	private void OnImagePickedEvent(string data)
	{
		_IsWaitngForResponce = false;
		IOSImagePickResult obj = new IOSImagePickResult(data);
		OnImagePicked__BackingField(obj);
	}

	private void OnImageSaveFailed()
	{
		Result obj = new Result(new Error());
		OnImageSaved__BackingField(obj);
	}

	private void OnImageSaveSuccess()
	{
		Result obj = new Result();
		OnImageSaved__BackingField(obj);
	}

	private void OnVideoPickedEvent(string path)
	{
		OnVideoPathPicked__BackingField(path);
	}

	private IEnumerator SaveScreenshot()
	{
		yield return new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0f, 0f, width, height), 0, 0);
		tex.Apply();
		SaveTextureToCameraRoll(tex);
		UnityEngine.Object.Destroy(tex);
	}
}
