using System.Collections;
using System.Collections.Generic;
using System.Text;
using Facebook.Unity;
using Percent;
using Percent.Event;
using SA.Analytics.Google;
using SA.Common.Pattern;
using UnityEngine;

public class ThirdPartyManager : MonoBehaviour
{
	private PercentTracker tracker;

	public static ThirdPartyManager instance;

	private GameManager GM;

	private MNPopup popup2;

	private Dictionary<string, string> purchaseEvent = new Dictionary<string, string>();

	private RewardType currentReward;

	private string Url = "https://api.stage31.net/v3/receipt/validation";

	public string DevUrl = "https://adev.stage31.net/v3/receipt/validation";

	private string gameID = "24204120";

	private string uuid = "0000";

	private float purchaseAmount;

	private string currency;

	private bool isValid;

	private void Awake()
	{
		if (instance != null)
		{
			return;
		}
		instance = this;
		GM = GameManager.Instance;
		popup2 = new MNPopup("SYSTEM", "Purchase Success");
		popup2.AddAction("Ok", () =>
		{
			Debug.Log("Ok action callback");
			MNP.HidePreloader();
		});
		popup2.AddDismissListener(() =>
		{
			Debug.Log("dismiss listener");
			MNP.HidePreloader();
		});
		Init();
		if (FB.IsInitialized)
		{
			FB.ActivateApp();
			return;
		}
		FB.Init(() =>
		{
			FB.ActivateApp();
		});
	}

	private void Init()
	{
		GA_Settings.Instance.AppVersion = "구글 " + Application.version;
		Manager.StartTracking();
		string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
		IronSource.Agent.setUserId(deviceUniqueIdentifier);
		string appKey = "96a171ad";
		IronSource.Agent.init(appKey);
		IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
		IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
		IronSource.Agent.loadInterstitial();
		IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
		IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
		tracker = base.gameObject.AddComponent<PercentTracker>();
		Singleton<UM_GameServiceManager>.Instance.Connect();
		UM_InAppPurchaseManager.Client.Connect();
		AppsFlyerSet();
	}

	private void AppsFlyerSet()
	{
		AppsFlyer.setAppsFlyerKey("7HW3VK6cCesiqWg4V99uW6");
		AppsFlyer.setAppID("com.zzoo.dokdo");
		AppsFlyer.init("7HW3VK6cCesiqWg4V99uW6", "AppsFlyerTrackerCallbacks");
	}

	public void ShowInterstitialAds()
	{
		if (CrossPromotion.isInterstitialShowable())
		{
			IronSource.Agent.showInterstitial();
			tracker.triggerSeeAdsInterstitial();
		}
	}

	public void ShowVideoAds(RewardType type)
	{
		if (GM.noAds)
		{
			Reward(type);
			return;
		}
		currentReward = type;
		IronSource.Agent.showRewardedVideo();
	}

	private void InterstitialAdOpenedEvent()
	{
		GM.TempMuteBGM(false);
	}

	private void InterstitialAdClosedEvent()
	{
		GM.TempMuteBGM(true);
		IronSource.Agent.loadInterstitial();
	}

	private void RewardedVideoAdOpenedEvent()
	{
		Time.timeScale = 0f;
		GM.TempMuteBGM(false);
	}

	private void RewardedVideoAdClosedEvent()
	{
		Time.timeScale = 1f;
		GM.TempMuteBGM(true);
		tracker.triggerSeeAdsReward();
		Reward(currentReward);
	}

	private void Reward(RewardType type)
	{
		switch (type)
		{
		case RewardType.Coin:
			GM.FreeCoinAds();
			Manager.Client.SendEventHit("Ads_Free Coin", "Tier_" + GM.CurrentTier, "BodyLevel_" + GM.Level_Body, 1);
			break;
		case RewardType.HP100:
			GM.FullHP();
			Manager.Client.SendEventHit("Ads_Fix", "Tier_" + GM.CurrentTier, "BodyLevel_" + GM.Level_Body, 1);
			break;
		case RewardType.HP30:
			Object.FindObjectOfType<Panel_dead>().Revive(30);
			Manager.Client.SendEventHit("Ads_ReBorn", "Tier_" + GM.CurrentTier, "BodyLevel_" + GM.Level_Body, 1);
			break;
		case RewardType.MarketReset:
			GM.AdsMarketReset();
			Manager.Client.SendEventHit("Ads_MarketReset", "Tier_" + GM.CurrentTier, "BodyLevel_" + GM.Level_Body, 1);
			break;
		case RewardType.AdsBox:
			GM.AdsBoxReward();
			GM.AddHP((int)((float)(int)GM.MyHP() * 0.3f));
			Manager.Client.SendEventHit("Ads_Box", "Tier_" + GM.CurrentTier, "BodyLevel_" + GM.Level_Body, 1);
			break;
		}
		GM.Save();
	}

