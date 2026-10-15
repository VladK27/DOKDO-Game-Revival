using SA.Common.Models;

public class GK_TBM_MatchDataUpdateResult : Result
{
	private GK_TBM_Match _Match;

	public GK_TBM_Match Match
	{
		get
		{
			return _Match;
		}
	}

	public GK_TBM_MatchDataUpdateResult(GK_TBM_Match updatedMatch)
	{
		_Match = updatedMatch;
	}

	public GK_TBM_MatchDataUpdateResult(string errorData)
		: base(new Error(errorData))
	{
	}

	public GK_TBM_MatchDataUpdateResult(Error error)
		: base(error)
	{
	}
}
