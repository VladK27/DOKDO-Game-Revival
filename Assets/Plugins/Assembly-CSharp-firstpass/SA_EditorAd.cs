using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class SA_EditorAd : Singleton<SA_EditorAd>
{
	public const float MIN_LOAD_TIME = 1f;

	public const float MAX_LOAD_TIME = 3f;

	private bool _IsInterstitialLoading;

	private bool _IsVideoLoading;

	private bool _IsInterstitialReady;

	private bool _IsVideoReady;

	private int _FillRate = 100;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<bool> OnInterstitialFinished__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<bool> OnInterstitialLoadComplete__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnInterstitialLeftApplication__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<bool> OnVideoFinished__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<bool> OnVideoLoadComplete__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnVideoLeftApplication__BackingField = () =>
	{
	};

	private SA_Ad_EditorUIController _EditorUI;

	public bool IsVideoReady
	{
		get
		{
			return _IsVideoReady;
		}
	}

	public bool IsVideoLoading
	{
		get
		{
			return _IsVideoLoading;
		}
	}

	public bool IsInterstitialReady
	{
		get
		{
			return _IsInterstitialReady;
		}
	}

	public bool IsInterstitialLoading
	{
		get
		{
			return _IsInterstitialLoading;
		}
	}

	public bool HasFill
	{
		get
		{
			int num = UnityEngine.Random.Range(1, 100);
			if (num <= _FillRate)
			{
				return true;
			}
			return false;
		}
	}

	public int FillRate
	{
		get
		{
			return _FillRate;
		}
	}

	private SA_Ad_EditorUIController EditorUI
	{
		get
		{
			return _EditorUI;
		}
	}

	public static event Action<bool> OnInterstitialFinished
	{
		add
		{
			Action<bool> action = OnInterstitialFinished__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialFinished__BackingField, (Action<bool>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<bool> action = OnInterstitialFinished__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialFinished__BackingField, (Action<bool>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<bool> OnInterstitialLoadComplete
	{
		add
		{
			Action<bool> action = OnInterstitialLoadComplete__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialLoadComplete__BackingField, (Action<bool>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<bool> action = OnInterstitialLoadComplete__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialLoadComplete__BackingField, (Action<bool>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action OnInterstitialLeftApplication
	{
		add
		{
			Action action = OnInterstitialLeftApplication__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialLeftApplication__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnInterstitialLeftApplication__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialLeftApplication__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<bool> OnVideoFinished
	{
		add
		{
			Action<bool> action = OnVideoFinished__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnVideoFinished__BackingField, (Action<bool>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<bool> action = OnVideoFinished__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnVideoFinished__BackingField, (Action<bool>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<bool> OnVideoLoadComplete
	{
		add
		{
			Action<bool> action = OnVideoLoadComplete__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnVideoLoadComplete__BackingField, (Action<bool>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<bool> action = OnVideoLoadComplete__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnVideoLoadComplete__BackingField, (Action<bool>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action OnVideoLeftApplication
	{
		add
		{
			Action action = OnVideoLeftApplication__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnVideoLeftApplication__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnVideoLeftApplication__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnVideoLeftApplication__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void SetFillRate(int fillRate)
	{
		_FillRate = fillRate;
	}

	public void LoadInterstitial()
	{
		if (!_IsInterstitialLoading && !IsInterstitialReady)
		{
			_IsInterstitialLoading = true;
			float time = UnityEngine.Random.Range(1f, 3f);
			Invoke("OnInterstitialRequestComplete", time);
		}
	}

	public void ShowInterstitial()
	{
		if (!IsInterstitialReady)
		{
		}
	}

	public void LoadVideo()
	{
		if (!_IsVideoLoading && !IsVideoReady)
		{
			_IsVideoLoading = true;
			float time = UnityEngine.Random.Range(1f, 3f);
			Invoke("OnVideoRequestComplete", time);
		}
	}

	public void ShowVideo()
	{
		if (!IsVideoReady)
		{
		}
	}

	private void OnVideoRequestComplete()
	{
		_IsVideoLoading = false;
		_IsVideoReady = HasFill;
		OnVideoLoadComplete__BackingField(_IsVideoReady);
	}

	private void OnInterstitialRequestComplete()
	{
		_IsInterstitialLoading = false;
		_IsInterstitialReady = HasFill;
		OnInterstitialLoadComplete__BackingField(_IsInterstitialReady);
	}

	private void OnInterstitialFinished_UIEvent(bool IsRewarded)
	{
		_IsInterstitialReady = false;
		OnInterstitialFinished__BackingField(IsRewarded);
	}

	private void OnVideoFinished_UIEvent(bool IsRewarded)
	{
		_IsVideoReady = false;
		OnVideoFinished__BackingField(IsRewarded);
	}

	private void OnInterstitialLeftApplication_UIEvent()
	{
		OnInterstitialLeftApplication__BackingField();
	}

	private void OnVideoLeftApplication_UIEvent()
	{
		OnVideoLeftApplication__BackingField();
	}
}
