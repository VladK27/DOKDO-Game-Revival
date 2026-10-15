public class MNP
{
	public static void ShowPreloader(string title, string message)
	{
		MNAndroidNative.ShowPreloader(title, message, MNP_PlatformSettings.Instance.AndroidDialogTheme);
	}

	public static void HidePreloader()
	{
		MNAndroidNative.HidePreloader();
	}
}
