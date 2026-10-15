using System.Collections.Generic;
using UnityEngine;

public class SP_FB_API_v6 : SP_FB_API
{
	public bool IsLoggedIn
	{
		get
		{
			return false;
		}
	}

	public string UserId
	{
		get
		{
			return string.Empty;
		}
	}

	public string AccessToken
	{
		get
		{
			return string.Empty;
		}
	}

	public string AppId
	{
		get
		{
			return string.Empty;
		}
	}

	public static bool IsAPIEnabled
	{
		get
		{
			return false;
		}
	}

	public void Init()
	{
	}

	public void Login(params string[] scopes)
	{
	}

	public void Logout()
	{
	}

	public void API(string query, FB_HttpMethod method, SPFacebook.FB_Delegate callback)
	{
	}

	public void API(string query, FB_HttpMethod method, SPFacebook.FB_Delegate callback, WWWForm form)
	{
	}

	public void AppRequest(string message, FB_RequestActionType actionType, string objectId, string[] to, string data = "", string title = "")
	{
	}

	public void AppRequest(string message, FB_RequestActionType actionType, string objectId, List<object> filters = null, string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "")
	{
	}

	public void AppRequest(string message, string[] to = null, List<object> filters = null, string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "")
	{
	}

	public void FeedShare(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string actionName = "", string actionLink = "", string reference = "")
	{
	}

	public void AppInvite(string appLinkUrl, string previewImageUrl)
	{
	}
}
