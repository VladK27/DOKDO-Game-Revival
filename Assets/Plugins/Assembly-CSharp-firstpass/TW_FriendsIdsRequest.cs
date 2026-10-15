using System;
using System.Collections.Generic;
using ANMiniJSON;
using UnityEngine;

public class TW_FriendsIdsRequest : TW_APIRequest
{
	public static TW_FriendsIdsRequest Create()
	{
		return new GameObject("TW_FriendsIdsRequest").AddComponent<TW_FriendsIdsRequest>();
	}

	private void Awake()
	{
		SetUrl("https://api.twitter.com/1.1/friends/ids.json");
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
