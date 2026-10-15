using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class SA_AmazonGameCircleManager : AMN_Singleton<SA_AmazonGameCircleManager>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AMN_InitializeResult> OnInitializeResult__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AMN_RequestPlayerDataResult> OnRequestPlayerDataReceived__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AMN_RequestAchievementsResult> OnRequestAchievementsReceived__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AMN_UpdateAchievementResult> OnUpdateAchievementReceived__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AMN_RequestLeaderboardsResult> OnRequestLeaderboardsReceived__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AMN_SubmitLeaderboardResult> OnSubmitLeaderboardReceived__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AMN_LocalPlayerScoreLoadedResult> OnLocalPlayerScoreLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AMN_ScoresLoadedResult> OnScoresLoaded__BackingField = delegate
	{
	};

	private GC_Player _player;

	private Dictionary<string, GC_Player> _Players = new Dictionary<string, GC_Player>();

	private bool _isInitialized;

	public bool IsInitialized
	{
		get
		{
			return _isInitialized;
		}
	}

	public GC_Player Player
	{
		get
		{
			return _player;
		}
		set
		{
			_player = value;
		}
	}

	public Dictionary<string, GC_Player> Players
	{
		get
		{
			return _Players;
		}
	}

	public List<GC_Achievement> Achievements
	{
		get
		{
			return AmazonNativeSettings.Instance.Achievements;
		}
	}

	public List<GC_Leaderboard> Leaderboards
	{
		get
		{
			return AmazonNativeSettings.Instance.Leaderboards;
		}
	}

	public event Action<AMN_InitializeResult> OnInitializeResult
	{
		add
		{
			Action<AMN_InitializeResult> action = OnInitializeResult__BackingField;
			Action<AMN_InitializeResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInitializeResult__BackingField, (Action<AMN_InitializeResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AMN_InitializeResult> action = OnInitializeResult__BackingField;
			Action<AMN_InitializeResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInitializeResult__BackingField, (Action<AMN_InitializeResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<AMN_RequestPlayerDataResult> OnRequestPlayerDataReceived
	{
		add
		{
			Action<AMN_RequestPlayerDataResult> action = OnRequestPlayerDataReceived__BackingField;
			Action<AMN_RequestPlayerDataResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRequestPlayerDataReceived__BackingField, (Action<AMN_RequestPlayerDataResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AMN_RequestPlayerDataResult> action = OnRequestPlayerDataReceived__BackingField;
			Action<AMN_RequestPlayerDataResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRequestPlayerDataReceived__BackingField, (Action<AMN_RequestPlayerDataResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<AMN_RequestAchievementsResult> OnRequestAchievementsReceived
	{
		add
		{
			Action<AMN_RequestAchievementsResult> action = OnRequestAchievementsReceived__BackingField;
			Action<AMN_RequestAchievementsResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRequestAchievementsReceived__BackingField, (Action<AMN_RequestAchievementsResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AMN_RequestAchievementsResult> action = OnRequestAchievementsReceived__BackingField;
			Action<AMN_RequestAchievementsResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRequestAchievementsReceived__BackingField, (Action<AMN_RequestAchievementsResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<AMN_UpdateAchievementResult> OnUpdateAchievementReceived
	{
		add
		{
			Action<AMN_UpdateAchievementResult> action = OnUpdateAchievementReceived__BackingField;
			Action<AMN_UpdateAchievementResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnUpdateAchievementReceived__BackingField, (Action<AMN_UpdateAchievementResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AMN_UpdateAchievementResult> action = OnUpdateAchievementReceived__BackingField;
			Action<AMN_UpdateAchievementResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnUpdateAchievementReceived__BackingField, (Action<AMN_UpdateAchievementResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<AMN_RequestLeaderboardsResult> OnRequestLeaderboardsReceived
	{
		add
		{
			Action<AMN_RequestLeaderboardsResult> action = OnRequestLeaderboardsReceived__BackingField;
			Action<AMN_RequestLeaderboardsResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRequestLeaderboardsReceived__BackingField, (Action<AMN_RequestLeaderboardsResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AMN_RequestLeaderboardsResult> action = OnRequestLeaderboardsReceived__BackingField;
			Action<AMN_RequestLeaderboardsResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRequestLeaderboardsReceived__BackingField, (Action<AMN_RequestLeaderboardsResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<AMN_SubmitLeaderboardResult> OnSubmitLeaderboardReceived
	{
		add
		{
			Action<AMN_SubmitLeaderboardResult> action = OnSubmitLeaderboardReceived__BackingField;
			Action<AMN_SubmitLeaderboardResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnSubmitLeaderboardReceived__BackingField, (Action<AMN_SubmitLeaderboardResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AMN_SubmitLeaderboardResult> action = OnSubmitLeaderboardReceived__BackingField;
			Action<AMN_SubmitLeaderboardResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnSubmitLeaderboardReceived__BackingField, (Action<AMN_SubmitLeaderboardResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<AMN_LocalPlayerScoreLoadedResult> OnLocalPlayerScoreLoaded
	{
		add
		{
			Action<AMN_LocalPlayerScoreLoadedResult> action = OnLocalPlayerScoreLoaded__BackingField;
			Action<AMN_LocalPlayerScoreLoadedResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLocalPlayerScoreLoaded__BackingField, (Action<AMN_LocalPlayerScoreLoadedResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AMN_LocalPlayerScoreLoadedResult> action = OnLocalPlayerScoreLoaded__BackingField;
			Action<AMN_LocalPlayerScoreLoadedResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLocalPlayerScoreLoaded__BackingField, (Action<AMN_LocalPlayerScoreLoadedResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<AMN_ScoresLoadedResult> OnScoresLoaded
	{
		add
		{
			Action<AMN_ScoresLoadedResult> action = OnScoresLoaded__BackingField;
			Action<AMN_ScoresLoadedResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnScoresLoaded__BackingField, (Action<AMN_ScoresLoadedResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AMN_ScoresLoadedResult> action = OnScoresLoaded__BackingField;
			Action<AMN_ScoresLoadedResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnScoresLoaded__BackingField, (Action<AMN_ScoresLoadedResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		SubscribeToEvents();
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void OnServiceConnected()
	{
		AMN_InitializeResult obj = new AMN_InitializeResult(true);
		OnInitializeResult__BackingField(obj);
	}

	private void OnServiceDisconnected(string error)
	{
		AMN_InitializeResult obj = new AMN_InitializeResult(error);
		OnInitializeResult__BackingField(obj);
	}

	public void Connect()
	{
		if (!_isInitialized)
		{
			Init();
		}
	}

	public void Disconnect()
	{
	}

	public void ShowGCOverlay()
	{
	}

	public void ShowSignInPage()
	{
	}

	public void RetrieveLocalPlayer()
	{
	}

	public void ShowAchievementsOverlay()
	{
	}

	public void RequestAchievements()
	{
	}

	public GC_Achievement GetAchievement(string id)
	{
		return null;
	}

	public void UpdateAchievementProgress(string achieve_id, float score)
	{
	}

	public void ShowLeaderboardsOverlay()
	{
	}

	public void RequestLeaderboards()
	{
	}

	public void SubmitLeaderBoardProgress(string leaderBId, long score)
	{
	}

	public GC_Leaderboard GetLeaderboard(string id)
	{
		return null;
	}

	public void LoadLocalPlayerScores(string id, GC_ScoreTimeSpan timeSpan)
	{
	}

	public void LoadTopScores(string id, GC_ScoreTimeSpan timeSpan)
	{
	}

	public void AddPlayer(GC_Player player)
	{
		if (!_Players.ContainsKey(player.PlayerId))
		{
			_Players.Add(player.PlayerId, player);
		}
	}

	public GC_Player GetPlayerById(string id)
	{
		if (_Players.ContainsKey(id))
		{
			return _Players[id];
		}
		return null;
	}

	private void Init()
	{
		_isInitialized = true;
	}

	private void SubscribeToEvents()
	{
	}
}
