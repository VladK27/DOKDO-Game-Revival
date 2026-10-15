using System;
using System.Collections.Generic;

public interface iTBM_Matchmaker
{
	List<UM_TBM_Match> Matches { get; }

	List<UM_TBM_Invite> Invitations { get; }

	event Action<UM_TBM_MatchResult> MatchFoundEvent;

	event Action<UM_TBM_MatchResult> MatchLoadedEvent;

	event Action<UM_TBM_MatchResult> InvitationAccepted;

	event Action<string> InvitationDeclined;

	event Action<UM_TBM_MatchResult> MatchUpdatedEvent;

	event Action<UM_TBM_MatchResult> TurnEndedEvent;

	event Action<UM_TBM_MatchesLoadResult> MatchesListLoadedEvent;

	event Action MatchesListUpdated;

	void SetGroup(int group);

	void SetMask(int mask);

	void FindMatch(int minPlayers, int maxPlayers, string[] recipients = null);

	void ShowNativeFindMatchUI(int minPlayers, int maxPlayers);

	void LoadMatchesInfo();

	void LoadMatch(string matchId);

	void RemoveMatch(string matchId);

	void TakeTurn(string matchId, byte[] matchData, UM_TBM_Participant nextParticipant);

	void QuitInTurn(string matchId, UM_TBM_Participant nextParticipant);

	void QuitOutOfTurn(string matchId);

	void Rematch(string matchId);

	void FinishMatch(string matchId, byte[] matchData, params UM_TMB_ParticipantResult[] results);

	void AcceptInvite(UM_TBM_Invite invite);

	void DeclineInvite(UM_TBM_Invite invite);
}
