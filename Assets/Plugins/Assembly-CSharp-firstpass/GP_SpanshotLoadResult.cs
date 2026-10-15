public class GP_SpanshotLoadResult : GooglePlayResult
{
	private GP_Snapshot _snapshot;

	public GP_Snapshot Snapshot
	{
		get
		{
			return _snapshot;
		}
	}

	public GP_SpanshotLoadResult(string code)
		: base(code)
	{
	}

	public void SetSnapShot(GP_Snapshot s)
	{
		_snapshot = s;
	}
}
