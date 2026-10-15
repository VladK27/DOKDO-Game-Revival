using System.Collections.Generic;
using SA.Common.Models;

public class GK_FetchResult : Result
{
	private List<GK_SavedGame> _SavedGames = new List<GK_SavedGame>();

	public List<GK_SavedGame> SavedGames
	{
		get
		{
			return _SavedGames;
		}
	}

	public GK_FetchResult(List<GK_SavedGame> saves)
	{
		_SavedGames = saves;
	}

	public GK_FetchResult(string errorData)
		: base(new Error(errorData))
	{
	}
}
