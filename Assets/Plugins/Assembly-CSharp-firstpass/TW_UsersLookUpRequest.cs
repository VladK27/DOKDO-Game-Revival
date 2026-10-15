using System.Collections;
using System.Collections.Generic;
using ANMiniJSON;
using UnityEngine;

public class TW_UsersLookUpRequest : TW_APIRequest
{
	public static TW_UsersLookUpRequest Create()
	{
		return new GameObject("TW_UersLookUpRequest").AddComponent<TW_UsersLookUpRequest>();
	}

	private void Awake()
	{
		SetUrl("https://api.twitter.com/1.1/users/lookup.json");
	}

	protected override void OnResult(string data)
	{
		List<TwitterUserInfo> list = new List<TwitterUserInfo>();
		foreach (object item in Json.Deserialize(data) as List<object>)
		{
			TwitterUserInfo twitterUserInfo = new TwitterUserInfo(item as IDictionary);
			TwitterDataCash.AddUser(twitterUserInfo);
			list.Add(twitterUserInfo);
		}
		TW_APIRequstResult tW_APIRequstResult = new TW_APIRequstResult(true, data);
		tW_APIRequstResult.users = list;
		SendCompleteResult(tW_APIRequstResult);
	}
}
