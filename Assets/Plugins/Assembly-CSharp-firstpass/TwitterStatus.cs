using System;
using System.Collections;
using ANMiniJSON;

public class TwitterStatus
{
	private string _rawJSON;

	private string _text;

	private string _geo;

	public string rawJSON
	{
		get
		{
			return _rawJSON;
		}
	}

	public string text
	{
		get
		{
			return _text;
		}
	}

	public string geo
	{
		get
		{
			return _geo;
		}
	}

	public TwitterStatus(IDictionary JSON)
	{
		_rawJSON = Json.Serialize(JSON);
		_text = Convert.ToString(JSON["text"]);
		_geo = Convert.ToString(JSON["geo"]);
	}
}
