using UnityEngine;

public class SA_AmazonAdsExample : MonoBehaviour
{
	public string api_key = "f06565f7696840d7adce3d08ea18d742";

	public bool isTestMode = true;

	public static bool isInterstitialLoaded;

	private float RefreshInterval = 30f;

	private bool IsBannerCreated;

	private string message;

	private int bannerId;

	private void Start()
	{
		AMN_Singleton<SA_AmazonAdsManager>.Instance.Create();
		SubscribeToEvents();
		InvokeRepeating("RefreshBanner", RefreshInterval, RefreshInterval);
	}

	private void InitializeAmazonAds()
	{
		if (!AMN_Singleton<SA_AmazonAdsManager>.Instance.IsInitialized)
		{
			SA_StatusBar.text = "Initializing Amazon Ads";
			AMN_Singleton<SA_AmazonAdsManager>.Instance.Init(api_key, isTestMode);
		}
	}

	private void CreateBanner()
	{
		if (AMN_Singleton<SA_AmazonAdsManager>.Instance.IsInitialized)
		{
			bannerId = AMN_Singleton<SA_AmazonAdsManager>.Instance.CreateBanner(AmazonAdBanner.BannerAligns.Bottom);
			IsBannerCreated = true;
		}
		else
		{
			Debug.Log("Amazon ads are not initialized yet");
		}
	}

	private void RefreshBanner()
	{
		if (AMN_Singleton<SA_AmazonAdsManager>.Instance.IsInitialized)
		{
			AMN_Singleton<SA_AmazonAdsManager>.Instance.RefreshBanner(bannerId);
		}
		else
		{
			Debug.Log("Amazon ads are not initialized yet");
		}
	}

	private void DestroyBanner()
	{
		if (AMN_Singleton<SA_AmazonAdsManager>.Instance.IsInitialized)
		{
			if (IsBannerCreated)
			{
				AMN_Singleton<SA_AmazonAdsManager>.Instance.DestroyBanner(bannerId);
				IsBannerCreated = false;
			}
			else
			{
				Debug.Log("Banner is not created yet!");
			}
		}
		else
		{
			Debug.Log("Amazon ads are not initialized yet");
		}
	}

	private void HideBanner()
	{
		HideBanner(true);
	}

	private void ShowBanner()
	{
		HideBanner(false);
	}

	private void HideBanner(bool hide)
	{
		if (AMN_Singleton<SA_AmazonAdsManager>.Instance.IsInitialized)
		{
			if (IsBannerCreated)
			{
				AMN_Singleton<SA_AmazonAdsManager>.Instance.HideBanner(hide, bannerId);
			}
			else
			{
				Debug.Log("Banner is not created yet!");
			}
		}
		else
		{
			Debug.Log("Amazon ads are not initialized yet");
		}
	}

	private void LoadInterstitial()
	{
		if (AMN_Singleton<SA_AmazonAdsManager>.Instance.IsInitialized)
		{
			AMN_Singleton<SA_AmazonAdsManager>.Instance.LoadInterstitial();
		}
		else
		{
			Debug.Log("Amazon ads are not initialized yet");
		}
	}

	public void ShowInterstitial()
	{
		if (AMN_Singleton<SA_AmazonAdsManager>.Instance.IsInitialized)
		{
			if (isInterstitialLoaded)
			{
				AMN_Singleton<SA_AmazonAdsManager>.Instance.ShowInterstitial();
				isInterstitialLoaded = false;
			}
		}
		else
		{
			Debug.Log("Amazon ads are not initialized yet");
		}
	}

	private void SubscribeToEvents()
	{
		AMN_Singleton<SA_AmazonAdsManager>.Instance.OnInterstitialDataReceived += OnInterstitialDataReceived;
		AMN_Singleton<SA_AmazonAdsManager>.Instance.OnInterstitialDismissed += OnInterstitialDismissed;
	}

	private void OnInterstitialDataReceived(AMN_InterstitialDataResult result)
	{
		AMN_AdProperties properties = result.Properties;
		Debug.Log("OnInterstitialDataReceived with result success " + result.isSuccess + " " + properties);
	}

	private void OnInterstitialDismissed(AMN_InterstitialDismissedResult result)
	{
		message = result.Error_message;
		Debug.Log("OnInterstitialDismissed with result success " + result.isSuccess + " and message " + message);
	}
}
