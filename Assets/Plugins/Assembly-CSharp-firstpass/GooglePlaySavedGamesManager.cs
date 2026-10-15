using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class GooglePlaySavedGamesManager : Singleton<GooglePlaySavedGamesManager>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action ActionGameSaveUIClosed__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action ActionNewGameSaveRequest__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GooglePlayResult> ActionAvailableGameSavesLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GP_SpanshotLoadResult> ActionGameSaveLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GP_SpanshotLoadResult> ActionGameSaveResult__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GP_SnapshotConflict> ActionConflict__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<GP_DeleteSnapshotResult> ActionGameSaveRemoved__BackingField = delegate
	{
	};

	private List<GP_SnapshotMeta> _AvailableGameSaves = new List<GP_SnapshotMeta>();

	public List<GP_SnapshotMeta> AvailableGameSaves
	{
		get
		{
			return _AvailableGameSaves;
		}
	}

	public static event Action ActionGameSaveUIClosed
	{
		add
		{
			Action action = ActionGameSaveUIClosed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionGameSaveUIClosed__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ActionGameSaveUIClosed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionGameSaveUIClosed__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action ActionNewGameSaveRequest
	{
		add
		{
			Action action = ActionNewGameSaveRequest__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionNewGameSaveRequest__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ActionNewGameSaveRequest__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionNewGameSaveRequest__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GooglePlayResult> ActionAvailableGameSavesLoaded
	{
		add
		{
			Action<GooglePlayResult> action = ActionAvailableGameSavesLoaded__BackingField;
			Action<GooglePlayResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionAvailableGameSavesLoaded__BackingField, (Action<GooglePlayResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GooglePlayResult> action = ActionAvailableGameSavesLoaded__BackingField;
			Action<GooglePlayResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionAvailableGameSavesLoaded__BackingField, (Action<GooglePlayResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GP_SpanshotLoadResult> ActionGameSaveLoaded
	{
		add
		{
			Action<GP_SpanshotLoadResult> action = ActionGameSaveLoaded__BackingField;
			Action<GP_SpanshotLoadResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionGameSaveLoaded__BackingField, (Action<GP_SpanshotLoadResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GP_SpanshotLoadResult> action = ActionGameSaveLoaded__BackingField;
			Action<GP_SpanshotLoadResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionGameSaveLoaded__BackingField, (Action<GP_SpanshotLoadResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GP_SpanshotLoadResult> ActionGameSaveResult
	{
		add
		{
			Action<GP_SpanshotLoadResult> action = ActionGameSaveResult__BackingField;
			Action<GP_SpanshotLoadResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionGameSaveResult__BackingField, (Action<GP_SpanshotLoadResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GP_SpanshotLoadResult> action = ActionGameSaveResult__BackingField;
			Action<GP_SpanshotLoadResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionGameSaveResult__BackingField, (Action<GP_SpanshotLoadResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GP_SnapshotConflict> ActionConflict
	{
		add
		{
			Action<GP_SnapshotConflict> action = ActionConflict__BackingField;
			Action<GP_SnapshotConflict> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionConflict__BackingField, (Action<GP_SnapshotConflict>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GP_SnapshotConflict> action = ActionConflict__BackingField;
			Action<GP_SnapshotConflict> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionConflict__BackingField, (Action<GP_SnapshotConflict>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<GP_DeleteSnapshotResult> ActionGameSaveRemoved
	{
		add
		{
			Action<GP_DeleteSnapshotResult> action = ActionGameSaveRemoved__BackingField;
			Action<GP_DeleteSnapshotResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionGameSaveRemoved__BackingField, (Action<GP_DeleteSnapshotResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GP_DeleteSnapshotResult> action = ActionGameSaveRemoved__BackingField;
			Action<GP_DeleteSnapshotResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionGameSaveRemoved__BackingField, (Action<GP_DeleteSnapshotResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void ShowSavedGamesUI(string title, int maxNumberOfSavedGamesToShow, bool allowAddButton = true, bool allowDelete = true)
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.ShowSavedGamesUI_Bridge(title, maxNumberOfSavedGamesToShow, allowAddButton, allowDelete);
		}
	}

	public void CreateNewSnapshot(string name, string description, Texture2D coverImage, string spanshotData, long PlayedTime)
	{
		CreateNewSnapshot(name, description, coverImage, GetBytes(spanshotData), PlayedTime);
	}

	public void CreateNewSnapshot(string name, string description, Texture2D coverImage, byte[] spanshotData, long PlayedTime)
	{
		string imageData = string.Empty;
		if (coverImage != null)
		{
			byte[] inArray = coverImage.EncodeToPNG();
			imageData = Convert.ToBase64String(inArray);
		}
		else
		{
			UnityEngine.Debug.LogWarning("GooglePlaySavedGmaesManager::CreateNewSnapshot:  coverImage is null");
		}
		string data = Convert.ToBase64String(spanshotData);
		AN_GMSGeneralProxy.CreateNewSpanshot_Bridge(name, description, imageData, data, PlayedTime);
	}

	public void LoadSpanshotByName(string name)
	{
		AN_GMSGeneralProxy.OpenSpanshotByName_Bridge(name);
	}

	public void DeleteSpanshotByName(string name)
	{
		AN_GMSGeneralProxy.DeleteSpanshotByName_Bridge(name);
	}

	public void LoadAvailableSavedGames()
	{
		AN_GMSGeneralProxy.LoadSpanshots_Bridge();
	}

	private static byte[] GetBytes(string str)
	{
		byte[] array = new byte[str.Length * 2];
		Buffer.BlockCopy(str.ToCharArray(), 0, array, 0, array.Length);
		return array;
	}

	private static string GetString(byte[] bytes)
	{
		char[] array = ((bytes.Length % 2 == 0) ? new char[bytes.Length / 2] : new char[bytes.Length / 2 + 1]);
		Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);
		return new string(array);
	}

	private void OnLoadSnapshotsResult(string data)
	{
		UnityEngine.Debug.Log("SavedGamesManager: OnLoadSnapshotsResult");
		string[] array = data.Split("|"[0]);
		GooglePlayResult googlePlayResult = new GooglePlayResult(array[0]);
		if (googlePlayResult.IsSucceeded)
		{
			_AvailableGameSaves.Clear();
			for (int i = 1; i < array.Length && !(array[i] == "endofline"); i += 5)
			{
				GP_SnapshotMeta gP_SnapshotMeta = new GP_SnapshotMeta();
				gP_SnapshotMeta.Title = array[i];
				gP_SnapshotMeta.LastModifiedTimestamp = Convert.ToInt64(array[i + 1]);
				gP_SnapshotMeta.Description = array[i + 2];
				gP_SnapshotMeta.CoverImageUrl = array[i + 3];
				gP_SnapshotMeta.TotalPlayedTime = Convert.ToInt64(array[i + 4]);
				_AvailableGameSaves.Add(gP_SnapshotMeta);
			}
			UnityEngine.Debug.Log("Loaded: " + _AvailableGameSaves.Count + " Snapshots");
		}
		ActionAvailableGameSavesLoaded__BackingField(googlePlayResult);
	}

	private void OnSavedGamePicked(string data)
	{
		UnityEngine.Debug.Log("SavedGamesManager: OnSavedGamePicked");
		string[] array = data.Split("|"[0]);
		GP_SpanshotLoadResult gP_SpanshotLoadResult = new GP_SpanshotLoadResult(array[0]);
		if (gP_SpanshotLoadResult.IsSucceeded)
		{
			string title = array[1];
			long lastModifiedTimestamp = Convert.ToInt64(array[2]);
			string description = array[3];
			string coverImageUrl = array[4];
			long totalPlayedTime = Convert.ToInt64(array[5]);
			byte[] bytes = Convert.FromBase64String(array[6]);
			GP_Snapshot gP_Snapshot = new GP_Snapshot();
			gP_Snapshot.meta.Title = title;
			gP_Snapshot.meta.Description = description;
			gP_Snapshot.meta.CoverImageUrl = coverImageUrl;
			gP_Snapshot.meta.LastModifiedTimestamp = lastModifiedTimestamp;
			gP_Snapshot.meta.TotalPlayedTime = totalPlayedTime;
			gP_Snapshot.bytes = bytes;
			gP_Snapshot.stringData = GetString(bytes);
			gP_SpanshotLoadResult.SetSnapShot(gP_Snapshot);
		}
		ActionGameSaveLoaded__BackingField(gP_SpanshotLoadResult);
	}

	private void OnSavedGameSaveResult(string data)
	{
		UnityEngine.Debug.Log("SavedGamesManager: OnSavedGameSaveResult");
		string[] array = data.Split("|"[0]);
		GP_SpanshotLoadResult gP_SpanshotLoadResult = new GP_SpanshotLoadResult(array[0]);
		if (gP_SpanshotLoadResult.IsSucceeded)
		{
			string title = array[1];
			long lastModifiedTimestamp = Convert.ToInt64(array[2]);
			string description = array[3];
			string coverImageUrl = array[4];
			long totalPlayedTime = Convert.ToInt64(array[5]);
			byte[] bytes = Convert.FromBase64String(array[6]);
			GP_Snapshot gP_Snapshot = new GP_Snapshot();
			gP_Snapshot.meta.Title = title;
			gP_Snapshot.meta.Description = description;
			gP_Snapshot.meta.CoverImageUrl = coverImageUrl;
			gP_Snapshot.meta.LastModifiedTimestamp = lastModifiedTimestamp;
			gP_Snapshot.meta.TotalPlayedTime = totalPlayedTime;
			gP_Snapshot.bytes = bytes;
			gP_Snapshot.stringData = GetString(bytes);
			gP_SpanshotLoadResult.SetSnapShot(gP_Snapshot);
		}
		ActionGameSaveResult__BackingField(gP_SpanshotLoadResult);
	}

	private void OnConflict(string data)
	{
		UnityEngine.Debug.Log("SavedGamesManager: OnConflict");
		string[] array = data.Split("|"[0]);
		string title = array[0];
		long lastModifiedTimestamp = Convert.ToInt64(array[1]);
		string description = array[2];
		string coverImageUrl = array[3];
		long totalPlayedTime = Convert.ToInt64(array[4]);
		byte[] bytes = Convert.FromBase64String(array[5]);
		GP_Snapshot gP_Snapshot = new GP_Snapshot();
		gP_Snapshot.meta.Title = title;
		gP_Snapshot.meta.Description = description;
		gP_Snapshot.meta.CoverImageUrl = coverImageUrl;
		gP_Snapshot.meta.LastModifiedTimestamp = lastModifiedTimestamp;
		gP_Snapshot.meta.TotalPlayedTime = totalPlayedTime;
		gP_Snapshot.bytes = bytes;
		gP_Snapshot.stringData = GetString(bytes);
		title = array[6];
		lastModifiedTimestamp = Convert.ToInt64(array[7]);
		description = array[8];
		coverImageUrl = array[9];
		totalPlayedTime = Convert.ToInt64(array[10]);
		bytes = Convert.FromBase64String(array[11]);
		GP_Snapshot gP_Snapshot2 = new GP_Snapshot();
		gP_Snapshot2.meta.Title = title;
		gP_Snapshot2.meta.Description = description;
		gP_Snapshot2.meta.CoverImageUrl = coverImageUrl;
		gP_Snapshot2.meta.LastModifiedTimestamp = lastModifiedTimestamp;
		gP_Snapshot2.meta.TotalPlayedTime = totalPlayedTime;
		gP_Snapshot2.bytes = bytes;
		gP_Snapshot2.stringData = GetString(bytes);
		GP_SnapshotConflict obj = new GP_SnapshotConflict(gP_Snapshot, gP_Snapshot2);
		ActionConflict__BackingField(obj);
	}

	private void OnNewGameSaveRequest(string data)
	{
		UnityEngine.Debug.Log("SavedGamesManager: OnNewGameSaveRequest");
		ActionNewGameSaveRequest__BackingField();
	}

	private void OnSavedGamesUIClosed(string data)
	{
		UnityEngine.Debug.Log("OnSavedGamesUIClosed");
		ActionGameSaveUIClosed__BackingField();
	}

	private void OnDeleteResult(string data)
	{
		string[] array = data.Split("|"[0]);
		GP_DeleteSnapshotResult gP_DeleteSnapshotResult = new GP_DeleteSnapshotResult(array[0]);
		if (gP_DeleteSnapshotResult.IsSucceeded)
		{
			gP_DeleteSnapshotResult.SetId(array[1]);
		}
		ActionGameSaveRemoved__BackingField(gP_DeleteSnapshotResult);
	}
}
