using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class GP_TBM_Controller : iTBM_Matchmaker
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<UM_TBM_MatchResult> MatchFoundEvent__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<UM_TBM_MatchResult> MatchLoadedEvent__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<UM_TBM_MatchResult> InvitationAccepted__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<string> InvitationDeclined__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<UM_TBM_MatchResult> TurnEndedEvent__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<UM_TBM_MatchResult> MatchUpdatedEvent__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<UM_TBM_MatchesLoadResult> MatchesListLoadedEvent__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action MatchesListUpdated__BackingField = () =>
	{
	};

	public List<UM_TBM_Match> _Matches = new List<UM_TBM_Match>();

	public List<UM_TBM_Invite> _Invitations = new List<UM_TBM_Invite>();

	private int DataEventCount;

	private const int PLACING_UNINITIALIZED = -1;

	public List<UM_TBM_Match> Matches
	{
		get
		{
			return _Matches;
		}
	}

	public List<UM_TBM_Invite> Invitations
	{
		get
		{
			return _Invitations;
		}
	}

	public event Action<UM_TBM_MatchResult> MatchFoundEvent
	{
		add
		{
			Action<UM_TBM_MatchResult> action = MatchFoundEvent__BackingField;
			Action<UM_TBM_MatchResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref MatchFoundEvent__BackingField, (Action<UM_TBM_MatchResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_TBM_MatchResult> action = MatchFoundEvent__BackingField;
			Action<UM_TBM_MatchResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref MatchFoundEvent__BackingField, (Action<UM_TBM_MatchResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<UM_TBM_MatchResult> MatchLoadedEvent
	{
		add
		{
			Action<UM_TBM_MatchResult> action = MatchLoadedEvent__BackingField;
			Action<UM_TBM_MatchResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref MatchLoadedEvent__BackingField, (Action<UM_TBM_MatchResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_TBM_MatchResult> action = MatchLoadedEvent__BackingField;
			Action<UM_TBM_MatchResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref MatchLoadedEvent__BackingField, (Action<UM_TBM_MatchResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<UM_TBM_MatchResult> InvitationAccepted
	{
		add
		{
			Action<UM_TBM_MatchResult> action = InvitationAccepted__BackingField;
			Action<UM_TBM_MatchResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref InvitationAccepted__BackingField, (Action<UM_TBM_MatchResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_TBM_MatchResult> action = InvitationAccepted__BackingField;
			Action<UM_TBM_MatchResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref InvitationAccepted__BackingField, (Action<UM_TBM_MatchResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<string> InvitationDeclined
	{
		add
		{
			Action<string> action = InvitationDeclined__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref InvitationDeclined__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = InvitationDeclined__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref InvitationDeclined__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<UM_TBM_MatchResult> TurnEndedEvent
	{
		add
		{
			Action<UM_TBM_MatchResult> action = TurnEndedEvent__BackingField;
			Action<UM_TBM_MatchResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref TurnEndedEvent__BackingField, (Action<UM_TBM_MatchResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_TBM_MatchResult> action = TurnEndedEvent__BackingField;
			Action<UM_TBM_MatchResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref TurnEndedEvent__BackingField, (Action<UM_TBM_MatchResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<UM_TBM_MatchResult> MatchUpdatedEvent
	{
		add
		{
			Action<UM_TBM_MatchResult> action = MatchUpdatedEvent__BackingField;
			Action<UM_TBM_MatchResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref MatchUpdatedEvent__BackingField, (Action<UM_TBM_MatchResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_TBM_MatchResult> action = MatchUpdatedEvent__BackingField;
			Action<UM_TBM_MatchResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref MatchUpdatedEvent__BackingField, (Action<UM_TBM_MatchResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<UM_TBM_MatchesLoadResult> MatchesListLoadedEvent
	{
		add
		{
			Action<UM_TBM_MatchesLoadResult> action = MatchesListLoadedEvent__BackingField;
			Action<UM_TBM_MatchesLoadResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref MatchesListLoadedEvent__BackingField, (Action<UM_TBM_MatchesLoadResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_TBM_MatchesLoadResult> action = MatchesListLoadedEvent__BackingField;
			Action<UM_TBM_MatchesLoadResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref MatchesListLoadedEvent__BackingField, (Action<UM_TBM_MatchesLoadResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action MatchesListUpdated
	{
		add
		{
			Action action = MatchesListUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref MatchesListUpdated__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = MatchesListUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref MatchesListUpdated__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public GP_TBM_Controller()
	{
		GooglePlayTBM.ActionMatchInitiated += HandleActionMatchInitiated;
		GooglePlayTBM.ActionMatchUpdated += HandleActionMatchUpdated;
		GooglePlayTBM.ActionMatchDataLoaded += HandleActionMatchDataLoaded;
		GooglePlayTBM.ActionMatchLeaved += HandleActionMatchLeaved;
		GooglePlayTBM.ActionMatchTurnFinished += HandleActionTurnFinished;
		GooglePlayInvitationManager.ActionInvitationReceived += HandleActionInvitationReceived;
		GooglePlayInvitationManager.ActionInvitationAccepted += HandleActionInvitationAccepted;
		GooglePlayInvitationManager.ActionInvitationsListLoaded += HandleActionInvitationsListLoaded;
		GooglePlayTBM.ActionMatchesResultLoaded += HandleActionMatchesResultLoaded;
		GooglePlayTBM.ActionMatchInvitationAccepted += HandleActionMatchInvitationAccepted;
		GooglePlayTBM.ActionMatchInvitationDeclined += HandleActionMatchInvitationDeclined;
		GooglePlayConnection.ActionPlayerConnected += HandleActionPlayerConnected;
	}

	public void SetGroup(int group)
	{
		Singleton<GooglePlayTBM>.Instance.SetVariant(group);
	}

	public void SetMask(int mask)
	{
		Singleton<GooglePlayTBM>.Instance.SetExclusiveBitMask(mask);
	}

	public void FindMatch(int minPlayers, int maxPlayers, string[] recipients = null)
	{
		Singleton<GooglePlayTBM>.Instance.CreateMatch(minPlayers - 1, maxPlayers - 1, recipients);
	}

	public void ShowNativeFindMatchUI(int minPlayers, int maxPlayers)
	{
		Singleton<GooglePlayTBM>.Instance.StartSelectOpponentsView(minPlayers - 1, maxPlayers - 1, true);
	}

	public void LoadMatchesInfo()
	{
		if (DataEventCount == 0)
		{
			DataEventCount = 2;
			List<GP_TBM_MatchTurnStatus> list = new List<GP_TBM_MatchTurnStatus>();
			list.Add(GP_TBM_MatchTurnStatus.MATCH_TURN_STATUS_MY_TURN);
			list.Add(GP_TBM_MatchTurnStatus.MATCH_TURN_STATUS_COMPLETE);
			list.Add(GP_TBM_MatchTurnStatus.MATCH_TURN_STATUS_INVITED);
			list.Add(GP_TBM_MatchTurnStatus.MATCH_TURN_STATUS_THEIR_TURN);
			Singleton<GooglePlayTBM>.Instance.LoadMatchesInfo(GP_TBM_MatchesSortOrder.SORT_ORDER_MOST_RECENT_FIRST, list.ToArray());
			Singleton<GooglePlayInvitationManager>.Instance.LoadInvitations();
		}
	}

	private void CheckDataCounter()
	{
		DataEventCount--;
		if (DataEventCount == 0)
		{
			UM_TBM_MatchesLoadResult uM_TBM_MatchesLoadResult = new UM_TBM_MatchesLoadResult(new GooglePlayResult(GP_GamesStatusCodes.STATUS_OK));
			uM_TBM_MatchesLoadResult.SetMatches(_Matches);
			uM_TBM_MatchesLoadResult.SetInvitations(_Invitations);
			MatchesListUpdated__BackingField();
			MatchesListLoadedEvent__BackingField(uM_TBM_MatchesLoadResult);
		}
	}

	public void LoadMatch(string matchId)
	{
		Singleton<GooglePlayTBM>.Instance.LoadMatchInfo(matchId);
	}

	public void TakeTurn(string matchId, byte[] matchData, UM_TBM_Participant nextParticipant)
	{
		string pendingParticipantId = string.Empty;
		if (nextParticipant != null)
		{
			pendingParticipantId = nextParticipant.Id;
		}
		Singleton<GooglePlayTBM>.Instance.TakeTrun(matchId, matchData, pendingParticipantId);
	}

	public void QuitInTurn(string matchId, UM_TBM_Participant nextParticipant)
	{
		string pendingParticipantId = string.Empty;
		if (nextParticipant != null)
		{
			pendingParticipantId = nextParticipant.Id;
		}
		Singleton<GooglePlayTBM>.Instance.LeaveMatchDuringTurn(matchId, pendingParticipantId);
	}

	public void QuitOutOfTurn(string matchId)
	{
		Singleton<GooglePlayTBM>.Instance.LeaveMatch(matchId);
	}

	public void RemoveMatch(string matchId)
	{
		Singleton<GooglePlayTBM>.Instance.DismissMatch(matchId);
		RemoveMatchFromTheList(matchId);
	}

	public void FinishMatch(string matchId, byte[] matchData, params UM_TMB_ParticipantResult[] results)
	{
		List<GP_ParticipantResult> list = new List<GP_ParticipantResult>();
		foreach (UM_TMB_ParticipantResult uM_TMB_ParticipantResult in results)
		{
			GP_TBM_ParticipantResult result = GP_TBM_ParticipantResult.MATCH_RESULT_UNINITIALIZED;
			switch (uM_TMB_ParticipantResult.Outcome)
			{
			case UM_TBM_Outcome.Won:
				result = GP_TBM_ParticipantResult.MATCH_RESULT_WIN;
				break;
			case UM_TBM_Outcome.Lost:
				result = GP_TBM_ParticipantResult.MATCH_RESULT_LOSS;
				break;
			case UM_TBM_Outcome.Tied:
				result = GP_TBM_ParticipantResult.MATCH_RESULT_TIE;
				break;
			case UM_TBM_Outcome.Disconnected:
				result = GP_TBM_ParticipantResult.MATCH_RESULT_DISCONNECT;
				break;
			case UM_TBM_Outcome.None:
				result = GP_TBM_ParticipantResult.MATCH_RESULT_UNINITIALIZED;
				break;
			}
			GP_ParticipantResult item = new GP_ParticipantResult(uM_TMB_ParticipantResult.ParticipantId, result, -1);
			list.Add(item);
		}
		Singleton<GooglePlayTBM>.Instance.FinishMatch(matchId, matchData, list.ToArray());
	}

	public void ConfirmhMatchFinis(string matchId)
	{
		Singleton<GooglePlayTBM>.Instance.ConfirmMatchFinish(matchId);
	}

	public void Rematch(string matchId)
	{
		Singleton<GooglePlayTBM>.Instance.Rematch(matchId);
	}

	public void AcceptInvite(UM_TBM_Invite invite)
	{
		Singleton<GooglePlayTBM>.Instance.AcceptInvitation(invite.Id);
	}

	public void DeclineInvite(UM_TBM_Invite invite)
	{
		Singleton<GooglePlayTBM>.Instance.DeclineInvitation(invite.Id);
	}

	private void HandleActionMatchInitiated(GP_TBM_MatchInitiatedResult res)
	{
		UM_TBM_MatchResult uM_TBM_MatchResult = new UM_TBM_MatchResult(res);
		if (res.Match != null)
		{
			UM_TBM_Match match = new UM_TBM_Match(res.Match);
			uM_TBM_MatchResult.SetMatch(match);
			UpdateMatchData(match);
		}
		MatchFoundEvent__BackingField(uM_TBM_MatchResult);
	}

	private void HandleActionMatchDataLoaded(GP_TBM_LoadMatchResult res)
	{
		UM_TBM_MatchResult uM_TBM_MatchResult = new UM_TBM_MatchResult(res);
		if (res.Match != null)
		{
			UM_TBM_Match match = new UM_TBM_Match(res.Match);
			uM_TBM_MatchResult.SetMatch(match);
			UpdateMatchData(match);
		}
		MatchLoadedEvent__BackingField(uM_TBM_MatchResult);
	}

	private void HandleActionMatchesResultLoaded(GP_TBM_LoadMatchesResult res)
	{
		_Matches.Clear();
		if (res.IsSucceeded)
		{
			foreach (KeyValuePair<string, GP_TBM_Match> loadedMatch in res.LoadedMatches)
			{
				GP_TBM_Match value = loadedMatch.Value;
				UM_TBM_Match match = new UM_TBM_Match(value);
				UpdateMatchData(match);
			}
		}
		CheckDataCounter();
	}

	private void HandleActionInvitationsListLoaded(List<GP_Invite> res)
	{
		_Invitations.Clear();
		foreach (GP_Invite re in res)
		{
			UM_TBM_Invite item = new UM_TBM_Invite(re);
			_Invitations.Add(item);
		}
		CheckDataCounter();
	}

	private void HandleActionMatchUpdated(GP_TBM_UpdateMatchResult res)
	{
		UM_TBM_MatchResult uM_TBM_MatchResult = new UM_TBM_MatchResult(res);
		if (res.Match != null)
		{
			UM_TBM_Match match = new UM_TBM_Match(res.Match);
			uM_TBM_MatchResult.SetMatch(match);
			UpdateMatchData(match);
		}
		MatchUpdatedEvent__BackingField(uM_TBM_MatchResult);
	}

	private void HandleActionTurnFinished(GP_TBM_UpdateMatchResult res)
	{
		UM_TBM_MatchResult uM_TBM_MatchResult = new UM_TBM_MatchResult(res);
		if (res.Match != null)
		{
			UM_TBM_Match match = new UM_TBM_Match(res.Match);
			uM_TBM_MatchResult.SetMatch(match);
			UpdateMatchData(match);
		}
		TurnEndedEvent__BackingField(uM_TBM_MatchResult);
	}

	private void HandleActionPlayerConnected()
	{
		Singleton<GooglePlayTBM>.Instance.RegisterMatchUpdateListener();
		Singleton<GooglePlayInvitationManager>.Instance.RegisterInvitationListener();
	}

	private void HandleActionInvitationAccepted(GP_Invite invite)
	{
		UnityEngine.Debug.Log("GP_TBM_Controller::HandleActionInvitationAccepted");
	}

	private void HandleActionInvitationReceived(GP_Invite invite)
	{
		UnityEngine.Debug.Log("GP_TBM_Controller::HandleActionInvitationReceived");
		LoadMatchesInfo();
	}

	private void HandleActionMatchInvitationDeclined(string invitationId)
	{
		RemoveInvitationsFromTheList(invitationId);
		MatchesListUpdated__BackingField();
		InvitationDeclined__BackingField(invitationId);
	}

	private void HandleActionMatchInvitationAccepted(string invitationId, GP_TBM_MatchInitiatedResult res)
	{
		UnityEngine.Debug.Log("GP_TBM_Controller::HandleActionMatchInvitationAccepted");
		UM_TBM_MatchResult uM_TBM_MatchResult = new UM_TBM_MatchResult(res);
		if (res.IsSucceeded)
		{
			RemoveInvitationsFromTheList(invitationId);
			UM_TBM_Match match = new UM_TBM_Match(res.Match);
			uM_TBM_MatchResult.SetMatch(match);
			UpdateMatchData(match);
			UnityEngine.Debug.Log("GP_TBM_Controller::HandleActionMatchInvitationAccepted, list updated");
		}
		InvitationAccepted__BackingField(uM_TBM_MatchResult);
	}

	private void HandleActionMatchLeaved(GP_TBM_LeaveMatchResult res)
	{
		if (res.IsSucceeded)
		{
			RemoveMatchFromTheList(res.MatchId);
		}
	}

	private void UpdateMatchData(UM_TBM_Match match)
	{
		bool flag = false;
		if (match.IsEnded && match.IsLocalPlayerTurn)
		{
			Singleton<GooglePlayTBM>.Instance.ConfirmMatchFinish(match.Id);
		}
		for (int i = 0; i < Matches.Count; i++)
		{
			if (Matches[i].Id.Equals(match.Id))
			{
				flag = true;
				Matches[i] = match;
			}
		}
		if (!flag)
		{
			Matches.Add(match);
		}
		MatchesListUpdated__BackingField();
	}

	private void RemoveMatchFromTheList(string matchId)
	{
		foreach (UM_TBM_Match match in _Matches)
		{
			if (match.Id.Equals(matchId))
			{
				_Matches.Remove(match);
				MatchesListUpdated__BackingField();
				break;
			}
		}
	}

	private void RemoveInvitationsFromTheList(string inviteId)
	{
		foreach (UM_TBM_Invite invitation in _Invitations)
		{
			if (invitation.Id.Equals(inviteId))
			{
				_Invitations.Remove(invitation);
				break;
			}
		}
	}
}
