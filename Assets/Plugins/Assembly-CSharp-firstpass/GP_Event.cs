using SA.Common.Util;
using UnityEngine;

public class GP_Event
{
	public string Id;

	public string Description;

	public string IconImageUrl;

	public string FormattedValue;

	public long Value;

	private Texture2D _icon;

	public Texture2D icon
	{
		get
		{
			return _icon;
		}
	}

	public void LoadIcon()
	{
		if (!(icon != null))
		{
			Loader.LoadWebTexture(IconImageUrl, OnTextureLoaded);
		}
	}

	private void OnTextureLoaded(Texture2D tex)
	{
		if (this != null)
		{
			_icon = tex;
		}
	}
}
