using System;
using SA.Common.Pattern;
using UnityEngine;

public class IOSADBanner : GoogleMobileAdBanner
{
	private int _id;

	private GADBannerSize _size;

	private TextAnchor _anchor;

	private bool _IsLoaded;

	private bool _IsOnScreen;

	private bool firstLoad = true;

	private bool _ShowOnLoad = true;

	private bool destroyOnLoad;

	private int _width;

	private int _height;

	private Action<GoogleMobileAdBanner> _OnLoadedAction = delegate
	{
	};

	private Action<GoogleMobileAdBanner> _OnFailedLoadingAction = delegate
	{
	};

	private Action<GoogleMobileAdBanner> _OnOpenedAction = delegate
	{
	};

	private Action<GoogleMobileAdBanner> _OnClosedAction = delegate
	{
	};

	private Action<GoogleMobileAdBanner> _OnLeftApplicationAction = delegate
	{
	};

	public int id
	{
		get
		{
			return _id;
		}
	}

	public int width
	{
		get
		{
			return _width;
		}
	}

	public int height
	{
		get
		{
			return _height;
		}
	}

	public GADBannerSize size
	{
		get
		{
			return _size;
		}
	}

	public bool IsLoaded
	{
		get
		{
			return _IsLoaded;
		}
	}

	public bool IsOnScreen
	{
		get
		{
			return _IsOnScreen;
		}
	}

	public bool ShowOnLoad
	{
		get
		{
			return _ShowOnLoad;
		}
		set
		{
			_ShowOnLoad = value;
		}
	}

	public TextAnchor anchor
	{
		get
		{
			return _anchor;
		}
	}

	public GoogleGravity gravity
	{
		get
		{
			switch (_anchor)
			{
			case TextAnchor.LowerCenter:
				return (GoogleGravity)81;
			case TextAnchor.LowerLeft:
				return GoogleGravity.BOTTOM | GoogleGravity.LEFT;
			case TextAnchor.LowerRight:
				return GoogleGravity.BOTTOM | GoogleGravity.RIGHT;
			case TextAnchor.MiddleCenter:
				return GoogleGravity.CENTER;
			case TextAnchor.MiddleLeft:
				return (GoogleGravity)19;
			case TextAnchor.MiddleRight:
				return (GoogleGravity)21;
			case TextAnchor.UpperCenter:
				return (GoogleGravity)49;
			case TextAnchor.UpperLeft:
				return (GoogleGravity)51;
			case TextAnchor.UpperRight:
				return (GoogleGravity)53;
			default:
				return GoogleGravity.TOP;
			}
		}
	}

	public Action<GoogleMobileAdBanner> OnLoadedAction
	{
		get
		{
			return _OnLoadedAction;
		}
		set
		{
			_OnLoadedAction = value;
		}
	}

	public Action<GoogleMobileAdBanner> OnFailedLoadingAction
	{
		get
		{
			return _OnFailedLoadingAction;
		}
		set
		{
			_OnFailedLoadingAction = value;
		}
	}

	public Action<GoogleMobileAdBanner> OnOpenedAction
	{
		get
		{
			return _OnOpenedAction;
		}
		set
		{
			_OnOpenedAction = value;
		}
	}

	public Action<GoogleMobileAdBanner> OnClosedAction
	{
		get
		{
			return _OnClosedAction;
		}
		set
		{
			_OnClosedAction = value;
		}
	}

	public Action<GoogleMobileAdBanner> OnLeftApplicationAction
	{
		get
		{
			return _OnLeftApplicationAction;
		}
		set
		{
			_OnLeftApplicationAction = value;
		}
	}

	public IOSADBanner(TextAnchor anchor, GADBannerSize size, int id)
	{
		_id = id;
		_size = size;
		_anchor = anchor;
	}

	public IOSADBanner(int x, int y, GADBannerSize size, int id)
	{
		_id = id;
		_size = size;
	}

	public void Hide()
	{
		if (_IsOnScreen)
		{
			_IsOnScreen = false;
		}
	}

	public void Show()
	{
		if (!_IsOnScreen)
		{
			_IsOnScreen = true;
		}
	}

	public void Refresh()
	{
	}

	public void SetBannerPosition(int x, int y)
	{
	}

	public void SetBannerPosition(TextAnchor anchor)
	{
	}

	public void DestroyAfterLoad()
	{
		destroyOnLoad = true;
		ShowOnLoad = false;
	}

	public void SetDimentions(int w, int h)
	{
		_width = Mathf.FloorToInt((float)w * (Screen.dpi / 160f));
		_height = Mathf.FloorToInt((float)h * (Screen.dpi / 160f));
	}

	public void OnBannerAdLoaded()
	{
		if (destroyOnLoad)
		{
			Singleton<IOSAdMobController>.Instance.DirectBannerDestory(id);
			return;
		}
		_IsLoaded = true;
		if (ShowOnLoad && firstLoad)
		{
			Show();
			firstLoad = false;
		}
		_OnLoadedAction(this);
	}

	public void OnBannerAdFailedToLoad()
	{
		_OnFailedLoadingAction(this);
	}

	public void OnBannerAdOpened()
	{
		_OnOpenedAction(this);
	}

	public void OnBannerAdClosed()
	{
		_OnClosedAction(this);
	}

	public void OnBannerAdLeftApplication()
	{
		_OnLeftApplicationAction(this);
	}
}
