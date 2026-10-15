using System;
using System.Collections.Generic;

public interface iRTM_Matchmaker
{
	List<UM_RTM_Invite> Invitations { get; }

	UM_RTM_Room CurrentRoom { get; }

	event Action<UM_RTM_Invite> InvitationReceived;

	event Action<UM_RTM_Invite> InvitationAccepted;

	event Action<string> InvitationDeclined;

	event Action<UM_RTM_RoomCreatedResult> RoomCreated;

	event Action RoomUpdated;

	event Action<string, byte[]> MatchDataReceived;

	void OpenInvitationUI(int minPlayers, int maxPlayers);

	void AcceptInvite(UM_RTM_Invite invite);

	void DeclineInvite(UM_RTM_Invite invite);

	void FindMatch(int minPlayers, int maxPlayers);

	void SendDataToAll(byte[] data, UM_RTM_PackageType type);

	void SendDataToPlayer(byte[] data, UM_RTM_PackageType type, params string[] receivers);

	void LeaveMatch();
}
