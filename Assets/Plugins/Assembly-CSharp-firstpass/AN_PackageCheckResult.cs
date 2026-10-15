using SA.Common.Models;

public class AN_PackageCheckResult : Result
{
	private string _packageName;

	public string packageName
	{
		get
		{
			return _packageName;
		}
	}

	public AN_PackageCheckResult(string packId)
	{
		_packageName = packId;
	}

	public AN_PackageCheckResult(string packId, Error error)
		: base(error)
	{
		_packageName = packId;
	}
}
