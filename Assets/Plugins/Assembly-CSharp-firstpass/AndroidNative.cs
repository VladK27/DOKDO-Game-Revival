using System.Text;

public class AndroidNative
{
	public const string DATA_SPLITTER = "|";

	public const string DATA_EOF = "endofline";

	public const string DATA_SPLITTER2 = "|%|";

	private const string UTILITY_CLASSS = "com.androidnative.features.common.AndroidNativeUtility";

	private const string CLASS_NAME = "com.androidnative.AN_Bridge";

	public static string[] StringToArray(string val)
	{
		return val.Split("|"[0]);
	}

	public static string ArrayToString(string[] array)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (string value in array)
		{
			stringBuilder.Append(value);
			stringBuilder.Append("|");
		}
		return stringBuilder.ToString().TrimEnd("|"[0]);
	}

	public static void TwitterInit(string consumer_key, string consumer_secret)
	{
		CallAndroidNativeBridge("TwitterInit", consumer_key, consumer_secret);
	}

	public static void AuthificateUser()
	{
		CallAndroidNativeBridge("AuthificateUser");
	}

	public static void LoadUserData()
	{
		CallAndroidNativeBridge("LoadUserData");
	}

	public static void TwitterPost(string status)
	{
		CallAndroidNativeBridge("TwitterPost", status);
	}

	public static void TwitterPostWithImage(string status, string data)
	{
		CallAndroidNativeBridge("TwitterPostWithImage", status, data);
	}

	public static void LogoutFromTwitter()
	{
		CallAndroidNativeBridge("LogoutFromTwitter");
	}

	public static void InitCameraAPI(string folderName, int maxSize, int mode, int format)
	{
		CallAndroidNativeBridge("InitCameraAPI", folderName, maxSize.ToString(), mode.ToString(), format);
	}

	public static void SaveToGalalry(string ImageData, string name)
	{
		CallAndroidNativeBridge("SaveToGalalry", ImageData, name);
	}

	public static void GetImageFromGallery()
	{
		CallAndroidNativeBridge("GetImageFromGallery");
	}

	public static void GetImagesFromGallery()
	{
		CallAndroidNativeBridge("GetImagesFromGallery");
	}

	public static void GetImageFromCamera(bool bSaveToGallery = false)
	{
		CallAndroidNativeBridge("GetImageFromCamera", bSaveToGallery.ToString());
	}

	public static void isPackageInstalled(string packagename)
	{
		CallAndroidNativeBridge("isPackageInstalled", packagename);
	}

	public static void runPackage(string packagename)
	{
		CallAndroidNativeBridge("runPackage", packagename);
	}

	public static void LoadAndroidId()
	{
		CallAndroidNativeBridge("loadAndroidId");
	}

	public static void LoadPackagesList()
	{
		CallUtility("loadPackagesList");
	}

	public static void InvitePlusFriends()
	{
		CallUtility("InvitePlusFriends");
	}

	public static void LoadNetworkInfo()
	{
		CallUtility("loadNetworkInfo");
	}

	public static void OpenSettingsPage(string action)
	{
		CallUtility("openSettingsPage", action);
	}

	public static void LaunchApplication(string bundle, string data)
	{
		CallUtility("launchApplication", bundle, data);
	}

	public static void LoadContacts()
	{
		CallAndroidNativeBridge("loadAddressBook");
	}

	public static void LoadPackageInfo()
	{
		CallAndroidNativeBridge("LoadPackageInfo");
	}

	public static void GetInternalStoragePath()
	{
		CallUtility("GetInternalStoragePath");
	}

	public static void GetExternalStoragePath()
	{
		CallUtility("GetExternalStoragePath");
	}

	public static string GetExternalStoragePublicDirectory(string type)
	{
		return CallUtilityForResult<string>("GetExternalStoragePublicDirectory", new object[1] { type });
	}

	public static void LoadLocaleInfo()
	{
		CallUtility("LoadLocaleInfo");
	}

	public static void StartLockTask()
	{
		CallAndroidNativeBridge("StartLockTask");
	}

	public static void StopLockTask()
	{
		CallAndroidNativeBridge("StopLockTask");
	}

	public static void OpenAppInStore(string appPackageName)
	{
		CallAndroidNativeBridge("OpenAppInStore", appPackageName);
	}

	public static void GenerateRefreshToken(string redirectUrl, string clientId)
	{
		CallAndroidNativeBridge("GenerateRefreshToken", redirectUrl, clientId);
	}

	public static void SaveToCacheStorage(string key, string value)
	{
		CallUtility("SaveToCacheStorage", key, value);
	}

	public static string GetFromCacheStorage(string key)
	{
		return CallUtilityForResult<string>("GetFromCacheStorage", new object[1] { key });
	}

	public static void RemoveData(string key)
	{
		CallAndroidNativeBridge("RemoveData", key);
	}

	private static void CallUtility(string methodName, params object[] args)
	{
		AN_ProxyPool.CallStatic("com.androidnative.features.common.AndroidNativeUtility", methodName, args);
	}

	private static ReturnType CallUtilityForResult<ReturnType>(string methodName, params object[] args)
	{
		return AN_ProxyPool.CallStatic<ReturnType>("com.androidnative.features.common.AndroidNativeUtility", methodName, args);
	}

	private static void CallAndroidNativeBridge(string methodName, params object[] args)
	{
		AN_ProxyPool.CallStatic("com.androidnative.AN_Bridge", methodName, args);
	}

	private static void CallStatic(string className, string methodName, params object[] args)
	{
		AN_ProxyPool.CallStatic(className, methodName, args);
	}
}
