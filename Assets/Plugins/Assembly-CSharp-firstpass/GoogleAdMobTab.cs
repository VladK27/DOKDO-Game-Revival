using SA.Common.Pattern;
using UnityEngine;
using UnityEngine.UI;

public class GoogleAdMobTab : FeatureTab
{
	private const string MY_BANNERS_AD_UNIT_ID = "ca-app-pub-6101605888755494/1824764765";

	private const string MY_INTERSTISIALS_AD_UNIT_ID = "ca-app-pub-6101605888755494/3301497967";

	private const string MY_REWARDED_VIDEO_AD_UNIT_ID = "ca-app-pub-6101605888755494/5378283960";

	private GoogleMobileAdBanner Banner;

	private GoogleMobileAdBanner SmartBanner;

	private bool IsInterstisialsAdReady;

	[SerializeField]
	private Button ShowInterstitialButton;

	public Toggle CustomPosTgl;

	public Toggle UpperLeftTgl;

	public Toggle UpperCenterTgl;

	public Toggle BottomLeftTgl;

	public Toggle BottomCenterTgl;

	public Toggle BottomRightTgl;

	public Button BannerHideBtn;

	public Button BannerCreateBtn;

	public Button BannerRefreshBtn;

	public Button BannerChangePosToMiddleBtn;

	public Button BannerChangePosRandomBtn;

	public Button BannerDestroyBtn;

	public Toggle SmartBotPosTgl;

	public Toggle SmartTopPosTgl;

	public Button SmartHide;

	public Button SmartCreate;

	public Button SmartRefresh;

	public Button SmartDestroy;

	private TextAnchor? BannerPosition;

	private TextAnchor SmartBannerPosition = TextAnchor.UpperCenter;

	private void Start()
	{
		AndroidAdMob.Client.Init("ca-app-pub-6101605888755494/1824764765");
		AndroidAdMob.Client.SetInterstisialsUnitID("ca-app-pub-6101605888755494/3301497967");
		Singleton<AndroidAdMobController>.Instance.SetRewardedVideoAdUnitID("ca-app-pub-6101605888755494/5378283960");
		AndroidAdMob.Client.SetGender(GoogleGender.Male);
		AndroidAdMob.Client.AddKeyword("game");
		AndroidAdMob.Client.SetBirthday(1989, AndroidMonth.MARCH, 18);
		AndroidAdMob.Client.TagForChildDirectedTreatment(false);
		AndroidAdMob.Client.OnInterstitialLoaded += OnInterstisialsLoaded;
		AndroidAdMob.Client.OnInterstitialOpened += OnInterstisialsOpen;
		Singleton<AndroidAdMobController>.Instance.OnRewardedVideoLoaded += HandleOnRewardedVideoLoaded;
		Singleton<AndroidAdMobController>.Instance.OnRewardedVideoAdClosed += HandleOnRewardedVideoAdClosed;
		AndroidAdMob.Client.OnAdInAppRequest += OnInAppRequest;
		CustomPosTgl.onValueChanged.AddListener((bool b) =>
		{
			if (b)
			{
				BannerPosition = null;
			}
		});
		UpperLeftTgl.onValueChanged.AddListener((bool b) =>
		{
			if (b)
			{
				BannerPosition = TextAnchor.UpperLeft;
			}
		});
		UpperCenterTgl.onValueChanged.AddListener((bool b) =>
		{
			if (b)
			{
				BannerPosition = TextAnchor.UpperCenter;
			}
		});
		BottomLeftTgl.onValueChanged.AddListener((bool b) =>
		{
			if (b)
			{
				BannerPosition = TextAnchor.LowerLeft;
			}
		});
		BottomCenterTgl.onValueChanged.AddListener((bool b) =>
		{
			if (b)
			{
				BannerPosition = TextAnchor.LowerCenter;
			}
		});
		BottomRightTgl.onValueChanged.AddListener((bool b) =>
		{
			if (b)
			{
				BannerPosition = TextAnchor.LowerRight;
			}
		});
		SmartTopPosTgl.onValueChanged.AddListener((bool b) =>
		{
			if (b)
			{
				SmartBannerPosition = TextAnchor.UpperCenter;
			}
		});
		SmartBotPosTgl.onValueChanged.AddListener((bool b) =>
		{
			if (b)
			{
				SmartBannerPosition = TextAnchor.LowerCenter;
			}
		});
	}

	private void HandleOnRewardedVideoAdClosed()
	{
	}

	private void HandleOnRewardedVideoLoaded()
	{
	}

	public void StartInterstitialAd()
	{
		AndroidAdMob.Client.StartInterstitialAd();
	}

