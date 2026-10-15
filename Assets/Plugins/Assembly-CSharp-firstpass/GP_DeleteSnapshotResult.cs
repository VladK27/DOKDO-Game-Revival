public class GP_DeleteSnapshotResult : GooglePlayResult
{
	private string _SnapshotId;

	public string SnapshotId
	{
		get
		{
			return _SnapshotId;
		}
	}

	public GP_DeleteSnapshotResult(string code)
		: base(code)
	{
	}

	public void SetId(string sid)
	{
		_SnapshotId = sid;
	}
}
