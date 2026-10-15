using System;
using System.Collections.Generic;
using UnityEngine;

public class AndroidImagesPickResult : AndroidActivityResult
{
	private Dictionary<string, Texture2D> _Images = new Dictionary<string, Texture2D>();

	public Dictionary<string, Texture2D> Images
	{
		get
		{
			return _Images;
		}
	}

	public AndroidImagesPickResult(string resultCode, string imagesData)
		: base("0", resultCode)
	{
		string[] array = imagesData.Split(new string[1] { "|" }, StringSplitOptions.None);
		for (int i = 0; i < array.Length && !array[i].Equals("endofline"); i += 2)
		{
			string key = array[i];
			string s = array[i + 1];
			byte[] data = Convert.FromBase64String(s);
			Texture2D texture2D = new Texture2D(1, 1, TextureFormat.DXT5, false);
			texture2D.LoadImage(data);
			_Images.Add(key, texture2D);
		}
	}
}