	public void Restore()
	{
		MNP.HidePreloader();
		MNP.ShowPreloader("SYSTEM", "LOADING");
		bool IsPurchased1 = UM_InAppPurchaseManager.Client.IsProductPurchased("dokdo_noads");
		bool IsPurchased2 = UM_InAppPurchaseManager.Client.IsProductPurchased("dokdo_pick");
		if (IsPurchased1)
		{
			MNP.HidePreloader();
			MNPopup mNPopup = new MNPopup("SYSTEM", "Restore Complete");
			mNPopup.AddAction("Ok", () =>
			{
				if (IsPurchased1)
				{
					GM.noAds = true;
					GM.Save();
				}
				if (IsPurchased2)
				{
					GM.pickboat = true;
					EventManager.TriggerEvent(MyEvent.AssiUpdate);
					ShipController component = GameObject.Find("MyPos").GetComponent<ShipController>();
					if (component != null)
					{
						component.SetAssi();
					}
					GM.Save();
				}
				Debug.Log("Get Item");
			});
			mNPopup.AddDismissListener(() =>
			{
				Debug.Log("dismiss listener");
			});
			mNPopup.Show();
		}
		else
		{
			MNP.HidePreloader();
			MNPopup mNPopup2 = new MNPopup("SYSTEM", "You don't have Restore Items");
			mNPopup2.AddAction("Ok", () =>
			{
				Debug.Log("Ok action callback");
			});
			mNPopup2.AddDismissListener(() =>
			{
				Debug.Log("dismiss listener");
			});
			mNPopup2.Show();
		}
	}

	public void BuyProduct(string product)
	{
		UM_InAppPurchaseManager.Client.OnPurchaseFinished += OnPurchaseFlowFinishedAction;
		MNP.HidePreloader();
		if (UUIDLoader.hasUUID())
		{
			uuid = UUIDLoader.tryGetUUID();
		}
		if (UM_InAppPurchaseManager.Client.IsConnected)
		{
			UM_InAppPurchaseManager.Client.Purchase(product);
			Debug.Log("구매 시작 : " + product);
		}
		else
		{
			UM_InAppPurchaseManager.Client.OnPurchaseFinished -= OnPurchaseFlowFinishedAction;
			Debug.Log("Not Connected");
		}
	}

	private IEnumerator inappResult(string product)
	{
		yield return new WaitForSeconds(0.1f);
		MNP.HidePreloader();
		yield return new WaitForSeconds(0.1f);
		Debug.Log("보상 지급");
		tracker.triggerInAppPurchase(product);
		if ((int)GM.MyCurrnetHP <= 0)
		{
			if (isValid)
			{
				Manager.Client.SendEventHit("IAP_Dead", "Tier_" + GM.CurrentTier, "BodyLevel_" + GM.Level_Body, 1);
			}
		}
		else if (isValid)
		{
			Manager.Client.SendEventHit("IAP_In_Island", "Total_Island_" + GM.TotalIsland(), "BodyLevel_" + GM.Level_Body, 1);
		}
		switch (product)
		{
		case "dokdo_noads":
			GM.noAds = true;
			GM.PosiSound(1);
			if (isValid)
			{
				Manager.Client.SendEventHit("IAP_NoAds", "Tier_" + GM.CurrentTier, "BodyLevel_" + GM.Level_Body, 1);
			}
			break;
		case "dokdo_coin1":
			GM.AddMoney(30000);
			if (isValid)
			{
				Manager.Client.SendEventHit("IAP_30000", "Tier_" + GM.CurrentTier, "BodyLevel_" + GM.Level_Body, 1);
			}
			break;
		case "dokdo_coin2":
			GM.AddMoney(130000);
			if (isValid)
			{
				Manager.Client.SendEventHit("IAP_130000", "Tier_" + GM.CurrentTier, "BodyLevel_" + GM.Level_Body, 1);
			}
			break;
		case "dokdo_coin3":
			GM.AddMoney(400000);
			if (isValid)
			{
				Manager.Client.SendEventHit("IAP_400000", "Tier_" + GM.CurrentTier, "BodyLevel_" + GM.Level_Body, 1);
			}
			break;
		case "dokdo_coin4":
			GM.AddMoney(800000);
			if (isValid)
			{
				Manager.Client.SendEventHit("IAP_800000", "Tier_" + GM.CurrentTier, "BodyLevel_" + GM.Level_Body, 1);
			}
			break;
		case "dokdo_package":
			GM.AddResource(itemType.coin, 1000000);
			GM.AddResource(itemType.fish, 25000);
			GM.AddResource(itemType.wood, 5000);
			GM.AddResource(itemType.iron, 2500);
			GM.AddResource(itemType.silver, 500);
			GM.AddResource(itemType.gold, 250);
			GM.AddResource(itemType.diamond, 25);
			GM.CoinSound();
			if (isValid)
			{
				Manager.Client.SendEventHit("IAP_Package", "Tier_" + GM.CurrentTier, "BodyLevel_" + GM.Level_Body, 1);
			}
			break;
		case "dokdo_revival10":
			GM.AddRevival(10);
			GM.PosiSound(1);
			if (isValid)
			{
				Manager.Client.SendEventHit("IAP_Revival10", "Tier_" + GM.CurrentTier, "BodyLevel_" + GM.Level_Body, 1);
			}
			break;
		case "dokdo_revival100":
			GM.AddRevival(100);
			GM.PosiSound(1);
			if (isValid)
			{
				Manager.Client.SendEventHit("IAP_Revival100", "Tier_" + GM.CurrentTier, "BodyLevel_" + GM.Level_Body, 1);
			}
			break;
		case "dokdo_coin5":
			GM.AddMoney(5000000);
			GM.coinDay = UnbiasedTime.Instance.Now().AddDays(7.0).ToString();
			if (isValid)
			{
				Manager.Client.SendEventHit("IAP_5000000", "Tier_" + GM.CurrentTier, "BodyLevel_" + GM.Level_Body, 1);
			}
			break;
		case "dokdo_pick":
		{
			GM.pickboat = true;
			EventManager.TriggerEvent(MyEvent.AssiUpdate);
			ShipController component = GameObject.Find("MyPos").GetComponent<ShipController>();
			if (component != null)
			{
				component.SetAssi();
			}
			if (isValid)
			{
				Manager.Client.SendEventHit("IAP_pick", "Tier_" + GM.CurrentTier, "BodyLevel_" + GM.Level_Body, 1);
			}
			Debug.Log("123");
			break;
		}
		}
		GM.Save();
		MNP.HidePreloader();
		yield return new WaitForSeconds(0.1f);
		popup2.Show();
		MNP.HidePreloader();
		if (isValid)
		{
			FB.LogPurchase(purchaseAmount, currency);
			purchaseEvent.Clear();
			purchaseEvent.Add("af_currency", currency);
			purchaseEvent.Add("af_revenue", purchaseAmount.ToString());
			purchaseEvent.Add("af_quantity", "1");
			AppsFlyer.trackRichEvent("af_purchase", purchaseEvent);
		}
	}

