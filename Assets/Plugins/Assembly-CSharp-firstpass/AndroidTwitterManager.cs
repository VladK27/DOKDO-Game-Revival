using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class AndroidTwitterManager : Singleton<AndroidTwitterManager>, TwitterManagerInterface
{
	private bool _IsAuthed;

	private bool _IsInited;

	private string _AccessToken = string.Empty;

	private string _AccessTokenSecret = string.Empty;

	private TwitterUserInfo _userInfo;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnTwitterLoginStarted__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnTwitterLogOut__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnTwitterPostStarted__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<TWResult> OnTwitterInitedAction__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<TWResult> OnAuthCompleteAction__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<TWResult> OnPostingCompleteAction__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<TWResult> OnUserDataRequestCompleteAction__BackingField = delegate
	{
	};

	public bool IsAuthed
	{
		get
		{
			return _IsAuthed;
		}
	}

	public bool IsInited
	{
		get
		{
			return _IsInited;
		}
	}

	public TwitterUserInfo userInfo
	{
		get
		{
			return _userInfo;
		}
	}

	public string AccessToken
	{
		get
		{
			return _AccessToken;
		}
	}

	public string AccessTokenSecret
	{
		get
		{
			return _AccessTokenSecret;
		}
	}

	public event Action OnTwitterLoginStarted
	{
		add
		{
			Action action = OnTwitterLoginStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnTwitterLoginStarted__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnTwitterLoginStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnTwitterLoginStarted__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OnTwitterLogOut
	{
		add
		{
			Action action = OnTwitterLogOut__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnTwitterLogOut__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnTwitterLogOut__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnTwitterLogOut__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OnTwitterPostStarted
	{
		add
		{
			Action action = OnTwitterPostStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnTwitterPostStarted__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnTwitterPostStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnTwitterPostStarted__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<TWResult> OnTwitterInitedAction
	{
		add
		{
			Action<TWResult> action = OnTwitterInitedAction__BackingField;
			Action<TWResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnTwitterInitedAction__BackingField, (Action<TWResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<TWResult> action = OnTwitterInitedAction__BackingField;
			Action<TWResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnTwitterInitedAction__BackingField, (Action<TWResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<TWResult> OnAuthCompleteAction
	{
		add
		{
			Action<TWResult> action = OnAuthCompleteAction__BackingField;
			Action<TWResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAuthCompleteAction__BackingField, (Action<TWResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<TWResult> action = OnAuthCompleteAction__BackingField;
			Action<TWResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAuthCompleteAction__BackingField, (Action<TWResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<TWResult> OnPostingCompleteAction
	{
		add
		{
			Action<TWResult> action = OnPostingCompleteAction__BackingField;
			Action<TWResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPostingCompleteAction__BackingField, (Action<TWResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<TWResult> action = OnPostingCompleteAction__BackingField;
			Action<TWResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPostingCompleteAction__BackingField, (Action<TWResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<TWResult> OnUserDataRequestCompleteAction
	{
		add
		{
			Action<TWResult> action = OnUserDataRequestCompleteAction__BackingField;
			Action<TWResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnUserDataRequestCompleteAction__BackingField, (Action<TWResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<TWResult> action = OnUserDataRequestCompleteAction__BackingField;
			Action<TWResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnUserDataRequestCompleteAction__BackingField, (Action<TWResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void Init()
	{
		try
		{
			Type type = Type.GetType("AN_SoomlaGrow");
			MethodInfo method = type.GetMethod("Init", BindingFlags.Static | BindingFlags.Public);
			method.Invoke(null, null);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError("AndroidNative: Soomla Initalization failed" + ex.Message);
		}
		Init(SocialPlatfromSettings.Instance.TWITTER_CONSUMER_KEY, SocialPlatfromSettings.Instance.TWITTER_CONSUMER_SECRET);
	}

	public void Init(string consumer_key, string consumer_secret)
	{
		if (!_IsInited)
		{
			_IsInited = true;
			AndroidNative.TwitterInit(consumer_key, consumer_secret);
		}
	}

	public void AuthenticateUser()
	{
		OnTwitterLoginStarted__BackingField();
		if (_IsAuthed)
		{
			OnAuthSuccess();
		}
		else
		{
			AndroidNative.AuthificateUser();
		}
	}

	public void LoadUserData()
	{
		if (_IsAuthed)
		{
			AndroidNative.LoadUserData();
			return;
		}
		UnityEngine.Debug.LogWarning("Auth user before loadin data, fail event generated");
		TWResult obj = new TWResult(false, null);
		OnUserDataRequestCompleteAction__BackingField(obj);
	}

	public void Post(string status)
	{
		OnTwitterPostStarted__BackingField();
		if (!_IsAuthed)
		{
			UnityEngine.Debug.LogWarning("Auth user before posting data, fail event generated");
			TWResult obj = new TWResult(false, null);
			OnPostingCompleteAction__BackingField(obj);
		}
		else
		{
			AndroidNative.TwitterPost(status);
		}
	}

	public void Post(string status, Texture2D texture)
	{
		OnTwitterPostStarted__BackingField();
		if (!_IsAuthed)
		{
			UnityEngine.Debug.LogWarning("Auth user before posting data, fail event generated");
			TWResult obj = new TWResult(false, null);
			OnPostingCompleteAction__BackingField(obj);
		}
		else
		{
			byte[] inArray = texture.EncodeToPNG();
			string data = Convert.ToBase64String(inArray);
			AndroidNative.TwitterPostWithImage(status, data);
		}
	}

	public TwitterPostingTask PostWithAuthCheck(string status)
	{
		return PostWithAuthCheck(status, null);
	}

	public TwitterPostingTask PostWithAuthCheck(string status, Texture2D texture)
	{
		TwitterPostingTask twitterPostingTask = TwitterPostingTask.Cretae();
		twitterPostingTask.Post(status, texture, this);
		return twitterPostingTask;
	}

	public void LogOut()
	{
		OnTwitterLogOut__BackingField();
		_IsAuthed = false;
		AndroidNative.LogoutFromTwitter();
	}

	private void OnInited(string data)
	{
		if (data.Equals("1"))
		{
			_IsAuthed = true;
		}
		TWResult obj = new TWResult(true, null);
		OnTwitterInitedAction__BackingField(obj);
	}

	private void OnAuthSuccess()
	{
		_IsAuthed = true;
		TWResult obj = new TWResult(true, null);
		OnAuthCompleteAction__BackingField(obj);
	}

	private void OnAuthFailed()
	{
		TWResult obj = new TWResult(false, null);
		OnAuthCompleteAction__BackingField(obj);
	}

	private void OnPostSuccess()
	{
		TWResult obj = new TWResult(true, null);
		OnPostingCompleteAction__BackingField(obj);
	}

	private void OnPostFailed()
	{
		TWResult obj = new TWResult(false, null);
		OnPostingCompleteAction__BackingField(obj);
	}

	private void OnUserDataLoaded(string data)
	{
		_userInfo = new TwitterUserInfo(data);
		TWResult obj = new TWResult(true, data);
		OnUserDataRequestCompleteAction__BackingField(obj);
	}

	private void OnUserDataLoadFailed()
	{
		TWResult obj = new TWResult(false, null);
		OnUserDataRequestCompleteAction__BackingField(obj);
	}

	private void OnAuthInfoReceived(string data)
	{
		UnityEngine.Debug.Log("OnAuthInfoReceived");
		string[] array = data.Split("|"[0]);
		_AccessToken = array[0];
		_AccessTokenSecret = array[1];
	}
}
