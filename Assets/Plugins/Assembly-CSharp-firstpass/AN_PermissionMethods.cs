internal static class AN_PermissionMethods
{
	public static string GetFullName(this AN_Permission permission)
	{
		string text = "android.permission.";
		switch (permission)
		{
		case AN_Permission.SET_ALARM:
			text = "com.android.alarm.permission.";
			break;
		case AN_Permission.INSTALL_SHORTCUT:
		case AN_Permission.UNINSTALL_SHORTCUT:
			text = "com.android.launcher.permission.";
			break;
		case AN_Permission.ADD_VOICEMAIL:
			text = "com.android.voicemail.permission.";
			break;
		}
		return text + permission;
	}

	public static bool IsNormalPermission(this AN_Permission permission)
	{
		switch (permission)
		{
		case AN_Permission.ACCESS_LOCATION_EXTRA_COMMANDS:
		case AN_Permission.ACCESS_NETWORK_STATE:
		case AN_Permission.ACCESS_NOTIFICATION_POLICY:
		case AN_Permission.ACCESS_WIFI_STATE:
		case AN_Permission.ACCESS_WIMAX_STATE:
		case AN_Permission.BLUETOOTH:
		case AN_Permission.BLUETOOTH_ADMIN:
		case AN_Permission.BROADCAST_STICKY:
		case AN_Permission.CHANGE_NETWORK_STATE:
		case AN_Permission.CHANGE_WIFI_MULTICAST_STATE:
		case AN_Permission.CHANGE_WIFI_STATE:
		case AN_Permission.CHANGE_WIMAX_STATE:
		case AN_Permission.DISABLE_KEYGUARD:
		case AN_Permission.EXPAND_STATUS_BAR:
		case AN_Permission.FLASHLIGHT:
		case AN_Permission.GET_PACKAGE_SIZE:
		case AN_Permission.INTERNET:
		case AN_Permission.KILL_BACKGROUND_PROCESSES:
		case AN_Permission.MODIFY_AUDIO_SETTINGS:
		case AN_Permission.NFC:
		case AN_Permission.READ_SYNC_SETTINGS:
		case AN_Permission.READ_SYNC_STATS:
		case AN_Permission.RECEIVE_BOOT_COMPLETED:
		case AN_Permission.REORDER_TASKS:
		case AN_Permission.REQUEST_INSTALL_PACKAGES:
		case AN_Permission.SET_TIME_ZONE:
		case AN_Permission.SET_WALLPAPER:
		case AN_Permission.SET_WALLPAPER_HINTS:
		case AN_Permission.SUBSCRIBED_FEEDS_READ:
		case AN_Permission.TRANSMIT_IR:
		case AN_Permission.USE_FINGERPRINT:
		case AN_Permission.VIBRATE:
		case AN_Permission.WAKE_LOCK:
		case AN_Permission.WRITE_SYNC_SETTINGS:
		case AN_Permission.SET_ALARM:
		case AN_Permission.INSTALL_SHORTCUT:
		case AN_Permission.UNINSTALL_SHORTCUT:
			return true;
		default:
			return false;
		}
	}
}
