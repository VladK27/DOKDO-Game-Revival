public class AMN_RequestPlayerDataResult : AMN_Result
{
	private string error;

	private GC_Player player;

	public string Error
	{
		get
		{
			return error;
		}
	}

	public GC_Player Player
	{
		get
		{
			return player;
		}
	}

	public AMN_RequestPlayerDataResult(bool success)
		: base(success)
	{
	}

	public AMN_RequestPlayerDataResult(string err)
		: base(false)
	{
		error = err;
	}

	public AMN_RequestPlayerDataResult(GC_Player pl)
		: base(true)
	{
		player = pl;
	}
}
