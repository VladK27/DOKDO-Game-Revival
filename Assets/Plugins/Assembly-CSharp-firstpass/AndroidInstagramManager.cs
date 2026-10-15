using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class AndroidInstagramManager : Singleton<AndroidInstagramManager>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<InstagramPostResult> OnPostingCompleteAction__BackingField = delegate
	{
	};

	public static event Action<InstagramPostResult> OnPostingCompleteAction
	{
		add
		{
			Action<InstagramPostResult> action = OnPostingCompleteAction__BackingField;
			Action<InstagramPostResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPostingCompleteAction__BackingField, (Action<InstagramPostResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<InstagramPostResult> action = OnPostingCompleteAction__BackingField;
			Action<InstagramPostResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPostingCompleteAction__BackingField, (Action<InstagramPostResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public void Share(Texture2D texture)
	{
		Share(texture, string.Empty);
	}

	public void Share(Texture2D texture, string message)
	{
		byte[] inArray = texture.EncodeToPNG();
		string data = Convert.ToBase64String(inArray);
		AN_SocialSharingProxy.InstagramPostImage(data, message);
	}

	private void OnPostSuccess()
	{
		OnPostingCompleteAction__BackingField(InstagramPostResult.RESULT_OK);
	}

	private void OnPostFailed(string data)
	{
		int num = Convert.ToInt32(data);
		InstagramPostResult obj = InstagramPostResult.NO_APPLICATION_INSTALLED;
		switch (num)
		{
		case 1:
			obj = InstagramPostResult.NO_APPLICATION_INSTALLED;
			break;
		case 2:
			obj = InstagramPostResult.INTERNAL_EXCEPTION;
			break;
		}
		OnPostingCompleteAction__BackingField(obj);
	}
}
