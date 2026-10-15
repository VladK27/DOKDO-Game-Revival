using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class ISN_GameSaves : Singleton<ISN_GameSaves>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_SaveRemoveResult> ActionSaveRemoved__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_SaveResult> ActionGameSaved__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_FetchResult> ActionSavesFetched__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GK_SavesResolveResult> ActionSavesResolved__BackingField = delegate
	{
	};

	private static Dictionary<string, GK_SavedGame> _CachedGameSaves = new Dictionary<string, GK_SavedGame>();

	public static event Action<GK_SaveRemoveResult> ActionSaveRemoved
	{
		add
		{
			Action<GK_SaveRemoveResult> action = ActionSaveRemoved__BackingField;
			Action<GK_SaveRemoveResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionSaveRemoved__BackingField, (Action<GK_SaveRemoveResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_SaveRemoveResult> action = ActionSaveRemoved__BackingField;
			Action<GK_SaveRemoveResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionSaveRemoved__BackingField, (Action<GK_SaveRemoveResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_SaveResult> ActionGameSaved
	{
		add
		{
			Action<GK_SaveResult> action = ActionGameSaved__BackingField;
			Action<GK_SaveResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionGameSaved__BackingField, (Action<GK_SaveResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_SaveResult> action = ActionGameSaved__BackingField;
			Action<GK_SaveResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionGameSaved__BackingField, (Action<GK_SaveResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_FetchResult> ActionSavesFetched
	{
		add
		{
			Action<GK_FetchResult> action = ActionSavesFetched__BackingField;
			Action<GK_FetchResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionSavesFetched__BackingField, (Action<GK_FetchResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_FetchResult> action = ActionSavesFetched__BackingField;
			Action<GK_FetchResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionSavesFetched__BackingField, (Action<GK_FetchResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GK_SavesResolveResult> ActionSavesResolved
	{
		add
		{
			Action<GK_SavesResolveResult> action = ActionSavesResolved__BackingField;
			Action<GK_SavesResolveResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionSavesResolved__BackingField, (Action<GK_SavesResolveResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_SavesResolveResult> action = ActionSavesResolved__BackingField;
			Action<GK_SavesResolveResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionSavesResolved__BackingField, (Action<GK_SavesResolveResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void SaveGame(byte[] data, string name)
	{
	}

	public void FetchSavedGames()
	{
	}

	public void DeleteSavedGame(string name)
	{
	}

	public void ResolveConflictingSavedGames(List<GK_SavedGame> conflicts, byte[] data)
	{
	}

	public void LoadSaveData(GK_SavedGame save)
	{
	}

	public void OnSaveSuccess(string data)
	{
		GK_SavedGame save = DeserializeGameSave(data);
		GK_SaveResult obj = new GK_SaveResult(save);
		ActionGameSaved__BackingField(obj);
	}

	public void OnSaveFailed(string erroData)
	{
		GK_SaveResult obj = new GK_SaveResult(erroData);
		ActionGameSaved__BackingField(obj);
	}

	public void OnFetchSuccess(string data)
	{
		List<GK_SavedGame> list = new List<GK_SavedGame>();
		string[] array = data.Split(new string[1] { "|%|" }, StringSplitOptions.None);
		for (int i = 0; i < array.Length && !(array[i] == "endofline"); i++)
		{
			GK_SavedGame item = DeserializeGameSave(array[i]);
			list.Add(item);
		}
		GK_FetchResult obj = new GK_FetchResult(list);
		ActionSavesFetched__BackingField(obj);
	}

	public void OnFetchFailed(string errorData)
	{
		GK_FetchResult obj = new GK_FetchResult(errorData);
		ActionSavesFetched__BackingField(obj);
	}

	public void OnResolveSuccess(string data)
	{
		List<GK_SavedGame> list = new List<GK_SavedGame>();
		string[] array = data.Split(new string[1] { "|%|" }, StringSplitOptions.None);
		for (int i = 0; i < array.Length && !(array[i] == "endofline"); i++)
		{
			GK_SavedGame item = DeserializeGameSave(array[i]);
			list.Add(item);
		}
		GK_SavesResolveResult obj = new GK_SavesResolveResult(list);
		ActionSavesResolved__BackingField(obj);
	}

	public void OnResolveFailed(string errorData)
	{
		GK_SavesResolveResult obj = new GK_SavesResolveResult(errorData);
		ActionSavesResolved__BackingField(obj);
	}

	public void OnDeleteSuccess(string name)
	{
		GK_SaveRemoveResult obj = new GK_SaveRemoveResult(name);
		ActionSaveRemoved__BackingField(obj);
	}

	public void OnDeleteFailed(string data)
	{
		string[] array = data.Split(new string[1] { "|%|" }, StringSplitOptions.None);
		string text = array[0];
		string errorData = array[1];
		GK_SaveRemoveResult obj = new GK_SaveRemoveResult(text, errorData);
		ActionSaveRemoved__BackingField(obj);
	}

	private void OnSaveDataLoaded(string data)
	{
		string[] array = data.Split(new string[1] { "|%|" }, StringSplitOptions.None);
		string key = array[0];
		string base64Data = array[1];
		if (_CachedGameSaves.ContainsKey(key))
		{
			_CachedGameSaves[key].GenerateDataLoadEvent(base64Data);
		}
	}

	private void OnSaveDataLoadFailed(string data)
	{
		string[] array = data.Split(new string[1] { "|%|" }, StringSplitOptions.None);
		string key = array[0];
		string erorrData = array[1];
		if (_CachedGameSaves.ContainsKey(key))
		{
			_CachedGameSaves[key].GenerateDataLoadFailedEvent(erorrData);
		}
	}

	private GK_SavedGame DeserializeGameSave(string serializedData)
	{
		string[] array = serializedData.Split('|');
		string id = array[0];
		string text = array[1];
		string device = array[2];
		string dateString = array[3];
		GK_SavedGame gK_SavedGame = new GK_SavedGame(id, text, device, dateString);
		if (_CachedGameSaves.ContainsKey(gK_SavedGame.Id))
		{
			_CachedGameSaves[gK_SavedGame.Id] = gK_SavedGame;
		}
		else
		{
			_CachedGameSaves.Add(gK_SavedGame.Id, gK_SavedGame);
		}
		return gK_SavedGame;
	}
}
