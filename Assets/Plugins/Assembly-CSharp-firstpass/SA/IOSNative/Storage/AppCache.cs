using System;
using UnityEngine;

namespace SA.IOSNative.Storage
{
	public static class AppCache
	{
		public static void Save(string key, Texture2D texture)
		{
			byte[] data = texture.EncodeToPNG();
			Save(key, data);
		}

		public static void Save(string key, byte[] data)
		{
			string value = Convert.ToBase64String(data);
			Save(key, value);
		}

		public static void Save(string key, string value)
		{
			PlayerPrefs.SetString(key, value);
		}

		public static Texture2D GetTexture(string key)
		{
			byte[] data = GetData(key);
			Texture2D texture2D = new Texture2D(1, 1);
			texture2D.LoadImage(data);
			texture2D.hideFlags = HideFlags.DontSave;
			return texture2D;
		}

		public static byte[] GetData(string key)
		{
			string s = GetString(key);
			return Convert.FromBase64String(s);
		}

		public static string GetString(string key)
		{
			return PlayerPrefs.GetString(key);
		}

		public static void Remove(string key)
		{
			PlayerPrefs.DeleteKey(key);
		}
	}
}
