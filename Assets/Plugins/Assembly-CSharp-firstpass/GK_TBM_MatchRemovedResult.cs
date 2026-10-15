using SA.Common.Models;

public class GK_TBM_MatchRemovedResult : Result
{
	private string _MatchId;

	public string MatchId
	{
		get
		{
			return _MatchId;
		}
	}

	public GK_TBM_MatchRemovedResult(string matchId)
	{
		_MatchId = matchId;
	}

	public GK_TBM_MatchRemovedResult()
		: base(new Error())
	{
	}
}
