using System.Collections.Generic;
using UnityEngine;

public class AMN_PlayerData : AMN_Singleton<AMN_PlayerData>
{
	private const string ENTITLEMENTS = "ENTITLEMENTS";

	public const string DATA_SPLITTER = "|";

	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}

	public static void AddNewSKU(string SKU)
	{
		string text;
		if (PlayerPrefs.HasKey("ENTITLEMENTS"))
		{
			text = PlayerPrefs.GetString("ENTITLEMENTS");
			text = text + SKU + "|";
		}
		else
		{
			text = SKU + "|";
		}
		PlayerPrefs.SetString("ENTITLEMENTS", text);
	}

	public static List<string> GetAvailableSKUs()
	{
		List<string> list = new List<string>();
		if (PlayerPrefs.HasKey("ENTITLEMENTS"))
		{
			string text = PlayerPrefs.GetString("ENTITLEMENTS");
			string[] array = text.Split("|"[0]);
			for (int i = 0; i < array.Length; i++)
			{
				list.Add(array[i]);
			}
		}
		return list;
	}
}
