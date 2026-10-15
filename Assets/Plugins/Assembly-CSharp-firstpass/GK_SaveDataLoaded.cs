using SA.Common.Models;

public class GK_SaveDataLoaded : Result
{
	private GK_SavedGame _SavedGame;

	public GK_SavedGame SavedGame
	{
		get
		{
			return _SavedGame;
		}
	}

	public GK_SaveDataLoaded(GK_SavedGame save)
	{
		_SavedGame = save;
	}

	public GK_SaveDataLoaded(Error error)
		: base(error)
	{
	}
}
