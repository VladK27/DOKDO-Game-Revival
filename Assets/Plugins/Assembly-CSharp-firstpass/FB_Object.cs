using System;
using System.Collections.Generic;

public class FB_Object
{
	public string Id;

	public List<string> ImageUrls = new List<string>();

	public string Title;

	public string Type;

	public DateTime CreatedTime;

	public string CreatedTimeString;

	public void SetCreatedTime(string time_string)
	{
		CreatedTimeString = time_string;
		CreatedTime = DateTime.Parse(time_string);
	}

	public void AddImageUrl(string url)
	{
		ImageUrls.Add(url);
	}
}
