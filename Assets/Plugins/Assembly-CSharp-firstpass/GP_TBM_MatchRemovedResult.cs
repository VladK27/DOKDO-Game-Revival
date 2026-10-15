public class GP_TBM_MatchRemovedResult
{
	private string _MatchId;

	public string MatchId
	{
		get
		{
			return _MatchId;
		}
	}

	public GP_TBM_MatchRemovedResult(string mId)
	{
		_MatchId = mId;
	}
}
