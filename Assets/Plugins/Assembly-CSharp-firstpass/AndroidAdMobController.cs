using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class AndroidAdMobController : Singleton<AndroidAdMobController>, GoogleMobileAdInterface
{
	private bool _IsInited;

	private Dictionary<int, AndroidADBanner> _banners;

	private bool _IsEditorTestingEnabled = true;

	private int _EditorFillRate = 100;

	private string _BannersUunitId;

	private string _InterstisialUnitId;

	private string _RewardedVideoAdUnitId;

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

	private const string DEVICES_SEPARATOR = ",";

	private bool _InterstitialShowOnLoad;

	private bool _RewardedVideoShowOnLoad;

	public List<GoogleMobileAdBanner> banners
	{
		get
		{
			List<GoogleMobileAdBanner> list = new List<GoogleMobileAdBanner>();
			if (_banners == null)
			{
				return list;
			}
			foreach (KeyValuePair<int, AndroidADBanner> banner in _banners)
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

	public bool IsEditorTestingEnabled
	{
		get
		{
			return SA_EditorTesting.IsInsideEditor && _IsEditorTestingEnabled;
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

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			foreach (KeyValuePair<int, AndroidADBanner> banner in _banners)
			{
				banner.Value.Pause();
			}
			return;
		}
		foreach (KeyValuePair<int, AndroidADBanner> banner2 in _banners)
		{
			banner2.Value.Resume();
		}
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
		_banners = new Dictionary<int, AndroidADBanner>();
		if (IsEditorTestingEnabled)
		{
			UnityEngine.Debug.Log("Initialized with Editor Testing Profile");
			Singleton<SA_EditorAd>.Instance.SetFillRate(_EditorFillRate);
		}
		else
		{
			AN_GoogleAdProxy.InitMobileAd(ad_unit_id);
		}
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

	public void InitEditorTesting(bool isTestingEnabled, int editorFillRate)
	{
		_IsEditorTestingEnabled = isTestingEnabled;
		_EditorFillRate = editorFillRate;
	}

	public void SetBannersUnitID(string ad_unit_id)
	{
		_BannersUunitId = ad_unit_id;
		AN_GoogleAdProxy.ChangeBannersUnitID(ad_unit_id);
	}

	public void SetInterstisialsUnitID(string ad_unit_id)
	{
		_InterstisialUnitId = ad_unit_id;
		AN_GoogleAdProxy.ChangeInterstisialsUnitID(ad_unit_id);
	}

	public void SetRewardedVideoAdUnitID(string id)
	{
		_RewardedVideoAdUnitId = id;
		AN_GoogleAdProxy.ChangeRewardedVideoUnitID(_RewardedVideoAdUnitId);
	}

	public void AddKeyword(string keyword)
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("AddKeyword shoudl be called only after Init function. Call ignored");
		}
		else
		{
			AN_GoogleAdProxy.AddKeyword(keyword);
		}
	}

	public void SetBirthday(int year, AndroidMonth month, int day)
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("SetBirthday shoudl be called only after Init function. Call ignored");
		}
		else
		{
			AN_GoogleAdProxy.SetBirthday(year, (int)month, day);
		}
	}

	public void TagForChildDirectedTreatment(bool tagForChildDirectedTreatment)
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("TagForChildDirectedTreatment shoudl be called only after Init function. Call ignored");
		}
		else
		{
			AN_GoogleAdProxy.TagForChildDirectedTreatment(tagForChildDirectedTreatment);
		}
	}

	public void AddTestDevice(string deviceId)
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("AddTestDevice shoudl be called only after Init function. Call ignored");
		}
		else
		{
			AN_GoogleAdProxy.AddTestDevice(deviceId);
		}
	}

	public void AddTestDevices(params string[] ids)
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("AddTestDevice shoudl be called only after Init function. Call ignored");
		}
		else if (ids.Length != 0)
		{
			AN_GoogleAdProxy.AddTestDevices(string.Join(",", ids));
		}
	}

	public void SetGender(GoogleGender gender)
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("SetGender shoudl be called only after Init function. Call ignored");
		}
		else
		{
			AN_GoogleAdProxy.SetGender((int)gender);
		}
	}

	public GoogleMobileAdBanner CreateAdBanner(TextAnchor anchor, GADBannerSize size)
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("CreateBannerAd shoudl be called only after Init function. Call ignored");
			return null;
		}
		AndroidADBanner androidADBanner = new AndroidADBanner(anchor, size, GADBannerIdFactory.nextId);
		_banners.Add(androidADBanner.id, androidADBanner);
		return androidADBanner;
	}

	public GoogleMobileAdBanner CreateAdBanner(int x, int y, GADBannerSize size)
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("CreateBannerAd shoudl be called only after Init function. Call ignored");
			return null;
		}
		AndroidADBanner androidADBanner = new AndroidADBanner(x, y, size, GADBannerIdFactory.nextId);
		_banners.Add(androidADBanner.id, androidADBanner);
		return androidADBanner;
	}

	public void DestroyBanner(int id)
	{
		if (_banners != null && _banners.ContainsKey(id))
		{
			AndroidADBanner androidADBanner = _banners[id];
			if (androidADBanner.IsLoaded)
			{
				_banners.Remove(id);
				AN_GoogleAdProxy.DestroyBanner(id);
			}
			else
			{
				androidADBanner.DestroyAfterLoad();
			}
		}
	}

	public void StartInterstitialAd()
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("StartInterstitialAd shoudl be called only after Init function. Call ignored");
		}
		else if (IsEditorTestingEnabled)
		{
			_InterstitialShowOnLoad = true;
			SA_EditorAd.OnInterstitialLoadComplete += HandleOnInterstitialLoadComplete_Editor;
			Singleton<SA_EditorAd>.Instance.LoadInterstitial();
		}
		else
		{
			AN_GoogleAdProxy.StartInterstitialAd();
		}
	}

	public void LoadInterstitialAd()
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("LoadInterstitialAd shoudl be called only after Init function. Call ignored");
		}
		else if (IsEditorTestingEnabled)
		{
			SA_EditorAd.OnInterstitialLoadComplete += HandleOnInterstitialLoadComplete_Editor;
			Singleton<SA_EditorAd>.Instance.LoadInterstitial();
		}
		else
		{
			AN_GoogleAdProxy.LoadInterstitialAd();
		}
	}

	private void HandleOnInterstitialLoadComplete_Editor(bool success)
	{
		SA_EditorAd.OnInterstitialLoadComplete -= HandleOnInterstitialLoadComplete_Editor;
		if (success)
		{
			OnInterstitialLoaded__BackingField();
			if (_InterstitialShowOnLoad)
			{
				_InterstitialShowOnLoad = false;
				ShowInterstitialAd();
			}
		}
		else
		{
			OnInterstitialFailedLoading__BackingField();
		}
	}

	public void ShowInterstitialAd()
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("ShowInterstitialAd shoudl be called only after Init function. Call ignored");
		}
		else if (IsEditorTestingEnabled)
		{
			SA_EditorAd.OnInterstitialLeftApplication += HandleOnInterstitialLeftApplication_Editor;
			SA_EditorAd.OnInterstitialFinished += HandleOnInterstitialFinished_Editor;
			Singleton<SA_EditorAd>.Instance.ShowInterstitial();
			OnInterstitialOpened__BackingField();
		}
		else
		{
			AN_GoogleAdProxy.ShowInterstitialAd();
		}
	}

	private void HandleOnInterstitialFinished_Editor(bool isRewarded)
	{
		SA_EditorAd.OnInterstitialLeftApplication -= HandleOnInterstitialLeftApplication_Editor;
		SA_EditorAd.OnInterstitialFinished -= HandleOnInterstitialFinished_Editor;
		OnInterstitialClosed__BackingField();
	}

	private void HandleOnInterstitialLeftApplication_Editor()
	{
		OnInterstitialLeftApplication__BackingField();
	}

	public void StartRewardedVideo()
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("StartRewardedVideo shoudl be called only after Init function. Call ignored");
			return;
		}
		_RewardedVideoShowOnLoad = true;
		LoadRewardedVideo();
	}

	public void LoadRewardedVideo()
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("ShowRewardedVideo shoudl be called only after Init function. Call ignored");
		}
		else if (IsEditorTestingEnabled)
		{
			SA_EditorAd.OnVideoLoadComplete += HandleOnVideoLoadComplete_Editor;
			Singleton<SA_EditorAd>.Instance.LoadVideo();
		}
		else
		{
			AN_GoogleAdProxy.LoadRewardedVideo();
		}
	}

	private void HandleOnVideoLoadComplete_Editor(bool success)
	{
		SA_EditorAd.OnVideoLoadComplete -= HandleOnVideoLoadComplete_Editor;
		if (success)
		{
			OnRewardedVideoLoaded__BackingField();
			if (_RewardedVideoShowOnLoad)
			{
				_RewardedVideoShowOnLoad = false;
				ShowRewardedVideo();
			}
		}
		else
		{
			OnRewardedVideoAdFailedToLoad__BackingField(-1);
		}
	}

	public void ShowRewardedVideo()
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("ShowRewardedVideo shoudl be called only after Init function. Call ignored");
		}
		else if (IsEditorTestingEnabled)
		{
			SA_EditorAd.OnVideoLeftApplication += HandleOnVideoLeftApplication_Editor;
			SA_EditorAd.OnVideoFinished += HandleOnVideoFinished_Editor;
			Singleton<SA_EditorAd>.Instance.ShowVideo();
			OnRewardedVideoAdOpened__BackingField();
		}
		else
		{
			AN_GoogleAdProxy.ShowRewardedVideo();
		}
	}

	private void HandleOnVideoFinished_Editor(bool isRewarded)
	{
		SA_EditorAd.OnVideoLeftApplication -= HandleOnVideoLeftApplication_Editor;
		SA_EditorAd.OnVideoFinished -= HandleOnVideoFinished_Editor;
		OnRewardedVideoAdClosed__BackingField();
	}

	private void HandleOnVideoLeftApplication_Editor()
	{
		OnRewardedVideoAdLeftApplication__BackingField();
	}

	public void RecordInAppResolution(GADInAppResolution resolution)
	{
		AN_GoogleAdProxy.RecordInAppResolution((int)resolution);
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
		AndroidADBanner androidADBanner = GetBanner(id) as AndroidADBanner;
		if (androidADBanner != null)
		{
			androidADBanner.SetDimentions(w, h);
			androidADBanner.OnBannerAdLoaded();
		}
	}

	private void OnBannerAdFailedToLoad(string bannerID)
	{
		int id = Convert.ToInt32(bannerID);
		AndroidADBanner androidADBanner = GetBanner(id) as AndroidADBanner;
		if (androidADBanner != null)
		{
			androidADBanner.OnBannerAdFailedToLoad();
		}
	}

	private void OnBannerAdOpened(string bannerID)
	{
		int id = Convert.ToInt32(bannerID);
		AndroidADBanner androidADBanner = GetBanner(id) as AndroidADBanner;
		if (androidADBanner != null)
		{
			androidADBanner.OnBannerAdOpened();
		}
	}

	private void OnBannerAdClosed(string bannerID)
	{
		int id = Convert.ToInt32(bannerID);
		AndroidADBanner androidADBanner = GetBanner(id) as AndroidADBanner;
		if (androidADBanner != null)
		{
			androidADBanner.OnBannerAdClosed();
		}
	}

	private void OnBannerAdLeftApplication(string bannerID)
	{
		int id = Convert.ToInt32(bannerID);
		AndroidADBanner androidADBanner = GetBanner(id) as AndroidADBanner;
		if (androidADBanner != null)
		{
			androidADBanner.OnBannerAdLeftApplication();
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

	private void RewardedCallback(string data)
	{
		string[] array = data.Split(new string[1] { "|" }, StringSplitOptions.None);
		OnRewarded__BackingField(array[0], int.Parse(array[1]));
	}

	private void RewardedVideoAdClosed()
	{
		OnRewardedVideoAdClosed__BackingField();
	}

	private void RewardedVideoAdFailedToLoad(string errorCode)
	{
		OnRewardedVideoAdFailedToLoad__BackingField(int.Parse(errorCode));
	}

	private void RewardedVideoAdLeftApplication()
	{
		OnRewardedVideoAdLeftApplication__BackingField();
	}

	private void RewardedVideoLoaded()
	{
		OnRewardedVideoLoaded__BackingField();
	}

	private void RewardedVideoAdOpened()
	{
		OnRewardedVideoAdOpened__BackingField();
	}

	private void RewardedVideoStarted()
	{
		OnRewardedVideoStarted__BackingField();
	}

	private void OnInAppPurchaseRequested(string productId)
	{
		OnAdInAppRequest__BackingField(productId);
	}
}
