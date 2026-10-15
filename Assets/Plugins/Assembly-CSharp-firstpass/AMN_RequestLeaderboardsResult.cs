using System.Collections.Generic;

public class AMN_RequestLeaderboardsResult : AMN_Result
{
	private string error;

	private List<GC_Leaderboard> leaderboardsList;

	public string Error
	{
		get
		{
			return error;
		}
	}

	public List<GC_Leaderboard> LeaderboardsList
	{
		get
		{
			return leaderboardsList;
		}
	}

	public AMN_RequestLeaderboardsResult(bool success)
		: base(success)
	{
	}

	public AMN_RequestLeaderboardsResult(string err)
		: base(false)
	{
		error = err;
	}

	public AMN_RequestLeaderboardsResult(List<GC_Leaderboard> list)
		: base(true)
	{
		leaderboardsList = list;
	}
}
