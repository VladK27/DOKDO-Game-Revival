using System;
using System.Text.RegularExpressions;
using Boomlagoon.JSON;
using Percent.Http;
using Percent.Tween;
using UnityEngine;
using UnityEngine.UI;

namespace Percent.View
{
	public class PrivacyPopup : MonoBehaviour
	{
		public ColorTween showBackgroundColorTweener;

		public PercentTween showFrameScaleTweener;

		public PercentTween showWaitFrameScaleTweener;

		public GameObject waitPopupGO;

		public TurnOnOffWindow turnOnOffWindow;

		public UUIDLoader uuidLoader;

		public Text descriptionText;

		public TextLoader textLoader;

		public RectTransform scrollMe;

		public GameObject thirdParties;

		public RawImage background;

		private TextTool textTool;

		private Scrollbar scrollBar;

		public int privacyPolicyVersion;

		public bool isSendTracker = true;

		private bool isEURegion = true;

		private readonly string PRIVACY_POLICY_VERSION = "PrivacyPolicyVersion";

		private readonly string PRIVACY_POLICY = "privacyPolicy";

		public static readonly string[] EU_CODES = new string[32]
		{
			"AT", "BE", "BG", "HR", "CY", "CZ", "DK", "EE", "FI", "FR",
			"DE", "GR", "HU", "IE", "IT", "LV", "LT", "LU", "MT", "NL",
			"PL", "PT", "RO", "SK", "SI", "ES", "SE", "AE", "BGN", "HRK",
			"EUR", "RON"
		};

		private AndroidJavaObject activityContext;

		private AndroidJavaObject className;

		private AndroidJavaObject pluginClass;

		private void Start()
		{
			Application.RequestAdvertisingIdentifierAsync((string advertisingId, bool trackingEnabled, string error) =>
			{
				UUIDLoader.advertisingId = advertisingId;
				UUIDLoader.isTrackingEnabled = trackingEnabled;
			});
			if (PlayerPrefs.HasKey(PRIVACY_POLICY_VERSION))
			{
				privacyPolicyVersion = PlayerPrefs.GetInt(PRIVACY_POLICY_VERSION);
			}
			textTool = new TextTool();
			replaceDescription(textLoader.readDeafultDescription());
			hideSelfWithoutThirdparties();
		}

		public void showWindow()
		{
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			textLoader.request(onReceieveText);
		}

		private void playShowWindow()
		{
			CancelInvoke();
			background.raycastTarget = true;
			CrossPromotion.hasAgreed = true;
			scrollBar = showFrameScaleTweener.transform.Find("InnerTextBox").Find("Scrollbar").GetComponent<Scrollbar>();
			showFrameScaleTweener.onEnd = () =>
			{
				scrollBar.GetComponent<ScrollToTop>().repositionScrollbar();
			};
			showBackgroundColorTweener.play();
			showFrameScaleTweener.play();
		}

		private void onReceieveText(bool isSuccess)
		{
			if (isSuccess)
			{
				if (isEURegion)
				{
					playShowWindow();
				}
			}
			else
			{
				hideSelf();
			}
		}

		public void hideWindowWithAgreement()
		{
			InvokeRepeating("getAndroidAdvertisingId", 0f, 0.1f);
		}

