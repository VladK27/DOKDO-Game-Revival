using System;
using System.Collections.Generic;
using SA.Common.Data;

public class GK_RTM_Match
{
	private int _ExpectedPlayerCount;

	private List<GK_Player> _Players = new List<GK_Player>();

	public int ExpectedPlayerCount
	{
		get
		{
			return _ExpectedPlayerCount;
		}
	}

	public List<GK_Player> Players
	{
		get
		{
			return _Players;
		}
	}

	public GK_RTM_Match(string matchData)
	{
		string[] array = matchData.Split(new string[1] { "|%|" }, StringSplitOptions.None);
		_ExpectedPlayerCount = Convert.ToInt32(array[0]);
		string[] array2 = Converter.ParseArray(array[1]);
		string[] array3 = array2;
		foreach (string playerID in array3)
		{
			GK_Player playerById = GameCenterManager.GetPlayerById(playerID);
			_Players.Add(playerById);
		}
	}
}
