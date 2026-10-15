using SA.Common.Models;

public class GK_TBM_MatchEndResult : Result
{
	private GK_TBM_Match _Match;

	public GK_TBM_Match Match
	{
		get
		{
			return _Match;
		}
	}

	public GK_TBM_MatchEndResult(GK_TBM_Match match)
	{
		_Match = match;
	}

	public GK_TBM_MatchEndResult(string errorData)
		: base(new Error(errorData))
	{
	}
}
