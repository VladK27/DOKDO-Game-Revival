public class AMN_ScoresLoadedResult : AMN_Result
{
	private string _LeaderboardId = string.Empty;

	private GC_Leaderboard _Leaderboard;

	public string LeaderboardId
	{
		get
		{
			return _LeaderboardId;
		}
	}

	public GC_Leaderboard Leaderboard
	{
		get
		{
			return _Leaderboard;
		}
	}

	public AMN_ScoresLoadedResult(GC_Leaderboard leaderboard)
		: base(true)
	{
		_Leaderboard = leaderboard;
		_LeaderboardId = _Leaderboard.Identifier;
	}

	public AMN_ScoresLoadedResult(string leaderboardId, string error)
		: base(false)
	{
		_LeaderboardId = leaderboardId;
	}
}