	public void LoadInterstitialAd()
	{
		AndroidAdMob.Client.LoadInterstitialAd();
	}

	public void ShowInterstitialAd()
	{
		AndroidAdMob.Client.ShowInterstitialAd();
	}

	public void LoadRewardedVideoAd()
	{
		Singleton<AndroidAdMobController>.Instance.LoadRewardedVideo();
	}

	public void ShowRewardedVideoAd()
	{
		Singleton<AndroidAdMobController>.Instance.ShowRewardedVideo();
	}

	public void BannerHide()
	{
		Banner.Hide();
	}

	public void BannerShow()
	{
		TextAnchor? bannerPosition = BannerPosition;
		Banner = ((!bannerPosition.HasValue) ? AndroidAdMob.Client.CreateAdBanner(300, 100, GADBannerSize.BANNER) : AndroidAdMob.Client.CreateAdBanner(BannerPosition.Value, GADBannerSize.BANNER));
	}

	public void BannerRefresh()
	{
		Banner.Refresh();
	}

	public void BannerDestroy()
	{
		AndroidAdMob.Client.DestroyBanner(Banner.id);
		Banner = null;
	}

	public void SmartBannerHide()
	{
		SmartBanner.Hide();
	}

	public void SmartBannerShow()
	{
		SmartBanner = AndroidAdMob.Client.CreateAdBanner(SmartBannerPosition, GADBannerSize.SMART_BANNER);
	}

	public void SmartBannerRefresh()
	{
		SmartBanner.Refresh();
	}

	public void SmartBannerDestroy()
	{
		AndroidAdMob.Client.DestroyBanner(SmartBanner.id);
		SmartBanner = null;
	}

	public void ChnagePostToMiddle()
	{
		Banner.SetBannerPosition(TextAnchor.MiddleCenter);
	}

	public void ChangePostRandom()
	{
		Banner.SetBannerPosition(Random.Range(0, Screen.width), Random.Range(0, Screen.height));
	}

	private void FixedUpdate()
	{
		if (IsInterstisialsAdReady)
		{
			ShowInterstitialButton.interactable = true;
		}
		else
		{
			ShowInterstitialButton.interactable = false;
		}
		if (Banner != null)
		{
			BannerDestroyBtn.interactable = true;
			if (Banner.IsLoaded)
			{
				BannerRefreshBtn.interactable = true;
				BannerChangePosToMiddleBtn.interactable = true;
				BannerChangePosRandomBtn.interactable = true;
				if (Banner.IsOnScreen)
				{
					BannerHideBtn.interactable = true;
					BannerCreateBtn.interactable = false;
				}
				else
				{
					BannerHideBtn.interactable = false;
					BannerCreateBtn.interactable = true;
				}
			}
			else
			{
				BannerRefreshBtn.interactable = false;
				BannerChangePosToMiddleBtn.interactable = false;
				BannerChangePosRandomBtn.interactable = false;
				BannerHideBtn.interactable = false;
				BannerCreateBtn.interactable = false;
			}
		}
		else
		{
			BannerHideBtn.interactable = false;
			BannerCreateBtn.interactable = true;
			BannerRefreshBtn.interactable = false;
			BannerDestroyBtn.interactable = false;
			BannerChangePosToMiddleBtn.interactable = false;
			BannerChangePosRandomBtn.interactable = false;
		}
		if (SmartBanner != null)
		{
			SmartDestroy.interactable = true;
			if (SmartBanner.IsLoaded)
			{
				SmartRefresh.interactable = true;
				if (SmartBanner.IsOnScreen)
				{
					SmartHide.interactable = true;
					SmartCreate.interactable = false;
				}
				else
				{
					SmartHide.interactable = false;
					SmartCreate.interactable = true;
				}
			}
			else
			{
				SmartRefresh.interactable = false;
				SmartHide.interactable = false;
				SmartCreate.interactable = false;
			}
		}
		else
		{
			SmartHide.interactable = false;
			SmartCreate.interactable = true;
			SmartRefresh.interactable = false;
			SmartDestroy.interactable = false;
		}
	}

	private void OnInterstisialsLoaded()
	{
		IsInterstisialsAdReady = true;
	}

	private void OnInterstisialsOpen()
	{
		IsInterstisialsAdReady = false;
	}

	private void OnInAppRequest(string productId)
	{
		AN_PoupsProxy.showMessage("In App Request", "In App Request for product Id: " + productId + " received");
		AndroidAdMob.Client.RecordInAppResolution(GADInAppResolution.RESOLUTION_SUCCESS);
	}
}
