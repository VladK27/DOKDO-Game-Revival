using System;
using Boomlagoon.JSON;
using Percent.Event;
using Percent.View;
using UnityEngine;

namespace Percent
{
	public class CrossPromotion : MonoBehaviour
	{
		public enum Status
		{
			INIT = 0,
			LOAD_SUCCESS = 1,
			LOAD_FAIL = 2,
			SHOW = 3,
			HIDE = 4,
			DESTROY = 5
		}

		public delegate void OnShowDelegate();

		public delegate void OnCloseDelegate();

		public delegate void OnLoadDataDelegate(bool isSuccess);

		public delegate void OnDestroyDelegate();

		internal delegate void OnStateChange(Status state);

		public static CrossPromotion instance;

		public Company company;

		public PrivacyPopup privacyPopup;

		public static bool hasAgreed = true;

		public bool testEURegion;

		public OnShowDelegate onShow;

		public OnCloseDelegate onClose;

		public OnLoadDataDelegate onLoadData;

		public OnDestroyDelegate onDestroy;

		private Status state;

		internal OnStateChange onStateChange;

		private TextureLifeCycle textureLifeCycle;

		private SceneChecker sceneChecker;

		private Interstitial interstitial;

		public static readonly string PREF_AGREEMENT = "GDPRAgreement";

		public static bool isFirstScreen = true;

		public static JSONObject ExtraData
		{
			get
			{
				return PromotionData.extraData;
			}
		}

		private Status State
		{
			set
			{
				state = value;
				if (onStateChange != null)
				{
					onStateChange(state);
				}
			}
		}

		public static void showAds(bool placeHolder = true)
		{
			if (instance != null)
			{
				instance.show();
			}
			else
			{
				Logger.error("Null Instance. showAds() will NOT be executed.");
			}
		}

		private void show()
		{
			sceneChecker.saveCalledShowScene();
			if (state.Equals(Status.LOAD_SUCCESS))
			{
				State = Status.SHOW;
				triggerShowEvent();
				if (onShow != null)
				{
					onShow();
				}
			}
		}

		private void triggerShowEvent()
		{
			PromotionType type = PromotionData.crossPromotionData.type;
			int gameId = PromotionData.crossPromotionData.gameId;
			base.gameObject.AddComponent<ExposeTracker>().trigger(type, gameId);
		}

		public static void addShowAdsToQueue()
		{
			if (instance == null)
			{
				Logger.error("CrossPromotion is null. isInterstitialShowable return true.");
			}
			else
			{
				instance.setPrivacyCompletion();
			}
		}

		private void setPrivacyCompletion()
		{
			onLoadData = (OnLoadDataDelegate)Delegate.Combine(onLoadData, new OnLoadDataDelegate(showAds));
		}

		public void hide()
		{
			if (state.Equals(Status.SHOW) && onClose != null)
			{
				onClose();
			}
			State = Status.HIDE;
		}

		public static bool isInterstitialShowable()
		{
			if (instance != null)
			{
				return instance.interstitial.isShowable();
			}
			Logger.error("CrossPromotion is null. isInterstitialShowable return true.");
			return true;
		}

		public static void reportShowInterstitial()
		{
			instance.interstitial.stampLastShowTime();
		}

		public static bool isEURegion()
		{
			if (instance.testEURegion)
			{
				return true;
			}
			string region = Util.getRegion();
			string[] eU_CODES = PrivacyPopup.EU_CODES;
			foreach (string text in eU_CODES)
			{
				if (text.Equals(region))
				{
					return true;
				}
			}
			return false;
		}

		public static void showPrivacyWindow()
		{
			if (instance == null)
			{
				Logger.error("Null Instance. showAds() will NOT be executed.");
				return;
			}
			if (!instance.privacyPopup.gameObject.activeSelf)
			{
				instance.privacyPopup.gameObject.SetActive(true);
			}
			if (isFirstScreen && PlayerPrefs.HasKey(PREF_AGREEMENT))
			{
				instance.privacyPopup.isSendTracker = false;
				instance.privacyPopup.hideSelf();
			}
			else
			{
				instance.privacyPopup.showWindow();
			}
		}

		public static Company getCompany()
		{
			if (instance != null)
			{
				return instance.company;
			}
			Logger.error("Null Instance. showAds() will NOT be executed.");
			return Company.Percent;
		}

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			if (Config.didCrossPromotionIDSet())
			{
				onInitialize();
			}
			else
			{
				Logger.error("Please set cross promotion ID X(");
			}
		}

		private void onInitialize()
		{
			State = Status.INIT;
			UnityEngine.Object.DontDestroyOnLoad(this);
			sceneChecker = base.gameObject.AddComponent<SceneChecker>();
			textureLifeCycle = GameObject.Find("TextureLifeCycle").GetComponent<TextureLifeCycle>();
			interstitial = GameObject.Find("Interstitial").GetComponent<Interstitial>();
		}

		internal void onPromotionDataLoad(bool isSuccess)
		{
			if (isSuccess)
			{
				textureLifeCycle.load();
				return;
			}
			State = Status.LOAD_FAIL;
			if (onLoadData != null)
			{
				onLoadData(false);
			}
			Logger.error("Could NOT Load Promotion Data.");
			hide();
		}

		internal void onTextureLoad(bool isSuccess)
		{
			if (isSuccess)
			{
				State = Status.LOAD_SUCCESS;
				if (onLoadData != null)
				{
					onLoadData(true);
				}
				if (sceneChecker.isShowableScene())
				{
					show();
				}
			}
			else
			{
				State = Status.LOAD_FAIL;
				if (onLoadData != null)
				{
					onLoadData(false);
				}
				hide();
			}
		}

		private void OnDestroy()
		{
			State = Status.DESTROY;
			if (onDestroy != null)
			{
				onDestroy();
			}
		}
	}
}
