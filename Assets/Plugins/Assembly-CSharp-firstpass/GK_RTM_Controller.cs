using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;

public class GK_RTM_Controller : iRTM_Matchmaker
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

	public GK_RTM_Controller()
	{
		GameCenterInvitations.ActionPlayerAcceptedInvitation += HandleActionPlayerAcceptedInvitation;
		GameCenter_RTM.ActionMatchStarted += HandleActionRoomCreated;
		GameCenter_RTM.ActionDataReceived += HandleActionMatchDataReceived;
	}

	private void HandleActionRoomCreated(GK_RTM_MatchStartedResult result)
	{
		if (result.IsSucceeded)
		{
			_CurrentRoom = new UM_RTM_Room(result.Match);
		}
		UM_RTM_RoomCreatedResult obj = new UM_RTM_RoomCreatedResult(result);
		RoomCreated__BackingField(obj);
	}

	private void HandleActionMatchDataReceived(GK_Player sender, byte[] data)
	{
		MatchDataReceived__BackingField(sender.Id, data);
	}

	private void HandleActionPlayerAcceptedInvitation(GK_MatchType type, GK_Invite invite)
	{
		if (type == GK_MatchType.RealTime)
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

	public void OpenInvitationUI(int minPlayers, int maxPlayers)
	{
		Singleton<GameCenter_RTM>.Instance.FindMatchWithNativeUI(minPlayers, maxPlayers, string.Empty);
	}

	public void AcceptInvite(UM_RTM_Invite invite)
	{
	}

	public void DeclineInvite(UM_RTM_Invite invite)
	{
	}

	public void FindMatch(int minPlayers, int maxPlayers)
	{
		Singleton<GameCenter_RTM>.Instance.FindMatch(minPlayers, maxPlayers, string.Empty);
	}

	public void SendDataToAll(byte[] data, UM_RTM_PackageType type)
	{
		Singleton<GameCenter_RTM>.Instance.SendData(data, type.GetGKPackageType());
	}

	public void SendDataToPlayer(byte[] data, UM_RTM_PackageType type, params string[] receivers)
	{
	}

	public void LeaveMatch()
	{
		Singleton<GameCenter_RTM>.Instance.Disconnect();
	}
}
