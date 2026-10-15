using System;

public class GK_Score
{
	private int _Rank;

	private long _Score;

	private long _Context;

	private string _PlayerId;

	private string _LeaderboardId;

	private GK_CollectionType _Collection;

	private GK_TimeSpan _TimeSpan;

	public int Rank
	{
		get
		{
			return _Rank;
		}
	}

	public long LongScore
	{
		get
		{
			return _Score;
		}
	}

	public float CurrencyScore
	{
		get
		{
			return (float)_Score / 100f;
		}
	}

	public float DecimalFloat_1
	{
		get
		{
			return (float)_Score / 10f;
		}
	}

	public float DecimalFloat_2
	{
		get
		{
			return (float)_Score / 100f;
		}
	}

	public float DecimalFloat_3
	{
		get
		{
			return (float)_Score / 100f;
		}
	}

	public long Context
	{
		get
		{
			return _Context;
		}
	}

	public TimeSpan Minutes
	{
		get
		{
			return System.TimeSpan.FromMinutes(_Score);
		}
	}

	public TimeSpan Seconds
	{
		get
		{
			return System.TimeSpan.FromSeconds(_Score);
		}
	}

	public TimeSpan Milliseconds
	{
		get
		{
			return System.TimeSpan.FromMilliseconds(_Score);
		}
	}

	public string PlayerId
	{
		get
		{
			return _PlayerId;
		}
	}

	public GK_Player Player
	{
		get
		{
			return GameCenterManager.GetPlayerById(PlayerId);
		}
	}

	public string LeaderboardId
	{
		get
		{
			return _LeaderboardId;
		}
	}

	public GK_Leaderboard Leaderboard
	{
		get
		{
			return GameCenterManager.GetLeaderboard(LeaderboardId);
		}
	}

	public GK_CollectionType Collection
	{
		get
		{
			return _Collection;
		}
	}

	public GK_TimeSpan TimeSpan
	{
		get
		{
			return _TimeSpan;
		}
	}

	[Obsolete("rank is deprecated, plase use Rank instead")]
	public int rank
	{
		get
		{
			return _Rank;
		}
	}

	[Obsolete("score is deprecated, plase use LongScore instead")]
	public long score
	{
		get
		{
			return _Score;
		}
	}

	[Obsolete("playerId is deprecated, plase use PlayerId instead")]
	public string playerId
	{
		get
		{
			return _PlayerId;
		}
	}

	[Obsolete("leaderboardId is deprecated, plase use LeaderboardId instead")]
	public string leaderboardId
	{
		get
		{
			return _LeaderboardId;
		}
	}

	[Obsolete("timeSpan is deprecated, plase use TimeSpan instead")]
	public GK_TimeSpan timeSpan
	{
		get
		{
			return _TimeSpan;
		}
	}

	[Obsolete("collection is deprecated, plase use Collection instead")]
	public GK_CollectionType collection
	{
		get
		{
			return _Collection;
		}
	}

	public GK_Score(long vScore, int vRank, long vContext, GK_TimeSpan vTimeSpan, GK_CollectionType sCollection, string lid, string pid)
	{
		_Score = vScore;
		_Rank = vRank;
		_Context = vContext;
		_PlayerId = pid;
		_LeaderboardId = lid;
		_TimeSpan = vTimeSpan;
		_Collection = sCollection;
	}
}
