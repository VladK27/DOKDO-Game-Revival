using System;
using System.Text;
using UnityEngine;

public static class UM_Storage
{
	public static void Save(string key, string data)
	{
		AndroidNative.SaveToCacheStorage(key, Convert.ToBase64String(Encoding.UTF8.GetBytes(data)));
	}

	public static void Save(string key, Texture2D texture)
	{
		AndroidNative.SaveToCacheStorage(key, Convert.ToBase64String(texture.EncodeToPNG()));
	}

	public static void Save(string key, byte[] data)
	{
		AndroidNative.SaveToCacheStorage(key, Convert.ToBase64String(data));
	}

	public static string GetString(string key)
	{
		return AN_Storage.GetString(key);
	}

	public static Texture2D GetTexture(string key)
	{
		return AN_Storage.GetTexture(key);
	}

	public static byte[] GetData(string key)
	{
		return AN_Storage.GetData(key);
	}
}
