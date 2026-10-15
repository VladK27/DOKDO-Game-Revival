using SA.Common.Models;

public class GK_LeaderboardResult : Result
{
	private GK_Leaderboard _Leaderboard;

	public GK_Leaderboard Leaderboard
	{
		get
		{
			return _Leaderboard;
		}
	}

	public GK_LeaderboardResult(GK_Leaderboard leaderboard)
	{
		Setinfo(leaderboard);
	}

	public GK_LeaderboardResult(GK_Leaderboard leaderboard, Error error)
		: base(error)
	{
		Setinfo(leaderboard);
	}

	private void Setinfo(GK_Leaderboard leaderboard)
	{
		_Leaderboard = leaderboard;
	}
}
