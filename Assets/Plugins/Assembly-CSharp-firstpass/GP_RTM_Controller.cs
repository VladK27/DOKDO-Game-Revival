using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;

public class GP_RTM_Controller : iRTM_Matchmaker
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<UM_RTM_Invite> InvitationReceived__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<UM_RTM_Invite> InvitationAccepted__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<string> InvitationDeclined__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<UM_RTM_RoomCreatedResult> RoomCreated__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action RoomUpdated__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<string, byte[]> MatchDataReceived__BackingField = delegate
	{
	};

	private List<UM_RTM_Invite> _Invitations = new List<UM_RTM_Invite>();

	private UM_RTM_Room _CurrentRoom = new UM_RTM_Room();

	public List<UM_RTM_Invite> Invitations
	{
		get
		{
			return _Invitations;
		}
	}

	public UM_RTM_Room CurrentRoom
	{
		get
		{
			return _CurrentRoom;
		}
	}

	public event Action<UM_RTM_Invite> InvitationReceived
	{
		add
		{
			Action<UM_RTM_Invite> action = InvitationReceived__BackingField;
			Action<UM_RTM_Invite> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref InvitationReceived__BackingField, (Action<UM_RTM_Invite>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_RTM_Invite> action = InvitationReceived__BackingField;
			Action<UM_RTM_Invite> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref InvitationReceived__BackingField, (Action<UM_RTM_Invite>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<UM_RTM_Invite> InvitationAccepted
	{
		add
		{
			Action<UM_RTM_Invite> action = InvitationAccepted__BackingField;
			Action<UM_RTM_Invite> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref InvitationAccepted__BackingField, (Action<UM_RTM_Invite>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_RTM_Invite> action = InvitationAccepted__BackingField;
			Action<UM_RTM_Invite> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref InvitationAccepted__BackingField, (Action<UM_RTM_Invite>)Delegate.Remove(action2, value), action);
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

	public event Action<UM_RTM_RoomCreatedResult> RoomCreated
	{
		add
		{
			Action<UM_RTM_RoomCreatedResult> action = RoomCreated__BackingField;
			Action<UM_RTM_RoomCreatedResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref RoomCreated__BackingField, (Action<UM_RTM_RoomCreatedResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_RTM_RoomCreatedResult> action = RoomCreated__BackingField;
			Action<UM_RTM_RoomCreatedResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref RoomCreated__BackingField, (Action<UM_RTM_RoomCreatedResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action RoomUpdated
	{
		add
		{
			Action action = RoomUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref RoomUpdated__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = RoomUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref RoomUpdated__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<string, byte[]> MatchDataReceived
	{
		add
		{
			Action<string, byte[]> action = MatchDataReceived__BackingField;
			Action<string, byte[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref MatchDataReceived__BackingField, (Action<string, byte[]>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string, byte[]> action = MatchDataReceived__BackingField;
			Action<string, byte[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref MatchDataReceived__BackingField, (Action<string, byte[]>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public GP_RTM_Controller()
	{
		GooglePlayRTM.ActionInvitationReceived += HandleActionInvitationReceived;
		GooglePlayRTM.ActionInvitationRemoved += HandleActionInvitationRemoved;
		GooglePlayRTM.ActionInvitationAccepted += HandleActionInvitationAccepted;
		GooglePlayRTM.ActionRoomCreated += HandleActionRoomCreated;
		GooglePlayRTM.ActionDataRecieved += HandleActionMatchDataReceived;
		GooglePlayRTM.ActionRoomUpdated += HandleActionRoomUpdated;
		GooglePlayConnection.ActionPlayerConnected += HandleActionPlayerConnected;
	}

	public void OpenInvitationUI(int minPlayers, int maxPlayers)
	{
		Singleton<GooglePlayRTM>.Instance.OpenInvitationBoxUI(minPlayers, maxPlayers);
	}

	public void AcceptInvite(UM_RTM_Invite invite)
	{
		Singleton<GooglePlayRTM>.Instance.AcceptInvitation(invite.Id);
	}

	public void DeclineInvite(UM_RTM_Invite invite)
	{
		Singleton<GooglePlayRTM>.Instance.DeclineInvitation(invite.Id);
	}

	public void FindMatch(int minPlayers, int maxPlayers)
	{
		Singleton<GooglePlayRTM>.Instance.FindMatch(minPlayers, maxPlayers);
	}

	public void SendDataToAll(byte[] data, UM_RTM_PackageType type)
	{
		Singleton<GooglePlayRTM>.Instance.SendDataToAll(data, type.GetGPPackageType());
	}

	public void SendDataToPlayer(byte[] data, UM_RTM_PackageType type, params string[] receivers)
	{
		Singleton<GooglePlayRTM>.Instance.SendDataToPlayers(data, type.GetGPPackageType(), receivers);
	}

	public void LeaveMatch()
	{
		Singleton<GooglePlayRTM>.Instance.LeaveRoom();
	}

	private void HandleActionRoomUpdated(GP_RTM_Room room)
	{
		_CurrentRoom = new UM_RTM_Room(room);
		RoomUpdated__BackingField();
	}

	private void HandleActionMatchDataReceived(GP_RTM_Network_Package package)
	{
		MatchDataReceived__BackingField(package.participantId, package.buffer);
	}

	private void HandleActionRoomCreated(GP_GamesStatusCodes status)
	{
		UM_RTM_RoomCreatedResult obj = new UM_RTM_RoomCreatedResult(status);
		_CurrentRoom = new UM_RTM_Room(Singleton<GooglePlayRTM>.Instance.currentRoom);
		RoomCreated__BackingField(obj);
	}

	private void HandleActionPlayerConnected()
	{
		Singleton<GooglePlayInvitationManager>.Instance.RegisterInvitationListener();
	}

	private void HandleActionInvitationReceived(GP_Invite invite)
	{
		UM_RTM_Invite uM_RTM_Invite = new UM_RTM_Invite(invite);
		_Invitations.Add(uM_RTM_Invite);
		InvitationReceived__BackingField(uM_RTM_Invite);
	}

	private void HandleActionInvitationRemoved(string id)
	{
		RemoveInvitation(id);
		InvitationDeclined__BackingField(id);
	}

	private void HandleActionInvitationAccepted(GP_Invite invite)
	{
		if (invite.InvitationType == GP_InvitationType.INVITATION_TYPE_REAL_TIME)
		{
			UM_RTM_Invite invite2 = null;
			if (!TryGetInvitation(invite.Id, out invite2))
			{
				invite2 = new UM_RTM_Invite(invite);
				_Invitations.Add(invite2);
			}
			InvitationAccepted__BackingField(invite2);
		}
	}

	private bool TryGetInvitation(string id, out UM_RTM_Invite invite)
	{
		invite = null;
		foreach (UM_RTM_Invite invitation in _Invitations)
		{
			if (invitation.Id.Equals(id))
			{
				invite = invitation;
				return true;
			}
		}
		return false;
	}

	private void RemoveInvitation(string id)
	{
		foreach (UM_RTM_Invite invitation in _Invitations)
		{
			if (invitation.Id.Equals(id))
			{
				_Invitations.Remove(invitation);
				break;
			}
		}
	}
}
