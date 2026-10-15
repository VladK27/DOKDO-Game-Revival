using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using ANMiniJSON;
using SA.Common.Pattern;
using UnityEngine;

public class SPFacebook : Singleton<SPFacebook>
{
	public delegate void FB_Delegate(FB_Result result);

	private FB_UserInfo _userInfo;

	private Dictionary<string, FB_UserInfo> _friends = new Dictionary<string, FB_UserInfo>();

	private Dictionary<string, FB_UserInfo> _invitableFriends = new Dictionary<string, FB_UserInfo>();

	private bool _IsInited;

	private Dictionary<string, FB_Score> _userScores = new Dictionary<string, FB_Score>();

	private Dictionary<string, FB_Score> _appScores = new Dictionary<string, FB_Score>();

	private int lastSubmitedScore;

	private Dictionary<string, Dictionary<string, FB_LikeInfo>> _likes = new Dictionary<string, Dictionary<string, FB_LikeInfo>>();

	private List<FB_AppRequest> _AppRequests = new List<FB_AppRequest>();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnPostStarted__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnLoginStarted__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnLogOut__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnFriendsRequestStarted__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<FB_AppInviteResult> OnAppInviteSent__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnInitCompleteAction__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<FB_PostResult> OnPostingCompleteAction__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<bool> OnFocusChangedAction__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<FB_LoginResult> OnAuthCompleteAction__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<FB_Result> OnUserDataRequestCompleteAction__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<FB_Result> OnFriendsDataRequestCompleteAction__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<FB_Result> OnInvitableFriendsDataRequestCompleteAction__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<FB_AppRequestResult> OnAppRequestCompleteAction__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<FB_Result> OnAppRequestsLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<FB_Result> OnAppScoresRequestCompleteAction__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<FB_Result> OnPlayerScoresRequestCompleteAction__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<FB_Result> OnSubmitScoreRequestCompleteAction__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<FB_Result> OnDeleteScoresRequestCompleteAction__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<FB_Result> OnLikesListLoadedAction__BackingField = delegate
	{
	};

	private SP_FB_API _FB;

