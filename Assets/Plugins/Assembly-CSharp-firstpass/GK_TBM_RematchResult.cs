using SA.Common.Models;

public class GK_TBM_RematchResult : Result
{
	private GK_TBM_Match _Match;

	public GK_TBM_Match Match
	{
		get
		{
			return _Match;
		}
	}

	public GK_TBM_RematchResult(GK_TBM_Match match)
	{
		_Match = match;
	}

	public GK_TBM_RematchResult(string errorData)
		: base(new Error(errorData))
	{
	}
}
