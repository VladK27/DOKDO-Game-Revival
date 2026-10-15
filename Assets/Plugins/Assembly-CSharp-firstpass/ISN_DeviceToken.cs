using System;

public class ISN_DeviceToken
{
	private string _tokenString;

	private byte[] _tokenBytes;

	public string DeviceId
	{
		get
		{
			return _tokenString;
		}
	}

	public byte[] Bytes
	{
		get
		{
			return _tokenBytes;
		}
	}

	public string TokenString
	{
		get
		{
			return _tokenString;
		}
	}

	public ISN_DeviceToken(string base64String, string token)
	{
		_tokenBytes = Convert.FromBase64String(base64String);
		_tokenString = token;
	}
}
