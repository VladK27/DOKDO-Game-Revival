using System.Collections.Generic;
using System.Text;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class GameSavesExample : BaseIOSFeaturePreview
{
	private Dictionary<string, List<GK_SavedGame>> GameSaves = new Dictionary<string, List<GK_SavedGame>>();

	private Dictionary<string, List<GK_SavedGame>> SavesConflicts = new Dictionary<string, List<GK_SavedGame>>();

	private string test_name = "savedgame1";

	private string test_name_2 = "savedgame2";

	private void Awake()
	{
	}

	private void OnGUI()
	{
		UpdateToStartPos();
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "GameCenter Game Saves", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Init"))
		{
			GameCenterManager.OnAuthFinished += HandleOnAuthFinished;
			GameCenterManager.Init();
		}
		StartY += YButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Save Game"))
		{
			Save(test_name);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Save Game 2"))
		{
			Save(test_name_2);
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Fetch Saved Games"))
		{
			Fetch();
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Load Saved Game"))
		{
			Load();
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Delete Saved Game"))
		{
			Delete(test_name);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Delete Saved Game 2"))
		{
			Delete(test_name_2);
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Resolve Conflicts"))
		{
			ResolveConflicts();
		}
	}

	private void Save(string name)
	{
		ISN_Logger.Log("Start to save game!");
		ISN_GameSaves.ActionGameSaved += HandleActionGameSaved;
		byte[] bytes = Encoding.UTF8.GetBytes("Some data");
		Singleton<ISN_GameSaves>.Instance.SaveGame(bytes, name);
	}

	private void Fetch()
	{
		ISN_Logger.Log("Start to fetch games!");
		ISN_GameSaves.ActionSavesFetched += HandleActionSavesFetched;
		Singleton<ISN_GameSaves>.Instance.FetchSavedGames();
	}

	private void Delete(string name)
	{
		ISN_Logger.Log("Start to delete game by name!");
		ISN_GameSaves.ActionSaveRemoved += HandleActionSaveRemoved;
		Singleton<ISN_GameSaves>.Instance.DeleteSavedGame(name);
	}

	private void Load()
	{
		ISN_Logger.Log("Start to load game!");
		GK_SavedGame loadedSave = GetLoadedSave(test_name);
		if (loadedSave == null)
		{
			ISN_Logger.Log("You don't have any saved game!");
			return;
		}
		loadedSave.ActionDataLoaded += HandleActionDataLoaded;
		loadedSave.LoadData();
	}

	private void ResolveConflicts()
	{
		ISN_Logger.Log("Trying to fix conflicts");
		List<GK_SavedGame> conflict = GetConflict();
		if (conflict == null)
		{
			ISN_Logger.Log("You don't have any conflicts!");
			return;
		}
		ISN_GameSaves.ActionSavesResolved += HandleActionSavesResolved;
		byte[] bytes = Encoding.UTF8.GetBytes("Some data after resolving");
		Singleton<ISN_GameSaves>.Instance.ResolveConflictingSavedGames(conflict, bytes);
	}

	private GK_SavedGame GetLoadedSave(string saveGameName)
	{
		return (!GameSaves.ContainsKey(saveGameName)) ? null : GameSaves[saveGameName][0];
	}

	private List<GK_SavedGame> GetConflict()
	{
		List<GK_SavedGame> result = null;
		using (Dictionary<string, List<GK_SavedGame>>.ValueCollection.Enumerator enumerator = SavesConflicts.Values.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				return enumerator.Current;
			}
			return result;
		}
	}

	private int GetConflictsCount()
	{
		ISN_Logger.Log("The total number of duplicates =" + SavesConflicts.Count);
		return SavesConflicts.Count;
	}

	private void CheckSavesOnDuplicates()
	{
		Dictionary<string, List<GK_SavedGame>> dictionary = new Dictionary<string, List<GK_SavedGame>>(GameSaves);
		foreach (KeyValuePair<string, List<GK_SavedGame>> item in dictionary)
		{
			if (item.Value.Count > 1)
			{
				if (!SavesConflicts.ContainsKey(item.Key))
				{
					SavesConflicts.Add(item.Key, item.Value);
				}
				GameSaves.Remove(item.Key);
			}
		}
		ISN_Logger.Log("------------------------------------------");
		ISN_Logger.Log("Duplicates " + SavesConflicts.Count);
		ISN_Logger.Log("Unique saves " + GameSaves.Count);
		ISN_Logger.Log("------------------------------------------");
	}

	private void HandleOnAuthFinished(Result result)
	{
		GameCenterManager.OnAuthFinished -= HandleOnAuthFinished;
		if (result.IsSucceeded)
		{
			ISN_Logger.Log("Player Authed");
		}
		else
		{
			IOSNativePopUpManager.showMessage("Game Center ", "Player authentication failed");
		}
	}

	private void HandleActionGameSaved(GK_SaveResult res)
	{
		ISN_GameSaves.ActionGameSaved -= HandleActionGameSaved;
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("Saved game with name " + res.SavedGame.Name);
			ISN_Logger.Log("------------------------------------------");
		}
		else
		{
			ISN_Logger.Log("Failed: " + res.Error.Message);
		}
	}

	private void HandleActionSaveRemoved(GK_SaveRemoveResult res)
	{
		ISN_GameSaves.ActionSaveRemoved -= HandleActionSaveRemoved;
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("Deleted game with name " + res.SaveName);
			ISN_Logger.Log("------------------------------------------");
		}
		else
		{
			ISN_Logger.Log("Failed: " + res.Error.Message);
		}
	}

	private void HandleActionDataLoaded(GK_SaveDataLoaded res)
	{
		res.SavedGame.ActionDataLoaded -= HandleActionDataLoaded;
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("Data loaded. data Length: " + res.SavedGame.Data.Length);
		}
		else
		{
			ISN_Logger.Log("Failed: " + res.Error.Message);
		}
	}

	private void HandleActionSavesFetched(GK_FetchResult res)
	{
		ISN_GameSaves.ActionSavesFetched -= HandleActionSavesFetched;
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("Received " + res.SavedGames.Count + " game saves");
			foreach (GK_SavedGame savedGame in res.SavedGames)
			{
				ISN_Logger.Log("The name of the save game " + savedGame.Name);
			}
			ISN_Logger.Log("------------------------------------------");
			GameSaves.Clear();
			foreach (GK_SavedGame savedGame2 in res.SavedGames)
			{
				if (!GameSaves.ContainsKey(savedGame2.Name))
				{
					GameSaves.Add(savedGame2.Name, new List<GK_SavedGame>());
				}
				GameSaves[savedGame2.Name].Add(savedGame2);
			}
			ISN_Logger.Log("Check the saves on duplicates");
			CheckSavesOnDuplicates();
		}
		else
		{
			ISN_Logger.Log("Failed: " + res.Error.Message + " with code " + res.Error.Code);
		}
	}

	private void HandleActionSavesResolved(GK_SavesResolveResult res)
	{
		ISN_GameSaves.ActionSavesResolved -= HandleActionSavesResolved;
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("The conflict is resolved");
			foreach (GK_SavedGame savedGame in res.SavedGames)
			{
				SavesConflicts.Remove(savedGame.Name);
				if (!GameSaves.ContainsKey(savedGame.Name))
				{
					GameSaves.Add(savedGame.Name, new List<GK_SavedGame>());
					GameSaves[savedGame.Name].Add(savedGame);
				}
			}
			ISN_Logger.Log("------------------------------------------");
			ISN_Logger.Log("Duplicates " + SavesConflicts.Count);
			ISN_Logger.Log("Unique saves " + GameSaves.Count);
			ISN_Logger.Log("------------------------------------------");
			{
				foreach (GK_SavedGame savedGame2 in res.SavedGames)
				{
					ISN_Logger.Log("The name of the save game " + savedGame2.Name);
				}
				return;
			}
		}
		ISN_Logger.Log("Failed: " + res.Error.Message);
	}
}
