using System.Collections.Generic;
using SA.Common.Pattern;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AndroidAdMobBanner : MonoBehaviour
{
	public string BannersUnityId;

	public GADBannerSize size = GADBannerSize.SMART_BANNER;

	public TextAnchor anchor = TextAnchor.LowerCenter;

	private static Dictionary<string, GoogleMobileAdBanner> _refisterdBanners;

	public static Dictionary<string, GoogleMobileAdBanner> registerdBanners
	{
		get
		{
			if (_refisterdBanners == null)
			{
				_refisterdBanners = new Dictionary<string, GoogleMobileAdBanner>();
			}
			return _refisterdBanners;
		}
	}

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
			if (!Singleton<AndroidAdMobController>.Instance.BannersUunitId.Equals(BannersUnityId))
			{
				Singleton<AndroidAdMobController>.Instance.SetBannersUnitID(BannersUnityId);
			}
		}
		else
		{
			Singleton<AndroidAdMobController>.Instance.Init(BannersUnityId);
		}
	}

	private void Start()
	{
		ShowBanner();
	}

	private void OnDestroy()
	{
		HideBanner();
	}

	public void ShowBanner()
	{
		GoogleMobileAdBanner googleMobileAdBanner;
		if (registerdBanners.ContainsKey(sceneBannerId))
		{
			googleMobileAdBanner = registerdBanners[sceneBannerId];
		}
		else
		{
			googleMobileAdBanner = Singleton<AndroidAdMobController>.Instance.CreateAdBanner(anchor, size);
			registerdBanners.Add(sceneBannerId, googleMobileAdBanner);
		}
		if (googleMobileAdBanner.IsLoaded && !googleMobileAdBanner.IsOnScreen)
		{
			googleMobileAdBanner.Show();
		}
	}

	public void HideBanner()
	{
		if (!registerdBanners.ContainsKey(sceneBannerId))
		{
			return;
		}
		GoogleMobileAdBanner googleMobileAdBanner = registerdBanners[sceneBannerId];
		if (googleMobileAdBanner.IsLoaded)
		{
			if (googleMobileAdBanner.IsOnScreen)
			{
				googleMobileAdBanner.Hide();
			}
		}
		else
		{
			googleMobileAdBanner.ShowOnLoad = false;
		}
	}
}
