using SA.Common.Models;

public class ISN_LoadSetLeaderboardsInfoResult : Result
{
	public GK_LeaderboardSet _LeaderBoardsSet;

	public GK_LeaderboardSet LeaderBoardsSet
	{
		get
		{
			return _LeaderBoardsSet;
		}
	}

	public ISN_LoadSetLeaderboardsInfoResult(GK_LeaderboardSet lbset)
	{
		_LeaderBoardsSet = lbset;
	}

	public ISN_LoadSetLeaderboardsInfoResult(GK_LeaderboardSet lbset, Error error)
		: base(error)
	{
		_LeaderBoardsSet = lbset;
	}
}
