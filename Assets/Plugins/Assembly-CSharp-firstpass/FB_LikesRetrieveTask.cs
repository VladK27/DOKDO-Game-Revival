using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class FB_LikesRetrieveTask : MonoBehaviour
{
	private string _userId;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<FB_Result, FB_LikesRetrieveTask> ActionComplete__BackingField = delegate
	{
	};

	public string userId
	{
		get
		{
			return _userId;
		}
	}

	public event Action<FB_Result, FB_LikesRetrieveTask> ActionComplete
	{
		add
		{
			Action<FB_Result, FB_LikesRetrieveTask> action = ActionComplete__BackingField;
			Action<FB_Result, FB_LikesRetrieveTask> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionComplete__BackingField, (Action<FB_Result, FB_LikesRetrieveTask>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FB_Result, FB_LikesRetrieveTask> action = ActionComplete__BackingField;
			Action<FB_Result, FB_LikesRetrieveTask> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionComplete__BackingField, (Action<FB_Result, FB_LikesRetrieveTask>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static FB_LikesRetrieveTask Create()
	{
		return new GameObject("FBLikesRetrieveTask").AddComponent<FB_LikesRetrieveTask>();
	}

	public void LoadLikes(string userId)
	{
		_userId = userId;
		Singleton<SPFacebook>.Instance.FB.API("/" + userId + "/likes", FB_HttpMethod.GET, OnUserLikesResult);
	}

	public void LoadLikes(string userId, string pageId)
	{
		_userId = userId;
		Singleton<SPFacebook>.Instance.FB.API("/" + userId + "/likes/" + pageId, FB_HttpMethod.GET, OnUserLikesResult);
	}

	private void OnUserLikesResult(FB_Result result)
	{
		ActionComplete__BackingField(result, this);
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
