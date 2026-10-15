using UnityEngine;

public class UM_ImagePickResult : UM_BaseResult
{
	private Texture2D _image;

	public Texture2D image
	{
		get
		{
			return _image;
		}
	}

	public UM_ImagePickResult(Texture2D tex)
	{
		_image = tex;
		if (_image == null)
		{
			_IsSucceeded = false;
		}
	}
}
