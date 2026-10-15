using SA.Common.Models;

public class GK_SaveRemoveResult : Result
{
	private string _SaveName = string.Empty;

	public string SaveName
	{
		get
		{
			return _SaveName;
		}
	}

	public GK_SaveRemoveResult(string name)
	{
		_SaveName = name;
	}

	public GK_SaveRemoveResult(string name, string errorData)
		: base(new Error(errorData))
	{
		_SaveName = name;
	}
}
