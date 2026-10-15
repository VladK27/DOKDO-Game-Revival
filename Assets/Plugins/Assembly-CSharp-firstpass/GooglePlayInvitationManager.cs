using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class GooglePlayInvitationManager : Singleton<GooglePlayInvitationManager>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GP_Invite> ActionInvitationReceived__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GP_Invite> ActionInvitationAccepted__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<List<GP_Invite>> ActionInvitationsListLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<AN_InvitationInboxCloseResult> ActionInvitationInboxClosed__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string> ActionInvitationRemoved__BackingField = delegate
	{
	};

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

	public static event Action<List<GP_Invite>> ActionInvitationsListLoaded
	{
		add
		{
			Action<List<GP_Invite>> action = ActionInvitationsListLoaded__BackingField;
			Action<List<GP_Invite>> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionInvitationsListLoaded__BackingField, (Action<List<GP_Invite>>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<List<GP_Invite>> action = ActionInvitationsListLoaded__BackingField;
			Action<List<GP_Invite>> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionInvitationsListLoaded__BackingField, (Action<List<GP_Invite>>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<AN_InvitationInboxCloseResult> ActionInvitationInboxClosed
	{
		add
		{
			Action<AN_InvitationInboxCloseResult> action = ActionInvitationInboxClosed__BackingField;
			Action<AN_InvitationInboxCloseResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionInvitationInboxClosed__BackingField, (Action<AN_InvitationInboxCloseResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AN_InvitationInboxCloseResult> action = ActionInvitationInboxClosed__BackingField;
			Action<AN_InvitationInboxCloseResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionInvitationInboxClosed__BackingField, (Action<AN_InvitationInboxCloseResult>)Delegate.Remove(action2, value), action);
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
		UnityEngine.Debug.Log("GooglePlayInvitationManager Created");
	}

	public void Init()
	{
	}

	private void OnInvitationReceived(string data)
	{
		string[] storeData = data.Split("|"[0]);
		ActionInvitationReceived__BackingField(InviteFromString(storeData, 0));
	}

	private void OnInvitationAccepted(string data)
	{
		string[] storeData = data.Split("|"[0]);
		ActionInvitationAccepted__BackingField(InviteFromString(storeData, 0));
		UnityEngine.Debug.Log("OnInvitationAccepted+++");
	}

	private void OnInvitationRemoved(string invId)
	{
		ActionInvitationRemoved__BackingField(invId);
	}

	private void OnInvitationBoxUiClosed(string response)
	{
		AN_InvitationInboxCloseResult obj = new AN_InvitationInboxCloseResult(response);
		ActionInvitationInboxClosed__BackingField(obj);
	}

	private void OnLoadInvitationsResult(string data)
	{
		string[] array = data.Split(new string[1] { "|%|" }, StringSplitOptions.None);
		List<GP_Invite> list = new List<GP_Invite>();
		GooglePlayResult googlePlayResult = new GooglePlayResult(array[0]);
		if (googlePlayResult.IsSucceeded)
		{
			for (int i = 1; i < array.Length && !(array[i] == "endofline"); i++)
			{
				string[] storeData = array[i].Split("|"[0]);
				GP_Invite item = InviteFromString(storeData, 0);
				list.Add(item);
			}
		}
		ActionInvitationsListLoaded__BackingField(list);
	}

	private GP_Invite InviteFromString(string[] storeData, int offset)
	{
		GP_Invite gP_Invite = new GP_Invite();
		gP_Invite.Id = storeData[offset];
		gP_Invite.CreationTimestamp = Convert.ToInt64(storeData[1 + offset]);
		gP_Invite.InvitationType = (GP_InvitationType)Convert.ToInt32(storeData[2 + offset]);
		gP_Invite.Variant = Convert.ToInt32(storeData[3 + offset]);
		gP_Invite.Participant = GooglePlayManager.ParseParticipanData(storeData, 4 + offset);
		return gP_Invite;
	}

	public void RegisterInvitationListener()
	{
		AN_GMSInvitationProxy.registerInvitationListener();
	}

	public void UnregisterInvitationListener()
	{
		AN_GMSInvitationProxy.unregisterInvitationListener();
	}

	public void LoadInvitations()
	{
		AN_GMSInvitationProxy.LoadInvitations();
	}
}
