using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using SA.Common.Util;
using UnityEngine;

public class GooglePlayRTM : Singleton<GooglePlayRTM>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GP_RTM_Network_Package> ActionDataRecieved__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GP_RTM_Room> ActionRoomUpdated__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GP_RTM_ReliableMessageSentResult> ActionReliableMessageSent__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GP_RTM_ReliableMessageDeliveredResult> ActionReliableMessageDelivered__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action ActionConnectedToRoom__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action ActionDisconnectedFromRoom__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string> ActionP2PConnected__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string> ActionP2PDisconnected__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string[]> ActionPeerDeclined__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string[]> ActionPeerInvitedToRoom__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string[]> ActionPeerJoined__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string[]> ActionPeerLeft__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string[]> ActionPeersConnected__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string[]> ActionPeersDisconnected__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action ActionRoomAutomatching__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action ActionRoomConnecting__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GP_GamesStatusCodes> ActionJoinedRoom__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GP_RTM_Result> ActionLeftRoom__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GP_GamesStatusCodes> ActionRoomConnected__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GP_GamesStatusCodes> ActionRoomCreated__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<AndroidActivityResult> ActionInvitationBoxUIClosed__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<AndroidActivityResult> ActionWatingRoomIntentClosed__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GP_Invite> ActionInvitationAccepted__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GP_Invite> ActionInvitationReceived__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string> ActionInvitationRemoved__BackingField = delegate
	{
	};

	private const int BYTE_LIMIT = 256;

	private GP_RTM_Room _currentRoom = new GP_RTM_Room();

	private List<GP_Invite> _invitations = new List<GP_Invite>();

	private Dictionary<int, GP_RTM_ReliableMessageListener> _ReliableMassageListeners = new Dictionary<int, GP_RTM_ReliableMessageListener>();

	public GP_RTM_Room currentRoom
	{
		get
		{
			return _currentRoom;
		}
	}

	public List<GP_Invite> invitations
	{
		get
		{
			return _invitations;
		}
	}

	public static event Action<GP_RTM_Network_Package> ActionDataRecieved
	{
		add
		{
			Action<GP_RTM_Network_Package> action = ActionDataRecieved__BackingField;
			Action<GP_RTM_Network_Package> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionDataRecieved__BackingField, (Action<GP_RTM_Network_Package>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GP_RTM_Network_Package> action = ActionDataRecieved__BackingField;
			Action<GP_RTM_Network_Package> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionDataRecieved__BackingField, (Action<GP_RTM_Network_Package>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GP_RTM_Room> ActionRoomUpdated
	{
		add
		{
			Action<GP_RTM_Room> action = ActionRoomUpdated__BackingField;
			Action<GP_RTM_Room> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRoomUpdated__BackingField, (Action<GP_RTM_Room>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GP_RTM_Room> action = ActionRoomUpdated__BackingField;
			Action<GP_RTM_Room> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRoomUpdated__BackingField, (Action<GP_RTM_Room>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GP_RTM_ReliableMessageSentResult> ActionReliableMessageSent
	{
		add
		{
			Action<GP_RTM_ReliableMessageSentResult> action = ActionReliableMessageSent__BackingField;
			Action<GP_RTM_ReliableMessageSentResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionReliableMessageSent__BackingField, (Action<GP_RTM_ReliableMessageSentResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GP_RTM_ReliableMessageSentResult> action = ActionReliableMessageSent__BackingField;
			Action<GP_RTM_ReliableMessageSentResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionReliableMessageSent__BackingField, (Action<GP_RTM_ReliableMessageSentResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GP_RTM_ReliableMessageDeliveredResult> ActionReliableMessageDelivered
	{
		add
		{
			Action<GP_RTM_ReliableMessageDeliveredResult> action = ActionReliableMessageDelivered__BackingField;
			Action<GP_RTM_ReliableMessageDeliveredResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionReliableMessageDelivered__BackingField, (Action<GP_RTM_ReliableMessageDeliveredResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GP_RTM_ReliableMessageDeliveredResult> action = ActionReliableMessageDelivered__BackingField;
			Action<GP_RTM_ReliableMessageDeliveredResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionReliableMessageDelivered__BackingField, (Action<GP_RTM_ReliableMessageDeliveredResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action ActionConnectedToRoom
	{
		add
		{
			Action action = ActionConnectedToRoom__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionConnectedToRoom__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ActionConnectedToRoom__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionConnectedToRoom__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action ActionDisconnectedFromRoom
	{
		add
		{
			Action action = ActionDisconnectedFromRoom__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionDisconnectedFromRoom__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ActionDisconnectedFromRoom__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionDisconnectedFromRoom__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string> ActionP2PConnected
	{
		add
		{
			Action<string> action = ActionP2PConnected__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionP2PConnected__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = ActionP2PConnected__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionP2PConnected__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string> ActionP2PDisconnected
	{
		add
		{
			Action<string> action = ActionP2PDisconnected__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionP2PDisconnected__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = ActionP2PDisconnected__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionP2PDisconnected__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string[]> ActionPeerDeclined
	{
		add
		{
			Action<string[]> action = ActionPeerDeclined__BackingField;
			Action<string[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPeerDeclined__BackingField, (Action<string[]>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string[]> action = ActionPeerDeclined__BackingField;
			Action<string[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPeerDeclined__BackingField, (Action<string[]>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string[]> ActionPeerInvitedToRoom
	{
		add
		{
			Action<string[]> action = ActionPeerInvitedToRoom__BackingField;
			Action<string[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPeerInvitedToRoom__BackingField, (Action<string[]>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string[]> action = ActionPeerInvitedToRoom__BackingField;
			Action<string[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPeerInvitedToRoom__BackingField, (Action<string[]>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string[]> ActionPeerJoined
	{
		add
		{
			Action<string[]> action = ActionPeerJoined__BackingField;
			Action<string[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPeerJoined__BackingField, (Action<string[]>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string[]> action = ActionPeerJoined__BackingField;
			Action<string[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPeerJoined__BackingField, (Action<string[]>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string[]> ActionPeerLeft
	{
		add
		{
			Action<string[]> action = ActionPeerLeft__BackingField;
			Action<string[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPeerLeft__BackingField, (Action<string[]>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string[]> action = ActionPeerLeft__BackingField;
			Action<string[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPeerLeft__BackingField, (Action<string[]>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string[]> ActionPeersConnected
	{
		add
		{
			Action<string[]> action = ActionPeersConnected__BackingField;
			Action<string[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPeersConnected__BackingField, (Action<string[]>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string[]> action = ActionPeersConnected__BackingField;
			Action<string[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPeersConnected__BackingField, (Action<string[]>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string[]> ActionPeersDisconnected
	{
		add
		{
			Action<string[]> action = ActionPeersDisconnected__BackingField;
			Action<string[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPeersDisconnected__BackingField, (Action<string[]>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string[]> action = ActionPeersDisconnected__BackingField;
			Action<string[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPeersDisconnected__BackingField, (Action<string[]>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action ActionRoomAutomatching
	{
		add
		{
			Action action = ActionRoomAutomatching__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRoomAutomatching__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ActionRoomAutomatching__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRoomAutomatching__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action ActionRoomConnecting
	{
		add
		{
			Action action = ActionRoomConnecting__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRoomConnecting__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ActionRoomConnecting__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRoomConnecting__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GP_GamesStatusCodes> ActionJoinedRoom
	{
		add
		{
			Action<GP_GamesStatusCodes> action = ActionJoinedRoom__BackingField;
			Action<GP_GamesStatusCodes> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionJoinedRoom__BackingField, (Action<GP_GamesStatusCodes>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GP_GamesStatusCodes> action = ActionJoinedRoom__BackingField;
			Action<GP_GamesStatusCodes> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionJoinedRoom__BackingField, (Action<GP_GamesStatusCodes>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GP_RTM_Result> ActionLeftRoom
	{
		add
		{
			Action<GP_RTM_Result> action = ActionLeftRoom__BackingField;
			Action<GP_RTM_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionLeftRoom__BackingField, (Action<GP_RTM_Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GP_RTM_Result> action = ActionLeftRoom__BackingField;
			Action<GP_RTM_Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionLeftRoom__BackingField, (Action<GP_RTM_Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GP_GamesStatusCodes> ActionRoomConnected
	{
		add
		{
			Action<GP_GamesStatusCodes> action = ActionRoomConnected__BackingField;
			Action<GP_GamesStatusCodes> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRoomConnected__BackingField, (Action<GP_GamesStatusCodes>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GP_GamesStatusCodes> action = ActionRoomConnected__BackingField;
			Action<GP_GamesStatusCodes> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRoomConnected__BackingField, (Action<GP_GamesStatusCodes>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GP_GamesStatusCodes> ActionRoomCreated
	{
		add
		{
			Action<GP_GamesStatusCodes> action = ActionRoomCreated__BackingField;
			Action<GP_GamesStatusCodes> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRoomCreated__BackingField, (Action<GP_GamesStatusCodes>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GP_GamesStatusCodes> action = ActionRoomCreated__BackingField;
			Action<GP_GamesStatusCodes> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRoomCreated__BackingField, (Action<GP_GamesStatusCodes>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<AndroidActivityResult> ActionInvitationBoxUIClosed
	{
		add
		{
			Action<AndroidActivityResult> action = ActionInvitationBoxUIClosed__BackingField;
			Action<AndroidActivityResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionInvitationBoxUIClosed__BackingField, (Action<AndroidActivityResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AndroidActivityResult> action = ActionInvitationBoxUIClosed__BackingField;
			Action<AndroidActivityResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionInvitationBoxUIClosed__BackingField, (Action<AndroidActivityResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<AndroidActivityResult> ActionWatingRoomIntentClosed
	{
		add
		{
			Action<AndroidActivityResult> action = ActionWatingRoomIntentClosed__BackingField;
			Action<AndroidActivityResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionWatingRoomIntentClosed__BackingField, (Action<AndroidActivityResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AndroidActivityResult> action = ActionWatingRoomIntentClosed__BackingField;
			Action<AndroidActivityResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionWatingRoomIntentClosed__BackingField, (Action<AndroidActivityResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GP_Invite> ActionInvitationAccepted
	{
		add
		{
			Action<GP_Invite> action = ActionInvitationAccepted__BackingField;
			Action<GP_Invite> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionInvitationAccepted__BackingField, (Action<GP_Invite>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GP_Invite> action = ActionInvitationAccepted__BackingField;
			Action<GP_Invite> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionInvitationAccepted__BackingField, (Action<GP_Invite>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GP_Invite> ActionInvitationReceived
	{
		add
		{
			Action<GP_Invite> action = ActionInvitationReceived__BackingField;
			Action<GP_Invite> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionInvitationReceived__BackingField, (Action<GP_Invite>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GP_Invite> action = ActionInvitationReceived__BackingField;
			Action<GP_Invite> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionInvitationReceived__BackingField, (Action<GP_Invite>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string> ActionInvitationRemoved
	{
		add
		{
			Action<string> action = ActionInvitationRemoved__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionInvitationRemoved__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = ActionInvitationRemoved__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionInvitationRemoved__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		_currentRoom = new GP_RTM_Room();
		GooglePlayInvitationManager.ActionInvitationReceived += OnInvitationReceived;
		GooglePlayInvitationManager.ActionInvitationRemoved += OnInvitationRemoved;
		GooglePlayInvitationManager.ActionInvitationAccepted += OnInvitationAccepted;
		Singleton<GooglePlayInvitationManager>.Instance.Init();
		UnityEngine.Debug.Log("GooglePlayRTM Created");
	}

	public void FindMatch(int minPlayers, int maxPlayers)
	{
		FindMatch(minPlayers, maxPlayers, new string[0]);
	}

	public void FindMatch(int minPlayers, int maxPlayers, params GooglePlayerTemplate[] playersToInvite)
	{
		List<string> list = new List<string>();
		foreach (GooglePlayerTemplate googlePlayerTemplate in playersToInvite)
		{
			list.Add(googlePlayerTemplate.playerId);
		}
		AN_GMSRTMProxy.RTMFindMatch(minPlayers, maxPlayers, list.ToArray());
	}

	public void FindMatch(int minPlayers, int maxPlayers, params string[] playersToInvite)
	{
		AN_GMSRTMProxy.RTMFindMatch(minPlayers, maxPlayers, playersToInvite);
	}

	public void FindMatch(GooglePlayerTemplate[] playersToInvite)
	{
		List<string> list = new List<string>();
		foreach (GooglePlayerTemplate googlePlayerTemplate in playersToInvite)
		{
			list.Add(googlePlayerTemplate.playerId);
		}
		AN_GMSRTMProxy.RTMFindMatch(list.ToArray());
	}

	public void FindMatch(string[] playersToInvite)
	{
		AN_GMSRTMProxy.RTMFindMatch(playersToInvite);
	}

	public void SendDataToAll(byte[] data, GP_RTM_PackageType sendType)
	{
		string data2 = Convert.ToBase64String(data);
		switch (sendType)
		{
		case GP_RTM_PackageType.RELIABLE:
		{
			GP_RTM_ReliableMessageListener gP_RTM_ReliableMessageListener = new GP_RTM_ReliableMessageListener(IdFactory.NextId, data);
			_ReliableMassageListeners.Add(gP_RTM_ReliableMessageListener.DataTokenId, gP_RTM_ReliableMessageListener);
			AN_GMSRTMProxy.sendDataToAll(data2, (int)sendType, gP_RTM_ReliableMessageListener.DataTokenId);
			break;
		}
		case GP_RTM_PackageType.UNRELIABLE:
			AN_GMSRTMProxy.sendDataToAll(data2, (int)sendType);
			break;
		}
	}

	public void SendDataToPlayers(byte[] data, GP_RTM_PackageType sendType, params string[] players)
	{
		string data2 = Convert.ToBase64String(data);
		string players2 = string.Join("|", players);
		switch (sendType)
		{
		case GP_RTM_PackageType.RELIABLE:
		{
			GP_RTM_ReliableMessageListener gP_RTM_ReliableMessageListener = new GP_RTM_ReliableMessageListener(IdFactory.NextId, data);
			_ReliableMassageListeners.Add(gP_RTM_ReliableMessageListener.DataTokenId, gP_RTM_ReliableMessageListener);
			AN_GMSRTMProxy.sendDataToPlayers(data2, players2, (int)sendType, gP_RTM_ReliableMessageListener.DataTokenId);
			break;
		}
		case GP_RTM_PackageType.UNRELIABLE:
			AN_GMSRTMProxy.sendDataToPlayers(data2, players2, (int)sendType);
			break;
		}
	}

	public void ShowWaitingRoomIntent()
	{
		AN_GMSRTMProxy.ShowWaitingRoomIntent();
	}

	public void OpenInvitationBoxUI(int minPlayers, int maxPlayers)
	{
		AN_GMSRTMProxy.InvitePlayers(minPlayers, maxPlayers);
	}

	public void LeaveRoom()
	{
		AN_GMSGiftsProxy.leaveRoom();
	}

	public void AcceptInvitation(string invitationId)
	{
		AN_GMSRTMProxy.RTM_AcceptInvitation(invitationId);
	}

	public void DeclineInvitation(string invitationId)
	{
		AN_GMSRTMProxy.RTM_DeclineInvitation(invitationId);
	}

	public void DismissInvitation(string invitationId)
	{
		AN_GMSRTMProxy.RTM_DismissInvitation(invitationId);
	}

	public void OpenInvitationInBoxUI()
	{
		AN_GMSGiftsProxy.showInvitationBox();
	}

	public void SetVariant(int val)
	{
		AN_GMSRTMProxy.RTM_SetVariant(val);
	}

	public void SetExclusiveBitMask(int val)
	{
		AN_GMSRTMProxy.RTM_SetExclusiveBitMask(val);
	}

	public void ClearReliableMessageListener(int dataTokenId)
	{
		if (_ReliableMassageListeners.ContainsKey(dataTokenId))
		{
			_ReliableMassageListeners.Remove(dataTokenId);
			UnityEngine.Debug.Log("[ClearReliableMessageListener] Remove data with token " + dataTokenId);
		}
	}

	private void OnWatingRoomIntentClosed(string data)
	{
		UnityEngine.Debug.Log("[OnWatingRoomIntentClosed] data " + data);
		string[] array = data.Split("|"[0]);
		AndroidActivityResult obj = new AndroidActivityResult(array[0], array[1]);
		ActionWatingRoomIntentClosed__BackingField(obj);
	}

	private void OnRoomUpdate(string data)
	{
		string[] array = data.Split("|"[0]);
		_currentRoom = new GP_RTM_Room
		{
			id = array[0],
			creatorId = array[1]
		};
		string[] array2 = array[2].Split(',');
		for (int i = 0; i < array2.Length && !(array2[i] == "endofline"); i += 6)
		{
			GP_Participant p = new GP_Participant(array2[i], array2[i + 1], array2[i + 2], array2[i + 3], array2[i + 4], array2[i + 5]);
			_currentRoom.AddParticipant(p);
		}
		_currentRoom.status = (GP_RTM_RoomStatus)Convert.ToInt32(array[3]);
		_currentRoom.creationTimestamp = Convert.ToInt64(array[4]);
		UnityEngine.Debug.Log("GooglePlayRTM OnRoomUpdate Room State: " + _currentRoom.status);
		ActionRoomUpdated__BackingField(_currentRoom);
	}

	private void OnReliableMessageSent(string data)
	{
		UnityEngine.Debug.Log("[OnReliableMessageSent] " + data);
		string[] array = data.Split("|"[0]);
		int messageTokedId = int.Parse(array[2]);
		int key = int.Parse(array[3]);
		if (_ReliableMassageListeners.ContainsKey(key))
		{
			GP_RTM_ReliableMessageSentResult obj = new GP_RTM_ReliableMessageSentResult(array[0], array[1], messageTokedId, _ReliableMassageListeners[key].Data);
			ActionReliableMessageSent__BackingField(obj);
			_ReliableMassageListeners[key].ReportSentMessage();
		}
		else
		{
			GP_RTM_ReliableMessageSentResult obj2 = new GP_RTM_ReliableMessageSentResult(array[0], array[1], messageTokedId, null);
			ActionReliableMessageSent__BackingField(obj2);
		}
	}

	private void OnReliableMessageDelivered(string data)
	{
		UnityEngine.Debug.Log("[OnReliableMessageDelivered] " + data);
		string[] array = data.Split("|"[0]);
		int messageTokedId = int.Parse(array[2]);
		int key = int.Parse(array[3]);
		if (_ReliableMassageListeners.ContainsKey(key))
		{
			GP_RTM_ReliableMessageDeliveredResult obj = new GP_RTM_ReliableMessageDeliveredResult(array[0], array[1], messageTokedId, _ReliableMassageListeners[key].Data);
			ActionReliableMessageDelivered__BackingField(obj);
			_ReliableMassageListeners[key].ReportDeliveredMessage();
		}
		else
		{
			GP_RTM_ReliableMessageDeliveredResult obj2 = new GP_RTM_ReliableMessageDeliveredResult(array[0], array[1], messageTokedId, null);
			ActionReliableMessageDelivered__BackingField(obj2);
		}
	}

	private void OnMatchDataRecieved(string data)
	{
		if (data.Equals(string.Empty))
		{
			UnityEngine.Debug.Log("OnMatchDataRecieved, no data avaiable");
			return;
		}
		string[] array = data.Split("|"[0]);
		GP_RTM_Network_Package obj = new GP_RTM_Network_Package(array[0], array[1]);
		ActionDataRecieved__BackingField(obj);
		UnityEngine.Debug.Log("GooglePlayManager -> DATA_RECEIVED");
	}

	private void OnConnectedToRoom(string data)
	{
		UnityEngine.Debug.Log("[OnConnectedToRoom] data " + data);
		ActionConnectedToRoom__BackingField();
	}

	private void OnDisconnectedFromRoom(string data)
	{
		UnityEngine.Debug.Log("[OnDisconnectedFromRoom] data " + data);
		ActionDisconnectedFromRoom__BackingField();
	}

	private void OnP2PConnected(string participantId)
	{
		UnityEngine.Debug.Log("[OnP2PConnected] participantId " + participantId);
		ActionP2PConnected__BackingField(participantId);
	}

	private void OnP2PDisconnected(string participantId)
	{
		UnityEngine.Debug.Log("[OnP2PDisconnected] participantId " + participantId);
		ActionP2PDisconnected__BackingField(participantId);
	}

	private void OnPeerDeclined(string data)
	{
		UnityEngine.Debug.Log("[OnPeerDeclined] data " + data);
		string[] obj = data.Split(","[0]);
		ActionPeerDeclined__BackingField(obj);
	}

	private void OnPeerInvitedToRoom(string data)
	{
		UnityEngine.Debug.Log("[OnPeerInvitedToRoom] data " + data);
		string[] obj = data.Split(","[0]);
		ActionPeerInvitedToRoom__BackingField(obj);
	}

	private void OnPeerJoined(string data)
	{
		UnityEngine.Debug.Log("[OnPeerJoined] data " + data);
		string[] obj = data.Split(","[0]);
		ActionPeerJoined__BackingField(obj);
	}

	private void OnPeerLeft(string data)
	{
		UnityEngine.Debug.Log("[OnPeerLeft] data " + data);
		string[] obj = data.Split(","[0]);
		ActionPeerLeft__BackingField(obj);
	}

	private void OnPeersConnected(string data)
	{
		UnityEngine.Debug.Log("[OnPeersConnected] data " + data);
		string[] obj = data.Split(","[0]);
		ActionPeersConnected__BackingField(obj);
	}

	private void OnPeersDisconnected(string data)
	{
		UnityEngine.Debug.Log("[OnPeersDisconnected] data " + data);
		string[] obj = data.Split(","[0]);
		ActionPeersDisconnected__BackingField(obj);
	}

	private void OnRoomAutoMatching(string data)
	{
		UnityEngine.Debug.Log("[OnRoomAutoMatching] data " + data);
		ActionRoomAutomatching__BackingField();
	}

	private void OnRoomConnecting(string data)
	{
		UnityEngine.Debug.Log("[OnRoomConnecting] data " + data);
		ActionRoomConnecting__BackingField();
	}

	private void OnJoinedRoom(string data)
	{
		UnityEngine.Debug.Log("[OnJoinedRoom] data " + data);
		GP_GamesStatusCodes obj = (GP_GamesStatusCodes)Convert.ToInt32(data);
		ActionJoinedRoom__BackingField(obj);
	}

	private void OnLeftRoom(string data)
	{
		UnityEngine.Debug.Log("[OnLeftRoom] Created OnRoomUpdate data " + data);
		string[] array = data.Split("|"[0]);
		GP_RTM_Result obj = new GP_RTM_Result(array[0], array[1]);
		_currentRoom = new GP_RTM_Room();
		ActionRoomUpdated__BackingField(_currentRoom);
		ActionLeftRoom__BackingField(obj);
	}

	private void OnRoomConnected(string data)
	{
		UnityEngine.Debug.Log("[OnRoomConnected] data " + data);
		GP_GamesStatusCodes obj = (GP_GamesStatusCodes)Convert.ToInt32(data);
		ActionRoomConnected__BackingField(obj);
	}

	private void OnRoomCreated(string data)
	{
		UnityEngine.Debug.Log("[OnRoomCreated] data " + data);
		GP_GamesStatusCodes obj = (GP_GamesStatusCodes)Convert.ToInt32(data);
		ActionRoomCreated__BackingField(obj);
	}

	private void OnInvitationBoxUiClosed(string data)
	{
		UnityEngine.Debug.Log("[OnInvitationBoxUiClosed] data " + data);
		string[] array = data.Split("|"[0]);
		AndroidActivityResult obj = new AndroidActivityResult(array[0], array[1]);
		ActionInvitationBoxUIClosed__BackingField(obj);
	}

	private void OnInvitationReceived(GP_Invite inv)
	{
		if (inv.InvitationType == GP_InvitationType.INVITATION_TYPE_REAL_TIME)
		{
			_invitations.Add(inv);
			ActionInvitationReceived__BackingField(inv);
		}
	}

	private void OnInvitationRemoved(string invitationId)
	{
		UnityEngine.Debug.Log("[OnInvitationRemoved] invitationId " + invitationId);
		foreach (GP_Invite invitation in _invitations)
		{
			if (invitation.Id.Equals(invitationId))
			{
				_invitations.Remove(invitation);
				return;
			}
		}
		ActionInvitationRemoved__BackingField(invitationId);
	}

	private void OnInvitationAccepted(GP_Invite inv)
	{
		ActionInvitationAccepted__BackingField(inv);
	}
}
