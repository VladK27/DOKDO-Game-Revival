using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Models;
using UnityEngine;

public class GK_Player
{
	private string _PlayerId;

	private string _DisplayName;

	private string _Alias;

	private Texture2D _SmallPhoto;

	private Texture2D _BigPhoto;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<GK_UserPhotoLoadResult> OnPlayerPhotoLoaded__BackingField = delegate
	{
	};

	private static Dictionary<string, Texture2D> LocalPhotosCache = new Dictionary<string, Texture2D>();

	public string Id
	{
		get
		{
			return _PlayerId;
		}
	}

	public string Alias
	{
		get
		{
			return _Alias;
		}
	}

	public string DisplayName
	{
		get
		{
			return _DisplayName;
		}
	}

	public Texture2D SmallPhoto
	{
		get
		{
			return _SmallPhoto;
		}
	}

	public Texture2D BigPhoto
	{
		get
		{
			return _BigPhoto;
		}
	}

	private string SmallPhotoCacheKey
	{
		get
		{
			return Id + GK_PhotoSize.GKPhotoSizeSmall;
		}
	}

	private string BigPhotoCacheKey
	{
		get
		{
			return Id + GK_PhotoSize.GKPhotoSizeNormal;
		}
	}

	public event Action<GK_UserPhotoLoadResult> OnPlayerPhotoLoaded
	{
		add
		{
			Action<GK_UserPhotoLoadResult> action = OnPlayerPhotoLoaded__BackingField;
			Action<GK_UserPhotoLoadResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPlayerPhotoLoaded__BackingField, (Action<GK_UserPhotoLoadResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_UserPhotoLoadResult> action = OnPlayerPhotoLoaded__BackingField;
			Action<GK_UserPhotoLoadResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPlayerPhotoLoaded__BackingField, (Action<GK_UserPhotoLoadResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public GK_Player(string pId, string pName, string pAlias)
	{
		_PlayerId = pId;
		_DisplayName = pName;
		_Alias = pAlias;
		_SmallPhoto = GetLocalCachedPhotoByKey(SmallPhotoCacheKey);
		_BigPhoto = GetLocalCachedPhotoByKey(BigPhotoCacheKey);
		if (IOSNativeSettings.Instance.AutoLoadUsersBigImages)
		{
			LoadPhoto(GK_PhotoSize.GKPhotoSizeNormal);
		}
		if (IOSNativeSettings.Instance.AutoLoadUsersSmallImages)
		{
			LoadPhoto(GK_PhotoSize.GKPhotoSizeSmall);
		}
	}

	public void LoadPhoto(GK_PhotoSize size)
	{
		if (size == GK_PhotoSize.GKPhotoSizeSmall)
		{
			if (_SmallPhoto != null)
			{
				GK_UserPhotoLoadResult obj = new GK_UserPhotoLoadResult(size, _SmallPhoto);
				OnPlayerPhotoLoaded__BackingField(obj);
				return;
			}
		}
		else if (_BigPhoto != null)
		{
			GK_UserPhotoLoadResult obj2 = new GK_UserPhotoLoadResult(size, _BigPhoto);
			OnPlayerPhotoLoaded__BackingField(obj2);
			return;
		}
		GameCenterManager.LoadGKPlayerPhoto(Id, size);
	}

	public void SetPhotoData(GK_PhotoSize size, string base64String)
	{
		if (base64String.Length != 0)
		{
			byte[] data = Convert.FromBase64String(base64String);
			Texture2D texture2D = new Texture2D(1, 1);
			texture2D.LoadImage(data);
			if (size == GK_PhotoSize.GKPhotoSizeSmall)
			{
				_SmallPhoto = texture2D;
				UpdatePhotosCache(SmallPhotoCacheKey, _SmallPhoto);
			}
			else
			{
				_BigPhoto = texture2D;
				UpdatePhotosCache(BigPhotoCacheKey, _BigPhoto);
			}
			GK_UserPhotoLoadResult obj = new GK_UserPhotoLoadResult(size, texture2D);
			OnPlayerPhotoLoaded__BackingField(obj);
		}
	}

	public void SetPhotoLoadFailedEventData(GK_PhotoSize size, string errorData)
	{
		GK_UserPhotoLoadResult obj = new GK_UserPhotoLoadResult(size, new Error(errorData));
		OnPlayerPhotoLoaded__BackingField(obj);
	}

	public static void UpdatePhotosCache(string key, Texture2D photo)
	{
		if (LocalPhotosCache.ContainsKey(key))
		{
			LocalPhotosCache[key] = photo;
		}
		else
		{
			LocalPhotosCache.Add(key, photo);
		}
	}

	public static Texture2D GetLocalCachedPhotoByKey(string key)
	{
		if (LocalPhotosCache.ContainsKey(key))
		{
			return LocalPhotosCache[key];
		}
		return null;
	}
}