		private void getAndroidAdvertisingId()
		{
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					activityContext = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
				}
				if (pluginClass != null)
				{
					className = pluginClass.CallStatic<AndroidJavaObject>("getInstance", new object[0]);
					activityContext.Call("runOnUiThread", (AndroidJavaRunnable)(() =>
					{
						className.Call("initialize", activityContext);
						if (pluginClass.GetStatic<bool>("hasFinished"))
						{
							UUIDLoader.advertisingId = pluginClass.GetStatic<string>("strAdid");
							UUIDLoader.isTrackingEnabled = pluginClass.GetStatic<bool>("isTrackingEnabled");
							checkTrackability();
							CancelInvoke();
						}
					}));
				}
				else
				{
					pluginClass = new AndroidJavaClass("com.percent.crosspromotion.GetAdvertisingId");
				}
			}
			catch (Exception ex)
			{
				Debug.Log("error: " + ex.Message);
			}
		}

		private void checkTrackability()
		{
			if (UUIDLoader.isTrackingEnabled)
			{
				CrossPromotion.hasAgreed = true;
				invokeHide();
			}
			else
			{
				turnOnOffWindow.showTurnOnWindow();
				showFrameScaleTweener.startDelay = 0f;
				showFrameScaleTweener.playReverse();
			}
		}

		public void invokeHide()
		{
			showFrameScaleTweener.startDelay = 0f;
			showFrameScaleTweener.playReverse();
			PercentTween percentTween = showFrameScaleTweener;
			percentTween.onEnd = (PercentTween.OnTweenEnd)Delegate.Combine(percentTween.onEnd, new PercentTween.OnTweenEnd(hideSelf));
			showBackgroundColorTweener.startDelay = 0f;
			showBackgroundColorTweener.playReverse();
		}

		public void hideSelf()
		{
			PlayerPrefs.SetInt(CrossPromotion.PREF_AGREEMENT, 1);
			hideSelfWithoutThirdparties();
			if (!thirdParties.activeSelf)
			{
				thirdParties.SetActive(true);
			}
			if (CrossPromotion.isFirstScreen)
			{
				CrossPromotion.isFirstScreen = false;
				SessionLifeCycle.initializeCrossPromotionSession();
			}
			Invoke("inactivateSelf", 0.5f);
		}

		private void inactivateSelf()
		{
			base.gameObject.SetActive(false);
			if (isSendTracker)
			{
				if (!CrossPromotion.hasAgreed)
				{
				}
			}
			else
			{
				isSendTracker = true;
			}
		}

		private void hideSelfWithoutThirdparties()
		{
			turnOnOffWindow.GetComponent<RectTransform>().localScale = Vector3.zero;
			PercentTween percentTween = showFrameScaleTweener;
			percentTween.onEnd = (PercentTween.OnTweenEnd)Delegate.Remove(percentTween.onEnd, new PercentTween.OnTweenEnd(hideSelf));
			background.raycastTarget = false;
		}

		public void showWaitWindow()
		{
			waitPopupGO.SetActive(true);
			showWaitFrameScaleTweener.play();
			showFrameScaleTweener.playReverse();
			CrossPromotion.hasAgreed = false;
		}

		public void showFrameWindow()
		{
			showFrameScaleTweener.play();
		}

		public void parseData(JSONObject json)
		{
			isEURegion = true;
			if (json.ContainsKey("success"))
			{
				isEURegion &= json.GetBoolean("success");
			}
			if (json.ContainsKey("accepted"))
			{
				isEURegion &= json.GetBoolean("accepted");
			}
			if (!isEURegion)
			{
				hideSelf();
			}
			else
			{
				extractDescription(json);
			}
		}

		private void extractDescription(JSONObject json)
		{
			if (!json.ContainsKey("version"))
			{
				return;
			}
			int num = (int)json.GetNumber("version");
			if (num == privacyPolicyVersion)
			{
				string text = textTool.loadTextFromCache(PRIVACY_POLICY);
				if (!text.Equals(string.Empty))
				{
					replaceDescription(text);
				}
				return;
			}
			privacyPolicyVersion = num;
			if (json.ContainsKey(PRIVACY_POLICY))
			{
				string text2 = json.GetString(PRIVACY_POLICY);
				if (!text2.Equals(string.Empty))
				{
					replaceDescription(text2);
					textTool.saveText(PRIVACY_POLICY, text2);
					PlayerPrefs.SetInt(PRIVACY_POLICY_VERSION, num);
				}
				else
				{
					PlayerPrefs.SetInt(PRIVACY_POLICY_VERSION, 0);
				}
			}
		}

		private void replaceDescription(string newDescription)
		{
			descriptionText.text = Regex.Unescape(newDescription);
			scrollMe.sizeDelta = new Vector2(scrollMe.sizeDelta.x, descriptionText.GetComponent<RectTransform>().sizeDelta.y + 500f);
		}
	}
}
