using System;
using SA.Common.Models;
using UnityEngine;

public class IOSImagePickResult : Result
{
	private Texture2D _image;

	public Texture2D Image
	{
		get
		{
			return _image;
		}
	}

	public IOSImagePickResult(string ImageData)
	{
		if (ImageData.Length == 0)
		{
			_Error = new Error(0, "No Image Data");
			return;
		}
		byte[] data = Convert.FromBase64String(ImageData);
		_image = new Texture2D(1, 1);
		_image.LoadImage(data);
		_image.hideFlags = HideFlags.DontSave;
		if (!IOSNativeSettings.Instance.DisablePluginLogs)
		{
			ISN_Logger.Log("IOSImagePickResult: w" + _image.width + " h: " + _image.height);
		}
	}
}
