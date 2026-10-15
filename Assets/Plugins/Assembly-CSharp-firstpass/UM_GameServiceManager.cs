using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class UM_GameServiceManager : Singleton<UM_GameServiceManager>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnPlayerConnected__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnPlayerDisconnected__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<UM_ConnectionState> OnConnectionStateChnaged__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<UM_LeaderboardResult> ActionScoreSubmitted__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<UM_LeaderboardResult> ActionScoresListLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<UM_Result> ActionFriendsListLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<UM_Result> ActionAchievementsInfoLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<UM_Result> ActionLeaderboardsInfoLoaded__BackingField = delegate
	{
	};

	private bool _IsInitedCalled;

	private bool _IsDataLoaded;

	private int _DataEventsCount;

	private int _CurrentEventsCount;

	private UM_Player _Player;

	private UM_ConnectionState _ConnectionSate;

	private static List<string> _FriendsList = new List<string>();

	private bool _WaitingForLeaderboardsData;

	private int _LeaderboardsDataEventsCount;

	private int _CurrentLeaderboardsEventsCount;

	public List<string> FriendsList
	{
		get
		{
			return _FriendsList;
		}
	}

	public UM_ConnectionState ConnectionSate
	{
		get
		{
			return _ConnectionSate;
		}
	}

	public bool IsConnected
	{
		get
		{
			return ConnectionSate == UM_ConnectionState.CONNECTED;
		}
	}

	[Obsolete("player is deprectaed, plase use Player instead")]
	public UM_Player player
	{
		get
		{
			return _Player;
		}
	}

	public UM_Player Player
	{
		get
		{
			return _Player;
		}
	}

	public static event Action OnPlayerConnected
	{
		add
		{
			Action action = OnPlayerConnected__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPlayerConnected__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnPlayerConnected__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPlayerConnected__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action OnPlayerDisconnected
	{
		add
		{
			Action action = OnPlayerDisconnected__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPlayerDisconnected__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnPlayerDisconnected__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPlayerDisconnected__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<UM_ConnectionState> OnConnectionStateChnaged
	{
		add
		{
			Action<UM_ConnectionState> action = OnConnectionStateChnaged__BackingField;
			Action<UM_ConnectionState> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnConnectionStateChnaged__BackingField, (Action<UM_ConnectionState>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_ConnectionState> action = OnConnectionStateChnaged__BackingField;
			Action<UM_ConnectionState> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnConnectionStateChnaged__BackingField, (Action<UM_ConnectionState>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<UM_LeaderboardResult> ActionScoreSubmitted
	{
		add
		{
			Action<UM_LeaderboardResult> action = ActionScoreSubmitted__BackingField;
			Action<UM_LeaderboardResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionScoreSubmitted__BackingField, (Action<UM_LeaderboardResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_LeaderboardResult> action = ActionScoreSubmitted__BackingField;
			Action<UM_LeaderboardResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionScoreSubmitted__BackingField, (Action<UM_LeaderboardResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<UM_LeaderboardResult> ActionScoresListLoaded
	{
		add
		{
			Action<UM_LeaderboardResult> action = ActionScoresListLoaded__BackingField;
			Action<UM_LeaderboardResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionScoresListLoaded__BackingField, (Action<UM_LeaderboardResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_LeaderboardResult> action = ActionScoresListLoaded__BackingField;
			Action<UM_LeaderboardResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionScoresListLoaded__BackingField, (Action<UM_LeaderboardResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<UM_Result> ActionFriendsListLoaded
	{
		add
		{
			Action<UM_Result> action = ActionFriendsListLoaded__BackingField;
			Action<UM_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionFriendsListLoaded__BackingField, (Action<UM_Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_Result> action = ActionFriendsListLoaded__BackingField;
			Action<UM_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionFriendsListLoaded__BackingField, (Action<UM_Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<UM_Result> ActionAchievementsInfoLoaded
	{
		add
		{
			Action<UM_Result> action = ActionAchievementsInfoLoaded__BackingField;
			Action<UM_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionAchievementsInfoLoaded__BackingField, (Action<UM_Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_Result> action = ActionAchievementsInfoLoaded__BackingField;
			Action<UM_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionAchievementsInfoLoaded__BackingField, (Action<UM_Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<UM_Result> ActionLeaderboardsInfoLoaded
	{
		add
		{
			Action<UM_Result> action = ActionLeaderboardsInfoLoaded__BackingField;
			Action<UM_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionLeaderboardsInfoLoaded__BackingField, (Action<UM_Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_Result> action = ActionLeaderboardsInfoLoaded__BackingField;
			Action<UM_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionLeaderboardsInfoLoaded__BackingField, (Action<UM_Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Init()
	{
		_IsInitedCalled = true;
		_DataEventsCount = 0;
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			if (UltimateMobileSettings.Instance.AutoLoadAchievementsInfo)
			{
				_DataEventsCount++;
				GameCenterManager.OnAchievementsLoaded += OnGameCenterServiceDataLoaded;
			}
			if (UltimateMobileSettings.Instance.AutoLoadLeaderboardsInfo)
			{
				_DataEventsCount += UltimateMobileSettings.Instance.Leaderboards.Count;
			}
			GameCenterManager.OnLeadrboardInfoLoaded += OnGameCenterServiceLeaderDataLoaded;
			foreach (UM_Achievement achievement in UltimateMobileSettings.Instance.Achievements)
			{
				GameCenterManager.RegisterAchievement(achievement.IOSId);
			}
			GameCenterManager.OnAuthFinished += OnAuthFinished;
			GameCenterManager.OnScoreSubmitted += IOS_HandleOnScoreSubmitted;
			GameCenterManager.OnScoresListLoaded += IOS_HandleOnScoresListLoaded;
			GameCenterManager.OnFriendsListLoaded += IOS_OnFriendsListLoaded;
			GameCenterManager.OnAchievementsLoaded += IOS_AchievementsDataLoaded;
			GameCenterManager.OnLeadrboardInfoLoaded += IOS_LeaderboardsDataLoaded;
			break;
		case RuntimePlatform.Android:
			if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
			{
				_DataEventsCount++;
				if (UltimateMobileSettings.Instance.AutoLoadAchievementsInfo)
				{
					_DataEventsCount++;
					AMN_Singleton<SA_AmazonGameCircleManager>.Instance.OnRequestAchievementsReceived += OnAmazonGameCircleRequestAchievementsReceived;
				}
				if (UltimateMobileSettings.Instance.AutoLoadLeaderboardsInfo)
				{
					_DataEventsCount += UltimateMobileSettings.Instance.Leaderboards.Count * 3 + 1;
					AMN_Singleton<SA_AmazonGameCircleManager>.Instance.OnRequestLeaderboardsReceived += OnAmazonGameCircleRequestLeaderboardsReceived;
				}
				AMN_Singleton<SA_AmazonGameCircleManager>.Instance.OnInitializeResult += OnAmazonInitializeResult;
				AMN_Singleton<SA_AmazonGameCircleManager>.Instance.OnRequestPlayerDataReceived += OnAmazonRequestPlayerDataReceived;
				AMN_Singleton<SA_AmazonGameCircleManager>.Instance.OnRequestAchievementsReceived += OnAmazonRequestAchievementsReceived;
				AMN_Singleton<SA_AmazonGameCircleManager>.Instance.OnRequestLeaderboardsReceived += OnAmazonRequestLeaderboardsReceived;
				AMN_Singleton<SA_AmazonGameCircleManager>.Instance.OnSubmitLeaderboardReceived += OnAmazonSubmitLeaderboardReceived;
				AMN_Singleton<SA_AmazonGameCircleManager>.Instance.OnScoresLoaded += OnAmazonScoresLoaded;
			}
			else
			{
				if (UltimateMobileSettings.Instance.AutoLoadAchievementsInfo)
				{
					_DataEventsCount++;
					GooglePlayManager.ActionAchievementsLoaded += OnGooglePlayServiceDataLoaded;
				}
				if (UltimateMobileSettings.Instance.AutoLoadLeaderboardsInfo)
				{
					_DataEventsCount++;
				}
				GooglePlayManager.ActionLeaderboardsLoaded += OnGooglePlayLeaderDataLoaded;
				GooglePlayConnection.ActionPlayerConnected += OnAndroidPlayerConnected;
				GooglePlayConnection.ActionPlayerDisconnected += OnAndroidPlayerDisconnected;
				GooglePlayManager.ActionScoreSubmited += Android_HandleActionScoreSubmited;
				GooglePlayManager.ActionScoresListLoaded += Android_HandleActionScoresListLoaded;
				GooglePlayManager.ActionFriendsListLoaded += Android_ActionFriendsListLoaded;
				GooglePlayManager.ActionAchievementsLoaded += Android_AchievementsDataLoaded;
				GooglePlayManager.ActionLeaderboardsLoaded += Android_LeaderboardsDataLoaded;
			}
			break;
		}
	}

	public void Connect()
	{
		if (!_IsInitedCalled)
		{
			Init();
		}
		if (_ConnectionSate == UM_ConnectionState.CONNECTED || _ConnectionSate == UM_ConnectionState.CONNECTING)
		{
			return;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.Init();
			break;
		case RuntimePlatform.Android:
			if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
			{
				AMN_Singleton<SA_AmazonGameCircleManager>.Instance.Connect();
			}
			else
			{
				Singleton<GooglePlayConnection>.Instance.Connect();
			}
			break;
		}
		SetConnectionState(UM_ConnectionState.CONNECTING);
	}

	public void Disconnect()
	{
		RuntimePlatform platform = Application.platform;
		if (platform != RuntimePlatform.IPhonePlayer && platform == RuntimePlatform.Android)
		{
			if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
			{
				AMN_Singleton<SA_AmazonGameCircleManager>.Instance.Disconnect();
			}
			else
			{
				Singleton<GooglePlayConnection>.Instance.Disconnect();
			}
		}
	}

	public void LoadFriends()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.RetrieveFriends();
			break;
		case RuntimePlatform.Android:
			if (UltimateMobileSettings.Instance.PlatformEngine != UM_PlatformDependencies.Amazon)
			{
				Singleton<GooglePlayManager>.Instance.LoadFriends();
			}
			break;
		}
	}

	public bool IsParticipantFriend(UM_TBM_Participant playerParticipant)
	{
		return FriendsList.Contains(playerParticipant.Playerid);
	}

	public UM_Player GetPlayer(string playerId)
	{
		UM_Player result = null;
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
		{
			GK_Player playerById3 = GameCenterManager.GetPlayerById(playerId);
			if (playerById3 != null)
			{
				result = new UM_Player(playerById3, null, null);
			}
			break;
		}
		case RuntimePlatform.Android:
			if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
			{
				GC_Player playerById = AMN_Singleton<SA_AmazonGameCircleManager>.Instance.GetPlayerById(playerId);
				if (playerById != null)
				{
					result = new UM_Player(null, null, playerById);
				}
			}
			else
			{
				GooglePlayerTemplate playerById2 = Singleton<GooglePlayManager>.Instance.GetPlayerById(playerId);
				if (playerById2 != null)
				{
					result = new UM_Player(null, playerById2, null);
				}
			}
			break;
		}
		return result;
	}

	public void LoadAchievementsInfo()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.LoadAchievements();
			break;
		case RuntimePlatform.Android:
			if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
			{
				AMN_Singleton<SA_AmazonGameCircleManager>.Instance.RequestAchievements();
			}
			else
			{
				Singleton<GooglePlayManager>.Instance.LoadAchievements();
			}
			break;
		}
	}

	public void ShowAchievementsUI()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.ShowAchievements();
			break;
		case RuntimePlatform.Android:
			if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
			{
				AMN_Singleton<SA_AmazonGameCircleManager>.Instance.ShowAchievementsOverlay();
			}
			else
			{
				Singleton<GooglePlayManager>.Instance.ShowAchievementsUI();
			}
			break;
		}
	}

	public void RevealAchievement(string id)
	{
		RevealAchievement(UltimateMobileSettings.Instance.GetAchievementById(id));
	}

	public void RevealAchievement(UM_Achievement achievement)
	{
		RuntimePlatform platform = Application.platform;
		if (platform == RuntimePlatform.Android && UltimateMobileSettings.Instance.PlatformEngine != UM_PlatformDependencies.Amazon)
		{
			Singleton<GooglePlayManager>.Instance.RevealAchievementById(achievement.AndroidId);
		}
	}

	[Obsolete("ReportAchievement is deprecated, please use UnlockAchievement instead.")]
	public void ReportAchievement(string id)
	{
		UnlockAchievement(id);
	}

	[Obsolete("ReportAchievement is deprecated, please use UnlockAchievement instead.")]
	public void ReportAchievement(UM_Achievement achievement)
	{
		ReportAchievement(achievement);
	}

	public void UnlockAchievement(string id)
	{
		UM_Achievement achievementById = UltimateMobileSettings.Instance.GetAchievementById(id);
		if (achievementById == null)
		{
			UnityEngine.Debug.LogError("Achievment not found with id: " + id);
		}
		else
		{
			UnlockAchievement(achievementById);
		}
	}

	private void UnlockAchievement(UM_Achievement achievement)
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.SubmitAchievement(100f, achievement.IOSId);
			break;
		case RuntimePlatform.Android:
			if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
			{
				AMN_Singleton<SA_AmazonGameCircleManager>.Instance.UpdateAchievementProgress(achievement.AmazonId, 100f);
			}
			else
			{
				Singleton<GooglePlayManager>.Instance.UnlockAchievementById(achievement.AndroidId);
			}
			break;
		}
	}

	public void IncrementAchievement(string id, float percentages)
	{
		UM_Achievement achievementById = UltimateMobileSettings.Instance.GetAchievementById(id);
		if (achievementById == null)
		{
			UnityEngine.Debug.LogError("Achievment not found with id: " + id);
		}
		else
		{
			IncrementAchievement(achievementById, percentages);
		}
	}

	public void IncrementAchievement(UM_Achievement achievement, float percentages)
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.SubmitAchievement(percentages, achievement.IOSId);
			break;
		case RuntimePlatform.Android:
		{
			if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
			{
				AMN_Singleton<SA_AmazonGameCircleManager>.Instance.UpdateAchievementProgress(achievement.AmazonId, percentages);
				break;
			}
			GPAchievement achievement2 = Singleton<GooglePlayManager>.Instance.GetAchievement(achievement.AndroidId);
			if (achievement2 != null)
			{
				if (achievement2.Type == GPAchievementType.TYPE_INCREMENTAL)
				{
					int numsteps = Mathf.CeilToInt((float)achievement2.TotalSteps / 100f * percentages);
					Singleton<GooglePlayManager>.Instance.IncrementAchievementById(achievement.AndroidId, numsteps);
				}
				else
				{
					Singleton<GooglePlayManager>.Instance.UnlockAchievementById(achievement.AndroidId);
				}
			}
			break;
		}
		}
	}

	public void IncrementAchievementByCurrentSteps(string id, int steps)
	{
		UM_Achievement achievementById = UltimateMobileSettings.Instance.GetAchievementById(id);
		if (achievementById == null)
		{
			UnityEngine.Debug.LogError("Achievement not found with id: " + id);
		}
		else
		{
			IncrementAchievementByCurrentSteps(achievementById, steps);
		}
	}

	public void IncrementAchievementByCurrentSteps(UM_Achievement achievement, int steps)
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
		{
			float percent = (float)steps / (float)achievement.Steps * 100f;
			GameCenterManager.SubmitAchievement(percent, achievement.IOSId);
			break;
		}
		case RuntimePlatform.Android:
		{
			if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
			{
				float score = (float)steps / (float)achievement.Steps * 100f;
				AMN_Singleton<SA_AmazonGameCircleManager>.Instance.UpdateAchievementProgress(achievement.AmazonId, score);
				break;
			}
			GPAchievement achievement2 = Singleton<GooglePlayManager>.Instance.GetAchievement(achievement.AndroidId);
			if (achievement2 != null)
			{
				if (achievement2.Type == GPAchievementType.TYPE_INCREMENTAL)
				{
					Singleton<GooglePlayManager>.Instance.IncrementAchievementById(achievement2.Id, steps - achievement2.CurrentSteps);
				}
				else
				{
					Singleton<GooglePlayManager>.Instance.UnlockAchievementById(achievement2.Id);
				}
			}
			break;
		}
		}
	}

	public void ResetAchievements()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.ResetAchievements();
			break;
		case RuntimePlatform.Android:
			if (UltimateMobileSettings.Instance.PlatformEngine != UM_PlatformDependencies.Amazon)
			{
				Singleton<GooglePlayManager>.Instance.ResetAllAchievements();
			}
			break;
		}
	}

	public float GetAchievementProgress(string id)
	{
		UM_Achievement achievementById = UltimateMobileSettings.Instance.GetAchievementById(id);
		if (achievementById == null)
		{
			UnityEngine.Debug.LogError("Achievment not found with id: " + id);
			return 0f;
		}
		return GetAchievementProgress(achievementById);
	}

	public float GetAchievementProgress(UM_Achievement achievement)
	{
		if (achievement == null)
		{
			return 0f;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			return GameCenterManager.GetAchievementProgress(achievement.IOSId);
		case RuntimePlatform.Android:
		{
			if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
			{
				GC_Achievement achievement2 = AMN_Singleton<SA_AmazonGameCircleManager>.Instance.GetAchievement(achievement.AmazonId);
				if (achievement2 != null)
				{
					return achievement2.Progress;
				}
				break;
			}
			GPAchievement achievement3 = Singleton<GooglePlayManager>.Instance.GetAchievement(achievement.AndroidId);
			if (achievement3 == null)
			{
				break;
			}
			if (achievement3.Type == GPAchievementType.TYPE_INCREMENTAL)
			{
				return (float)achievement3.CurrentSteps / (float)achievement3.TotalSteps * 100f;
			}
			if (achievement3.State == GPAchievementState.STATE_UNLOCKED)
			{
				return 100f;
			}
			return 0f;
		}
		}
		return 0f;
	}

	public int GetAchievementProgressInSteps(string id)
	{
		UM_Achievement achievementById = UltimateMobileSettings.Instance.GetAchievementById(id);
		if (achievementById == null)
		{
			UnityEngine.Debug.LogError("Achievement not found with id: " + id);
			return 0;
		}
		return GetAchievementProgressInSteps(achievementById);
	}

	public int GetAchievementProgressInSteps(UM_Achievement achievement)
	{
		if (achievement == null)
		{
			UnityEngine.Debug.LogError("Achievement is null. No progress can be retrieved.");
			return 0;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
		{
			float achievementProgress = GameCenterManager.GetAchievementProgress(achievement.IOSId);
			return Mathf.CeilToInt((float)achievement.Steps / 100f * achievementProgress);
		}
		case RuntimePlatform.Android:
		{
			if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
			{
				GC_Achievement achievement2 = AMN_Singleton<SA_AmazonGameCircleManager>.Instance.GetAchievement(achievement.AmazonId);
				if (achievement2 != null)
				{
					return Mathf.CeilToInt((float)achievement.Steps / 100f * achievement2.Progress);
				}
				break;
			}
			GPAchievement achievement3 = Singleton<GooglePlayManager>.Instance.GetAchievement(achievement.AndroidId);
			if (achievement3 == null)
			{
				break;
			}
			if (achievement3.Type == GPAchievementType.TYPE_INCREMENTAL)
			{
				return achievement3.CurrentSteps;
			}
			return (achievement3.State == GPAchievementState.STATE_UNLOCKED) ? 1 : 0;
		}
		}
		return 0;
	}

	public void LoadLeaderboardsInfo()
	{
		if (_WaitingForLeaderboardsData)
		{
			return;
		}
		_WaitingForLeaderboardsData = true;
		_LeaderboardsDataEventsCount = 0;
		_CurrentLeaderboardsEventsCount = 0;
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			_LeaderboardsDataEventsCount = UltimateMobileSettings.Instance.Leaderboards.Count;
			{
				foreach (UM_Leaderboard leaderboard in UltimateMobileSettings.Instance.Leaderboards)
				{
					GameCenterManager.LoadLeaderboardInfo(leaderboard.IOSId);
				}
				break;
			}
		case RuntimePlatform.Android:
			if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
			{
				AMN_Singleton<SA_AmazonGameCircleManager>.Instance.RequestLeaderboards();
			}
			else
			{
				Singleton<GooglePlayManager>.Instance.LoadLeaderBoards();
			}
			break;
		}
	}

	public UM_Leaderboard GetLeaderboard(string leaderboardId)
	{
		return UltimateMobileSettings.Instance.GetLeaderboardById(leaderboardId);
	}

	public void ShowLeaderBoardsUI()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.ShowLeaderboards();
			break;
		case RuntimePlatform.Android:
			if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
			{
				AMN_Singleton<SA_AmazonGameCircleManager>.Instance.ShowLeaderboardsOverlay();
			}
			else
			{
				Singleton<GooglePlayManager>.Instance.ShowLeaderBoardsUI();
			}
			break;
		}
	}

	public void ShowLeaderBoardUI(string id)
	{
		ShowLeaderBoardUI(UltimateMobileSettings.Instance.GetLeaderboardById(id));
	}

	public void ShowLeaderBoardUI(UM_Leaderboard leaderboard)
	{
		if (leaderboard == null)
		{
			return;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.ShowLeaderboard(leaderboard.IOSId);
			break;
		case RuntimePlatform.Android:
			if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
			{
				AMN_Singleton<SA_AmazonGameCircleManager>.Instance.ShowLeaderboardsOverlay();
			}
			else
			{
				Singleton<GooglePlayManager>.Instance.ShowLeaderBoardById(leaderboard.AndroidId);
			}
			break;
		}
	}

	public void SubmitScore(string LeaderboardId, long score, long context = 0L)
	{
		SubmitScore(UltimateMobileSettings.Instance.GetLeaderboardById(LeaderboardId), score, 0L);
	}

	public void SubmitScore(UM_Leaderboard leaderboard, long score, long context = 0L)
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.ReportScore(score, leaderboard.IOSId, context);
			break;
		case RuntimePlatform.Android:
			if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
			{
				AMN_Singleton<SA_AmazonGameCircleManager>.Instance.SubmitLeaderBoardProgress(leaderboard.AmazonId, score);
			}
			else
			{
				Singleton<GooglePlayManager>.Instance.SubmitScoreById(leaderboard.AndroidId, score, context.ToString());
			}
			break;
		}
	}

	public UM_Score GetCurrentPlayerScore(string leaderBoardId, UM_TimeSpan timeSpan = UM_TimeSpan.ALL_TIME, UM_CollectionType collection = UM_CollectionType.GLOBAL)
	{
		return GetCurrentPlayerScore(UltimateMobileSettings.Instance.GetLeaderboardById(leaderBoardId), timeSpan, collection);
	}

	public UM_Score GetCurrentPlayerScore(UM_Leaderboard leaderboard, UM_TimeSpan timeSpan = UM_TimeSpan.ALL_TIME, UM_CollectionType collection = UM_CollectionType.GLOBAL)
	{
		if (leaderboard != null)
		{
			return leaderboard.GetCurrentPlayerScore(timeSpan, collection);
		}
		return null;
	}

	public void LoadPlayerCenteredScores(string leaderboardId, int maxResults, UM_TimeSpan timeSpan = UM_TimeSpan.ALL_TIME, UM_CollectionType collection = UM_CollectionType.GLOBAL)
	{
		UM_Leaderboard leaderboardById = UltimateMobileSettings.Instance.GetLeaderboardById(leaderboardId);
		LoadPlayerCenteredScores(leaderboardById, maxResults, timeSpan, collection);
	}

	public void LoadPlayerCenteredScores(UM_Leaderboard leaderboard, int maxResults, UM_TimeSpan timeSpan = UM_TimeSpan.ALL_TIME, UM_CollectionType collection = UM_CollectionType.GLOBAL)
	{
		if (leaderboard == null)
		{
			return;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
		{
			UM_Score currentPlayerScore = GetCurrentPlayerScore(leaderboard, timeSpan, collection);
			int num = 0;
			if (currentPlayerScore != null)
			{
				num = currentPlayerScore.Rank;
			}
			int num2 = Math.Max(1, num - maxResults / 2);
			int length = num2 + maxResults - 1;
			GameCenterManager.LoadScore(leaderboard.IOSId, num2, length, timeSpan.Get_GK_TimeSpan(), collection.Get_GK_CollectionType());
			break;
		}
		case RuntimePlatform.Android:
			if (UltimateMobileSettings.Instance.PlatformEngine != UM_PlatformDependencies.Amazon)
			{
				Singleton<GooglePlayManager>.Instance.LoadPlayerCenteredScores(leaderboard.AndroidId, timeSpan.Get_GP_TimeSpan(), collection.Get_GP_CollectionType(), maxResults);
			}
			break;
		}
	}

	public void LoadTopScores(string leaderboardId, int maxResults, UM_TimeSpan timeSpan = UM_TimeSpan.ALL_TIME, UM_CollectionType collection = UM_CollectionType.GLOBAL)
	{
		UM_Leaderboard leaderboardById = UltimateMobileSettings.Instance.GetLeaderboardById(leaderboardId);
		LoadTopScores(leaderboardById, maxResults, timeSpan, collection);
	}

	public void LoadTopScores(UM_Leaderboard leaderboard, int maxResults, UM_TimeSpan timeSpan = UM_TimeSpan.ALL_TIME, UM_CollectionType collection = UM_CollectionType.GLOBAL)
	{
		if (leaderboard == null)
		{
			return;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.LoadScore(leaderboard.IOSId, 1, maxResults, timeSpan.Get_GK_TimeSpan(), collection.Get_GK_CollectionType());
			break;
		case RuntimePlatform.Android:
			if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
			{
				AMN_Singleton<SA_AmazonGameCircleManager>.Instance.LoadTopScores(leaderboard.AmazonId, timeSpan.Get_GC_TimeSpan());
			}
			else
			{
				Singleton<GooglePlayManager>.Instance.LoadTopScores(leaderboard.AndroidId, timeSpan.Get_GP_TimeSpan(), collection.Get_GP_CollectionType(), maxResults);
			}
			break;
		}
	}

	private void OnServiceConnected()
	{
		if (_IsDataLoaded || _DataEventsCount <= 0)
		{
			_IsDataLoaded = true;
			OnAllLoaded();
			return;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			if (UltimateMobileSettings.Instance.AutoLoadAchievementsInfo)
			{
				GameCenterManager.LoadAchievements();
			}
			if (!UltimateMobileSettings.Instance.AutoLoadLeaderboardsInfo)
			{
				break;
			}
			{
				foreach (UM_Leaderboard leaderboard in UltimateMobileSettings.Instance.Leaderboards)
				{
					GameCenterManager.LoadLeaderboardInfo(leaderboard.IOSId);
				}
				break;
			}
		case RuntimePlatform.Android:
			if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
			{
				UnityEngine.Debug.Log("Start To Load Amazon Player");
				AMN_Singleton<SA_AmazonGameCircleManager>.Instance.RetrieveLocalPlayer();
				if (UltimateMobileSettings.Instance.AutoLoadAchievementsInfo)
				{
					AMN_Singleton<SA_AmazonGameCircleManager>.Instance.RequestAchievements();
				}
				if (UltimateMobileSettings.Instance.AutoLoadLeaderboardsInfo)
				{
					AMN_Singleton<SA_AmazonGameCircleManager>.Instance.RequestLeaderboards();
				}
			}
			else
			{
				if (UltimateMobileSettings.Instance.AutoLoadAchievementsInfo)
				{
					Singleton<GooglePlayManager>.Instance.LoadAchievements();
				}
				if (UltimateMobileSettings.Instance.AutoLoadLeaderboardsInfo)
				{
					Singleton<GooglePlayManager>.Instance.LoadLeaderBoards();
				}
			}
			break;
		}
	}

	private void OnGooglePlayServiceDataLoaded(GooglePlayResult result)
	{
		_CurrentEventsCount++;
		if (_CurrentEventsCount == _DataEventsCount)
		{
			OnAllLoaded();
		}
	}

	private void OnGooglePlayLeaderDataLoaded(GooglePlayResult res)
	{
		foreach (GPLeaderBoard leaderBoard in Singleton<GooglePlayManager>.Instance.LeaderBoards)
		{
			UM_Leaderboard leaderboardByAndroidId = UltimateMobileSettings.Instance.GetLeaderboardByAndroidId(leaderBoard.Id);
			if (leaderboardByAndroidId != null)
			{
				leaderboardByAndroidId.Setup(leaderBoard);
			}
		}
		OnGooglePlayServiceDataLoaded(res);
	}

	private void OnGameCenterServiceDataLoaded(Result e)
	{
		_CurrentEventsCount++;
		if (_CurrentEventsCount == _DataEventsCount)
		{
			OnAllLoaded();
		}
	}

	private void OnGameCenterServiceLeaderDataLoaded(GK_LeaderboardResult res)
	{
		if (res.IsSucceeded && res.Leaderboard != null)
		{
			UM_Leaderboard leaderboardByIOSId = UltimateMobileSettings.Instance.GetLeaderboardByIOSId(res.Leaderboard.Id);
			if (leaderboardByIOSId != null)
			{
				leaderboardByIOSId.Setup(res.Leaderboard);
			}
		}
		OnGameCenterServiceDataLoaded(res);
	}

	private void OnAllLoaded()
	{
		UnityEngine.Debug.Log("All Data Loaded! We are ready to GO!");
		_IsDataLoaded = true;
		_Player = new UM_Player(GameCenterManager.Player, Singleton<GooglePlayManager>.Instance.player, AMN_Singleton<SA_AmazonGameCircleManager>.Instance.Player);
		SetConnectionState(UM_ConnectionState.CONNECTED);
		OnPlayerConnected__BackingField();
	}

	private void OnAchievementsDataLoaded(UM_Result result)
	{
		ActionAchievementsInfoLoaded__BackingField(result);
	}

	private void OnLeaderboardsDataLoaded(UM_Result result)
	{
		_WaitingForLeaderboardsData = false;
		ActionLeaderboardsInfoLoaded__BackingField(result);
	}

	private void OnAuthFinished(Result res)
	{
		if (res.IsSucceeded)
		{
			OnServiceConnected();
			return;
		}
		SetConnectionState(UM_ConnectionState.DISCONNECTED);
		OnPlayerDisconnected__BackingField();
	}

	private void IOS_HandleOnScoreSubmitted(GK_LeaderboardResult res)
	{
		UM_Leaderboard leaderboardByIOSId = UltimateMobileSettings.Instance.GetLeaderboardByIOSId(res.Leaderboard.Id);
		if (leaderboardByIOSId != null)
		{
			leaderboardByIOSId.Setup(res.Leaderboard);
			UM_LeaderboardResult obj = new UM_LeaderboardResult(leaderboardByIOSId, res);
			ActionScoreSubmitted__BackingField(obj);
		}
	}

	private void IOS_HandleOnScoresListLoaded(GK_LeaderboardResult res)
	{
		UM_Leaderboard leaderboardByIOSId = UltimateMobileSettings.Instance.GetLeaderboardByIOSId(res.Leaderboard.Id);
		if (leaderboardByIOSId != null)
		{
			leaderboardByIOSId.Setup(res.Leaderboard);
			UM_LeaderboardResult obj = new UM_LeaderboardResult(leaderboardByIOSId, res);
			ActionScoresListLoaded__BackingField(obj);
		}
	}

	private void IOS_OnFriendsListLoaded(Result res)
	{
		SetFriendList(GameCenterManager.FriendsList);
		ActionFriendsListLoaded__BackingField(new UM_Result(res));
	}

	private void IOS_AchievementsDataLoaded(Result res)
	{
		UM_Result result = new UM_Result(res);
		OnAchievementsDataLoaded(result);
	}

	private void IOS_LeaderboardsDataLoaded(GK_LeaderboardResult res)
	{
		if (_WaitingForLeaderboardsData)
		{
			_CurrentLeaderboardsEventsCount++;
			if (_CurrentLeaderboardsEventsCount >= _LeaderboardsDataEventsCount)
			{
				UM_Result result = new UM_Result();
				OnLeaderboardsDataLoaded(result);
			}
		}
	}

	private void OnAndroidPlayerConnected()
	{
		OnServiceConnected();
	}

	private void OnAndroidPlayerDisconnected()
	{
		SetConnectionState(UM_ConnectionState.DISCONNECTED);
		OnPlayerDisconnected__BackingField();
	}

	private void Android_HandleActionScoresListLoaded(GP_LeaderboardResult res)
	{
		UM_Leaderboard leaderboardByAndroidId = UltimateMobileSettings.Instance.GetLeaderboardByAndroidId(res.Leaderboard.Id);
		if (leaderboardByAndroidId != null)
		{
			leaderboardByAndroidId.Setup(res.Leaderboard);
			UM_LeaderboardResult obj = new UM_LeaderboardResult(leaderboardByAndroidId, res);
			ActionScoresListLoaded__BackingField(obj);
		}
	}

	private void Android_HandleActionScoreSubmited(GP_LeaderboardResult res)
	{
		UM_Leaderboard leaderboardByAndroidId = UltimateMobileSettings.Instance.GetLeaderboardByAndroidId(res.Leaderboard.Id);
		if (leaderboardByAndroidId != null)
		{
			leaderboardByAndroidId.Setup(res.Leaderboard);
			UM_LeaderboardResult obj = new UM_LeaderboardResult(leaderboardByAndroidId, res);
			ActionScoreSubmitted__BackingField(obj);
		}
	}

	private void Android_ActionFriendsListLoaded(GooglePlayResult res)
	{
		SetFriendList(Singleton<GooglePlayManager>.Instance.friendsList);
		ActionFriendsListLoaded__BackingField(new UM_Result(res));
	}

	private void Android_AchievementsDataLoaded(GooglePlayResult res)
	{
		UM_Result result = new UM_Result(res);
		OnAchievementsDataLoaded(result);
	}

	private void Android_LeaderboardsDataLoaded(GooglePlayResult res)
	{
		UM_Result result = new UM_Result(res);
		OnLeaderboardsDataLoaded(result);
	}

	private void OnAmazonInitializeResult(AMN_InitializeResult result)
	{
		if (result.isSuccess)
		{
			OnServiceConnected();
			return;
		}
		SetConnectionState(UM_ConnectionState.DISCONNECTED);
		OnPlayerDisconnected__BackingField();
	}

	private void OnAmazonRequestPlayerDataReceived(AMN_RequestPlayerDataResult result)
	{
		_CurrentEventsCount++;
		if (_CurrentEventsCount == _DataEventsCount)
		{
			OnAllLoaded();
		}
	}

	private void OnAmazonGameCircleRequestLeaderboardsReceived(AMN_RequestLeaderboardsResult result)
	{
		AMN_Singleton<SA_AmazonGameCircleManager>.Instance.OnRequestLeaderboardsReceived -= OnAmazonGameCircleRequestLeaderboardsReceived;
		_CurrentEventsCount++;
		if (_CurrentEventsCount == _DataEventsCount)
		{
			OnAllLoaded();
			return;
		}
		AMN_Singleton<SA_AmazonGameCircleManager>.Instance.OnLocalPlayerScoreLoaded += OnAmazonLocalPlayerScoreLoaded;
		foreach (UM_Leaderboard leaderboard in UltimateMobileSettings.Instance.Leaderboards)
		{
			AMN_Singleton<SA_AmazonGameCircleManager>.Instance.LoadLocalPlayerScores(leaderboard.AmazonId, GC_ScoreTimeSpan.ALL_TIME);
			AMN_Singleton<SA_AmazonGameCircleManager>.Instance.LoadLocalPlayerScores(leaderboard.AmazonId, GC_ScoreTimeSpan.WEEK);
			AMN_Singleton<SA_AmazonGameCircleManager>.Instance.LoadLocalPlayerScores(leaderboard.AmazonId, GC_ScoreTimeSpan.TODAY);
		}
	}

	private void OnAmazonLocalPlayerScoreLoaded(AMN_LocalPlayerScoreLoadedResult result)
	{
		UnityEngine.Debug.Log(string.Format("[OnAmazonLocalPlayerScoreLoaded] {0}|{1}|{2}|{3}", result.LeaderboardId, result.TimeSpan.ToString(), result.Rank, result.Score));
		_CurrentEventsCount++;
		if (_CurrentEventsCount == _DataEventsCount)
		{
			AMN_Singleton<SA_AmazonGameCircleManager>.Instance.OnLocalPlayerScoreLoaded -= OnAmazonLocalPlayerScoreLoaded;
			OnAllLoaded();
		}
	}

	private void OnAmazonGameCircleRequestAchievementsReceived(AMN_RequestAchievementsResult result)
	{
		AMN_Singleton<SA_AmazonGameCircleManager>.Instance.OnRequestAchievementsReceived -= OnAmazonGameCircleRequestAchievementsReceived;
		_CurrentEventsCount++;
		if (_CurrentEventsCount == _DataEventsCount)
		{
			OnAllLoaded();
		}
	}

	private void OnAmazonRequestAchievementsReceived(AMN_RequestAchievementsResult result)
	{
		UM_Result result2 = new UM_Result(result);
		OnAchievementsDataLoaded(result2);
	}

	private void OnAmazonRequestLeaderboardsReceived(AMN_RequestLeaderboardsResult result)
	{
		UM_Result result2 = new UM_Result(result);
		OnLeaderboardsDataLoaded(result2);
	}

	private void OnAmazonSubmitLeaderboardReceived(AMN_SubmitLeaderboardResult result)
	{
		UM_Leaderboard leaderboardByAmazonId = UltimateMobileSettings.Instance.GetLeaderboardByAmazonId(result.LeaderboardID);
		if (leaderboardByAmazonId != null)
		{
			leaderboardByAmazonId.Setup(AMN_Singleton<SA_AmazonGameCircleManager>.Instance.GetLeaderboard(result.LeaderboardID));
			UM_LeaderboardResult obj = new UM_LeaderboardResult(leaderboardByAmazonId, result);
			ActionScoreSubmitted__BackingField(obj);
		}
	}

	private void OnAmazonScoresLoaded(AMN_ScoresLoadedResult result)
	{
		UM_Leaderboard leaderboardByAmazonId = UltimateMobileSettings.Instance.GetLeaderboardByAmazonId(result.LeaderboardId);
		if (leaderboardByAmazonId != null)
		{
			leaderboardByAmazonId.Setup(AMN_Singleton<SA_AmazonGameCircleManager>.Instance.GetLeaderboard(result.LeaderboardId));
			UM_LeaderboardResult obj = new UM_LeaderboardResult(leaderboardByAmazonId, result);
			ActionScoresListLoaded__BackingField(obj);
		}
	}

	private void SetConnectionState(UM_ConnectionState NewState)
	{
		if (_ConnectionSate != NewState)
		{
			_ConnectionSate = NewState;
			OnConnectionStateChnaged__BackingField(_ConnectionSate);
		}
	}

	private void SetFriendList(List<string> friendsIds)
	{
		_FriendsList.Clear();
		foreach (string friendsId in friendsIds)
		{
			if (!friendsId.Equals(Player.PlayerId))
			{
				_FriendsList.Add(friendsId);
			}
		}
	}
}
