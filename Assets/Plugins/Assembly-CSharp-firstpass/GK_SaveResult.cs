using SA.Common.Models;

public class GK_SaveResult : Result
{
	private GK_SavedGame _SavedGame;

	public GK_SavedGame SavedGame
	{
		get
		{
			return _SavedGame;
		}
	}

	public GK_SaveResult(GK_SavedGame save)
	{
		_SavedGame = save;
	}

	public GK_SaveResult(string errorData)
		: base(new Error(errorData))
	{
	}

	public GK_SaveResult(Error error)
		: base(error)
	{
	}
}
