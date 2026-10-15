using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Models;

public class GK_LeaderboardSet
{
	public string Title;

	public string Identifier;

	public string GroupIdentifier;

	public List<GK_LeaderBoardInfo> _BoardsInfo = new List<GK_LeaderBoardInfo>();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<ISN_LoadSetLeaderboardsInfoResult> OnLoaderboardsInfoLoaded__BackingField = delegate
	{
	};

	public List<GK_LeaderBoardInfo> BoardsInfo
	{
		get
		{
			return _BoardsInfo;
		}
	}

	public event Action<ISN_LoadSetLeaderboardsInfoResult> OnLoaderboardsInfoLoaded
	{
		add
		{
			Action<ISN_LoadSetLeaderboardsInfoResult> action = OnLoaderboardsInfoLoaded__BackingField;
			Action<ISN_LoadSetLeaderboardsInfoResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLoaderboardsInfoLoaded__BackingField, (Action<ISN_LoadSetLeaderboardsInfoResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<ISN_LoadSetLeaderboardsInfoResult> action = OnLoaderboardsInfoLoaded__BackingField;
			Action<ISN_LoadSetLeaderboardsInfoResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLoaderboardsInfoLoaded__BackingField, (Action<ISN_LoadSetLeaderboardsInfoResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public void LoadLeaderBoardsInfo()
	{
		GameCenterManager.LoadLeaderboardsForSet(Identifier);
	}

	public void AddBoardInfo(GK_LeaderBoardInfo info)
	{
		_BoardsInfo.Add(info);
	}

	public void SendFailLoadEvent()
	{
		ISN_LoadSetLeaderboardsInfoResult obj = new ISN_LoadSetLeaderboardsInfoResult(this, new Error());
		OnLoaderboardsInfoLoaded__BackingField(obj);
	}

	public void SendSuccessLoadEvent()
	{
		ISN_LoadSetLeaderboardsInfoResult obj = new ISN_LoadSetLeaderboardsInfoResult(this);
		OnLoaderboardsInfoLoaded__BackingField(obj);
	}
}
