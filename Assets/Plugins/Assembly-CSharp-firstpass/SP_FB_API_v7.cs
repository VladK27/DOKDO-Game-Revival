using System;
using System.Collections.Generic;
using Facebook.Unity;
using SA.Common.Pattern;
using UnityEngine;

public class SP_FB_API_v7 : SP_FB_API
{
	private const string PUBLISH_PERMISSION_STRING = "publish_actions";

	private string _UserId = string.Empty;

	private string _AccessToken = string.Empty;

	public bool IsLoggedIn
	{
		get
		{
			return FB.IsLoggedIn;
		}
	}

	public string UserId
	{
		get
		{
			return _UserId;
		}
	}

	public string AccessToken
	{
		get
		{
			return _AccessToken;
		}
	}

	public string AppId
	{
		get
		{
			return FB.AppId;
		}
	}

	public static bool IsAPIEnabled
	{
		get
		{
			return true;
		}
	}

	public void Init()
	{
		FB.Init(OnInitComplete, OnHideUnity);
	}

	public void Login(params string[] scopes)
	{
		List<string> list = new List<string>(scopes);
		if (list.Contains("publish_actions"))
		{
			list.Remove("publish_actions");
			if (FB.IsLoggedIn)
			{
				if (list.Count > 0)
				{
					Debug.Log("Logging with read permission");
					FB.LogInWithReadPermissions(list, ReadPermissionCallback);
					return;
				}
				Debug.Log("Logging with write permission");
				List<string> list2 = new List<string>();
				list2.Add("publish_actions");
				FB.LogInWithPublishPermissions(list2, LoginCallback);
			}
			else
			{
				FB.LogInWithReadPermissions(list, ReadPermissionCallback);
			}
		}
		else
		{
			FB.LogInWithReadPermissions(list, LoginCallback);
		}
	}

	private void ReadPermissionCallback(ILoginResult result)
	{
		LoginCallback(result);
		if (FB.IsLoggedIn)
		{
			Debug.Log("ReadPermissionCallback:Logging with write permission");
			List<string> list = new List<string>();
			list.Add("publish_actions");
			FB.LogInWithPublishPermissions(list, LoginCallback);
		}
	}

	public void Logout()
	{
		_UserId = string.Empty;
		_AccessToken = string.Empty;
		FB.LogOut();
	}

	public void API(string query, FB_HttpMethod method, SPFacebook.FB_Delegate callback)
	{
		new FB_GrapRequest_V7(query, ConvertHttpMethod(method), callback);
	}

	public void API(string query, FB_HttpMethod method, SPFacebook.FB_Delegate callback, WWWForm form)
	{
		new FB_GrapRequest_V7(query, ConvertHttpMethod(method), callback, form);
	}

	public void AppRequest(string message, FB_RequestActionType actionType, string objectId, string[] to, string data = "", string title = "")
	{
		FB.AppRequest(message, ConvertActionType(actionType), objectId, to, data, title, AppRequestCallback);
	}

	public void AppRequest(string message, FB_RequestActionType actionType, string objectId, List<object> filters = null, string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "")
	{
		FB.AppRequest(message, ConvertActionType(actionType), objectId, filters, excludeIds, maxRecipients, data, title, AppRequestCallback);
	}

	public void AppRequest(string message, string[] to = null, List<object> filters = null, string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "")
	{
		FB.AppRequest(message, to, filters, excludeIds, maxRecipients, data, title, AppRequestCallback);
	}

	public void FeedShare(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string actionName = "", string actionLink = "", string reference = "")
	{
		Uri link2 = null;
		if (!string.IsNullOrEmpty(link))
		{
			link2 = new Uri(link);
		}
		Uri picture2 = null;
		if (!string.IsNullOrEmpty(picture))
		{
			picture2 = new Uri(picture);
		}
		FB.FeedShare(toId, link2, linkName, linkCaption, linkDescription, picture2, reference, PostCallback);
	}

	public void AppInvite(string appLink, string imgLink)
	{
		FB.Mobile.AppInvite(new Uri(appLink), new Uri(imgLink), AppInviteResult);
	}

	private void AppInviteResult(IAppInviteResult result)
	{
		FB_AppInviteResult result2 = new FB_AppInviteResult(result.Cancelled, result.RawResult, result.Error, result.ResultDictionary);
		Singleton<SPFacebook>.Instance.AppInviteResultCallback(result2);
	}

	private void AppRequestCallback(IAppRequestResult result)
	{
		FB_AppRequestResult result2 = new FB_AppRequestResult(result.RawResult, result.Error);
		Singleton<SPFacebook>.Instance.AppRequestCallback(result2);
	}

	private void LoginCallback(ILoginResult result)
	{
		FB_LoginResult fB_LoginResult = ((result != null) ? new FB_LoginResult(result.RawResult, result.Error, result.Cancelled) : new FB_LoginResult(string.Empty, "Null Response", false));
		if (fB_LoginResult.IsSucceeded && !result.Cancelled && result.AccessToken != null)
		{
			_UserId = result.AccessToken.UserId;
			_AccessToken = result.AccessToken.TokenString;
			fB_LoginResult.SetCredential(_UserId, _AccessToken);
		}
		Singleton<SPFacebook>.Instance.LoginCallback(fB_LoginResult);
	}

	private void OnInitComplete()
	{
		if (Facebook.Unity.AccessToken.CurrentAccessToken != null)
		{
			_AccessToken = Facebook.Unity.AccessToken.CurrentAccessToken.TokenString;
			_UserId = Facebook.Unity.AccessToken.CurrentAccessToken.UserId;
		}
		Singleton<SPFacebook>.Instance.OnInitComplete();
	}

	private void OnHideUnity(bool isGameShown)
	{
		Singleton<SPFacebook>.Instance.OnHideUnity(isGameShown);
	}

	private void PostCallback(IShareResult result)
	{
		FB_PostResult result2 = new FB_PostResult(result.RawResult, result.Error);
		Singleton<SPFacebook>.Instance.PostCallback(result2);
	}

	private HttpMethod ConvertHttpMethod(FB_HttpMethod method)
	{
		switch (method)
		{
		case FB_HttpMethod.GET:
			return HttpMethod.GET;
		case FB_HttpMethod.POST:
			return HttpMethod.POST;
		case FB_HttpMethod.DELETE:
			return HttpMethod.DELETE;
		default:
			return HttpMethod.GET;
		}
	}

	private OGActionType ConvertActionType(FB_RequestActionType actionType)
	{
		switch (actionType)
		{
		case FB_RequestActionType.AskFor:
			return OGActionType.ASKFOR;
		case FB_RequestActionType.Send:
			return OGActionType.SEND;
		case FB_RequestActionType.Turn:
			return OGActionType.TURN;
		default:
			return OGActionType.ASKFOR;
		}
	}
}
