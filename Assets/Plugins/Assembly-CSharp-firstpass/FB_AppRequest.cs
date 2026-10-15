using System;
using SA.Common.Pattern;

public class FB_AppRequest
{
	public string Id;

	public string ApplicationId;

	public string Message = string.Empty;

	public FB_RequestActionType ActionType = FB_RequestActionType.Undefined;

	public FB_AppRequestState State;

	public string FromId;

	public string FromName;

	public DateTime CreatedTime;

	public string CreatedTimeString;

	public string Data = string.Empty;

	public FB_Object Object;

	public Action<FB_Result> OnDeleteRequestFinished = delegate
	{
	};

	public void SetCreatedTime(string time_string)
	{
		CreatedTimeString = time_string;
		CreatedTime = DateTime.Parse(time_string);
	}

	public void Delete()
	{
		Singleton<SPFacebook>.Instance.FB.API(Id, FB_HttpMethod.DELETE, OnDeleteActionFinish);
		State = FB_AppRequestState.Deleted;
	}

	private void OnDeleteActionFinish(FB_Result result)
	{
		if (result.IsSucceeded)
		{
			State = FB_AppRequestState.Deleted;
		}
		OnDeleteRequestFinished(result);
	}
}
