using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using ANMiniJSON;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class AndroidNativeUtility : Singleton<AndroidNativeUtility>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<AN_PackageCheckResult> OnPackageCheckResult__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string> OnAndroidIdLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string> InternalStoragePathLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string> ExternalStoragePathLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<AN_Locale> LocaleInfoLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string[]> ActionDevicePackagesListLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<AN_NetworkInfo> ActionNetworkInfoLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<AN_RefreshTokenResult> OnOAuthRefreshTokenLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<AN_AccessTokenResult> OnOAuthAccessTokenLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<AN_DeviceCodeResult> OnDeviceCodeLoaded__BackingField = delegate
	{
	};

	private string _redirectUrl = string.Empty;

	private string _clientId = string.Empty;

	private string _clientSecret = string.Empty;

	public static int SDKLevel
	{
		get
		{
			IntPtr clazz = AndroidJNI.FindClass("android.os.Build$VERSION");
			IntPtr staticFieldID = AndroidJNI.GetStaticFieldID(clazz, "SDK_INT", "I");
			return AndroidJNI.GetStaticIntField(clazz, staticFieldID);
		}
	}

	public static event Action<AN_PackageCheckResult> OnPackageCheckResult
	{
		add
		{
			Action<AN_PackageCheckResult> action = OnPackageCheckResult__BackingField;
			Action<AN_PackageCheckResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPackageCheckResult__BackingField, (Action<AN_PackageCheckResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AN_PackageCheckResult> action = OnPackageCheckResult__BackingField;
			Action<AN_PackageCheckResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPackageCheckResult__BackingField, (Action<AN_PackageCheckResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string> OnAndroidIdLoaded
	{
		add
		{
			Action<string> action = OnAndroidIdLoaded__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAndroidIdLoaded__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = OnAndroidIdLoaded__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAndroidIdLoaded__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string> InternalStoragePathLoaded
	{
		add
		{
			Action<string> action = InternalStoragePathLoaded__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref InternalStoragePathLoaded__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = InternalStoragePathLoaded__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref InternalStoragePathLoaded__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string> ExternalStoragePathLoaded
	{
		add
		{
			Action<string> action = ExternalStoragePathLoaded__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ExternalStoragePathLoaded__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = ExternalStoragePathLoaded__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ExternalStoragePathLoaded__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<AN_Locale> LocaleInfoLoaded
	{
		add
		{
			Action<AN_Locale> action = LocaleInfoLoaded__BackingField;
			Action<AN_Locale> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref LocaleInfoLoaded__BackingField, (Action<AN_Locale>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AN_Locale> action = LocaleInfoLoaded__BackingField;
			Action<AN_Locale> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref LocaleInfoLoaded__BackingField, (Action<AN_Locale>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string[]> ActionDevicePackagesListLoaded
	{
		add
		{
			Action<string[]> action = ActionDevicePackagesListLoaded__BackingField;
			Action<string[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionDevicePackagesListLoaded__BackingField, (Action<string[]>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string[]> action = ActionDevicePackagesListLoaded__BackingField;
			Action<string[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionDevicePackagesListLoaded__BackingField, (Action<string[]>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<AN_NetworkInfo> ActionNetworkInfoLoaded
	{
		add
		{
			Action<AN_NetworkInfo> action = ActionNetworkInfoLoaded__BackingField;
			Action<AN_NetworkInfo> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionNetworkInfoLoaded__BackingField, (Action<AN_NetworkInfo>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AN_NetworkInfo> action = ActionNetworkInfoLoaded__BackingField;
			Action<AN_NetworkInfo> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionNetworkInfoLoaded__BackingField, (Action<AN_NetworkInfo>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<AN_RefreshTokenResult> OnOAuthRefreshTokenLoaded
	{
		add
		{
			Action<AN_RefreshTokenResult> action = OnOAuthRefreshTokenLoaded__BackingField;
			Action<AN_RefreshTokenResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnOAuthRefreshTokenLoaded__BackingField, (Action<AN_RefreshTokenResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AN_RefreshTokenResult> action = OnOAuthRefreshTokenLoaded__BackingField;
			Action<AN_RefreshTokenResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnOAuthRefreshTokenLoaded__BackingField, (Action<AN_RefreshTokenResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<AN_AccessTokenResult> OnOAuthAccessTokenLoaded
	{
		add
		{
			Action<AN_AccessTokenResult> action = OnOAuthAccessTokenLoaded__BackingField;
			Action<AN_AccessTokenResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnOAuthAccessTokenLoaded__BackingField, (Action<AN_AccessTokenResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AN_AccessTokenResult> action = OnOAuthAccessTokenLoaded__BackingField;
			Action<AN_AccessTokenResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnOAuthAccessTokenLoaded__BackingField, (Action<AN_AccessTokenResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<AN_DeviceCodeResult> OnDeviceCodeLoaded
	{
		add
		{
			Action<AN_DeviceCodeResult> action = OnDeviceCodeLoaded__BackingField;
			Action<AN_DeviceCodeResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnDeviceCodeLoaded__BackingField, (Action<AN_DeviceCodeResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AN_DeviceCodeResult> action = OnDeviceCodeLoaded__BackingField;
			Action<AN_DeviceCodeResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnDeviceCodeLoaded__BackingField, (Action<AN_DeviceCodeResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void GenerateRefreshToken(string redirectUrl, string clientId, string clientSecret)
	{
		_redirectUrl = redirectUrl;
		_clientId = clientId;
		_clientSecret = clientSecret;
		AndroidNative.GenerateRefreshToken(_redirectUrl, _clientId);
	}

	public void RefreshOAuthToken(string refreshToken, string clientId, string clientSecret)
	{
		StartCoroutine(RefreshOAuthTokenRequest(clientId, clientSecret, refreshToken));
	}

	public void ObtainUserDeviceCode(string clientId)
	{
		StartCoroutine(ObtainUserDeviceCodeRequest(clientId));
	}

	public void CheckIsPackageInstalled(string packageName)
	{
		AndroidNative.isPackageInstalled(packageName);
	}

	public void StartApplication(string bundle)
	{
		AndroidNative.runPackage(bundle);
	}

	public void StartApplication(string packageName, Dictionary<string, string> extras)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (KeyValuePair<string, string> extra in extras)
		{
			stringBuilder.AppendFormat("{0}{1}{2}", extra.Key, "|", extra.Value);
			stringBuilder.Append("|%|");
		}
		stringBuilder.Append("endofline");
		UnityEngine.Debug.Log("[StartApplication] with Extras " + stringBuilder.ToString());
		AndroidNative.LaunchApplication(packageName, stringBuilder.ToString());
	}

	public void LoadAndroidId()
	{
		AndroidNative.LoadAndroidId();
	}

	public void GetInternalStoragePath()
	{
		AndroidNative.GetInternalStoragePath();
	}

	public void GetExternalStoragePath()
	{
		AndroidNative.GetExternalStoragePath();
	}

	public string GetExternalStoragePublicDirectory(AN_ExternalStorageType type)
	{
		return AndroidNative.GetExternalStoragePublicDirectory(type.ToString());
	}

	public void LoadLocaleInfo()
	{
		AndroidNative.LoadLocaleInfo();
	}

	public void LoadPackagesList()
	{
		AndroidNative.LoadPackagesList();
	}

	public void LoadNetworkInfo()
	{
		AndroidNative.LoadNetworkInfo();
	}

	public static void OpenSettingsPage(string action)
	{
		AndroidNative.OpenSettingsPage(action);
	}

	public static void ShowPreloader(string title, string message)
	{
		AN_PoupsProxy.ShowPreloader(title, message, AndroidNativeSettings.Instance.DialogTheme);
	}

	public static void ShowPreloader(string title, string message, AndroidDialogTheme theme)
	{
		AN_PoupsProxy.ShowPreloader(title, message, AndroidNativeSettings.Instance.DialogTheme);
	}

	public static void HidePreloader()
	{
		AN_PoupsProxy.HidePreloader();
	}

	public static void OpenAppRatingPage(string url)
	{
		AN_PoupsProxy.OpenAppRatePage(url);
	}

	public static void RedirectToGooglePlayRatingPage(string url)
	{
		OpenAppRatingPage(url);
	}

	public static void HideCurrentPopup()
	{
		AN_PoupsProxy.HideCurrentPopup();
	}

	public static void InvitePlusFriends()
	{
		AndroidNative.InvitePlusFriends();
	}

	private void RefreshTokenCodeReceived(string data)
	{
		UnityEngine.Debug.Log(data);
		string[] array = data.Split(new string[1] { "|" }, StringSplitOptions.None);
		int num = int.Parse(array[0]);
		if (num == 1)
		{
			StartCoroutine(GenerateRefreshTokenRequest(array[1], _clientId, _clientSecret, _redirectUrl));
			return;
		}
		AN_RefreshTokenResult obj = new AN_RefreshTokenResult("Request Authorization Code error");
		OnOAuthRefreshTokenLoaded__BackingField(obj);
	}

	private IEnumerator GenerateRefreshTokenRequest(string code, string clientId, string clientSecret, string redirectUrl)
	{
		WWWForm requestForm = new WWWForm();
		requestForm.AddField("grant_type", "authorization_code");
		requestForm.AddField("code", code);
		requestForm.AddField("client_id", clientId);
		requestForm.AddField("client_secret", clientSecret);
		requestForm.AddField("redirect_uri", redirectUrl);
		WWW response = new WWW("https://accounts.google.com/o/oauth2/token", requestForm);
		yield return response;
		if (string.IsNullOrEmpty(response.error))
		{
			Dictionary<string, object> dictionary = Json.Deserialize(response.text) as Dictionary<string, object>;
			string accessToken = ((!dictionary.ContainsKey("access_token")) ? string.Empty : dictionary["access_token"].ToString());
			string refreshToken = ((!dictionary.ContainsKey("refresh_token")) ? string.Empty : dictionary["refresh_token"].ToString());
			string tokenType = ((!dictionary.ContainsKey("token_type")) ? string.Empty : dictionary["token_type"].ToString());
			long expiresIn = ((!dictionary.ContainsKey("expires_in")) ? 0 : ((long)dictionary["expires_in"]));
			AN_RefreshTokenResult obj = new AN_RefreshTokenResult(accessToken, refreshToken, tokenType, expiresIn);
			OnOAuthRefreshTokenLoaded__BackingField(obj);
		}
		else
		{
			AN_RefreshTokenResult obj2 = new AN_RefreshTokenResult(response.error);
			OnOAuthRefreshTokenLoaded__BackingField(obj2);
		}
	}

	private IEnumerator RefreshOAuthTokenRequest(string clientId, string clientSecret, string refreshToken)
	{
		WWWForm requestForm = new WWWForm();
		requestForm.AddField("grant_type", "refresh_token");
		requestForm.AddField("client_id", clientId);
		requestForm.AddField("client_secret", clientSecret);
		requestForm.AddField("refresh_token", refreshToken);
		WWW response = new WWW("https://accounts.google.com/o/oauth2/token", requestForm);
		yield return response;
		if (string.IsNullOrEmpty(response.error))
		{
			Dictionary<string, object> dictionary = Json.Deserialize(response.text) as Dictionary<string, object>;
			string accessToken = ((!dictionary.ContainsKey("access_token")) ? string.Empty : dictionary["access_token"].ToString());
			string tokenType = ((!dictionary.ContainsKey("token_type")) ? string.Empty : dictionary["token_type"].ToString());
			long expiresIn = ((!dictionary.ContainsKey("expires_in")) ? 0 : ((long)dictionary["expires_in"]));
			AN_AccessTokenResult obj = new AN_AccessTokenResult(accessToken, tokenType, expiresIn);
			OnOAuthAccessTokenLoaded__BackingField(obj);
		}
		else
		{
			AN_AccessTokenResult obj2 = new AN_AccessTokenResult(response.error);
			OnOAuthAccessTokenLoaded__BackingField(obj2);
		}
	}

	private IEnumerator ObtainUserDeviceCodeRequest(string clientId)
	{
		WWWForm requestForm = new WWWForm();
		requestForm.AddField("client_id", clientId);
		requestForm.AddField("scope", "email profile");
		WWW response = new WWW("https://accounts.google.com/o/oauth2/device/code", requestForm);
		yield return response;
		UnityEngine.Debug.Log(response.text);
		if (string.IsNullOrEmpty(response.error))
		{
			Dictionary<string, object> dictionary = Json.Deserialize(response.text) as Dictionary<string, object>;
			string deviceCode = ((!dictionary.ContainsKey("device_code")) ? string.Empty : dictionary["device_code"].ToString());
			string userCode = ((!dictionary.ContainsKey("user_code")) ? string.Empty : dictionary["user_code"].ToString());
			string verificationUrl = ((!dictionary.ContainsKey("verification_url")) ? string.Empty : dictionary["verification_url"].ToString());
			long expiresIn = ((!dictionary.ContainsKey("expires_in")) ? 0 : ((long)dictionary["expires_in"]));
			long interval = ((!dictionary.ContainsKey("interval")) ? 0 : ((long)dictionary["interval"]));
			AN_DeviceCodeResult obj = new AN_DeviceCodeResult(deviceCode, userCode, verificationUrl, expiresIn, interval);
			OnDeviceCodeLoaded__BackingField(obj);
		}
		else
		{
			AN_DeviceCodeResult obj2 = new AN_DeviceCodeResult(response.error);
			OnDeviceCodeLoaded__BackingField(obj2);
		}
	}

	private void OnAndroidIdLoadedEvent(string id)
	{
		OnAndroidIdLoaded__BackingField(id);
	}

	private void OnPacakgeFound(string packageName)
	{
		AN_PackageCheckResult obj = new AN_PackageCheckResult(packageName);
		OnPackageCheckResult__BackingField(obj);
	}

	private void OnPacakgeNotFound(string packageName)
	{
		AN_PackageCheckResult obj = new AN_PackageCheckResult(packageName, new Error(0, "Pacakge not Found"));
		OnPackageCheckResult__BackingField(obj);
	}

	private void OnExternalStoragePathLoaded(string path)
	{
		ExternalStoragePathLoaded__BackingField(path);
	}

	private void OnInternalStoragePathLoaded(string path)
	{
		InternalStoragePathLoaded__BackingField(path);
	}

	private void OnLocaleInfoLoaded(string data)
	{
		string[] array = data.Split("|"[0]);
		AN_Locale aN_Locale = new AN_Locale();
		aN_Locale.CountryCode = array[0];
		aN_Locale.DisplayCountry = array[1];
		aN_Locale.LanguageCode = array[2];
		aN_Locale.DisplayLanguage = array[3];
		LocaleInfoLoaded__BackingField(aN_Locale);
	}

	private void OnPackagesListLoaded(string data)
	{
		string[] obj = data.Split("|"[0]);
		ActionDevicePackagesListLoaded__BackingField(obj);
	}

	private void OnNetworkInfoLoaded(string data)
	{
		string[] array = data.Split("|"[0]);
		AN_NetworkInfo aN_NetworkInfo = new AN_NetworkInfo();
		aN_NetworkInfo.SubnetMask = array[0];
		aN_NetworkInfo.IpAddress = array[1];
		aN_NetworkInfo.MacAddress = array[2];
		aN_NetworkInfo.SSID = array[3];
		aN_NetworkInfo.BSSID = array[4];
		aN_NetworkInfo.LinkSpeed = Convert.ToInt32(array[5]);
		aN_NetworkInfo.NetworkId = Convert.ToInt32(array[6]);
		ActionNetworkInfoLoaded__BackingField(aN_NetworkInfo);
	}
}
