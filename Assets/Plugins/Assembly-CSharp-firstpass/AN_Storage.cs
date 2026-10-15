using System;
using System.Text;
using UnityEngine;

public static class AN_Storage
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
		string fromCacheStorage = AndroidNative.GetFromCacheStorage(key);
		if (!fromCacheStorage.Equals(string.Empty))
		{
			byte[] bytes = Convert.FromBase64String(fromCacheStorage);
			return Encoding.UTF8.GetString(bytes);
		}
		return string.Empty;
	}

	public static Texture2D GetTexture(string key)
	{
		string fromCacheStorage = AndroidNative.GetFromCacheStorage(key);
		if (!fromCacheStorage.Equals(string.Empty))
		{
			byte[] data = Convert.FromBase64String(fromCacheStorage);
			Texture2D texture2D = new Texture2D(1, 1, TextureFormat.DXT5, false);
			texture2D.LoadImage(data);
			return texture2D;
		}
		return Texture2D.whiteTexture;
	}

	public static byte[] GetData(string key)
	{
		string fromCacheStorage = AndroidNative.GetFromCacheStorage(key);
		if (!fromCacheStorage.Equals(string.Empty))
		{
			return Convert.FromBase64String(fromCacheStorage);
		}
		return new byte[0];
	}

	public static void RemoveData(string key)
	{
		AndroidNative.RemoveData(key);
	}
}
