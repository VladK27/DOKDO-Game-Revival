using System;

public class ISN_DeviceGUID
{
	private byte[] _Bytes;

	private string _Base64;

	public string Base64String
	{
		get
		{
			return _Base64;
		}
	}

	public byte[] Bytes
	{
		get
		{
			return _Bytes;
		}
	}

	public ISN_DeviceGUID(string data)
	{
		_Base64 = data;
		_Bytes = Convert.FromBase64String(data);
	}
}
