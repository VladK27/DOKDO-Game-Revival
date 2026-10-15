using SA.Common.Models;

public class GK_UserInfoLoadResult : Result
{
	private string _playerId;

	private GK_Player _tpl;

	public string playerId
	{
		get
		{
			return _playerId;
		}
	}

	public GK_Player playerTemplate
	{
		get
		{
			return _tpl;
		}
	}

	public GK_UserInfoLoadResult(GK_Player tpl)
	{
		_tpl = tpl;
	}

	public GK_UserInfoLoadResult(string id)
		: base(new Error(0, "unknown erro"))
	{
		_playerId = id;
	}
}
