using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Util;
using UnityEngine;

public class GooglePlayerTemplate
{
	private string _id;

	private string _name;

	private string _iconImageUrl;

	private string _hiResImageUrl;

	private Texture2D _icon;

	private Texture2D _image;

	private bool _hasIconImage;

	private bool _hasHiResImage;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Texture2D> BigPhotoLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Texture2D> SmallPhotoLoaded__BackingField = delegate
	{
	};

	public string playerId
	{
		get
		{
			return _id;
		}
	}

	public string name
	{
		get
		{
			return _name;
		}
	}

	public bool hasIconImage
	{
		get
		{
			return _hasIconImage;
		}
	}

	public bool hasHiResImage
	{
		get
		{
			return _hasHiResImage;
		}
	}

	public string iconImageUrl
	{
		get
		{
			return _iconImageUrl;
		}
	}

	public string hiResImageUrl
	{
		get
		{
			return _hiResImageUrl;
		}
	}

	public Texture2D icon
	{
		get
		{
			return _icon;
		}
	}

	public Texture2D image
	{
		get
		{
			return _image;
		}
	}

	public event Action<Texture2D> BigPhotoLoaded
	{
		add
		{
			Action<Texture2D> action = BigPhotoLoaded__BackingField;
			Action<Texture2D> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BigPhotoLoaded__BackingField, (Action<Texture2D>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Texture2D> action = BigPhotoLoaded__BackingField;
			Action<Texture2D> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BigPhotoLoaded__BackingField, (Action<Texture2D>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<Texture2D> SmallPhotoLoaded
	{
		add
		{
			Action<Texture2D> action = SmallPhotoLoaded__BackingField;
			Action<Texture2D> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SmallPhotoLoaded__BackingField, (Action<Texture2D>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Texture2D> action = SmallPhotoLoaded__BackingField;
			Action<Texture2D> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SmallPhotoLoaded__BackingField, (Action<Texture2D>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public GooglePlayerTemplate(string pId, string pName, string iconUrl, string imageUrl, string pHasIconImage, string pHasHiResImage)
	{
		_id = pId;
		_name = pName;
		_iconImageUrl = iconUrl;
		_hiResImageUrl = imageUrl;
		if (pHasIconImage.Equals("1"))
		{
			_hasIconImage = true;
		}
		if (pHasHiResImage.Equals("1"))
		{
			_hasHiResImage = true;
		}
		if (AndroidNativeSettings.Instance.LoadProfileIcons)
		{
			LoadIcon();
		}
		if (AndroidNativeSettings.Instance.LoadProfileImages)
		{
			LoadImage();
		}
	}

	public void LoadImage()
	{
		if (image != null)
		{
			BigPhotoLoaded__BackingField(image);
		}
		else
		{
			Loader.LoadWebTexture(_hiResImageUrl, OnProfileImageLoaded);
		}
	}

	public void LoadIcon()
	{
		if (icon != null)
		{
			SmallPhotoLoaded__BackingField(icon);
		}
		else
		{
			Loader.LoadWebTexture(_iconImageUrl, OnProfileIconLoaded);
		}
	}

	private void OnProfileImageLoaded(Texture2D tex)
	{
		if (this != null)
		{
			_image = tex;
			BigPhotoLoaded__BackingField(_image);
		}
	}

	private void OnProfileIconLoaded(Texture2D tex)
	{
		if (this != null)
		{
			_icon = tex;
			SmallPhotoLoaded__BackingField(_icon);
		}
	}
}
