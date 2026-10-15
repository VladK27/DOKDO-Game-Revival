using SA.Common.Models;

public class GK_RTM_MatchStartedResult : Result
{
	private GK_RTM_Match _Match;

	public GK_RTM_Match Match
	{
		get
		{
			return _Match;
		}
	}

	public GK_RTM_MatchStartedResult(GK_RTM_Match match)
	{
		_Match = match;
	}

	public GK_RTM_MatchStartedResult(string errorData)
		: base(new Error(errorData))
	{
	}
}
