using SA.Common.Models;

public class GK_TBM_MatchInitResult : Result
{
	private GK_TBM_Match _Match;

	public GK_TBM_Match Match
	{
		get
		{
			return _Match;
		}
	}

	public GK_TBM_MatchInitResult(GK_TBM_Match match)
	{
		_Match = match;
	}

	public GK_TBM_MatchInitResult(string errorData)
		: base(new Error(errorData))
	{
	}
}
