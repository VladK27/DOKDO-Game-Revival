using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class GameCenterManager : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<Result> OnAuthFinished__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_LeaderboardResult> OnScoreSubmitted__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_LeaderboardResult> OnScoresListLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_LeaderboardResult> OnLeadrboardInfoLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<Result> OnLeaderboardSetsInfoLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<Result> OnAchievementsReset__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<Result> OnAchievementsLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_AchievementProgressResult> OnAchievementsProgress__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnGameCenterViewDismissed__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<Result> OnFriendsListLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_UserInfoLoadResult> OnUserInfoLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_PlayerSignatureResult> OnPlayerSignatureRetrieveResult__BackingField = delegate
	{
	};

	private static bool _IsInitialized = false;

	private static bool _IsAchievementsInfoLoaded = false;

	private static Dictionary<string, GK_Player> _players = new Dictionary<string, GK_Player>();

	private static List<string> _friendsList = new List<string>();

	private static List<GK_LeaderboardSet> _LeaderboardSets = new List<GK_LeaderboardSet>();

	private static Dictionary<int, GK_FriendRequest> _FriendRequests = new Dictionary<int, GK_FriendRequest>();

	private static GK_Player _player = null;

	private const string ISN_GC_PP_KEY = "ISN_GameCenterManager";

	public static List<GK_AchievementTemplate> Achievements
	{
		get
		{
			return IOSNativeSettings.Instance.Achievements;
		}
	}

	public static List<GK_Leaderboard> Leaderboards
	{
		get
		{
			return IOSNativeSettings.Instance.Leaderboards;
		}
	}

	public static Dictionary<string, GK_Player> Players
	{
		get
		{
			return _players;
		}
	}

	public static GK_Player Player
	{
		get
		{
			return _player;
		}
	}

	public static bool IsInitialized
	{
		get
		{
			return _IsInitialized;
		}
	}

	public static List<GK_LeaderboardSet> LeaderboardSets
	{
		get
		{
			return _LeaderboardSets;
		}
	}

	public static bool IsPlayerAuthenticated
	{
		get
		{
			return false;
		}
	}

	public static bool IsAchievementsInfoLoaded
	{
		get
		{
			return _IsAchievementsInfoLoaded;
		}
	}

	public static List<string> FriendsList
	{
		get
		{
			return _friendsList;
		}
	}

	public static bool IsPlayerUnderage
	{
		get
		{
			return false;
		}
	}

	public static event Action<Result> OnAuthFinished
	{
		add
		{
			Action<Result> action = OnAuthFinished__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAuthFinished__BackingField, (Action<Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Result> action = OnAuthFinished__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAuthFinished__BackingField, (Action<Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_LeaderboardResult> OnScoreSubmitted
	{
		add
		{
			Action<GK_LeaderboardResult> action = OnScoreSubmitted__BackingField;
			Action<GK_LeaderboardResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnScoreSubmitted__BackingField, (Action<GK_LeaderboardResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_LeaderboardResult> action = OnScoreSubmitted__BackingField;
			Action<GK_LeaderboardResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnScoreSubmitted__BackingField, (Action<GK_LeaderboardResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_LeaderboardResult> OnScoresListLoaded
	{
		add
		{
			Action<GK_LeaderboardResult> action = OnScoresListLoaded__BackingField;
			Action<GK_LeaderboardResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnScoresListLoaded__BackingField, (Action<GK_LeaderboardResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_LeaderboardResult> action = OnScoresListLoaded__BackingField;
			Action<GK_LeaderboardResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnScoresListLoaded__BackingField, (Action<GK_LeaderboardResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_LeaderboardResult> OnLeadrboardInfoLoaded
	{
		add
		{
			Action<GK_LeaderboardResult> action = OnLeadrboardInfoLoaded__BackingField;
			Action<GK_LeaderboardResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLeadrboardInfoLoaded__BackingField, (Action<GK_LeaderboardResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_LeaderboardResult> action = OnLeadrboardInfoLoaded__BackingField;
			Action<GK_LeaderboardResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLeadrboardInfoLoaded__BackingField, (Action<GK_LeaderboardResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<Result> OnLeaderboardSetsInfoLoaded
	{
		add
		{
			Action<Result> action = OnLeaderboardSetsInfoLoaded__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLeaderboardSetsInfoLoaded__BackingField, (Action<Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Result> action = OnLeaderboardSetsInfoLoaded__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLeaderboardSetsInfoLoaded__BackingField, (Action<Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<Result> OnAchievementsReset
	{
		add
		{
			Action<Result> action = OnAchievementsReset__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAchievementsReset__BackingField, (Action<Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Result> action = OnAchievementsReset__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAchievementsReset__BackingField, (Action<Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<Result> OnAchievementsLoaded
	{
		add
		{
			Action<Result> action = OnAchievementsLoaded__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAchievementsLoaded__BackingField, (Action<Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Result> action = OnAchievementsLoaded__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAchievementsLoaded__BackingField, (Action<Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_AchievementProgressResult> OnAchievementsProgress
	{
		add
		{
			Action<GK_AchievementProgressResult> action = OnAchievementsProgress__BackingField;
			Action<GK_AchievementProgressResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAchievementsProgress__BackingField, (Action<GK_AchievementProgressResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_AchievementProgressResult> action = OnAchievementsProgress__BackingField;
			Action<GK_AchievementProgressResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAchievementsProgress__BackingField, (Action<GK_AchievementProgressResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action OnGameCenterViewDismissed
	{
		add
		{
			Action action = OnGameCenterViewDismissed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnGameCenterViewDismissed__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnGameCenterViewDismissed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnGameCenterViewDismissed__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<Result> OnFriendsListLoaded
	{
		add
		{
			Action<Result> action = OnFriendsListLoaded__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnFriendsListLoaded__BackingField, (Action<Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Result> action = OnFriendsListLoaded__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnFriendsListLoaded__BackingField, (Action<Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_UserInfoLoadResult> OnUserInfoLoaded
	{
		add
		{
			Action<GK_UserInfoLoadResult> action = OnUserInfoLoaded__BackingField;
			Action<GK_UserInfoLoadResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnUserInfoLoaded__BackingField, (Action<GK_UserInfoLoadResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_UserInfoLoadResult> action = OnUserInfoLoaded__BackingField;
			Action<GK_UserInfoLoadResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnUserInfoLoaded__BackingField, (Action<GK_UserInfoLoadResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_PlayerSignatureResult> OnPlayerSignatureRetrieveResult
	{
		add
		{
			Action<GK_PlayerSignatureResult> action = OnPlayerSignatureRetrieveResult__BackingField;
			Action<GK_PlayerSignatureResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPlayerSignatureRetrieveResult__BackingField, (Action<GK_PlayerSignatureResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_PlayerSignatureResult> action = OnPlayerSignatureRetrieveResult__BackingField;
			Action<GK_PlayerSignatureResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPlayerSignatureRetrieveResult__BackingField, (Action<GK_PlayerSignatureResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	[Obsolete("init is deprecated, please use Init instead.")]
	public static void init()
	{
		Init();
	}

	public static void Init()
	{
		if (_IsInitialized)
		{
			return;
		}
		_IsInitialized = true;
		Singleton<GameCenterInvitations>.Instance.Init();
		GameObject gameObject = new GameObject("GameCenterManager");
		gameObject.AddComponent<GameCenterManager>();
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		foreach (GK_Leaderboard leaderboard in Leaderboards)
		{
			leaderboard.Refresh();
		}
	}

	public static void RetrievePlayerSignature()
	{
	}

	public static void ShowGmaeKitNotification(string title, string message)
	{
	}

	public static void RegisterAchievement(string achievementId)
	{
		GK_AchievementTemplate gK_AchievementTemplate = new GK_AchievementTemplate();
		gK_AchievementTemplate.Id = achievementId;
		RegisterAchievement(gK_AchievementTemplate);
	}

	public static void RegisterAchievement(GK_AchievementTemplate achievement)
	{
		bool flag = false;
		int index = 0;
		foreach (GK_AchievementTemplate achievement2 in Achievements)
		{
			if (achievement2.Id.Equals(achievement.Id))
			{
				flag = true;
				index = Achievements.IndexOf(achievement2);
				break;
			}
		}
		if (flag)
		{
			Achievements[index] = achievement;
		}
		else
		{
			Achievements.Add(achievement);
		}
	}

	public static void ShowLeaderboard(string leaderboardId)
	{
		ShowLeaderboard(leaderboardId, GK_TimeSpan.ALL_TIME);
	}

	public static void ShowLeaderboard(string leaderboardId, GK_TimeSpan timeSpan)
	{
	}

	public static void ShowLeaderboards()
	{
	}

	public static void ReportScore(long score, string leaderboardId, long context = 0L)
	{
		if (!IOSNativeSettings.Instance.DisablePluginLogs)
		{
			ISN_Logger.Log("unity reportScore: " + leaderboardId);
		}
	}

	public static void ReportScore(double score, string leaderboardId)
	{
		if (!IOSNativeSettings.Instance.DisablePluginLogs)
		{
			ISN_Logger.Log("unity reportScore double: " + leaderboardId);
		}
	}

	public static void RetrieveFriends()
	{
	}

	[Obsolete("LoadUsersData is deprecated, please use LoadGKPlayerInfo instead.")]
	public static void LoadUsersData(string[] UIDs)
	{
		LoadGKPlayerInfo(UIDs[0]);
	}

	public static void LoadGKPlayerInfo(string playerId)
	{
	}

	public static void LoadGKPlayerPhoto(string playerId, GK_PhotoSize size)
	{
	}

	[Obsolete("LoadCurrentPlayerScore is deprecated, please use LoadLeaderboardInfo instead.")]
	public static void LoadCurrentPlayerScore(string leaderboardId, GK_TimeSpan timeSpan = GK_TimeSpan.ALL_TIME, GK_CollectionType collection = GK_CollectionType.GLOBAL)
	{
		LoadLeaderboardInfo(leaderboardId);
	}

	public static void LoadLeaderboardInfo(string leaderboardId)
	{
	}

	private IEnumerator LoadLeaderboardInfoLocal(string leaderboardId)
	{
		yield return new WaitForSeconds(4f);
	}

	public static void LoadScore(string leaderboardId, int startIndex, int length, GK_TimeSpan timeSpan = GK_TimeSpan.ALL_TIME, GK_CollectionType collection = GK_CollectionType.GLOBAL)
	{
	}

	public static void IssueLeaderboardChallenge(string leaderboardId, string message, string playerId)
	{
	}

	public static void IssueLeaderboardChallenge(string leaderboardId, string message, string[] playerIds)
	{
	}

	public static void IssueLeaderboardChallenge(string leaderboardId, string message)
	{
	}

	public static void IssueAchievementChallenge(string achievementId, string message, string playerId)
	{
	}

	public static void LoadLeaderboardSetInfo()
	{
	}

	public static void LoadLeaderboardsForSet(string setId)
	{
	}

	public static void LoadAchievements()
	{
	}

	public static void IssueAchievementChallenge(string achievementId, string message, string[] playerIds)
	{
	}

	public static void IssueAchievementChallenge(string achievementId, string message)
	{
	}

	public static void ShowAchievements()
	{
	}

	public static void ResetAchievements()
	{
		if (IOSNativeSettings.Instance.UsePPForAchievements)
		{
			ResetStoredProgress();
		}
	}

	public static void SubmitAchievement(float percent, string achievementId)
	{
		SubmitAchievement(percent, achievementId, true);
	}

	public static void SubmitAchievementNoCache(float percent, string achievementId)
	{
	}

	public static void SubmitAchievement(float percent, string achievementId, bool isCompleteNotification)
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			ISN_CacheManager.SaveAchievementRequest(achievementId, percent);
		}
		if (IOSNativeSettings.Instance.UsePPForAchievements)
		{
			SaveAchievementProgress(achievementId, percent);
		}
	}

	public static float GetAchievementProgress(string id)
	{
		float num = 0f;
		if (IOSNativeSettings.Instance.UsePPForAchievements)
		{
			return GetStoredAchievementProgress(id);
		}
		GK_AchievementTemplate achievement = GetAchievement(id);
		return achievement.Progress;
	}

	public static GK_AchievementTemplate GetAchievement(string achievementId)
	{
		foreach (GK_AchievementTemplate achievement in Achievements)
		{
			if (achievement.Id.Equals(achievementId))
			{
				return achievement;
			}
		}
		GK_AchievementTemplate gK_AchievementTemplate = new GK_AchievementTemplate();
		gK_AchievementTemplate.Id = achievementId;
		Achievements.Add(gK_AchievementTemplate);
		return gK_AchievementTemplate;
	}

	public static GK_Leaderboard GetLeaderboard(string id)
	{
		foreach (GK_Leaderboard leaderboard in Leaderboards)
		{
			if (leaderboard.Id.Equals(id))
			{
				return leaderboard;
			}
		}
		GK_Leaderboard gK_Leaderboard = new GK_Leaderboard(id);
		Leaderboards.Add(gK_Leaderboard);
		return gK_Leaderboard;
	}

	public static GK_Player GetPlayerById(string playerID)
	{
		if (_players.ContainsKey(playerID))
		{
			return _players[playerID];
		}
		return null;
	}

	public static void SendFriendRequest(GK_FriendRequest request, List<string> emails, List<string> players)
	{
		_FriendRequests.Add(request.Id, request);
	}

	private void OnLoaderBoardInfoRetrivedFail(string data)
	{
		string[] array = data.Split(new string[1] { "|%|" }, StringSplitOptions.None);
		string id = array[0];
		int requestId = Convert.ToInt32(array[3]);
		string errorData = array[4];
		GK_Leaderboard leaderboard = GetLeaderboard(id);
		leaderboard.ReportLocalPlayerScoreUpdateFail(errorData, requestId);
	}

	private void OnLoaderBoardInfoRetrived(string data)
	{
		string[] array = data.Split(new string[1] { "|%|" }, StringSplitOptions.None);
		string text = array[0];
		GK_TimeSpan vTimeSpan = (GK_TimeSpan)Convert.ToInt32(array[1]);
		GK_CollectionType sCollection = (GK_CollectionType)Convert.ToInt32(array[2]);
		int requestId = Convert.ToInt32(array[3]);
		long vScore = Convert.ToInt64(array[4]);
		int vRank = Convert.ToInt32(array[5]);
		int num = Convert.ToInt32(array[6]);
		int mR = Convert.ToInt32(array[7]);
		string title = array[8];
		string description = array[9];
		GK_Leaderboard leaderboard = GetLeaderboard(text);
		leaderboard.UpdateMaxRange(mR);
		leaderboard.Info.Title = title;
		leaderboard.Info.Description = description;
		GK_Score score = new GK_Score(vScore, vRank, num, vTimeSpan, sCollection, text, Player.Id);
		leaderboard.ReportLocalPlayerScoreUpdate(score, requestId);
	}

	public void onScoreSubmittedEvent(string data)
	{
		string[] array = data.Split(new string[1] { "|%|" }, StringSplitOptions.None);
		string leaderboardId = array[0];
		StartCoroutine(LoadLeaderboardInfoLocal(leaderboardId));
	}

	public void onScoreSubmittedFailed(string data)
	{
		string[] array = data.Split(new string[1] { "|%|" }, StringSplitOptions.None);
		string id = array[0];
		string errorData = array[2];
		GK_Leaderboard leaderboard = GetLeaderboard(id);
		GK_LeaderboardResult obj = new GK_LeaderboardResult(leaderboard, new Error(errorData));
		OnScoreSubmitted__BackingField(obj);
	}

	private void OnLeaderboardScoreListLoaded(string data)
	{
		string[] array = data.Split(new string[1] { "|%|" }, StringSplitOptions.None);
		string text = array[0];
		GK_TimeSpan vTimeSpan = (GK_TimeSpan)Convert.ToInt32(array[1]);
		GK_CollectionType sCollection = (GK_CollectionType)Convert.ToInt32(array[2]);
		GK_Leaderboard leaderboard = GetLeaderboard(text);
		for (int i = 3; i < array.Length; i += 4)
		{
			string text2 = array[i];
			long vScore = Convert.ToInt64(array[i + 1]);
			int vRank = Convert.ToInt32(array[i + 2]);
			int num = Convert.ToInt32(array[i + 3]);
			GK_Score gK_Score = new GK_Score(vScore, vRank, num, vTimeSpan, sCollection, text, text2);
			leaderboard.UpdateScore(gK_Score);
			if (Player != null && Player.Id.Equals(text2))
			{
				leaderboard.UpdateCurrentPlayerScore(gK_Score);
			}
		}
		GK_LeaderboardResult obj = new GK_LeaderboardResult(leaderboard);
		OnScoresListLoaded__BackingField(obj);
	}

	private void OnLeaderboardScoreListLoadFailed(string data)
	{
		string[] array = data.Split(new string[1] { "|%|" }, StringSplitOptions.None);
		string id = array[0];
		string errorData = array[3];
		GK_Leaderboard leaderboard = GetLeaderboard(id);
		GK_LeaderboardResult obj = new GK_LeaderboardResult(leaderboard, new Error(errorData));
		OnScoresListLoaded__BackingField(obj);
	}

	private void onAchievementsReset(string array)
	{
		Result obj = new Result();
		OnAchievementsReset__BackingField(obj);
	}

	private void onAchievementsResetFailed(string errorData)
	{
		Result obj = new Result(new Error(errorData));
		OnAchievementsReset__BackingField(obj);
	}

	private void onAchievementProgressChanged(string array)
	{
		string[] array2 = array.Split('|');
		GK_AchievementTemplate achievement = GetAchievement(array2[0]);
		achievement.Progress = Convert.ToSingle(array2[1]);
		GK_AchievementProgressResult obj = new GK_AchievementProgressResult(achievement);
		SaveLocalProgress(achievement);
		OnAchievementsProgress__BackingField(obj);
	}

	private void onAchievementsLoaded(string array)
	{
		Result obj = new Result();
		if (array.Equals(string.Empty))
		{
			OnAchievementsLoaded__BackingField(obj);
			return;
		}
		string[] array2 = array.Split('|');
		for (int i = 0; i < array2.Length; i += 3)
		{
			GK_AchievementTemplate achievement = GetAchievement(array2[i]);
			achievement.Description = array2[i + 1];
			achievement.Progress = Convert.ToSingle(array2[i + 2]);
			SaveLocalProgress(achievement);
		}
		_IsAchievementsInfoLoaded = true;
		OnAchievementsLoaded__BackingField(obj);
	}

	private void onAchievementsLoadedFailed(string errorData)
	{
		Result obj = new Result(new Error(errorData));
		OnAchievementsLoaded__BackingField(obj);
	}

	private void onAuthenticateLocalPlayer(string array)
	{
		string[] array2 = array.Split('|');
		_player = new GK_Player(array2[0], array2[1], array2[2]);
		ISN_CacheManager.SendAchievementCachedRequest();
		Result obj = ((!IsPlayerAuthenticated) ? new Result(new Error()) : new Result());
		OnAuthFinished__BackingField(obj);
	}

	private void onAuthenticationFailed(string errorData)
	{
		Result obj = new Result(new Error(errorData));
		OnAuthFinished__BackingField(obj);
	}

	private void OnUserInfoLoadedEvent(string array)
	{
		ISN_Logger.Log("OnUserInfoLoadedEvent");
		string[] array2 = array.Split('|');
		string text = array2[0];
		string pAlias = array2[1];
		string pName = array2[2];
		GK_Player gK_Player = new GK_Player(text, pName, pAlias);
		if (_players.ContainsKey(text))
		{
			_players[text] = gK_Player;
		}
		else
		{
			_players.Add(text, gK_Player);
		}
		if (gK_Player.Id == _player.Id)
		{
			_player = gK_Player;
		}
		ISN_Logger.Log("Player Info loaded, for player with id: " + gK_Player.Id);
		GK_UserInfoLoadResult obj = new GK_UserInfoLoadResult(gK_Player);
		OnUserInfoLoaded__BackingField(obj);
	}

	private void OnUserInfoLoadFailedEvent(string playerId)
	{
		GK_UserInfoLoadResult obj = new GK_UserInfoLoadResult(playerId);
		OnUserInfoLoaded__BackingField(obj);
	}

	private void OnUserPhotoLoadedEvent(string array)
	{
		string[] array2 = array.Split('|');
		string playerID = array2[0];
		GK_PhotoSize size = (GK_PhotoSize)Convert.ToInt32(array2[1]);
		string base64String = array2[2];
		GK_Player playerById = GetPlayerById(playerID);
		if (playerById != null)
		{
			playerById.SetPhotoData(size, base64String);
		}
	}

	private void OnUserPhotoLoadFailedEvent(string data)
	{
		string[] array = data.Split(new string[1] { "|%|" }, StringSplitOptions.None);
		string playerID = array[0];
		GK_PhotoSize size = (GK_PhotoSize)Convert.ToInt32(array[1]);
		string errorData = array[2];
		GK_Player playerById = GetPlayerById(playerID);
		if (playerById != null)
		{
			playerById.SetPhotoLoadFailedEventData(size, errorData);
		}
	}

	private void OnFriendListLoadedEvent(string data)
	{
		string[] array = data.Split('|');
		_friendsList.Clear();
		for (int i = 0; i < array.Length; i++)
		{
			_friendsList.Add(array[i]);
		}
		if (!IOSNativeSettings.Instance.DisablePluginLogs)
		{
			ISN_Logger.Log("Friends list loaded, total friends: " + _friendsList.Count);
		}
		Result obj = new Result();
		OnFriendsListLoaded__BackingField(obj);
	}

	private void OnFriendListLoadFailEvent(string errorData)
	{
		Result obj = new Result(new Error(errorData));
		OnFriendsListLoaded__BackingField(obj);
	}

	private void OnGameCenterViewDismissedEvent(string data)
	{
		OnGameCenterViewDismissed__BackingField();
	}

	private void VerificationSignatureRetrieveFailed(string array)
	{
		Error errro = new Error(array);
		GK_PlayerSignatureResult obj = new GK_PlayerSignatureResult(errro);
		OnPlayerSignatureRetrieveResult__BackingField(obj);
	}

	private void VerificationSignatureRetrieved(string array)
	{
		string[] array2 = array.Split('|');
		GK_PlayerSignatureResult obj = new GK_PlayerSignatureResult(array2[0], array2[1], array2[2], array2[3]);
		OnPlayerSignatureRetrieveResult__BackingField(obj);
	}

	private void SaveLocalProgress(GK_AchievementTemplate tpl)
	{
		if (IOSNativeSettings.Instance.UsePPForAchievements)
		{
			SaveAchievementProgress(tpl.Id, tpl.Progress);
		}
	}

	private static void ResetStoredProgress()
	{
		foreach (GK_AchievementTemplate achievement in Achievements)
		{
			PlayerPrefs.DeleteKey("ISN_GameCenterManager" + achievement.Id);
		}
	}

	private static void SaveAchievementProgress(string achievementId, float progress)
	{
		float storedAchievementProgress = GetStoredAchievementProgress(achievementId);
		if (progress > storedAchievementProgress)
		{
			PlayerPrefs.SetFloat("ISN_GameCenterManager" + achievementId, Mathf.Clamp(progress, 0f, 100f));
		}
	}

	private static float GetStoredAchievementProgress(string achievementId)
	{
		float result = 0f;
		if (PlayerPrefs.HasKey("ISN_GameCenterManager" + achievementId))
		{
			result = PlayerPrefs.GetFloat("ISN_GameCenterManager" + achievementId);
		}
		return result;
	}

	private void ISN_OnLBSetsLoaded(string array)
	{
		string[] array2 = array.Split('|');
		for (int i = 0; i + 2 < array2.Length; i += 3)
		{
			GK_LeaderboardSet gK_LeaderboardSet = new GK_LeaderboardSet();
			gK_LeaderboardSet.Title = array2[i];
			gK_LeaderboardSet.Identifier = array2[i + 1];
			gK_LeaderboardSet.GroupIdentifier = array2[i + 2];
			LeaderboardSets.Add(gK_LeaderboardSet);
		}
		Result obj = new Result();
		OnLeaderboardSetsInfoLoaded__BackingField(obj);
	}

	private void ISN_OnLBSetsLoadFailed(string array)
	{
		Result obj = new Result(new Error());
		OnLeaderboardSetsInfoLoaded__BackingField(obj);
	}

	private void ISN_OnLBSetsBoardsLoadFailed(string identifier)
	{
		foreach (GK_LeaderboardSet leaderboardSet in LeaderboardSets)
		{
			if (leaderboardSet.Identifier.Equals(identifier))
			{
				leaderboardSet.SendFailLoadEvent();
				break;
			}
		}
	}

	private void ISN_OnLBSetsBoardsLoaded(string array)
	{
		string[] array2 = array.Split('|');
		string value = array2[0];
		foreach (GK_LeaderboardSet leaderboardSet in LeaderboardSets)
		{
			if (leaderboardSet.Identifier.Equals(value))
			{
				for (int i = 1; i < array2.Length; i += 3)
				{
					GK_LeaderBoardInfo gK_LeaderBoardInfo = new GK_LeaderBoardInfo();
					gK_LeaderBoardInfo.Title = array2[i];
					gK_LeaderBoardInfo.Description = array2[i + 1];
					gK_LeaderBoardInfo.Identifier = array2[i + 2];
					leaderboardSet.AddBoardInfo(gK_LeaderBoardInfo);
				}
				leaderboardSet.SendSuccessLoadEvent();
				break;
			}
		}
	}

	public static void DispatchLeaderboardUpdateEvent(GK_LeaderboardResult result, bool isInternal)
	{
		if (isInternal)
		{
			OnScoreSubmitted__BackingField(result);
		}
		else
		{
			OnLeadrboardInfoLoaded__BackingField(result);
		}
	}

	public static List<GK_TBM_Participant> ParseParticipantsData(string[] data, int index)
	{
		List<GK_TBM_Participant> list = new List<GK_TBM_Participant>();
		for (int i = index; i < data.Length && !(data[i] == "endofline"); i += 5)
		{
			GK_TBM_Participant item = ParseParticipanData(data, i);
			list.Add(item);
		}
		return list;
	}

	public static GK_TBM_Participant ParseParticipanData(string[] data, int index)
	{
		return new GK_TBM_Participant(data[index], data[index + 1], data[index + 2], data[index + 3], data[index + 4]);
	}
}
