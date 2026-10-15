using SA.Common.Pattern;
using UnityEngine;

public class FB_PostingTask : AsyncTask
{
	private string _toId = string.Empty;

	private string _link = string.Empty;

	private string _linkName = string.Empty;

	private string _linkCaption = string.Empty;

	private string _linkDescription = string.Empty;

	private string _picture = string.Empty;

	private string _actionName = string.Empty;

	private string _actionLink = string.Empty;

	private string _reference = string.Empty;

	public static FB_PostingTask Cretae()
	{
		return new GameObject("FB_PostingTask").AddComponent<FB_PostingTask>();
	}

	public void FeedShare(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string actionName = "", string actionLink = "", string reference = "")
	{
		_toId = toId;
		_link = link;
		_linkName = linkName;
		_linkCaption = linkCaption;
		_linkDescription = linkDescription;
		_picture = picture;
		_actionName = actionName;
		_actionLink = actionLink;
		_reference = reference;
		if (Singleton<SPFacebook>.Instance.IsInited)
		{
			OnFBInited();
			return;
		}
		SPFacebook.OnInitCompleteAction += OnFBInited;
		Singleton<SPFacebook>.Instance.Init();
	}

	private void OnFBInited()
	{
		SPFacebook.OnInitCompleteAction -= OnFBInited;
		if (Singleton<SPFacebook>.Instance.IsLoggedIn)
		{
			OnFBAuth(null);
			return;
		}
		SPFacebook.OnAuthCompleteAction += OnFBAuth;
		Singleton<SPFacebook>.Instance.Login();
	}

	private void OnFBAuth(FB_Result result)
	{
		SPFacebook.OnAuthCompleteAction -= OnFBAuth;
		if (Singleton<SPFacebook>.Instance.IsLoggedIn)
		{
			Singleton<SPFacebook>.Instance.FB.FeedShare(_toId, _link, _linkName, _linkCaption, _linkDescription, _picture, _actionName, _actionLink, _reference);
		}
		else
		{
			FB_PostResult result2 = new FB_PostResult(string.Empty, "Auth failed");
			Singleton<SPFacebook>.Instance.PostCallback(result2);
		}
		Object.Destroy(base.gameObject);
	}
}
