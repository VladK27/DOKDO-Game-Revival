using System.Collections.Generic;
using SA.AndroidNative.DynamicLinks;
using UnityEngine;

public class AndroidNativeSettings : ScriptableObject
{
	public const string VERSION_NUMBER = "9.8/21";

	public const string GOOGLE_PLAY_SDK_VERSION_NUMBER = "11.0.2";

	public const string GOOGLE_PLAY_SDK_LEAGCY_VERSION_NUMBER = "11020000";

	public bool EnablePlusAPI = true;

	public bool EnableGamesAPI = true;

	public bool EnableAppInviteAPI = true;

	public bool EnableDriveAPI;

	public bool LoadProfileIcons = true;

	public bool LoadProfileImages = true;

	public bool LoadQuestsImages = true;

	public bool LoadQuestsIcons = true;

	public bool LoadEventsIcons = true;

	public bool ShowConnectingPopup = true;

	public bool AutoLoadLocalPlayerScore;

	public bool GoogleFitEnabled;

	public bool EnableATCSupport;

	public bool OneSignalEnabled;

	public string OneSignalAppID = "YOUR_ONESIGNAL_APP_ID";

	public string OneSignalDownloadLink = "https://goo.gl/Vc6tfK";

	public string OneSignalDocLink = "https://goo.gl/aZjkxV";

	public bool UseParsePushNotifications;

	public string ParseAppId = "YOUR_PARSE_APP_ID";

	public string DotNetKey = "YOUR_PARSE_DOT_NET_KEY";

	public string ParseDocLink = "http://goo.gl/9BgQ8r";

	public string ParseDownloadLink = "https://goo.gl/dm7jYL";

	public AndroidDialogTheme DialogTheme;

	public bool EnableSoomla;

	public string SoomlaDownloadLink = "http://goo.gl/7LYwuj";

	public string SoomlaDocsLink = "https://goo.gl/es5j1N";

	public string SoomlaGameKey = string.Empty;

	public string SoomlaEnvKey = string.Empty;

	public int Ad_EditorFillRateIndex = 4;

	public int Ad_EditorFillRate = 100;

	public bool Is_Ad_EditorTestingEnabled = true;

	public int InApps_EditorFillRateIndex = 4;

	public int InApps_EditorFillRate = 100;

	public bool Is_InApps_EditorTestingEnabled = true;

	public bool Is_Leaderboards_Editor_Notifications_Enabled = true;

	public bool Is_Achievements_Editor_Notifications_Enabled = true;

	public string GCM_SenderId = "YOUR_SENDER_ID_HERE";

	public AN_PushNotificationService PushService;

	public bool SaveCameraImageToGallery;

	public bool UseProductNameAsFolderName = true;

	public string GalleryFolderName = string.Empty;

	public int MaxImageLoadSize = 512;

	public AN_CameraCaptureType CameraCaptureMode;

	public AndroidCameraImageFormat ImageFormat;

	public bool ShowAppPermissions;

	public bool EnableBillingAPI = true;

	public bool EnablePSAPI = true;

	public bool EnableSocialAPI = true;

	public bool EnableCameraAPI = true;

	public bool ExpandNativeAPI;

	public bool ExpandPSAPI;

	public bool ExpandBillingAPI;

	public bool ExpandSocialAPI;

	public bool ExpandCameraAPI;

	public bool ThirdPartyParams;

	public bool ShowPSSettingsResources;

	public bool ShowActions;

	public bool GCMSettingsActinve;

	public bool LocalNotificationsAPI = true;

	public bool ImmersiveModeAPI = true;

	public bool ApplicationInformationAPI = true;

	public bool ExternalAppsAPI = true;

	public bool PoupsandPreloadersAPI = true;

	public bool CheckAppLicenseAPI = true;

	public bool NetworkStateAPI;

	public bool FirebaseAnalytics;

	public bool FirebaseDynamicLinks;

	public string FirebaseAppId = string.Empty;

	public string FirebaseApiKey = string.Empty;

	public List<LinkEditorTemplate> DynamicLinks = new List<LinkEditorTemplate>();

	public bool InAppPurchasesAPI = true;

	public bool GooglePlayServicesAPI = true;

	public bool PlayServicesAdvancedSignInAPI = true;

	public bool GoogleButtonAPI = true;

	public bool AnalyticsAPI = true;

	public bool GoogleCloudSaveAPI = true;

	public bool PushNotificationsAPI = true;

	public bool GoogleMobileAdAPI = true;

	public bool GoogleOAuthAPI = true;

	public bool GalleryAPI = true;

	public bool CameraAPI = true;

	public bool KeepManifestClean = true;

	public string GooglePlayServiceAppID = "0";

	public int ToolbarSelectedIndex;

	public string base64EncodedPublicKey = "REPLACE_WITH_YOUR_PUBLIC_KEY";

	public bool ShowStoreProducts = true;

	public List<GoogleProductTemplate> InAppProducts = new List<GoogleProductTemplate>();

	public bool ShowLeaderboards = true;

	public List<GPLeaderBoard> Leaderboards = new List<GPLeaderBoard>();

	public bool ShowAchievements = true;

	public List<GPAchievement> Achievements = new List<GPAchievement>();

	public bool ShowWhenAppIsForeground = true;

	public bool EnableVibrationLocal;

	public Texture2D LocalNotificationSmallIcon;

	public Texture2D LocalNotificationLargeIcon;

	public AudioClip LocalNotificationSound;

	public int LocalNotificationWakeLockTimer = 10000;

	public bool ReplaceOldNotificationWithNew;

	public bool ShowPushWhenAppIsForeground = true;

	public bool EnableVibrationPush;

	public Color PushNotificationColor = Color.white;

	public Texture2D PushNotificationSmallIcon;

	public Texture2D PushNotificationLargeIcon;

	public AudioClip PushNotificationSound;

	public const string ANSettingsAssetName = "AndroidNativeSettings";

	public const string ANSettingsAssetExtension = ".asset";

	private static AndroidNativeSettings instance;

	public static AndroidNativeSettings Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Resources.Load("AndroidNativeSettings") as AndroidNativeSettings;
				if (instance == null)
				{
					instance = ScriptableObject.CreateInstance<AndroidNativeSettings>();
				}
			}
			return instance;
		}
	}

	public bool IsBase64KeyWasReplaced
	{
		get
		{
			if (base64EncodedPublicKey.Equals("REPLACE_WITH_YOUR_PUBLIC_KEY") || base64EncodedPublicKey.Equals(string.Empty))
			{
				return false;
			}
			return true;
		}
	}
}
