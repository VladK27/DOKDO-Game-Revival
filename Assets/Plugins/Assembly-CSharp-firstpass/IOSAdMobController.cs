using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class IOSAdMobController : Singleton<IOSAdMobController>, GoogleMobileAdInterface
{
	private bool _IsInited;

	private Dictionary<int, IOSADBanner> _banners;

	private string _BannersUunitId = string.Empty;

	private string _InterstisialUnitId = string.Empty;

	private string _RewardedVideoAdUnitId = string.Empty;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnInterstitialLoaded__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnInterstitialFailedLoading__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnInterstitialOpened__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnInterstitialClosed__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnInterstitialLeftApplication__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<string> OnAdInAppRequest__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<string, int> OnRewarded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnRewardedVideoAdClosed__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<int> OnRewardedVideoAdFailedToLoad__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnRewardedVideoAdLeftApplication__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnRewardedVideoLoaded__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnRewardedVideoAdOpened__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnRewardedVideoStarted__BackingField = () =>
	{
	};

	private const string DEVICES_SEPARATOR = ",";

	public List<GoogleMobileAdBanner> banners
	{
		get
		{
			List<GoogleMobileAdBanner> list = new List<GoogleMobileAdBanner>();
			if (_banners == null)
			{
				return list;
			}
			foreach (KeyValuePair<int, IOSADBanner> banner in _banners)
			{
				list.Add(banner.Value);
			}
			return list;
		}
	}

	public bool IsInited
	{
		get
		{
			return _IsInited;
		}
	}

	public string BannersUunitId
	{
		get
		{
			return _BannersUunitId;
		}
	}

	public string InterstisialUnitId
	{
		get
		{
			return _InterstisialUnitId;
		}
	}

	public string RewardedVideoAdUnitId
	{
		get
		{
			return _RewardedVideoAdUnitId;
		}
	}

	public event Action OnInterstitialLoaded
	{
		add
		{
			Action action = OnInterstitialLoaded__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialLoaded__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnInterstitialLoaded__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialLoaded__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OnInterstitialFailedLoading
	{
		add
		{
			Action action = OnInterstitialFailedLoading__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialFailedLoading__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnInterstitialFailedLoading__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialFailedLoading__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OnInterstitialOpened
	{
		add
		{
			Action action = OnInterstitialOpened__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialOpened__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnInterstitialOpened__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialOpened__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OnInterstitialClosed
	{
		add
		{
			Action action = OnInterstitialClosed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialClosed__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnInterstitialClosed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialClosed__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OnInterstitialLeftApplication
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

	public event Action<string> OnAdInAppRequest
	{
		add
		{
			Action<string> action = OnAdInAppRequest__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAdInAppRequest__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = OnAdInAppRequest__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAdInAppRequest__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<string, int> OnRewarded
	{
		add
		{
			Action<string, int> action = OnRewarded__BackingField;
			Action<string, int> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewarded__BackingField, (Action<string, int>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string, int> action = OnRewarded__BackingField;
			Action<string, int> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewarded__BackingField, (Action<string, int>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OnRewardedVideoAdClosed
	{
		add
		{
			Action action = OnRewardedVideoAdClosed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoAdClosed__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnRewardedVideoAdClosed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoAdClosed__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<int> OnRewardedVideoAdFailedToLoad
	{
		add
		{
			Action<int> action = OnRewardedVideoAdFailedToLoad__BackingField;
			Action<int> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoAdFailedToLoad__BackingField, (Action<int>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<int> action = OnRewardedVideoAdFailedToLoad__BackingField;
			Action<int> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoAdFailedToLoad__BackingField, (Action<int>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OnRewardedVideoAdLeftApplication
	{
		add
		{
			Action action = OnRewardedVideoAdLeftApplication__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoAdLeftApplication__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnRewardedVideoAdLeftApplication__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoAdLeftApplication__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OnRewardedVideoLoaded
	{
		add
		{
			Action action = OnRewardedVideoLoaded__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoLoaded__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnRewardedVideoLoaded__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoLoaded__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OnRewardedVideoAdOpened
	{
		add
		{
			Action action = OnRewardedVideoAdOpened__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoAdOpened__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnRewardedVideoAdOpened__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoAdOpened__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OnRewardedVideoStarted
	{
		add
		{
			Action action = OnRewardedVideoStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoStarted__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnRewardedVideoStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoStarted__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void Init(string ad_unit_id)
	{
		if (_IsInited)
		{
			UnityEngine.Debug.LogWarning("Init shoudl be called only once. Call ignored");
			return;
		}
		_IsInited = true;
		_BannersUunitId = ad_unit_id;
		_InterstisialUnitId = ad_unit_id;
		_RewardedVideoAdUnitId = ad_unit_id;
		_banners = new Dictionary<int, IOSADBanner>();
	}

	public void Init(string banners_unit_id, string interstisial_unit_id)
	{
		if (_IsInited)
		{
			UnityEngine.Debug.LogWarning("Init shoudl be called only once. Call ignored");
			return;
		}
		Init(banners_unit_id);
		SetInterstisialsUnitID(interstisial_unit_id);
	}

	public void InitEditorTesting(bool isEditorTestingEnabled, int fillRate)
	{
	}

	public void SetBannersUnitID(string ad_unit_id)
	{
		_BannersUunitId = ad_unit_id;
	}

	public void SetInterstisialsUnitID(string ad_unit_id)
	{
		_InterstisialUnitId = ad_unit_id;
	}

	public void SetRewardedVideoAdUnitID(string id)
	{
		_RewardedVideoAdUnitId = id;
	}

	public GoogleMobileAdBanner CreateAdBanner(TextAnchor anchor, GADBannerSize size)
	{
		if (!IsInited)
		{
			UnityEngine.Debug.LogWarning("CreateBannerAd shoudl be called only after Init function. Call ignored");
			return null;
		}
		IOSADBanner iOSADBanner = new IOSADBanner(anchor, size, GADBannerIdFactory.nextId);
		_banners.Add(iOSADBanner.id, iOSADBanner);
		return iOSADBanner;
	}

	public GoogleMobileAdBanner CreateAdBanner(int x, int y, GADBannerSize size)
	{
		if (!IsInited)
		{
			UnityEngine.Debug.LogWarning("CreateBannerAd shoudl be called only after Init function. Call ignored");
			return null;
		}
		IOSADBanner iOSADBanner = new IOSADBanner(x, y, size, GADBannerIdFactory.nextId);
		_banners.Add(iOSADBanner.id, iOSADBanner);
		return iOSADBanner;
	}

	public void DestroyBanner(int id)
	{
		if (_banners != null && _banners.ContainsKey(id))
		{
			IOSADBanner iOSADBanner = _banners[id];
			if (iOSADBanner.IsLoaded)
			{
				_banners.Remove(id);
			}
			else
			{
				iOSADBanner.DestroyAfterLoad();
			}
		}
	}

	public void DirectBannerDestory(int id)
	{
	}

	public void RecordInAppResolution(GADInAppResolution resolution)
	{
	}

	public void AddKeyword(string keyword)
	{
		if (!IsInited)
		{
			UnityEngine.Debug.LogWarning("AddKeyword shoudl be called only after Init function. Call ignored");
		}
	}

	public void AddTestDevice(string deviceId)
	{
		if (!IsInited)
		{
			UnityEngine.Debug.LogWarning("AddTestDevice shoudl be called only after Init function. Call ignored");
		}
	}

	public void AddTestDevices(params string[] ids)
	{
		if (!IsInited)
		{
			UnityEngine.Debug.LogWarning("AddTestDevice shoudl be called only after Init function. Call ignored");
		}
		else if (ids.Length != 0)
		{
		}
	}

	public void SetGender(GoogleGender gender)
	{
		if (!IsInited)
		{
			UnityEngine.Debug.LogWarning("SetGender shoudl be called only after Init function. Call ignored");
		}
	}

	public void SetBirthday(int year, AndroidMonth month, int day)
	{
		if (!IsInited)
		{
			UnityEngine.Debug.LogWarning("SetBirthday shoudl be called only after Init function. Call ignored");
		}
	}

	public void TagForChildDirectedTreatment(bool tagForChildDirectedTreatment)
	{
		if (!IsInited)
		{
			UnityEngine.Debug.LogWarning("TagForChildDirectedTreatment shoudl be called only after Init function. Call ignored");
		}
	}

	public void StartInterstitialAd()
	{
		if (!IsInited)
		{
			UnityEngine.Debug.LogWarning("StartInterstitialAd shoudl be called only after Init function. Call ignored");
		}
	}

	public void LoadInterstitialAd()
	{
		if (!IsInited)
		{
			UnityEngine.Debug.LogWarning("LoadInterstitialAd shoudl be called only after Init function. Call ignored");
		}
	}

	public void ShowInterstitialAd()
	{
		if (!IsInited)
		{
			UnityEngine.Debug.LogWarning("ShowInterstitialAd shoudl be called only after Init function. Call ignored");
		}
	}

	public void StartRewardedVideo()
	{
	}

	public void LoadRewardedVideo()
	{
	}

	public void ShowRewardedVideo()
	{
	}

	public GoogleMobileAdBanner GetBanner(int id)
	{
		if (_banners.ContainsKey(id))
		{
			return _banners[id];
		}
		UnityEngine.Debug.LogWarning("Banner id: " + id + " not found");
		return null;
	}

	private void OnBannerAdLoaded(string data)
	{
		string[] array = data.Split("|"[0]);
		int id = Convert.ToInt32(array[0]);
		int w = Convert.ToInt32(array[1]);
		int h = Convert.ToInt32(array[2]);
		IOSADBanner iOSADBanner = GetBanner(id) as IOSADBanner;
		if (iOSADBanner != null)
		{
			iOSADBanner.SetDimentions(w, h);
			iOSADBanner.OnBannerAdLoaded();
		}
	}

	private void OnBannerAdFailedToLoad(string bannerID)
	{
		int id = Convert.ToInt32(bannerID);
		IOSADBanner iOSADBanner = GetBanner(id) as IOSADBanner;
		if (iOSADBanner != null)
		{
			iOSADBanner.OnBannerAdFailedToLoad();
		}
	}

	private void OnBannerAdOpened(string bannerID)
	{
		int id = Convert.ToInt32(bannerID);
		IOSADBanner iOSADBanner = GetBanner(id) as IOSADBanner;
		if (iOSADBanner != null)
		{
			iOSADBanner.OnBannerAdOpened();
		}
	}

	private void OnBannerAdClosed(string bannerID)
	{
		int id = Convert.ToInt32(bannerID);
		IOSADBanner iOSADBanner = GetBanner(id) as IOSADBanner;
		if (iOSADBanner != null)
		{
			iOSADBanner.OnBannerAdClosed();
		}
	}

	private void OnBannerAdLeftApplication(string bannerID)
	{
		int id = Convert.ToInt32(bannerID);
		IOSADBanner iOSADBanner = GetBanner(id) as IOSADBanner;
		if (iOSADBanner != null)
		{
			iOSADBanner.OnBannerAdLeftApplication();
		}
	}

	private void OnInterstitialAdLoaded()
	{
		OnInterstitialLoaded__BackingField();
	}

	private void OnInterstitialAdFailedToLoad()
	{
		OnInterstitialFailedLoading__BackingField();
	}

	private void OnInterstitialAdOpened()
	{
		OnInterstitialOpened__BackingField();
	}

	private void OnInterstitialAdClosed()
	{
		OnInterstitialClosed__BackingField();
	}

	private void OnInterstitialAdLeftApplication()
	{
		OnInterstitialLeftApplication__BackingField();
	}

	private void RewardBasedVideoAdDidReceiveAd()
	{
		OnRewardedVideoLoaded__BackingField();
	}

	private void RewardBasedVideoAdDidOpen()
	{
		OnRewardedVideoAdOpened__BackingField();
	}

	private void RewardBasedVideoAdDidStartPlaying()
	{
		OnRewardedVideoStarted__BackingField();
	}

	private void RewardBasedVideoAdDidClose()
	{
		OnRewardedVideoAdClosed__BackingField();
	}

	private void RewardBasedVideoAdDidFailToLoadWithError()
	{
		OnRewardedVideoAdFailedToLoad__BackingField(0);
	}

	private void RewardBasedVideoAdWillLeaveApplication()
	{
		OnRewardedVideoAdLeftApplication__BackingField();
	}

	private void RewardUserWithReward(string data)
	{
		string[] array = data.Split('|');
		string arg = array[0];
		int arg2 = Convert.ToInt32(array[1]);
		OnRewarded__BackingField(arg, arg2);
	}

	private void OnInAppPurchaseRequested(string productId)
	{
		OnAdInAppRequest__BackingField(productId);
	}
}
