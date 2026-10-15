using SA.Common.Pattern;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AndroidAdMobBannerInterstitial : MonoBehaviour
{
	public string InterstitialUnityId;

	public string sceneBannerId
	{
		get
		{
			return SceneManager.GetActiveScene().name + "_" + base.gameObject.name;
		}
	}

	private void Awake()
	{
		if (Singleton<AndroidAdMobController>.Instance.IsInited)
		{
			if (!Singleton<AndroidAdMobController>.Instance.InterstisialUnitId.Equals(InterstitialUnityId))
			{
				Singleton<AndroidAdMobController>.Instance.SetInterstisialsUnitID(InterstitialUnityId);
			}
		}
		else
		{
			Singleton<AndroidAdMobController>.Instance.Init(InterstitialUnityId);
		}
	}

	private void Start()
	{
		ShowBanner();
	}

	public void ShowBanner()
	{
		Singleton<AndroidAdMobController>.Instance.StartInterstitialAd();
	}
}
