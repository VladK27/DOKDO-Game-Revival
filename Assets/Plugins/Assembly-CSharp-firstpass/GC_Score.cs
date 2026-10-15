using System;

public class GC_Score
{
	private string _playerId = string.Empty;

	private string _leaderboardId = string.Empty;

	private int _rank;

	private long _score;

	private GC_ScoreTimeSpan _timeSpan = GC_ScoreTimeSpan.ALL_TIME;

	public string PlayerId
	{
		get
		{
			return _playerId;
		}
	}

	public GC_Player Player
	{
		get
		{
			return AMN_Singleton<SA_AmazonGameCircleManager>.Instance.GetPlayerById(_playerId);
		}
	}

	public string LeaderboardId
	{
		get
		{
			return _leaderboardId;
		}
	}

	public int Rank
	{
		get
		{
			return _rank;
		}
	}

	public long Score
	{
		get
		{
			return _score;
		}
	}

	public float CurrencyScore
	{
		get
		{
			return (float)_score / 100f;
		}
	}

	public TimeSpan TimeScore
	{
		get
		{
			return System.TimeSpan.FromMilliseconds(_score);
		}
	}

	public GC_ScoreTimeSpan TimeSpan
	{
		get
		{
			return _timeSpan;
		}
	}

	public GC_Score(string playerId, string leaderboardId, int rank, long score, GC_ScoreTimeSpan timeSpan)
	{
		_playerId = playerId;
		_leaderboardId = leaderboardId;
		_rank = rank;
		_score = score;
		_timeSpan = timeSpan;
	}
}
