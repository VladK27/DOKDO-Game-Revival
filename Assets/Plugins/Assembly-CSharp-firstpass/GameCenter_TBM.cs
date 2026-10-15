using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class GameCenter_TBM : Singleton<GameCenter_TBM>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_TBM_LoadMatchResult> ActionMatchInfoLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_TBM_LoadMatchesResult> ActionMatchesInfoLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_TBM_MatchDataUpdateResult> ActionMatchDataUpdated__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_TBM_MatchInitResult> ActionMatchFound__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_TBM_MatchQuitResult> ActionMatchQuit__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_TBM_EndTrunResult> ActionTrunEnded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_TBM_MatchEndResult> ActionMacthEnded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_TBM_RematchResult> ActionRematched__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_TBM_MatchRemovedResult> ActionMatchRemoved__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_TBM_MatchInitResult> ActionMatchInvitationAccepted__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_TBM_MatchRemovedResult> ActionMatchInvitationDeclined__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_TBM_Match> ActionPlayerQuitForMatch__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_TBM_MatchTurnResult> ActionTrunReceived__BackingField = delegate
	{
	};

	private Dictionary<string, GK_TBM_Match> _Matches = new Dictionary<string, GK_TBM_Match>();

	public Dictionary<string, GK_TBM_Match> Matches
	{
		get
		{
			return _Matches;
		}
	}

	public List<GK_TBM_Match> MatchesList
	{
		get
		{
			List<GK_TBM_Match> list = new List<GK_TBM_Match>();
			foreach (KeyValuePair<string, GK_TBM_Match> match in _Matches)
			{
				list.Add(match.Value);
			}
			return list;
		}
	}

	public static event Action<GK_TBM_LoadMatchResult> ActionMatchInfoLoaded
	{
		add
		{
			Action<GK_TBM_LoadMatchResult> action = ActionMatchInfoLoaded__BackingField;
			Action<GK_TBM_LoadMatchResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMatchInfoLoaded__BackingField, (Action<GK_TBM_LoadMatchResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_TBM_LoadMatchResult> action = ActionMatchInfoLoaded__BackingField;
			Action<GK_TBM_LoadMatchResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMatchInfoLoaded__BackingField, (Action<GK_TBM_LoadMatchResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_TBM_LoadMatchesResult> ActionMatchesInfoLoaded
	{
		add
		{
			Action<GK_TBM_LoadMatchesResult> action = ActionMatchesInfoLoaded__BackingField;
			Action<GK_TBM_LoadMatchesResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMatchesInfoLoaded__BackingField, (Action<GK_TBM_LoadMatchesResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_TBM_LoadMatchesResult> action = ActionMatchesInfoLoaded__BackingField;
			Action<GK_TBM_LoadMatchesResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMatchesInfoLoaded__BackingField, (Action<GK_TBM_LoadMatchesResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_TBM_MatchDataUpdateResult> ActionMatchDataUpdated
	{
		add
		{
			Action<GK_TBM_MatchDataUpdateResult> action = ActionMatchDataUpdated__BackingField;
			Action<GK_TBM_MatchDataUpdateResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMatchDataUpdated__BackingField, (Action<GK_TBM_MatchDataUpdateResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_TBM_MatchDataUpdateResult> action = ActionMatchDataUpdated__BackingField;
			Action<GK_TBM_MatchDataUpdateResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMatchDataUpdated__BackingField, (Action<GK_TBM_MatchDataUpdateResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_TBM_MatchInitResult> ActionMatchFound
	{
		add
		{
			Action<GK_TBM_MatchInitResult> action = ActionMatchFound__BackingField;
			Action<GK_TBM_MatchInitResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMatchFound__BackingField, (Action<GK_TBM_MatchInitResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_TBM_MatchInitResult> action = ActionMatchFound__BackingField;
			Action<GK_TBM_MatchInitResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMatchFound__BackingField, (Action<GK_TBM_MatchInitResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_TBM_MatchQuitResult> ActionMatchQuit
	{
		add
		{
			Action<GK_TBM_MatchQuitResult> action = ActionMatchQuit__BackingField;
			Action<GK_TBM_MatchQuitResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMatchQuit__BackingField, (Action<GK_TBM_MatchQuitResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_TBM_MatchQuitResult> action = ActionMatchQuit__BackingField;
			Action<GK_TBM_MatchQuitResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMatchQuit__BackingField, (Action<GK_TBM_MatchQuitResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_TBM_EndTrunResult> ActionTrunEnded
	{
		add
		{
			Action<GK_TBM_EndTrunResult> action = ActionTrunEnded__BackingField;
			Action<GK_TBM_EndTrunResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionTrunEnded__BackingField, (Action<GK_TBM_EndTrunResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_TBM_EndTrunResult> action = ActionTrunEnded__BackingField;
			Action<GK_TBM_EndTrunResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionTrunEnded__BackingField, (Action<GK_TBM_EndTrunResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_TBM_MatchEndResult> ActionMacthEnded
	{
		add
		{
			Action<GK_TBM_MatchEndResult> action = ActionMacthEnded__BackingField;
			Action<GK_TBM_MatchEndResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMacthEnded__BackingField, (Action<GK_TBM_MatchEndResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_TBM_MatchEndResult> action = ActionMacthEnded__BackingField;
			Action<GK_TBM_MatchEndResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMacthEnded__BackingField, (Action<GK_TBM_MatchEndResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_TBM_RematchResult> ActionRematched
	{
		add
		{
			Action<GK_TBM_RematchResult> action = ActionRematched__BackingField;
			Action<GK_TBM_RematchResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRematched__BackingField, (Action<GK_TBM_RematchResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_TBM_RematchResult> action = ActionRematched__BackingField;
			Action<GK_TBM_RematchResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRematched__BackingField, (Action<GK_TBM_RematchResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_TBM_MatchRemovedResult> ActionMatchRemoved
	{
		add
		{
			Action<GK_TBM_MatchRemovedResult> action = ActionMatchRemoved__BackingField;
			Action<GK_TBM_MatchRemovedResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMatchRemoved__BackingField, (Action<GK_TBM_MatchRemovedResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_TBM_MatchRemovedResult> action = ActionMatchRemoved__BackingField;
			Action<GK_TBM_MatchRemovedResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMatchRemoved__BackingField, (Action<GK_TBM_MatchRemovedResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_TBM_MatchInitResult> ActionMatchInvitationAccepted
	{
		add
		{
			Action<GK_TBM_MatchInitResult> action = ActionMatchInvitationAccepted__BackingField;
			Action<GK_TBM_MatchInitResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMatchInvitationAccepted__BackingField, (Action<GK_TBM_MatchInitResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_TBM_MatchInitResult> action = ActionMatchInvitationAccepted__BackingField;
			Action<GK_TBM_MatchInitResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMatchInvitationAccepted__BackingField, (Action<GK_TBM_MatchInitResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_TBM_MatchRemovedResult> ActionMatchInvitationDeclined
	{
		add
		{
			Action<GK_TBM_MatchRemovedResult> action = ActionMatchInvitationDeclined__BackingField;
			Action<GK_TBM_MatchRemovedResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMatchInvitationDeclined__BackingField, (Action<GK_TBM_MatchRemovedResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_TBM_MatchRemovedResult> action = ActionMatchInvitationDeclined__BackingField;
			Action<GK_TBM_MatchRemovedResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMatchInvitationDeclined__BackingField, (Action<GK_TBM_MatchRemovedResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_TBM_Match> ActionPlayerQuitForMatch
	{
		add
		{
			Action<GK_TBM_Match> action = ActionPlayerQuitForMatch__BackingField;
			Action<GK_TBM_Match> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPlayerQuitForMatch__BackingField, (Action<GK_TBM_Match>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_TBM_Match> action = ActionPlayerQuitForMatch__BackingField;
			Action<GK_TBM_Match> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPlayerQuitForMatch__BackingField, (Action<GK_TBM_Match>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_TBM_MatchTurnResult> ActionTrunReceived
	{
		add
		{
			Action<GK_TBM_MatchTurnResult> action = ActionTrunReceived__BackingField;
			Action<GK_TBM_MatchTurnResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionTrunReceived__BackingField, (Action<GK_TBM_MatchTurnResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_TBM_MatchTurnResult> action = ActionTrunReceived__BackingField;
			Action<GK_TBM_MatchTurnResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionTrunReceived__BackingField, (Action<GK_TBM_MatchTurnResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void LoadMatchesInfo()
	{
	}

	public void LoadMatch(string matchId)
	{
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

	public void SaveCurrentTurn(string matchId, byte[] matchData)
	{
	}

	public void EndTurn(string matchId, byte[] matchData, string nextPlayerId)
	{
	}

	public void QuitInTurn(string matchId, GK_TurnBasedMatchOutcome outcome, string nextPlayerId, byte[] matchData)
	{
	}

	public void QuitOutOfTurn(string matchId, GK_TurnBasedMatchOutcome outcome)
	{
	}

	public void EndMatch(string matchId, byte[] matchData)
	{
	}

	public void Rematch(string matchId)
	{
	}

	public void RemoveMatch(string matchId)
	{
	}

	public void AcceptInvite(string matchId)
	{
	}

	public void DeclineInvite(string matchId)
	{
	}

	public void UpdateParticipantOutcome(string matchId, int outcome, string playerId)
	{
	}

	public GK_TBM_Match GetMatchById(string matchId)
	{
		if (_Matches.ContainsKey(matchId))
		{
			return _Matches[matchId];
		}
		return null;
	}

	public static void PrintMatchInfo(GK_TBM_Match match)
	{
		string empty = string.Empty;
		empty += "----------------------------------------\n";
		empty += "Printing basic match info, for \n";
		empty = empty + "Match ID: " + match.Id + "\n";
		empty = string.Concat(empty, "Status:", match.Status, "\n");
		empty = ((match.CurrentParticipant == null) ? (empty + "CurrentPlayerID: ---- \n") : ((match.CurrentParticipant.Player == null) ? (empty + "CurrentPlayerID: ---- \n") : (empty + "CurrentPlayerID: " + match.CurrentParticipant.Player.Id + "\n")));
		empty = empty + "Data: " + match.UTF8StringData + "\n";
		empty += "*******Participants*******\n";
		foreach (GK_TBM_Participant participant in match.Participants)
		{
			empty = ((participant.Player == null) ? (empty + "PlayerId: ---  \n") : (empty + "PlayerId: " + participant.Player.Id + "\n"));
			empty = string.Concat(empty, "Status: ", participant.Status, "\n");
			empty = string.Concat(empty, "MatchOutcome: ", participant.MatchOutcome, "\n");
			empty = empty + "TimeoutDate: " + participant.TimeoutDate.ToString("DD MMM YYYY HH:mm:ss") + "\n";
			empty = empty + "LastTurnDate: " + participant.LastTurnDate.ToString("DD MMM YYYY HH:mm:ss") + "\n";
			empty += "**********************\n";
		}
		empty += "----------------------------------------\n";
		ISN_Logger.Log(empty);
	}

	public void OnLoadMatchesResult(string data)
	{
		ISN_Logger.Log("TBM::OnLoadMatchesResult: " + data);
		GK_TBM_LoadMatchesResult gK_TBM_LoadMatchesResult = new GK_TBM_LoadMatchesResult(true);
		_Matches = new Dictionary<string, GK_TBM_Match>();
		if (data.Length == 0)
		{
			ActionMatchesInfoLoaded__BackingField(gK_TBM_LoadMatchesResult);
			return;
		}
		string[] array = data.Split(new string[1] { "|%|" }, StringSplitOptions.None);
		if (array.Length > 0)
		{
			gK_TBM_LoadMatchesResult.LoadedMatches = new Dictionary<string, GK_TBM_Match>();
			for (int i = 0; i < array.Length && !(array[i] == "endofline"); i++)
			{
				GK_TBM_Match gK_TBM_Match = ParceMatchInfo(array[i]);
				UpdateMatchInfo(gK_TBM_Match);
				gK_TBM_LoadMatchesResult.LoadedMatches.Add(gK_TBM_Match.Id, gK_TBM_Match);
			}
		}
		ActionMatchesInfoLoaded__BackingField(gK_TBM_LoadMatchesResult);
	}

	private void OnLoadMatchesResultFailed(string errorData)
	{
		GK_TBM_LoadMatchesResult obj = new GK_TBM_LoadMatchesResult(errorData);
		ActionMatchesInfoLoaded__BackingField(obj);
	}

	private void OnLoadMatchResult(string data)
	{
		GK_TBM_Match match = ParceMatchInfo(data);
		GK_TBM_LoadMatchResult obj = new GK_TBM_LoadMatchResult(match);
		ActionMatchInfoLoaded__BackingField(obj);
	}

	private void OnLoadMatchResultFailed(string errorData)
	{
		GK_TBM_LoadMatchResult obj = new GK_TBM_LoadMatchResult(errorData);
		ActionMatchInfoLoaded__BackingField(obj);
	}

	private void OnUpdateMatchResult(string data)
	{
		string[] array = data.Split('|');
		string text = array[0];
		GK_TBM_Match matchById = GetMatchById(text);
		GK_TBM_MatchDataUpdateResult obj;
		if (matchById == null)
		{
			Error error = new Error(0, "Match with id: " + text + " not found");
			obj = new GK_TBM_MatchDataUpdateResult(error);
		}
		else
		{
			matchById.SetData(array[1]);
			obj = new GK_TBM_MatchDataUpdateResult(matchById);
		}
		ActionMatchDataUpdated__BackingField(obj);
	}

	private void OnUpdateMatchResultFailed(string errorData)
	{
		GK_TBM_MatchDataUpdateResult obj = new GK_TBM_MatchDataUpdateResult(errorData);
		ActionMatchDataUpdated__BackingField(obj);
	}

	private void OnMatchFoundResult(string data)
	{
		GK_TBM_Match match = ParceMatchInfo(data);
		UpdateMatchInfo(match);
		GK_TBM_MatchInitResult obj = new GK_TBM_MatchInitResult(match);
		ActionMatchFound__BackingField(obj);
	}

	private void OnMatchFoundResultFailed(string errorData)
	{
		GK_TBM_MatchInitResult obj = new GK_TBM_MatchInitResult(errorData);
		ActionMatchFound__BackingField(obj);
	}

	private void OnPlayerQuitForMatch(string data)
	{
		GK_TBM_Match gK_TBM_Match = ParceMatchInfo(data);
		UpdateMatchInfo(gK_TBM_Match);
		ActionPlayerQuitForMatch__BackingField(gK_TBM_Match);
	}

	private void OnMatchQuitResult(string matchId)
	{
		GK_TBM_MatchQuitResult obj = new GK_TBM_MatchQuitResult(matchId);
		ActionMatchQuit__BackingField(obj);
	}

	private void OnMatchQuitResultFailed(string errorData)
	{
		GK_TBM_MatchQuitResult obj = new GK_TBM_MatchQuitResult(errorData);
		ActionMatchQuit__BackingField(obj);
	}

	private void OnEndTurnResult(string data)
	{
		GK_TBM_Match match = ParceMatchInfo(data);
		UpdateMatchInfo(match);
		GK_TBM_EndTrunResult obj = new GK_TBM_EndTrunResult(match);
		ActionTrunEnded__BackingField(obj);
	}

	private void OnEndTurnResultFailed(string errorData)
	{
		GK_TBM_EndTrunResult obj = new GK_TBM_EndTrunResult(errorData);
		ActionTrunEnded__BackingField(obj);
	}

	private void OnEndMatch(string data)
	{
		GK_TBM_Match match = ParceMatchInfo(data);
		UpdateMatchInfo(match);
		GK_TBM_MatchEndResult obj = new GK_TBM_MatchEndResult(match);
		ActionMacthEnded__BackingField(obj);
	}

	private void OnEndMatchResult(string errorData)
	{
		GK_TBM_MatchEndResult obj = new GK_TBM_MatchEndResult(errorData);
		ActionMacthEnded__BackingField(obj);
	}

	private void OnRematchResult(string data)
	{
		GK_TBM_Match match = ParceMatchInfo(data);
		UpdateMatchInfo(match);
		GK_TBM_RematchResult obj = new GK_TBM_RematchResult(match);
		ActionRematched__BackingField(obj);
	}

	private void OnRematchFailed(string errorData)
	{
		GK_TBM_RematchResult obj = new GK_TBM_RematchResult(errorData);
		ActionRematched__BackingField(obj);
	}

	private void OnMatchRemoved(string matchId)
	{
		GK_TBM_MatchRemovedResult obj = new GK_TBM_MatchRemovedResult(matchId);
		if (_Matches.ContainsKey(matchId))
		{
			_Matches.Remove(matchId);
		}
		ActionMatchRemoved__BackingField(obj);
	}

	private void OnMatchRemoveFailed(string errorData)
	{
		GK_TBM_MatchRemovedResult obj = new GK_TBM_MatchRemovedResult(errorData);
		ActionMatchRemoved__BackingField(obj);
	}

	private void OnMatchInvitationAccepted(string data)
	{
		GK_TBM_Match match = ParceMatchInfo(data);
		UpdateMatchInfo(match);
		GK_TBM_MatchInitResult obj = new GK_TBM_MatchInitResult(match);
		ActionMatchInvitationAccepted__BackingField(obj);
	}

	private void OnMatchInvitationAcceptedFailed(string errorData)
	{
		GK_TBM_MatchInitResult obj = new GK_TBM_MatchInitResult(errorData);
		ActionMatchInvitationAccepted__BackingField(obj);
	}

	private void OnMatchInvitationDeclined(string matchId)
	{
		GK_TBM_MatchRemovedResult obj = new GK_TBM_MatchRemovedResult(matchId);
		if (_Matches.ContainsKey(matchId))
		{
			_Matches.Remove(matchId);
		}
		ActionMatchInvitationDeclined__BackingField(obj);
	}

	private void OnMatchInvitationDeclineFailed(string errorData)
	{
		GK_TBM_MatchRemovedResult obj = new GK_TBM_MatchRemovedResult(errorData);
		ActionMatchInvitationDeclined__BackingField(obj);
	}

	private void OnTrunReceived(string data)
	{
		GK_TBM_Match match = ParceMatchInfo(data);
		UpdateMatchInfo(match);
		GK_TBM_MatchTurnResult obj = new GK_TBM_MatchTurnResult(match);
		ActionTrunReceived__BackingField(obj);
	}

	private void UpdateMatchInfo(GK_TBM_Match match)
	{
		if (_Matches.ContainsKey(match.Id))
		{
			_Matches[match.Id] = match;
		}
		else
		{
			_Matches.Add(match.Id, match);
		}
	}

	private static GK_TBM_Match ParceMatchInfo(string data)
	{
		string[] matchData = data.Split('|');
		return ParceMatchInfo(matchData, 0);
	}

	public static GK_TBM_Match ParceMatchInfo(string[] MatchData, int index)
	{
		GK_TBM_Match gK_TBM_Match = new GK_TBM_Match();
		gK_TBM_Match.Id = MatchData[index];
		gK_TBM_Match.Status = (GK_TurnBasedMatchStatus)Convert.ToInt64(MatchData[index + 1]);
		gK_TBM_Match.Message = MatchData[index + 2];
		gK_TBM_Match.CreationTimestamp = DateTime.Parse(MatchData[index + 3]);
		gK_TBM_Match.SetData(MatchData[index + 4]);
		string playerId = MatchData[index + 5];
		gK_TBM_Match.Participants = GameCenterManager.ParseParticipantsData(MatchData, index + 6);
		foreach (GK_TBM_Participant participant in gK_TBM_Match.Participants)
		{
			participant.SetMatchId(gK_TBM_Match.Id);
		}
		gK_TBM_Match.CurrentParticipant = gK_TBM_Match.GetParticipantByPlayerId(playerId);
		return gK_TBM_Match;
	}
}
