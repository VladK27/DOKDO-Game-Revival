public class SA_EditorNotifications
{
	private static SA_Notifications_EditorUIController _EditorUI;

	private static SA_Notifications_EditorUIController EditorUI
	{
		get
		{
			return _EditorUI;
		}
	}

	public static void ShowNotification(string title, string message, SA_EditorNotificationType type)
	{
		if (SA_EditorTesting.IsInsideEditor)
		{
			EditorUI.ShowNotification(title, message, type);
		}
	}
}
