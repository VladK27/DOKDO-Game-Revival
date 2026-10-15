using System;

public class ISN_Build
{
	private string _Version = "1.0";

	private int _Number = 1;

	private static ISN_Build _Current;

	public string Version
	{
		get
		{
			return _Version;
		}
	}

	public int Number
	{
		get
		{
			return _Number;
		}
	}

	public static ISN_Build Current
	{
		get
		{
			if (_Current == null)
			{
				_Current = new ISN_Build();
			}
			return _Current;
		}
	}

	public ISN_Build()
	{
	}

	public ISN_Build(string data)
	{
		string[] array = data.Split('|');
		_Version = array[0];
		_Number = Convert.ToInt32(array[1]);
	}
}
