using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Util;
using UnityEngine;

public class FB_Score
{
	public string UserId;

	public string UserName;

	public string AppId;

	public string AppName;

	public int value;

	private Dictionary<FB_ProfileImageSize, Texture2D> profileImages = new Dictionary<FB_ProfileImageSize, Texture2D>();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<FB_Score> OnProfileImageLoaded__BackingField = delegate
	{
	};

	public event Action<FB_Score> OnProfileImageLoaded
	{
		add
		{
			Action<FB_Score> action = OnProfileImageLoaded__BackingField;
			Action<FB_Score> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnProfileImageLoaded__BackingField, (Action<FB_Score>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FB_Score> action = OnProfileImageLoaded__BackingField;
			Action<FB_Score> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnProfileImageLoaded__BackingField, (Action<FB_Score>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public string GetProfileUrl(FB_ProfileImageSize size)
	{
		return "https://graph.facebook.com/" + UserId + "/picture?type=" + size;
	}

	public Texture2D GetProfileImage(FB_ProfileImageSize size)
	{
		if (profileImages.ContainsKey(size))
		{
			return profileImages[size];
		}
		return null;
	}

	public void LoadProfileImage(FB_ProfileImageSize size)
	{
		if (GetProfileImage(size) != null)
		{
			UnityEngine.Debug.LogWarning("Profile image already loaded, size: " + size);
			OnProfileImageLoaded__BackingField(this);
		}
		switch (size)
		{
		case FB_ProfileImageSize.large:
			Loader.LoadWebTexture(GetProfileUrl(size), OnLargeImageLoaded);
			break;
		case FB_ProfileImageSize.normal:
			Loader.LoadWebTexture(GetProfileUrl(size), OnNormalImageLoaded);
			break;
		case FB_ProfileImageSize.small:
			Loader.LoadWebTexture(GetProfileUrl(size), OnSmallImageLoaded);
			break;
		case FB_ProfileImageSize.square:
			Loader.LoadWebTexture(GetProfileUrl(size), OnSquareImageLoaded);
			break;
		}
		UnityEngine.Debug.Log("LOAD IMAGE URL: " + GetProfileUrl(size));
	}

	private void OnSquareImageLoaded(Texture2D image)
	{
		if (this != null)
		{
			if (image != null && !profileImages.ContainsKey(FB_ProfileImageSize.square))
			{
				profileImages.Add(FB_ProfileImageSize.square, image);
			}
			OnProfileImageLoaded__BackingField(this);
		}
	}

	private void OnLargeImageLoaded(Texture2D image)
	{
		if (this != null)
		{
			if (image != null && !profileImages.ContainsKey(FB_ProfileImageSize.large))
			{
				profileImages.Add(FB_ProfileImageSize.large, image);
			}
			OnProfileImageLoaded__BackingField(this);
		}
	}

	private void OnNormalImageLoaded(Texture2D image)
	{
		if (this != null)
		{
			if (image != null && !profileImages.ContainsKey(FB_ProfileImageSize.normal))
			{
				profileImages.Add(FB_ProfileImageSize.normal, image);
			}
			OnProfileImageLoaded__BackingField(this);
		}
	}

	private void OnSmallImageLoaded(Texture2D image)
	{
		if (this != null)
		{
			if (image != null && !profileImages.ContainsKey(FB_ProfileImageSize.small))
			{
				profileImages.Add(FB_ProfileImageSize.small, image);
			}
			OnProfileImageLoaded__BackingField(this);
		}
	}
}
