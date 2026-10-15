using System.Collections.Generic;
using SA.Common.Models;

public class GK_SavesResolveResult : Result
{
	private List<GK_SavedGame> _ResolvedSaves = new List<GK_SavedGame>();

	public List<GK_SavedGame> SavedGames
	{
		get
		{
			return _ResolvedSaves;
		}
	}

	public GK_SavesResolveResult(List<GK_SavedGame> saves)
	{
		_ResolvedSaves = saves;
	}

	public GK_SavesResolveResult(Error error)
		: base(error)
	{
	}

	public GK_SavesResolveResult(string errorData)
		: base(new Error(errorData))
	{
	}
}
