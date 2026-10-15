using System;
using System.Diagnostics;
using System.Threading;

public class AmazonAdBanner
{
	public enum BannerAligns
	{
		Top = 0,
		TopLeft = 1,
		TopRight = 2,
		Bottom = 3,
		BottomLeft = 4,
		BottomRight = 5
	}

	private int _id;

	private BannerAligns _position;

	private AMN_AdProperties _properties;

	private bool _isLoaded;

	private bool _isOnScreen;

	private int _width;

	private int _height;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AmazonAdBanner> OnLoadedAction__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AmazonAdBanner> OnFailedLoadingAction__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AmazonAdBanner> OnExpandedAction__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AmazonAdBanner> OnDismissedAction__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AmazonAdBanner> OnCollapsedAction__BackingField = delegate
	{
	};

	public int Id
	{
		get
		{
			return _id;
		}
	}

	public bool IsLoaded
	{
		get
		{
			return _isLoaded;
		}
	}

	public bool IsOnScreen
	{
		get
		{
			return _isOnScreen;
		}
	}

	public int Width
	{
		get
		{
			return _width;
		}
	}

	public int Height
	{
		get
		{
			return _height;
		}
	}

	public AMN_AdProperties Properties
	{
		get
		{
			return _properties;
		}
	}

	public event Action<AmazonAdBanner> OnLoadedAction
	{
		add
		{
			Action<AmazonAdBanner> action = OnLoadedAction__BackingField;
			Action<AmazonAdBanner> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLoadedAction__BackingField, (Action<AmazonAdBanner>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AmazonAdBanner> action = OnLoadedAction__BackingField;
			Action<AmazonAdBanner> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLoadedAction__BackingField, (Action<AmazonAdBanner>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<AmazonAdBanner> OnFailedLoadingAction
	{
		add
		{
			Action<AmazonAdBanner> action = OnFailedLoadingAction__BackingField;
			Action<AmazonAdBanner> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnFailedLoadingAction__BackingField, (Action<AmazonAdBanner>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AmazonAdBanner> action = OnFailedLoadingAction__BackingField;
			Action<AmazonAdBanner> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnFailedLoadingAction__BackingField, (Action<AmazonAdBanner>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<AmazonAdBanner> OnExpandedAction
	{
		add
		{
			Action<AmazonAdBanner> action = OnExpandedAction__BackingField;
			Action<AmazonAdBanner> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnExpandedAction__BackingField, (Action<AmazonAdBanner>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AmazonAdBanner> action = OnExpandedAction__BackingField;
			Action<AmazonAdBanner> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnExpandedAction__BackingField, (Action<AmazonAdBanner>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<AmazonAdBanner> OnDismissedAction
	{
		add
		{
			Action<AmazonAdBanner> action = OnDismissedAction__BackingField;
			Action<AmazonAdBanner> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnDismissedAction__BackingField, (Action<AmazonAdBanner>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AmazonAdBanner> action = OnDismissedAction__BackingField;
			Action<AmazonAdBanner> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnDismissedAction__BackingField, (Action<AmazonAdBanner>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<AmazonAdBanner> OnCollapsedAction
	{
		add
		{
			Action<AmazonAdBanner> action = OnCollapsedAction__BackingField;
			Action<AmazonAdBanner> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnCollapsedAction__BackingField, (Action<AmazonAdBanner>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AmazonAdBanner> action = OnCollapsedAction__BackingField;
			Action<AmazonAdBanner> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnCollapsedAction__BackingField, (Action<AmazonAdBanner>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public AmazonAdBanner(BannerAligns position, int id)
	{
		_id = id;
		_position = position;
		AMN_AdvertisingProxy.CreateBanner(GetPosition(_position), _id);
	}

	public void SetProperties(int width, int height, AMN_AdProperties props)
	{
		_width = width;
		_height = height;
		_properties = props;
	}

	public void Hide(bool hide)
	{
		AMN_AdvertisingProxy.HideBanner(hide, _id);
	}

	public void Destroy()
	{
		AMN_AdvertisingProxy.DestroyBanner(_id);
	}

	public void Refresh()
	{
		AMN_AdvertisingProxy.RefreshBanner(_id);
	}

	public void HandleOnBannerAdLoaded()
	{
		_isLoaded = true;
		OnLoadedAction__BackingField(this);
	}

	public void HandleOnBannerAdFailedToLoad()
	{
		OnFailedLoadingAction__BackingField(this);
	}

	public void HandleOnBannerAdExpanded()
	{
		_isOnScreen = true;
		OnExpandedAction__BackingField(this);
	}

	public void HandleOnBannerAdDismissed()
	{
		_isOnScreen = false;
		OnDismissedAction__BackingField(this);
	}

	public void HandleOnBannerAdCollapsed()
	{
		_isOnScreen = false;
		OnCollapsedAction__BackingField(this);
	}

	private string GetPosition(BannerAligns BannerAlign)
	{
		string result = "BM";
		switch (BannerAlign)
		{
		case BannerAligns.Top:
			result = "TM";
			break;
		case BannerAligns.TopLeft:
			result = "TL";
			break;
		case BannerAligns.TopRight:
			result = "TR";
			break;
		case BannerAligns.Bottom:
			result = "BM";
			break;
		case BannerAligns.BottomLeft:
			result = "BL";
			break;
		case BannerAligns.BottomRight:
			result = "BR";
			break;
		}
		return result;
	}
}
