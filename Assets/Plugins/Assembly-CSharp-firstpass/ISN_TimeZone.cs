using System;

public class ISN_TimeZone
{
	private long _SecondsFromGMT = 7200L;

	private string _Name = "Europe/Kiev";

	private static ISN_TimeZone _LocalTimeZone;

	public string Name
	{
		get
		{
			return _Name;
		}
	}

	public long SecondsFromGMT
	{
		get
		{
			return _SecondsFromGMT;
		}
	}

	public static ISN_TimeZone LocalTimeZone
	{
		get
		{
			if (_LocalTimeZone == null)
			{
				_LocalTimeZone = new ISN_TimeZone();
			}
			return _LocalTimeZone;
		}
	}

	public ISN_TimeZone()
	{
	}

	public ISN_TimeZone(string data)
	{
		string[] array = data.Split('|');
		_Name = array[0];
		_SecondsFromGMT = Convert.ToInt64(array[1]);
	}
}
