using UnityEngine;

public class WP8NativeSettings : ScriptableObject
{
	public const string VERSION_NUMBER = "3.6/21";

	private const string WP8NativeSettingsAssetName = "WP8NativeSettings";

	private const string WP8NativeSettingsAssetExtension = ".asset";

	private static WP8NativeSettings instance;

	public static WP8NativeSettings Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Resources.Load("WP8NativeSettings") as WP8NativeSettings;
				if (instance == null)
				{
					instance = ScriptableObject.CreateInstance<WP8NativeSettings>();
				}
			}
			return instance;
		}
	}
}
