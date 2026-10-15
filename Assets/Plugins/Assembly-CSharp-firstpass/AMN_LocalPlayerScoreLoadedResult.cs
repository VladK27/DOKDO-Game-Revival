public class AMN_LocalPlayerScoreLoadedResult : AMN_Result
{
	private string _LeaderboardId = string.Empty;

	private GC_ScoreTimeSpan _TimeSpan = GC_ScoreTimeSpan.ALL_TIME;

	private int _Rank;

	private long _Score;

	private string _Error = string.Empty;

	public string LeaderboardId
	{
		get
		{
			return _LeaderboardId;
		}
	}

	public GC_ScoreTimeSpan TimeSpan
	{
		get
		{
			return _TimeSpan;
		}
	}

	public int Rank
	{
		get
		{
			return _Rank;
		}
	}

	public long Score
	{
		get
		{
			return _Score;
		}
	}

	public string Error
	{
		get
		{
			return _Error;
		}
	}

	public AMN_LocalPlayerScoreLoadedResult(string leaderboardId, GC_ScoreTimeSpan timeSpan, int rank, long score)
		: base(true)
	{
		_LeaderboardId = leaderboardId;
		_TimeSpan = timeSpan;
		_Rank = rank;
		_Score = score;
	}

	public AMN_LocalPlayerScoreLoadedResult(string leaderboardId, GC_ScoreTimeSpan timeSpan, string error)
		: base(false)
	{
		_LeaderboardId = leaderboardId;
		_TimeSpan = timeSpan;
		_Error = error;
	}
}
