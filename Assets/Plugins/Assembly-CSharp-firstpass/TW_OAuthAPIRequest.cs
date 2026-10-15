using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class TW_OAuthAPIRequest : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<TW_APIRequstResult> OnResult__BackingField = delegate
	{
	};

	private bool IsFirst = true;

	private string GetParams = string.Empty;

	private string requestUrl;

	private Dictionary<string, string> Headers = new Dictionary<string, string>();

	private SortedDictionary<string, string> requestParams = new SortedDictionary<string, string>();

	public event Action<TW_APIRequstResult> OnResult
	{
		add
		{
			Action<TW_APIRequstResult> action = OnResult__BackingField;
			Action<TW_APIRequstResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnResult__BackingField, (Action<TW_APIRequstResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<TW_APIRequstResult> action = OnResult__BackingField;
			Action<TW_APIRequstResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnResult__BackingField, (Action<TW_APIRequstResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static TW_OAuthAPIRequest Create()
	{
		return new GameObject("TW_OAuthAPIRequest").AddComponent<TW_OAuthAPIRequest>();
	}

	public void Send(string url)
	{
		requestUrl = url;
		StartCoroutine(Request());
	}

	public void AddParam(string name, int value)
	{
		AddParam(name, value.ToString());
	}

	public void AddParam(string name, string value)
	{
		if (!IsFirst)
		{
			GetParams += "&";
		}
		else
		{
			GetParams += "?";
		}
		GetParams = GetParams + name + "=" + value;
		IsFirst = false;
		requestParams.Add(name, value);
	}

	protected void SetUrl(string url)
	{
		requestUrl = url;
	}

	private IEnumerator Request()
	{
		TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
		string oauth_consumer_key = SocialPlatfromSettings.Instance.TWITTER_CONSUMER_KEY;
		string oauth_token = string.Empty;
		oauth_token = Singleton<AndroidTwitterManager>.Instance.AccessToken;
		string oauth_signature_method = "HMAC-SHA1";
		string oauth_timestamp = Convert.ToInt64(ts.TotalSeconds).ToString();
		string oauth_nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
		string oauth_version = "1.0";
		requestParams.Add("oauth_version", oauth_version);
		requestParams.Add("oauth_consumer_key", oauth_consumer_key);
		requestParams.Add("oauth_nonce", oauth_nonce);
		requestParams.Add("oauth_signature_method", oauth_signature_method);
		requestParams.Add("oauth_timestamp", oauth_timestamp);
		requestParams.Add("oauth_token", oauth_token);
		string baseString = string.Empty;
		baseString += "GET&";
		baseString = baseString + Uri.EscapeDataString(requestUrl) + "&";
		foreach (KeyValuePair<string, string> requestParam in requestParams)
		{
			baseString += Uri.EscapeDataString(requestParam.Key + "=" + requestParam.Value + "&");
		}
		baseString = baseString.Substring(0, baseString.Length - 3);
		string consumerSecret = SocialPlatfromSettings.Instance.TWITTER_CONSUMER_SECRET;
		string oauth_token_secret = string.Empty;
		string signingKey = string.Concat(str2: Uri.EscapeDataString(Singleton<AndroidTwitterManager>.Instance.AccessTokenSecret), str0: Uri.EscapeDataString(consumerSecret), str1: "&");
		HMACSHA1 hasher = new HMACSHA1(new ASCIIEncoding().GetBytes(signingKey));
		string signatureString = Convert.ToBase64String(hasher.ComputeHash(new ASCIIEncoding().GetBytes(baseString)));
		string authorizationHeaderParams = string.Empty;
		authorizationHeaderParams += "OAuth ";
		authorizationHeaderParams = authorizationHeaderParams + "oauth_nonce=\"" + Uri.EscapeDataString(oauth_nonce) + "\",";
		authorizationHeaderParams = authorizationHeaderParams + "oauth_signature_method=\"" + Uri.EscapeDataString(oauth_signature_method) + "\",";
		authorizationHeaderParams = authorizationHeaderParams + "oauth_timestamp=\"" + Uri.EscapeDataString(oauth_timestamp) + "\",";
		authorizationHeaderParams = authorizationHeaderParams + "oauth_consumer_key=\"" + Uri.EscapeDataString(oauth_consumer_key) + "\",";
		authorizationHeaderParams = authorizationHeaderParams + "oauth_token=\"" + Uri.EscapeDataString(oauth_token) + "\",";
		authorizationHeaderParams = authorizationHeaderParams + "oauth_signature=\"" + Uri.EscapeDataString(signatureString) + "\",";
		authorizationHeaderParams = authorizationHeaderParams + "oauth_version=\"" + Uri.EscapeDataString(oauth_version) + "\"";
		requestUrl += GetParams;
		Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
		Headers.Add("Authorization", authorizationHeaderParams);
		WWW www = new WWW(requestUrl, null, Headers);
		yield return www;
		TW_APIRequstResult result = ((www.error != null) ? new TW_APIRequstResult(false, www.error) : new TW_APIRequstResult(true, www.text));
		OnResult__BackingField(result);
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
