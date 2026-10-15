using SA.Common.Models;

public class GK_TBM_MatchQuitResult : Result
{
	private string _MatchId;

	public string MatchId
	{
		get
		{
			return _MatchId;
		}
	}

	public GK_TBM_MatchQuitResult(string matchId)
	{
		_MatchId = matchId;
	}

	public GK_TBM_MatchQuitResult()
		: base(new Error())
	{
	}
}
