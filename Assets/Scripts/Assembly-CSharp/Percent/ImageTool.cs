using System.IO;
using UnityEngine;

namespace Percent
{
	public class ImageTool
	{
		internal static readonly string CACHE_PATH = Application.persistentDataPath + "/cache";

		private readonly string SLASH = "/";

		private readonly string EX_PNG = ".png";

		internal ImageTool()
		{
			if (!Directory.Exists(CACHE_PATH))
			{
				Directory.CreateDirectory(CACHE_PATH);
			}
		}

		internal bool isCacheExist(string resourceId)
		{
			string path = resolvePath(resourceId);
			if (isFileExist(path))
			{
				return true;
			}
			return false;
		}

		private bool isFileExist(string path)
		{
			FileInfo fileInfo = new FileInfo(path);
			if (fileInfo.Exists)
			{
				return true;
			}
			return false;
		}

		internal void saveTexture(string resourceId, Texture2D texture)
		{
			try
			{
				byte[] bytes = texture.EncodeToPNG();
				File.WriteAllBytes(resolvePath(resourceId), bytes);
			}
			catch
			{
				Logger.error("Save cache image FAIL.");
			}
		}

		private string resolvePath(string fileName)
		{
			return CACHE_PATH + SLASH + fileName + EX_PNG;
		}

		internal Texture2D loadTextureFromCache(string resourceId)
		{
			string text = resolvePath(resourceId);
			if (!isFileExist(text))
			{
				Logger.error("There is no cached file.");
				return null;
			}
			return loadPNG(text);
		}

		internal Texture2D loadPNG(string filePath)
		{
			Texture2D texture2D = null;
			if (isFileExist(filePath))
			{
				try
				{
					byte[] data = File.ReadAllBytes(filePath);
					texture2D = new Texture2D(2, 2, TextureFormat.RGBA32, false);
					texture2D.LoadImage(data);
				}
				catch
				{
					Logger.error("Cached file load FAIL");
				}
			}
			return texture2D;
		}

		internal void deleteAllCache()
		{
			if (Directory.Exists(CACHE_PATH))
			{
				Directory.Delete(CACHE_PATH, true);
			}
		}
	}
}
