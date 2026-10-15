using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class GameCenter_RTM : Singleton<GameCenter_RTM>
{
	private GK_RTM_Match _CurrentMatch;

	private Dictionary<string, GK_Player> _NearbyPlayers = new Dictionary<string, GK_Player>();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_RTM_MatchStartedResult> ActionMatchStarted__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<Error> ActionMatchFailed__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_Player, bool> ActionNearbyPlayerStateUpdated__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_RTM_QueryActivityResult> ActionActivityResultReceived__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<Error> ActionDataSendError__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_Player, byte[]> ActionDataReceived__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_Player, GK_PlayerConnectionState, GK_RTM_Match> ActionPlayerStateChanged__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_Player> ActionDiconnectedPlayerReinvited__BackingField = delegate
	{
	};

	public GK_RTM_Match CurrentMatch
	{
		get
		{
			return _CurrentMatch;
		}
	}

	public List<GK_Player> NearbyPlayersList
	{
		get
		{
			List<GK_Player> list = new List<GK_Player>();
			foreach (KeyValuePair<string, GK_Player> nearbyPlayer in _NearbyPlayers)
			{
				list.Add(nearbyPlayer.Value);
			}
			return list;
		}
	}

	public Dictionary<string, GK_Player> NearbyPlayers
	{
		get
		{
			return _NearbyPlayers;
		}
	}

	public static event Action<GK_RTM_MatchStartedResult> ActionMatchStarted
	{
		add
		{
			Action<GK_RTM_MatchStartedResult> action = ActionMatchStarted__BackingField;
			Action<GK_RTM_MatchStartedResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMatchStarted__BackingField, (Action<GK_RTM_MatchStartedResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_RTM_MatchStartedResult> action = ActionMatchStarted__BackingField;
			Action<GK_RTM_MatchStartedResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMatchStarted__BackingField, (Action<GK_RTM_MatchStartedResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<Error> ActionMatchFailed
	{
		add
		{
			Action<Error> action = ActionMatchFailed__BackingField;
			Action<Error> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMatchFailed__BackingField, (Action<Error>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Error> action = ActionMatchFailed__BackingField;
			Action<Error> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMatchFailed__BackingField, (Action<Error>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_Player, bool> ActionNearbyPlayerStateUpdated
	{
		add
		{
			Action<GK_Player, bool> action = ActionNearbyPlayerStateUpdated__BackingField;
			Action<GK_Player, bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionNearbyPlayerStateUpdated__BackingField, (Action<GK_Player, bool>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_Player, bool> action = ActionNearbyPlayerStateUpdated__BackingField;
			Action<GK_Player, bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionNearbyPlayerStateUpdated__BackingField, (Action<GK_Player, bool>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_RTM_QueryActivityResult> ActionActivityResultReceived
	{
		add
		{
			Action<GK_RTM_QueryActivityResult> action = ActionActivityResultReceived__BackingField;
			Action<GK_RTM_QueryActivityResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionActivityResultReceived__BackingField, (Action<GK_RTM_QueryActivityResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_RTM_QueryActivityResult> action = ActionActivityResultReceived__BackingField;
			Action<GK_RTM_QueryActivityResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionActivityResultReceived__BackingField, (Action<GK_RTM_QueryActivityResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<Error> ActionDataSendError
	{
		add
		{
			Action<Error> action = ActionDataSendError__BackingField;
			Action<Error> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionDataSendError__BackingField, (Action<Error>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Error> action = ActionDataSendError__BackingField;
			Action<Error> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionDataSendError__BackingField, (Action<Error>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_Player, byte[]> ActionDataReceived
	{
		add
		{
			Action<GK_Player, byte[]> action = ActionDataReceived__BackingField;
			Action<GK_Player, byte[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionDataReceived__BackingField, (Action<GK_Player, byte[]>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_Player, byte[]> action = ActionDataReceived__BackingField;
			Action<GK_Player, byte[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionDataReceived__BackingField, (Action<GK_Player, byte[]>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_Player, GK_PlayerConnectionState, GK_RTM_Match> ActionPlayerStateChanged
	{
		add
		{
			Action<GK_Player, GK_PlayerConnectionState, GK_RTM_Match> action = ActionPlayerStateChanged__BackingField;
			Action<GK_Player, GK_PlayerConnectionState, GK_RTM_Match> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPlayerStateChanged__BackingField, (Action<GK_Player, GK_PlayerConnectionState, GK_RTM_Match>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_Player, GK_PlayerConnectionState, GK_RTM_Match> action = ActionPlayerStateChanged__BackingField;
			Action<GK_Player, GK_PlayerConnectionState, GK_RTM_Match> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPlayerStateChanged__BackingField, (Action<GK_Player, GK_PlayerConnectionState, GK_RTM_Match>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_Player> ActionDiconnectedPlayerReinvited
	{
		add
		{
			Action<GK_Player> action = ActionDiconnectedPlayerReinvited__BackingField;
			Action<GK_Player> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionDiconnectedPlayerReinvited__BackingField, (Action<GK_Player>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_Player> action = ActionDiconnectedPlayerReinvited__BackingField;
			Action<GK_Player> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionDiconnectedPlayerReinvited__BackingField, (Action<GK_Player>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void FindMatch(int minPlayers, int maxPlayers, string msg = "", string[] playersToInvite = null)
	{
	}

	public void FindMatchWithNativeUI(int minPlayers, int maxPlayers, string msg = "", string[] playersToInvite = null)
	{
	}

	public void SetPlayerGroup(int group)
	{
	}

	public void SetPlayerAttributes(int attributes)
	{
	}

	public void StartMatchWithInvite(GK_Invite invite, bool useNativeUI)
	{
	}

	public void CancelPendingInviteToPlayer(GK_Player player)
	{
	}

	public void CancelMatchSearch()
	{
	}

	public void FinishMatchmaking()
	{
	}

	public void QueryActivity()
	{
	}

	public void QueryPlayerGroupActivity(int group)
	{
	}

	public void StartBrowsingForNearbyPlayers()
	{
	}

	public void StopBrowsingForNearbyPlayers()
	{
	}

	public void Rematch()
	{
	}

	public void Disconnect()
	{
		_CurrentMatch = null;
	}

	public void SendDataToAll(byte[] data, GK_MatchSendDataMode dataMode)
	{
	}

	public void SendData(byte[] data, GK_MatchSendDataMode dataMode, params GK_Player[] players)
	{
	}

	private void OnMatchStartFailed(string errorData)
	{
		GK_RTM_MatchStartedResult obj = new GK_RTM_MatchStartedResult(errorData);
		ActionMatchStarted__BackingField(obj);
	}

	private void OnMatchStarted(string matchData)
	{
		GK_RTM_Match match = ParseMatchData(matchData);
		GK_RTM_MatchStartedResult obj = new GK_RTM_MatchStartedResult(match);
		ActionMatchStarted__BackingField(obj);
	}

	private void OnMatchFailed(string errorData)
	{
		_CurrentMatch = null;
		Error obj = new Error(errorData);
		ActionMatchFailed__BackingField(obj);
	}

	private void OnNearbyPlayerInfoReceived(string data)
	{
		string[] array = data.Split('|');
		string playerID = array[0];
		GK_Player playerById = GameCenterManager.GetPlayerById(playerID);
		bool flag = Convert.ToBoolean(array[1]);
		if (flag)
		{
			if (!_NearbyPlayers.ContainsKey(playerById.Id))
			{
				_NearbyPlayers.Add(playerById.Id, playerById);
			}
		}
		else if (_NearbyPlayers.ContainsKey(playerById.Id))
		{
			_NearbyPlayers.Remove(playerById.Id);
		}
		ActionNearbyPlayerStateUpdated__BackingField(playerById, flag);
	}

	private void OnQueryActivity(string data)
	{
		int activity = Convert.ToInt32(data);
		GK_RTM_QueryActivityResult obj = new GK_RTM_QueryActivityResult(activity);
		ActionActivityResultReceived__BackingField(obj);
	}

	private void OnQueryActivityFailed(string errorData)
	{
		GK_RTM_QueryActivityResult obj = new GK_RTM_QueryActivityResult(errorData);
		ActionActivityResultReceived__BackingField(obj);
	}

	private void OnMatchInfoUpdated(string matchData)
	{
		GK_RTM_Match gK_RTM_Match = ParseMatchData(matchData);
		if (gK_RTM_Match.Players.Count == 0 && gK_RTM_Match.ExpectedPlayerCount == 0)
		{
			_CurrentMatch = null;
		}
	}

	private void OnMatchPlayerStateChanged(string data)
	{
		if (_CurrentMatch != null)
		{
			string[] array = data.Split('|');
			string playerID = array[0];
			GK_Player playerById = GameCenterManager.GetPlayerById(playerID);
			GK_PlayerConnectionState arg = (GK_PlayerConnectionState)Convert.ToInt32(array[1]);
			ActionPlayerStateChanged__BackingField(playerById, arg, CurrentMatch);
		}
	}

	private void OnDiconnectedPlayerReinvited(string playerId)
	{
		GK_Player playerById = GameCenterManager.GetPlayerById(playerId);
		ActionDiconnectedPlayerReinvited__BackingField(playerById);
	}

	private void OnMatchDataReceived(string data)
	{
		string[] array = data.Split('|');
		string playerID = array[0];
		GK_Player playerById = GameCenterManager.GetPlayerById(playerID);
		byte[] arg = Convert.FromBase64String(array[1]);
		ActionDataReceived__BackingField(playerById, arg);
	}

	private void OnSendDataError(string errorData)
	{
		Error obj = new Error(errorData);
		ActionDataSendError__BackingField(obj);
	}

	private GK_RTM_Match ParseMatchData(string matchData)
	{
		return _CurrentMatch = new GK_RTM_Match(matchData);
	}
}
