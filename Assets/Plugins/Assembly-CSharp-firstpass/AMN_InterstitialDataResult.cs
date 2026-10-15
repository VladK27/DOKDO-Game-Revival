using System;

public class AMN_InterstitialDataResult : AMN_Result
{
	private AMN_AdProperties properties;

	private string _error_message = "no_error";

	public AMN_AdProperties Properties
	{
		get
		{
			return properties;
		}
		set
		{
			properties = value;
		}
	}

	public string Error_message
	{
		get
		{
			return _error_message;
		}
		set
		{
			_error_message = value;
		}
	}

	public AMN_InterstitialDataResult(string error_message)
		: base(false)
	{
		Error_message = error_message;
	}

	public AMN_InterstitialDataResult(string[] data)
		: base(true)
	{
		Properties = new AMN_AdProperties(Convert.ToBoolean(data[0]), Convert.ToBoolean(data[1]), Convert.ToBoolean(data[2]), data[3]);
	}
}
