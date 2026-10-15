using System;
using System.Collections;
using System.Collections.Generic;
using ANMiniJSON;
using UnityEngine;

public class TW_SearchTweetsRequest : TW_APIRequest
{
	public static TW_SearchTweetsRequest Create()
	{
		return new GameObject("TW_SearchTweetsRequest").AddComponent<TW_SearchTweetsRequest>();
	}

	private void Awake()
	{
		SetUrl("https://api.twitter.com/1.1/search/tweets.json");
	}

	protected override void OnResult(string data)
	{
		List<TweetTemplate> list = new List<TweetTemplate>();
		Dictionary<string, object> dictionary = Json.Deserialize(data) as Dictionary<string, object>;
		List<object> list2 = dictionary["statuses"] as List<object>;
		foreach (object item in list2)
		{
			Dictionary<string, object> dictionary2 = item as Dictionary<string, object>;
			TweetTemplate tweetTemplate = new TweetTemplate();
			tweetTemplate.id = dictionary2["id_str"] as string;
			tweetTemplate.created_at = dictionary2["created_at"] as string;
			tweetTemplate.text = dictionary2["text"] as string;
			tweetTemplate.source = dictionary2["source"] as string;
			tweetTemplate.in_reply_to_status_id = dictionary2["in_reply_to_status_id"] as string;
			tweetTemplate.in_reply_to_user_id = dictionary2["in_reply_to_user_id"] as string;
			tweetTemplate.in_reply_to_screen_name = dictionary2["in_reply_to_screen_name"] as string;
			tweetTemplate.geo = dictionary2["geo"] as string;
			tweetTemplate.place = dictionary2["place"] as string;
			tweetTemplate.lang = dictionary2["lang"] as string;
			tweetTemplate.retweet_count = Convert.ToInt32(dictionary2["retweet_count"] as string);
			tweetTemplate.favorite_count = Convert.ToInt32(dictionary2["favorite_count"] as string);
			TwitterUserInfo twitterUserInfo = new TwitterUserInfo(dictionary2["user"] as IDictionary);
			tweetTemplate.user_id = twitterUserInfo.id;
			TwitterDataCash.AddTweet(tweetTemplate);
			TwitterDataCash.AddUser(twitterUserInfo);
			list.Add(tweetTemplate);
		}
		TW_APIRequstResult tW_APIRequstResult = new TW_APIRequstResult(true, data);
		tW_APIRequstResult.tweets = list;
		SendCompleteResult(tW_APIRequstResult);
	}
}
