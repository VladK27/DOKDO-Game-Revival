public class AndroidToast
{
	public const int LENGTH_SHORT = 0;

	public const int LENGTH_LONG = 1;

	public static void ShowToastNotification(string text)
	{
		ShowToastNotification(text, 0);
	}

	public static void ShowToastNotification(string text, int duration)
	{
		AN_NotificationProxy.ShowToastNotification(text, duration);
	}
}