	public bool IsInited
	{
		get
		{
			return _IsInited;
		}
	}

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
			return FB.UserId;
		}
	}

	public string AccessToken
	{
		get
		{
			return FB.AccessToken;
		}
	}

	public string AppId
	{
		get
		{
			return FB.AppId;
		}
	}

	public FB_UserInfo userInfo
	{
		get
		{
			return _userInfo;
		}
	}

	public Dictionary<string, FB_UserInfo> friends
	{
		get
		{
			return _friends;
		}
	}

	public Dictionary<string, FB_UserInfo> InvitableFriends
	{
		get
		{
			return _invitableFriends;
		}
	}

	public List<string> friendsIds
	{
		get
		{
			if (_friends == null)
			{
				return null;
			}
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, FB_UserInfo> friend in _friends)
			{
				list.Add(friend.Key);
			}
			return list;
		}
	}

	public List<string> InvitableFriendsIds
	{
		get
		{
			if (_invitableFriends == null)
			{
				return null;
			}
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, FB_UserInfo> invitableFriend in _invitableFriends)
			{
				list.Add(invitableFriend.Key);
			}
			return list;
		}
	}

	public List<FB_UserInfo> friendsList
	{
		get
		{
			if (_friends == null)
			{
				return null;
			}
			List<FB_UserInfo> list = new List<FB_UserInfo>();
			foreach (KeyValuePair<string, FB_UserInfo> friend in _friends)
			{
				list.Add(friend.Value);
			}
			return list;
		}
	}

	public List<FB_UserInfo> InvitableFriendsList
	{
		get
		{
			if (_invitableFriends == null)
			{
				return null;
			}
			List<FB_UserInfo> list = new List<FB_UserInfo>();
			foreach (KeyValuePair<string, FB_UserInfo> invitableFriend in _invitableFriends)
			{
				list.Add(invitableFriend.Value);
			}
			return list;
		}
	}

	public Dictionary<string, FB_Score> userScores
	{
		get
		{
			return _userScores;
		}
	}

	public Dictionary<string, FB_Score> appScores
	{
		get
		{
			return _appScores;
		}
	}

	public List<FB_Score> applicationScoreList
	{
		get
		{
			List<FB_Score> list = new List<FB_Score>();
			foreach (KeyValuePair<string, FB_Score> appScore in _appScores)
			{
				list.Add(appScore.Value);
			}
			return list;
		}
	}

	public List<FB_AppRequest> AppRequests
	{
		get
		{
			return _AppRequests;
		}
	}

	public SP_FB_API FB
	{
		get
		{
			if (_FB == null)
			{
				if (SP_FB_API_v7.IsAPIEnabled)
				{
					_FB = new SP_FB_API_v7();
				}
				if (_FB == null)
				{
					_FB = new SP_FB_API_v6();
				}
			}
			return _FB;
		}
	}

	public static event Action OnPostStarted
	{
		add
		{
			Action action = OnPostStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPostStarted__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnPostStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPostStarted__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action OnLoginStarted
	{
		add
		{
			Action action = OnLoginStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLoginStarted__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnLoginStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLoginStarted__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action OnLogOut
	{
		add
		{
			Action action = OnLogOut__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLogOut__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnLogOut__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLogOut__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action OnFriendsRequestStarted
	{
		add
		{
			Action action = OnFriendsRequestStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnFriendsRequestStarted__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnFriendsRequestStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnFriendsRequestStarted__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<FB_AppInviteResult> OnAppInviteSent
	{
		add
		{
			Action<FB_AppInviteResult> action = OnAppInviteSent__BackingField;
			Action<FB_AppInviteResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAppInviteSent__BackingField, (Action<FB_AppInviteResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FB_AppInviteResult> action = OnAppInviteSent__BackingField;
			Action<FB_AppInviteResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAppInviteSent__BackingField, (Action<FB_AppInviteResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action OnInitCompleteAction
	{
		add
		{
			Action action = OnInitCompleteAction__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInitCompleteAction__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnInitCompleteAction__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInitCompleteAction__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<FB_PostResult> OnPostingCompleteAction
	{
		add
		{
			Action<FB_PostResult> action = OnPostingCompleteAction__BackingField;
			Action<FB_PostResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPostingCompleteAction__BackingField, (Action<FB_PostResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FB_PostResult> action = OnPostingCompleteAction__BackingField;
			Action<FB_PostResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPostingCompleteAction__BackingField, (Action<FB_PostResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<bool> OnFocusChangedAction
	{
		add
		{
			Action<bool> action = OnFocusChangedAction__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnFocusChangedAction__BackingField, (Action<bool>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<bool> action = OnFocusChangedAction__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnFocusChangedAction__BackingField, (Action<bool>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<FB_LoginResult> OnAuthCompleteAction
	{
		add
		{
			Action<FB_LoginResult> action = OnAuthCompleteAction__BackingField;
			Action<FB_LoginResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAuthCompleteAction__BackingField, (Action<FB_LoginResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FB_LoginResult> action = OnAuthCompleteAction__BackingField;
			Action<FB_LoginResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAuthCompleteAction__BackingField, (Action<FB_LoginResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<FB_Result> OnUserDataRequestCompleteAction
	{
		add
		{
			Action<FB_Result> action = OnUserDataRequestCompleteAction__BackingField;
			Action<FB_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnUserDataRequestCompleteAction__BackingField, (Action<FB_Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FB_Result> action = OnUserDataRequestCompleteAction__BackingField;
			Action<FB_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnUserDataRequestCompleteAction__BackingField, (Action<FB_Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<FB_Result> OnFriendsDataRequestCompleteAction
	{
		add
		{
			Action<FB_Result> action = OnFriendsDataRequestCompleteAction__BackingField;
			Action<FB_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnFriendsDataRequestCompleteAction__BackingField, (Action<FB_Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FB_Result> action = OnFriendsDataRequestCompleteAction__BackingField;
			Action<FB_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnFriendsDataRequestCompleteAction__BackingField, (Action<FB_Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<FB_Result> OnInvitableFriendsDataRequestCompleteAction
	{
		add
		{
			Action<FB_Result> action = OnInvitableFriendsDataRequestCompleteAction__BackingField;
			Action<FB_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInvitableFriendsDataRequestCompleteAction__BackingField, (Action<FB_Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FB_Result> action = OnInvitableFriendsDataRequestCompleteAction__BackingField;
			Action<FB_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInvitableFriendsDataRequestCompleteAction__BackingField, (Action<FB_Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<FB_AppRequestResult> OnAppRequestCompleteAction
	{
		add
		{
			Action<FB_AppRequestResult> action = OnAppRequestCompleteAction__BackingField;
			Action<FB_AppRequestResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAppRequestCompleteAction__BackingField, (Action<FB_AppRequestResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FB_AppRequestResult> action = OnAppRequestCompleteAction__BackingField;
			Action<FB_AppRequestResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAppRequestCompleteAction__BackingField, (Action<FB_AppRequestResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<FB_Result> OnAppRequestsLoaded
	{
		add
		{
			Action<FB_Result> action = OnAppRequestsLoaded__BackingField;
			Action<FB_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAppRequestsLoaded__BackingField, (Action<FB_Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FB_Result> action = OnAppRequestsLoaded__BackingField;
			Action<FB_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAppRequestsLoaded__BackingField, (Action<FB_Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<FB_Result> OnAppScoresRequestCompleteAction
	{
		add
		{
			Action<FB_Result> action = OnAppScoresRequestCompleteAction__BackingField;
			Action<FB_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAppScoresRequestCompleteAction__BackingField, (Action<FB_Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FB_Result> action = OnAppScoresRequestCompleteAction__BackingField;
			Action<FB_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAppScoresRequestCompleteAction__BackingField, (Action<FB_Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<FB_Result> OnPlayerScoresRequestCompleteAction
	{
		add
		{
			Action<FB_Result> action = OnPlayerScoresRequestCompleteAction__BackingField;
			Action<FB_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPlayerScoresRequestCompleteAction__BackingField, (Action<FB_Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FB_Result> action = OnPlayerScoresRequestCompleteAction__BackingField;
			Action<FB_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPlayerScoresRequestCompleteAction__BackingField, (Action<FB_Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<FB_Result> OnSubmitScoreRequestCompleteAction
	{
		add
		{
			Action<FB_Result> action = OnSubmitScoreRequestCompleteAction__BackingField;
			Action<FB_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnSubmitScoreRequestCompleteAction__BackingField, (Action<FB_Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FB_Result> action = OnSubmitScoreRequestCompleteAction__BackingField;
			Action<FB_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnSubmitScoreRequestCompleteAction__BackingField, (Action<FB_Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<FB_Result> OnDeleteScoresRequestCompleteAction
	{
		add
		{
			Action<FB_Result> action = OnDeleteScoresRequestCompleteAction__BackingField;
			Action<FB_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnDeleteScoresRequestCompleteAction__BackingField, (Action<FB_Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FB_Result> action = OnDeleteScoresRequestCompleteAction__BackingField;
			Action<FB_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnDeleteScoresRequestCompleteAction__BackingField, (Action<FB_Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<FB_Result> OnLikesListLoadedAction
	{
		add
		{
			Action<FB_Result> action = OnLikesListLoadedAction__BackingField;
			Action<FB_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLikesListLoadedAction__BackingField, (Action<FB_Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FB_Result> action = OnLikesListLoadedAction__BackingField;
			Action<FB_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLikesListLoadedAction__BackingField, (Action<FB_Result>)Delegate.Remove(action2, value), action);
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
			UnityEngine.Debug.LogError("AndroidNative. Soomla Initalization failed" + ex.Message);
		}
		FB.Init();
	}

	public void Login()
	{
		Login(SocialPlatfromSettings.Instance.fb_scopes_list.ToArray());
	}

	public void Login(params string[] scopes)
	{
		OnLoginStarted__BackingField();
		FB.Login(scopes);
	}

	public void SendAppInvite(string appLinkUrl, string previewImageUrl)
	{
		FB.AppInvite(appLinkUrl, previewImageUrl);
	}

	public void Logout()
	{
		OnLogOut__BackingField();
		FB.Logout();
	}

	public void LoadUserData()
	{
		if (IsLoggedIn)
		{
			FB.API("/me?fields=id,birthday,name,first_name,last_name,link,email,locale,location,gender,age_range,picture", FB_HttpMethod.GET, UserDataCallBack);
			return;
		}
		UnityEngine.Debug.LogWarning("Auth user before loadin data, fail event generated");
		FB_Result obj = new FB_Result(string.Empty, "User isn't authed");
		OnUserDataRequestCompleteAction__BackingField(obj);
	}

	public void LoadInvitableFrientdsInfo(int limit)
	{
		if (IsLoggedIn)
		{
			FB.API("/me?fields=invitable_friends.limit(" + limit + ").fields(first_name,id,last_name,name,link,locale,location)", FB_HttpMethod.GET, InvitableFriendsDataCallBack);
			return;
		}
		UnityEngine.Debug.LogWarning("Auth user before loadin data, fail event generated");
		FB_Result obj = new FB_Result(string.Empty, "User isn't authed");
		OnInvitableFriendsDataRequestCompleteAction__BackingField(obj);
	}

	public FB_UserInfo GetInvitableFriendById(string id)
	{
		if (_invitableFriends != null && _invitableFriends.ContainsKey(id))
		{
			return _invitableFriends[id];
		}
		return null;
	}

	public void LoadFrientdsInfo(int limit)
	{
		OnFriendsRequestStarted__BackingField();
		if (IsLoggedIn)
		{
			FB.API("/me?fields=friends.limit(" + limit + ").fields(first_name,id,last_name,name,link,locale,location)", FB_HttpMethod.GET, FriendsDataCallBack);
			return;
		}
		UnityEngine.Debug.LogWarning("Auth user before loadin data, fail event generated");
		FB_Result obj = new FB_Result(string.Empty, "User isn't authed");
		OnFriendsDataRequestCompleteAction__BackingField(obj);
	}

	public FB_UserInfo GetFriendById(string id)
	{
		if (_friends != null && _friends.ContainsKey(id))
		{
			return _friends[id];
		}
		return null;
	}

	public void PostImage(string caption, Texture2D image)
	{
		OnPostStarted__BackingField();
		byte[] contents = image.EncodeToPNG();
		WWWForm wWWForm = new WWWForm();
		wWWForm.AddField("message", caption);
		wWWForm.AddBinaryData("image", contents, "picture.png");
		wWWForm.AddField("name", caption);
		FB.API("me/photos", FB_HttpMethod.POST, PostCallback_Internal, wWWForm);
	}

	public void PostText(string message)
	{
		OnPostStarted__BackingField();
		WWWForm wWWForm = new WWWForm();
		wWWForm.AddField("message", message);
		FB.API("me/feed", FB_HttpMethod.POST, PostCallback_Internal, wWWForm);
	}

	public void FeedShare(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string actionName = "", string actionLink = "", string reference = "")
	{
		FB_PostingTask fB_PostingTask = FB_PostingTask.Cretae();
		fB_PostingTask.FeedShare(toId, link, linkName, linkCaption, linkDescription, picture, actionName, actionLink, reference);
	}

	public void SendTrunRequest(string title, string message, string data = "", string[] to = null)
	{
		string to2 = string.Empty;
		if (to != null)
		{
			to2 = string.Join(",", to);
		}
		AN_FBProxy.SendTrunRequest(title, message, data, to2);
	}

	public void SendGift(string title, string message, string objectId, string data = "", string[] to = null)
	{
		AppRequest(message, FB_RequestActionType.Send, objectId, to, data, title);
	}

	public void AskGift(string title, string message, string objectId, string data = "", string[] to = null)
	{
		AppRequest(message, FB_RequestActionType.AskFor, objectId, to, data, title);
	}

	public void SendInvite(string title, string message, string data = "", string[] to = null)
	{
		AppRequest(message, to, null, null, null, data, title);
	}

	private void OnAppRequestFailed_AndroidCB(string error)
	{
		FB_AppRequestResult obj = new FB_AppRequestResult(string.Empty, error);
		OnAppRequestCompleteAction__BackingField(obj);
	}

	private void OnAppRequestCompleted_AndroidCB(string data)
	{
		UnityEngine.Debug.Log("OnAppRequestCompleted_AndroidCB: " + data);
		string[] array = data.Split("|"[0]);
		string requestId = array[0];
		string text = array[1];
		string[] collection = text.Split(',');
		FB_AppRequestResult obj = new FB_AppRequestResult(requestId, new List<string>(collection), data);
		OnAppRequestCompleteAction__BackingField(obj);
	}

	public void AppRequest(string message, FB_RequestActionType actionType, string objectId, string[] to, string data = "", string title = "")
	{
		if (!IsLoggedIn)
		{
			UnityEngine.Debug.LogWarning("Auth user before AppRequest, fail event generated");
			FB_AppRequestResult obj = new FB_AppRequestResult(string.Empty, "User isn't authed");
			OnAppRequestCompleteAction__BackingField(obj);
		}
		else
		{
			FB.AppRequest(message, actionType, objectId, to, data, title);
		}
	}

	public void AppRequest(string message, FB_RequestActionType actionType, string objectId, List<object> filters = null, string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "")
	{
		if (!IsLoggedIn)
		{
			UnityEngine.Debug.LogWarning("Auth user before AppRequest, fail event generated");
			FB_AppRequestResult obj = new FB_AppRequestResult(string.Empty, "User isn't authed");
			OnAppRequestCompleteAction__BackingField(obj);
		}
		else
		{
			FB.AppRequest(message, actionType, objectId, filters, excludeIds, maxRecipients, data, title);
		}
	}

	public void AppRequest(string message, string[] to = null, List<object> filters = null, string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "")
	{
		if (!IsLoggedIn)
		{
			UnityEngine.Debug.LogWarning("Auth user before AppRequest, fail event generated");
			FB_AppRequestResult obj = new FB_AppRequestResult(string.Empty, "User isn't authed");
			OnAppRequestCompleteAction__BackingField(obj);
		}
		else
		{
			FB.AppRequest(message, to, filters, excludeIds, maxRecipients, data, title);
		}
	}

	public void LoadPendingRequests()
	{
		FB.API("/" + UserId + "/apprequests?fields=id,application,data,message,action_type,created_time,from,object", FB_HttpMethod.GET, OnRequestsLoadComplete);
	}

	private void OnRequestsLoadComplete(FB_Result result)
	{
		if (result.IsSucceeded)
		{
			Dictionary<string, object> dictionary = Json.Deserialize(result.RawData) as Dictionary<string, object>;
			List<object> list = dictionary["data"] as List<object>;
			AppRequests.Clear();
			foreach (object item in list)
			{
				FB_AppRequest fB_AppRequest = new FB_AppRequest();
				Dictionary<string, object> dictionary2 = item as Dictionary<string, object>;
				Dictionary<string, object> dictionary3 = dictionary2["application"] as Dictionary<string, object>;
				fB_AppRequest.ApplicationId = Convert.ToString(dictionary3["id"]);
				if (!fB_AppRequest.ApplicationId.Equals(AppId))
				{
					break;
				}
				Dictionary<string, object> dictionary4 = dictionary2["from"] as Dictionary<string, object>;
				fB_AppRequest.FromId = Convert.ToString(dictionary4["id"]);
				fB_AppRequest.FromName = Convert.ToString(dictionary4["name"]);
				fB_AppRequest.Id = Convert.ToString(dictionary2["id"]);
				if (dictionary2.ContainsKey("created_time"))
				{
					fB_AppRequest.SetCreatedTime(Convert.ToString(dictionary2["created_time"]));
				}
				if (dictionary2.ContainsKey("data"))
				{
					fB_AppRequest.Data = Convert.ToString(dictionary2["data"]);
				}
				if (dictionary2.ContainsKey("message"))
				{
					fB_AppRequest.Message = Convert.ToString(dictionary2["message"]);
				}
				if (dictionary2.ContainsKey("message"))
				{
					fB_AppRequest.Message = Convert.ToString(dictionary2["message"]);
				}
				if (dictionary2.ContainsKey("action_type"))
				{
					switch (Convert.ToString(dictionary2["action_type"]))
					{
					case "send":
						fB_AppRequest.ActionType = FB_RequestActionType.Send;
						break;
					case "askfor":
						fB_AppRequest.ActionType = FB_RequestActionType.AskFor;
						break;
					case "turn":
						fB_AppRequest.ActionType = FB_RequestActionType.Turn;
						break;
					}
				}
				if (dictionary2.ContainsKey("object"))
				{
					FB_Object fB_Object = new FB_Object();
					Dictionary<string, object> dictionary5 = dictionary2["object"] as Dictionary<string, object>;
					fB_Object.Id = Convert.ToString(dictionary5["id"]);
					fB_Object.Title = Convert.ToString(dictionary5["id"]);
					fB_Object.Type = Convert.ToString(dictionary5["id"]);
					fB_Object.SetCreatedTime(Convert.ToString(dictionary5["created_time"]));
					if (dictionary5.ContainsKey("image"))
					{
						List<object> list2 = dictionary5["image"] as List<object>;
						UnityEngine.Debug.Log(dictionary5["image"]);
						foreach (object item2 in list2)
						{
							Dictionary<string, object> dictionary6 = item2 as Dictionary<string, object>;
							fB_Object.AddImageUrl(Convert.ToString(dictionary6["url"]));
						}
					}
					fB_AppRequest.Object = fB_Object;
				}
				AppRequests.Add(fB_AppRequest);
			}
			UnityEngine.Debug.Log("SPFacebook: " + AppRequests.Count + "App request Loaded");
		}
		else
		{
			UnityEngine.Debug.LogWarning("SPFacebook: App requests failed to load");
			UnityEngine.Debug.LogWarning(result.Error.ToString());
		}
		OnAppRequestsLoaded__BackingField(result);
	}

	public void LoadPlayerScores()
	{
		FB.API("/" + UserId + "/scores", FB_HttpMethod.GET, OnLoaPlayrScoresComplete);
	}

	public void LoadAppScores()
	{
		FB.API("/" + AppId + "/scores", FB_HttpMethod.GET, OnAppScoresComplete);
	}

	public void SubmitScore(int score)
	{
		lastSubmitedScore = score;
		FB.API("/" + UserId + "/scores?score=" + score, FB_HttpMethod.POST, OnScoreSubmited);
	}

	public void DeletePlayerScores()
	{
		FB.API("/" + UserId + "/scores", FB_HttpMethod.DELETE, OnScoreDeleted);
	}

	public void LoadCurrentUserLikes()
	{
		LoadLikes(FB.UserId);
	}

	public void LoadLikes(string userId)
	{
		FB_LikesRetrieveTask fB_LikesRetrieveTask = FB_LikesRetrieveTask.Create();
		fB_LikesRetrieveTask.ActionComplete += OnUserLikesResult;
		fB_LikesRetrieveTask.LoadLikes(userId);
	}

	public void LoadLikes(string userId, string pageId)
	{
		FB_LikesRetrieveTask fB_LikesRetrieveTask = FB_LikesRetrieveTask.Create();
		fB_LikesRetrieveTask.ActionComplete += OnUserLikesResult;
		fB_LikesRetrieveTask.LoadLikes(userId, pageId);
	}

	public FB_Score GetCurrentPlayerScoreByAppId(string appId)
	{
		if (_userScores.ContainsKey(appId))
		{
			return _userScores[appId];
		}
		FB_Score fB_Score = new FB_Score();
		fB_Score.UserId = FB.UserId;
		fB_Score.AppId = appId;
		fB_Score.value = 0;
		return fB_Score;
	}

	public int GetCurrentPlayerIntScoreByAppId(string appId)
	{
		return GetCurrentPlayerScoreByAppId(appId).value;
	}

	public int GetScoreByUserId(string userId)
	{
		if (_appScores.ContainsKey(userId))
		{
			return _appScores[userId].value;
		}
		return 0;
	}

	public FB_Score GetScoreObjectByUserId(string userId)
	{
		if (_appScores.ContainsKey(userId))
		{
			return _appScores[userId];
		}
		return null;
	}

	public List<FB_LikeInfo> GerUserLikesList(string userId)
	{
		List<FB_LikeInfo> list = new List<FB_LikeInfo>();
		if (_likes.ContainsKey(userId))
		{
			foreach (KeyValuePair<string, FB_LikeInfo> item in _likes[userId])
			{
				list.Add(item.Value);
			}
		}
		return list;
	}

	public bool IsUserLikesPage(string userId, string pageId)
	{
		if (_likes.ContainsKey(userId) && _likes[userId].ContainsKey(pageId))
		{
			return true;
		}
		return false;
	}

	private void OnUserLikesResult(FB_Result result, FB_LikesRetrieveTask task)
	{
		if (result.IsFailed)
		{
			OnLikesListLoadedAction__BackingField(result);
			return;
		}
		Dictionary<string, object> dictionary = Json.Deserialize(result.RawData) as Dictionary<string, object>;
		List<object> list = dictionary["data"] as List<object>;
		Dictionary<string, FB_LikeInfo> dictionary2 = null;
		if (_likes.ContainsKey(task.userId))
		{
			dictionary2 = _likes[task.userId];
		}
		else
		{
			dictionary2 = new Dictionary<string, FB_LikeInfo>();
			_likes.Add(task.userId, dictionary2);
		}
		foreach (object item in list)
		{
			Dictionary<string, object> dictionary3 = item as Dictionary<string, object>;
			FB_LikeInfo fB_LikeInfo = new FB_LikeInfo();
			fB_LikeInfo.Id = Convert.ToString(dictionary3["id"]);
			fB_LikeInfo.Name = Convert.ToString(dictionary3["name"]);
			fB_LikeInfo.CreatedTime = Convert.ToString(dictionary3["created_time"]);
			if (dictionary2.ContainsKey(fB_LikeInfo.Id))
			{
				dictionary2[fB_LikeInfo.Id] = fB_LikeInfo;
			}
			else
			{
				dictionary2.Add(fB_LikeInfo.Id, fB_LikeInfo);
			}
		}
		OnLikesListLoadedAction__BackingField(result);
	}

	private void OnScoreDeleted(FB_Result result)
	{
		if (result.IsFailed)
		{
			OnDeleteScoresRequestCompleteAction__BackingField(result);
			return;
		}
		if (result.RawData.Equals("true"))
		{
			FB_Score fB_Score = new FB_Score();
			fB_Score.AppId = AppId;
			fB_Score.UserId = UserId;
			fB_Score.value = 0;
			if (_appScores.ContainsKey(UserId))
			{
				_appScores[UserId].value = 0;
			}
			else
			{
				_appScores.Add(fB_Score.UserId, fB_Score);
			}
			if (_userScores.ContainsKey(AppId))
			{
				_userScores[AppId].value = 0;
			}
			else
			{
				_userScores.Add(AppId, fB_Score);
			}
		}
		OnDeleteScoresRequestCompleteAction__BackingField(result);
	}

	private void OnScoreSubmited(FB_Result result)
	{
		if (result.IsFailed)
		{
			OnSubmitScoreRequestCompleteAction__BackingField(result);
			return;
		}
		if (result.RawData.Equals("true"))
		{
			FB_Score fB_Score = new FB_Score();
			fB_Score.AppId = AppId;
			fB_Score.UserId = UserId;
			fB_Score.value = lastSubmitedScore;
			if (_appScores.ContainsKey(UserId))
			{
				_appScores[UserId].value = lastSubmitedScore;
			}
			else
			{
				_appScores.Add(fB_Score.UserId, fB_Score);
			}
			if (_userScores.ContainsKey(AppId))
			{
				_userScores[AppId].value = lastSubmitedScore;
			}
			else
			{
				_userScores.Add(AppId, fB_Score);
			}
		}
		OnSubmitScoreRequestCompleteAction__BackingField(result);
	}

	private void OnAppScoresComplete(FB_Result result)
	{
		if (result.IsFailed)
		{
			OnAppScoresRequestCompleteAction__BackingField(result);
			return;
		}
		Dictionary<string, object> dictionary = Json.Deserialize(result.RawData) as Dictionary<string, object>;
		List<object> list = dictionary["data"] as List<object>;
		foreach (object item in list)
		{
			FB_Score fB_Score = new FB_Score();
			Dictionary<string, object> dictionary2 = item as Dictionary<string, object>;
			if (dictionary2.ContainsKey("user"))
			{
				Dictionary<string, object> dictionary3 = dictionary2["user"] as Dictionary<string, object>;
				if (dictionary3.ContainsKey("id"))
				{
					fB_Score.UserId = Convert.ToString(dictionary3["id"]);
				}
				if (dictionary3.ContainsKey("name"))
				{
					fB_Score.UserName = Convert.ToString(dictionary3["name"]);
				}
			}
			if (dictionary2.ContainsKey("score"))
			{
				fB_Score.value = Convert.ToInt32(dictionary2["score"]);
			}
			if (dictionary2.ContainsKey("application"))
			{
				Dictionary<string, object> dictionary4 = dictionary2["application"] as Dictionary<string, object>;
				if (dictionary4.ContainsKey("id"))
				{
					fB_Score.AppId = Convert.ToString(dictionary4["id"]);
				}
				if (dictionary4.ContainsKey("name"))
				{
					fB_Score.AppName = Convert.ToString(dictionary4["name"]);
				}
			}
			AddToAppScores(fB_Score);
		}
		OnAppScoresRequestCompleteAction__BackingField(result);
	}

	private void AddToAppScores(FB_Score score)
	{
		if (_appScores.ContainsKey(score.UserId))
		{
			_appScores[score.UserId] = score;
		}
		else
		{
			_appScores.Add(score.UserId, score);
		}
		if (_userScores.ContainsKey(score.AppId))
		{
			_userScores[score.AppId] = score;
		}
		else
		{
			_userScores.Add(score.AppId, score);
		}
	}

	private void AddToUserScores(FB_Score score)
	{
		if (_userScores.ContainsKey(score.AppId))
		{
			_userScores[score.AppId] = score;
		}
		else
		{
			_userScores.Add(score.AppId, score);
		}
		if (_appScores.ContainsKey(score.UserId))
		{
			_appScores[score.UserId] = score;
		}
		else
		{
			_appScores.Add(score.UserId, score);
		}
	}

	private void OnLoaPlayrScoresComplete(FB_Result result)
	{
		if (result.IsFailed)
		{
			OnPlayerScoresRequestCompleteAction__BackingField(result);
			return;
		}
		Dictionary<string, object> dictionary = Json.Deserialize(result.RawData) as Dictionary<string, object>;
		List<object> list = dictionary["data"] as List<object>;
		foreach (object item in list)
		{
			FB_Score fB_Score = new FB_Score();
			Dictionary<string, object> dictionary2 = item as Dictionary<string, object>;
			Dictionary<string, object> dictionary3 = dictionary2["user"] as Dictionary<string, object>;
			fB_Score.UserId = Convert.ToString(dictionary3["id"]);
			fB_Score.UserName = Convert.ToString(dictionary3["name"]);
			fB_Score.value = Convert.ToInt32(dictionary2["score"]);
			fB_Score.AppId = Convert.ToString(FB.AppId);
			AddToUserScores(fB_Score);
		}
		OnPlayerScoresRequestCompleteAction__BackingField(result);
	}

	public void ParseInvitableFriendsData(string data)
	{
		ParseFriendsFromJson(data, _invitableFriends, true);
	}

	private void ParseFriendsFromJson(string data, Dictionary<string, FB_UserInfo> friends, bool invitable = false)
	{
		UnityEngine.Debug.Log("ParceFriendsData");
		UnityEngine.Debug.Log(data);
		try
		{
			_friends.Clear();
			IDictionary dictionary = Json.Deserialize(data) as IDictionary;
			IDictionary dictionary2 = ((!invitable) ? (dictionary["friends"] as IDictionary) : (dictionary["invitable_friends"] as IDictionary));
			IList list = dictionary2["data"] as IList;
			for (int i = 0; i < list.Count; i++)
			{
				FB_UserInfo fB_UserInfo = new FB_UserInfo(list[i] as IDictionary);
				_friends.Add(fB_UserInfo.Id, fB_UserInfo);
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogWarning("Parceing Friends Data failed");
			UnityEngine.Debug.LogWarning(ex.Message);
		}
	}

	public void ParseFriendsData(string data)
	{
		ParseFriendsFromJson(data, _friends);
	}

	public void OnInitComplete()
	{
		_IsInited = true;
		OnInitCompleteAction__BackingField();
		UnityEngine.Debug.Log("FB.Init completed: Is user logged in? " + IsLoggedIn);
	}

	public void OnHideUnity(bool isGameShown)
	{
		OnFocusChangedAction__BackingField(isGameShown);
	}

	public void LoginCallback(FB_LoginResult result)
	{
		OnAuthCompleteAction__BackingField(result);
	}

	private void PostCallback_Internal(FB_Result result)
	{
		FB_PostResult obj = new FB_PostResult(result.RawData, result.Error);
		OnPostingCompleteAction__BackingField(obj);
	}

	public void PostCallback(FB_PostResult result)
	{
		OnPostingCompleteAction__BackingField(result);
	}

	public void AppRequestCallback(FB_AppRequestResult result)
	{
		OnAppRequestCompleteAction__BackingField(result);
	}

	public void AppInviteResultCallback(FB_AppInviteResult result)
	{
		OnAppInviteSent__BackingField(result);
	}

	private void UserDataCallBack(FB_Result result)
	{
		if (result.IsFailed)
		{
			UnityEngine.Debug.LogWarning(result.Error);
		}
		else
		{
			UnityEngine.Debug.Log("[UserDataCallBack] result.RawData " + result.RawData);
			_userInfo = new FB_UserInfo(result.RawData);
		}
		OnUserDataRequestCompleteAction__BackingField(result);
	}

	private void InvitableFriendsDataCallBack(FB_Result result)
	{
		if (result.IsFailed)
		{
			UnityEngine.Debug.LogWarning(result.Error);
		}
		else
		{
			ParseInvitableFriendsData(result.RawData);
		}
		OnInvitableFriendsDataRequestCompleteAction__BackingField(result);
	}

	private void FriendsDataCallBack(FB_Result result)
	{
		if (result.IsFailed)
		{
			UnityEngine.Debug.LogWarning(result.Error);
		}
		else
		{
			ParseFriendsData(result.RawData);
		}
		OnFriendsDataRequestCompleteAction__BackingField(result);
	}
}
