using System;
using System.Collections;
using System.Collections.Generic;
using ANMiniJSON;
using UnityEngine;

public class TW_UserTimeLineRequest : TW_APIRequest
{
	public static TW_UserTimeLineRequest Create()
	{
		return new GameObject("TW_TimeLineRequest").AddComponent<TW_UserTimeLineRequest>();
	}

	private void Awake()
	{
		SetUrl("https://api.twitter.com/1.1/statuses/user_timeline.json");
	}

	protected override void OnResult(string data)
	{
		List<TweetTemplate> list = new List<TweetTemplate>();
		List<object> list2 = Json.Deserialize(data) as List<object>;
		foreach (object item in list2)
		{
			Dictionary<string, object> dictionary = item as Dictionary<string, object>;
			TweetTemplate tweetTemplate = new TweetTemplate();
			tweetTemplate.id = dictionary["id_str"] as string;
			tweetTemplate.created_at = dictionary["created_at"] as string;
			tweetTemplate.text = dictionary["text"] as string;
			tweetTemplate.source = dictionary["source"] as string;
			tweetTemplate.in_reply_to_status_id = dictionary["in_reply_to_status_id"] as string;
			tweetTemplate.in_reply_to_user_id = dictionary["in_reply_to_user_id"] as string;
			tweetTemplate.in_reply_to_screen_name = dictionary["in_reply_to_screen_name"] as string;
			tweetTemplate.geo = dictionary["geo"] as string;
			tweetTemplate.place = dictionary["place"] as string;
			tweetTemplate.lang = dictionary["lang"] as string;
			tweetTemplate.retweet_count = Convert.ToInt32(dictionary["retweet_count"] as string);
			tweetTemplate.favorite_count = Convert.ToInt32(dictionary["favorite_count"] as string);
			TwitterUserInfo twitterUserInfo = new TwitterUserInfo(dictionary["user"] as IDictionary);
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
