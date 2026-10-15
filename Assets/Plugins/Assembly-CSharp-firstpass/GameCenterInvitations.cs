using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Data;
using SA.Common.Pattern;
using UnityEngine;

public class GameCenterInvitations : Singleton<GameCenterInvitations>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_Player, GK_InviteRecipientResponse> ActionInviteeResponse__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_MatchType, GK_Invite> ActionPlayerAcceptedInvitation__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_MatchType, string[], GK_Player[]> ActionPlayerRequestedMatchWithRecipients__BackingField = delegate
	{
	};

	public static event Action<GK_Player, GK_InviteRecipientResponse> ActionInviteeResponse
	{
		add
		{
			Action<GK_Player, GK_InviteRecipientResponse> action = ActionInviteeResponse__BackingField;
			Action<GK_Player, GK_InviteRecipientResponse> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionInviteeResponse__BackingField, (Action<GK_Player, GK_InviteRecipientResponse>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_Player, GK_InviteRecipientResponse> action = ActionInviteeResponse__BackingField;
			Action<GK_Player, GK_InviteRecipientResponse> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionInviteeResponse__BackingField, (Action<GK_Player, GK_InviteRecipientResponse>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_MatchType, GK_Invite> ActionPlayerAcceptedInvitation
	{
		add
		{
			Action<GK_MatchType, GK_Invite> action = ActionPlayerAcceptedInvitation__BackingField;
			Action<GK_MatchType, GK_Invite> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPlayerAcceptedInvitation__BackingField, (Action<GK_MatchType, GK_Invite>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_MatchType, GK_Invite> action = ActionPlayerAcceptedInvitation__BackingField;
			Action<GK_MatchType, GK_Invite> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPlayerAcceptedInvitation__BackingField, (Action<GK_MatchType, GK_Invite>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_MatchType, string[], GK_Player[]> ActionPlayerRequestedMatchWithRecipients
	{
		add
		{
			Action<GK_MatchType, string[], GK_Player[]> action = ActionPlayerRequestedMatchWithRecipients__BackingField;
			Action<GK_MatchType, string[], GK_Player[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPlayerRequestedMatchWithRecipients__BackingField, (Action<GK_MatchType, string[], GK_Player[]>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_MatchType, string[], GK_Player[]> action = ActionPlayerRequestedMatchWithRecipients__BackingField;
			Action<GK_MatchType, string[], GK_Player[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPlayerRequestedMatchWithRecipients__BackingField, (Action<GK_MatchType, string[], GK_Player[]>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void Init()
	{
	}

	private void OnInviteeResponse(string data)
	{
		ISN_Logger.Log("OnInviteeResponse");
		string[] array = data.Split('|');
		GK_Player playerById = GameCenterManager.GetPlayerById(array[0]);
		GK_InviteRecipientResponse arg = (GK_InviteRecipientResponse)Convert.ToInt32(array[1]);
		ActionInviteeResponse__BackingField(playerById, arg);
	}

	private void OnPlayerAcceptedInvitation_RTM(string data)
	{
		ISN_Logger.Log("OnPlayerAcceptedInvitation_RTM");
		GK_Invite arg = new GK_Invite(data);
		ActionPlayerAcceptedInvitation__BackingField(GK_MatchType.RealTime, arg);
	}

	private void OnPlayerRequestedMatchWithRecipients_RTM(string data)
	{
		ISN_Logger.Log("OnPlayerRequestedMatchWithRecipients_RTM");
		string[] array = Converter.ParseArray(data);
		List<GK_Player> list = new List<GK_Player>();
		string[] array2 = array;
		foreach (string playerID in array2)
		{
			list.Add(GameCenterManager.GetPlayerById(playerID));
		}
		ActionPlayerRequestedMatchWithRecipients__BackingField(GK_MatchType.RealTime, array, list.ToArray());
	}

	private void OnPlayerAcceptedInvitation_TBM(string data)
	{
		ISN_Logger.Log("OnPlayerAcceptedInvitation_TBM");
		GK_Invite arg = new GK_Invite(data);
		ActionPlayerAcceptedInvitation__BackingField(GK_MatchType.TurnBased, arg);
	}

	private void OnPlayerRequestedMatchWithRecipients_TBM(string data)
	{
		ISN_Logger.Log("OnPlayerRequestedMatchWithRecipients_TBM");
		string[] array = Converter.ParseArray(data);
		List<GK_Player> list = new List<GK_Player>();
		string[] array2 = array;
		foreach (string playerID in array2)
		{
			list.Add(GameCenterManager.GetPlayerById(playerID));
		}
		ActionPlayerRequestedMatchWithRecipients__BackingField(GK_MatchType.RealTime, array, list.ToArray());
	}
}
