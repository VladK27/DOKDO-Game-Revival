using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class GP_AppInvitesController : Singleton<GP_AppInvitesController>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GP_SendAppInvitesResult> ActionAppInvitesSent__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GP_RetrieveAppInviteResult> ActionAppInviteRetrieved__BackingField = delegate
	{
	};

	public static event Action<GP_SendAppInvitesResult> ActionAppInvitesSent
	{
		add
		{
			Action<GP_SendAppInvitesResult> action = ActionAppInvitesSent__BackingField;
			Action<GP_SendAppInvitesResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionAppInvitesSent__BackingField, (Action<GP_SendAppInvitesResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GP_SendAppInvitesResult> action = ActionAppInvitesSent__BackingField;
			Action<GP_SendAppInvitesResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionAppInvitesSent__BackingField, (Action<GP_SendAppInvitesResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GP_RetrieveAppInviteResult> ActionAppInviteRetrieved
	{
		add
		{
			Action<GP_RetrieveAppInviteResult> action = ActionAppInviteRetrieved__BackingField;
			Action<GP_RetrieveAppInviteResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionAppInviteRetrieved__BackingField, (Action<GP_RetrieveAppInviteResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GP_RetrieveAppInviteResult> action = ActionAppInviteRetrieved__BackingField;
			Action<GP_RetrieveAppInviteResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionAppInviteRetrieved__BackingField, (Action<GP_RetrieveAppInviteResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void StartInvitationDialog(GP_AppInviteBuilder builder)
	{
		AN_AppInvitesProxy.StartInvitationDialog(builder.Id);
	}

	public void GetInvitation(bool autoLaunchDeepLink = false)
	{
		AN_AppInvitesProxy.GetInvitation(autoLaunchDeepLink);
	}

	private void OnInvitationDialogComplete(string InvitationIds)
	{
		string[] invites = AndroidNative.StringToArray(InvitationIds);
		GP_SendAppInvitesResult obj = new GP_SendAppInvitesResult(invites);
		ActionAppInvitesSent__BackingField(obj);
	}

	private void OnInvitationDialogFailed(string erroCode)
	{
		GP_SendAppInvitesResult obj = new GP_SendAppInvitesResult(erroCode);
		ActionAppInvitesSent__BackingField(obj);
	}

	private void OnInvitationLoadFailed(string erroCode)
	{
		GP_RetrieveAppInviteResult obj = new GP_RetrieveAppInviteResult(erroCode);
		ActionAppInviteRetrieved__BackingField(obj);
	}

	private void OnInvitationLoaded(string data)
	{
		string[] array = data.Split("|"[0]);
		string link = array[0];
		string id = array[1];
		bool isOpenedFromPlatStore = Convert.ToBoolean(array[2]);
		GP_AppInvite invite = new GP_AppInvite(id, link, isOpenedFromPlatStore);
		GP_RetrieveAppInviteResult obj = new GP_RetrieveAppInviteResult(invite);
		ActionAppInviteRetrieved__BackingField(obj);
	}
}