	private void OnPurchaseFlowFinishedAction(UM_PurchaseResult result)
	{
		Debug.Log("구매 종료");
		MNP.HidePreloader();
		UM_InAppPurchaseManager.Client.OnPurchaseFinished -= OnPurchaseFlowFinishedAction;
		if (result.isSuccess)
		{
			StartCoroutine(PostReceipt(result));
			return;
		}
		MNP.HidePreloader();
		Debug.Log("제품 : " + result.product.id + "구매 실패 또는 취소");
	}

	private IEnumerator PostReceipt(UM_PurchaseResult result)
	{
		Debug.Log("영수증 검증 시작");
		isValid = false;
		purchaseAmount = result.product.ActualPriceValue;
		currency = result.product.CurrencyCode;
		string os = "android";
		StringBuilder sb = new StringBuilder();
		sb.Append("{\"packageName\":\"");
		sb.Append(result.Google_PurchaseInfo.PackageName);
		sb.Append("\",\"productId\":\"");
		sb.Append(result.Google_PurchaseInfo.SKU);
		sb.Append("\",\"token\":\"");
		sb.Append(result.Google_PurchaseInfo.Token);
		sb.Append("\"}");
		string payload = sb.ToString();
		WWWForm data = new WWWForm();
		data.AddField("os", os);
		data.AddField("gameId", gameID);
		data.AddField("uuid", uuid);
		data.AddField("payload", payload);
		string url = Url;
		if (Debug.isDebugBuild)
		{
			url = DevUrl;
		}
		WWW www = new WWW(url, data);
		float timer = 0f;
		while (!www.isDone)
		{
			if (timer > 10f)
			{
				www.Dispose();
				MNP.HidePreloader();
				Debug.Log("시간 초과");
				StartCoroutine(inappResult(result.product.id));
				break;
			}
			timer += Time.deltaTime;
			yield return null;
		}
		MNP.HidePreloader();
		yield return www;
		if (www.text.Equals("false"))
		{
			Debug.Log("영수증 비일치");
			www.Dispose();
			MNP.HidePreloader();
			MNPopup mNPopup = new MNPopup("SYSTEM", "Unverified Order");
			mNPopup.AddAction("Ok", () =>
			{
				Debug.Log("Ok action callback");
			});
			mNPopup.AddDismissListener(() =>
			{
				Debug.Log("dismiss listener");
			});
			mNPopup.Show();
		}
		else
		{
			if (www.text.Equals("true"))
			{
				Debug.Log("영수증 일치");
				isValid = true;
				MNP.HidePreloader();
			}
			www.Dispose();
			MNP.HidePreloader();
			StartCoroutine(inappResult(result.product.id));
		}
	}
}
