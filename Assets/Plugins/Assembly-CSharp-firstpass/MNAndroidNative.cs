public class MNAndroidNative
{
	private const string CLASS_NAME = "com.stansassets.mnp.NativePopupsManager";

	private static void CallActivityFunction(string methodName, params object[] args)
	{
		MNProxyPool.CallStatic("com.stansassets.mnp.NativePopupsManager", methodName, args);
	}

	public static void showMessage(string title, string message, string actions, MNAndroidDialogTheme theme)
	{
		CallActivityFunction("ShowMessage", title, message, actions, (int)theme);
	}

	public static void ShowPreloader(string title, string message, MNAndroidDialogTheme theme)
	{
		CallActivityFunction("ShowPreloader", title, message, (int)theme);
	}

	public static void HidePreloader()
	{
		CallActivityFunction("HidePreloader");
	}

	public static void RedirectStoreRatingPage(string url)
	{
		CallActivityFunction("OpenAppRatingPage", url);
	}
}
