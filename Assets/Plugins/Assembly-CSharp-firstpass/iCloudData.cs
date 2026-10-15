using System;

public class iCloudData
{
	private string _key;

	private string _val;

	private bool _IsEmpty;

	public string key
	{
		get
		{
			return _key;
		}
	}

	public string stringValue
	{
		get
		{
			if (_IsEmpty)
			{
				return null;
			}
			return _val;
		}
	}

	public float floatValue
	{
		get
		{
			if (_IsEmpty)
			{
				return 0f;
			}
			return Convert.ToSingle(_val);
		}
	}

	public byte[] bytesValue
	{
		get
		{
			if (_IsEmpty)
			{
				return null;
			}
			return Convert.FromBase64String(_val);
		}
	}

	public bool IsEmpty
	{
		get
		{
			return _IsEmpty;
		}
	}

	public iCloudData(string k, string v)
	{
		_key = k;
		_val = v;
		if (_val.Equals("null"))
		{
			if (!IOSNativeSettings.Instance.DisablePluginLogs)
			{
				ISN_Logger.Log("ISN iCloud Empty set");
			}
			_IsEmpty = true;
		}
	}
}
