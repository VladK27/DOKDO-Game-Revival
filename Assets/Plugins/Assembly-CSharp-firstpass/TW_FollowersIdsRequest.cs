using System;
using System.Collections.Generic;
using ANMiniJSON;
using UnityEngine;

public class TW_FollowersIdsRequest : TW_APIRequest
{
	public static TW_FollowersIdsRequest Create()
	{
		return new GameObject("TW_FollowersIdsRequest").AddComponent<TW_FollowersIdsRequest>();
	}

	private void Awake()
	{
		SetUrl("https://api.twitter.com/1.1/followers/ids.json");
	}

	protected override void OnResult(string data)
	{
		Dictionary<string, object> dictionary = Json.Deserialize(data) as Dictionary<string, object>;
		TW_APIRequstResult tW_APIRequstResult = new TW_APIRequstResult(true, data);
		foreach (object item2 in dictionary["ids"] as List<object>)
		{
			string item = Convert.ToString(item2);
			tW_APIRequstResult.ids.Add(item);
		}
		SendCompleteResult(tW_APIRequstResult);
	}
}
