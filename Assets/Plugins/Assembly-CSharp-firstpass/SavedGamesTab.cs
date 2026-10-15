using System;
using System.Collections;
using SA.Common.Pattern;
using UnityEngine;
using UnityEngine.UI;

public class SavedGamesTab : FeatureTab
{
	public Image avatar;

	private Sprite logo;

	private Sprite defaulttexture;

	public Button connectButton;

	public Text connectButtonLabel;

	public Text playerLabel;

	public Button[] ConnectionDependedntButtons;

	private void Start()
	{
		playerLabel.text = "Player Disconnected";
		defaulttexture = avatar.sprite;
		GooglePlayConnection.ActionPlayerConnected += OnPlayerConnected;
		GooglePlayConnection.ActionPlayerDisconnected += OnPlayerDisconnected;
		GooglePlayConnection.ActionConnectionResultReceived += OnConnectionResult;
		GooglePlaySavedGamesManager.ActionNewGameSaveRequest += ActionNewGameSaveRequest;
		GooglePlaySavedGamesManager.ActionGameSaveLoaded += ActionGameSaveLoaded;
		GooglePlaySavedGamesManager.ActionConflict += ActionConflict;
		if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
		{
			OnPlayerConnected();
		}
	}

	private void OnDestroy()
	{
		GooglePlayConnection.ActionPlayerConnected -= OnPlayerConnected;
		GooglePlayConnection.ActionPlayerDisconnected -= OnPlayerDisconnected;
		GooglePlayConnection.ActionConnectionResultReceived -= OnConnectionResult;
		GooglePlaySavedGamesManager.ActionNewGameSaveRequest -= ActionNewGameSaveRequest;
		GooglePlaySavedGamesManager.ActionGameSaveLoaded -= ActionGameSaveLoaded;
		GooglePlaySavedGamesManager.ActionConflict -= ActionConflict;
	}

	public void ConncetButtonPress()
	{
		Debug.Log("GooglePlayManager State  -> " + GooglePlayConnection.State);
		if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
		{
			SA_StatusBar.text = "Disconnecting from Play Service...";
			Singleton<GooglePlayConnection>.Instance.Disconnect();
		}
		else
		{
			SA_StatusBar.text = "Connecting to Play Service...";
			Singleton<GooglePlayConnection>.Instance.Connect();
		}
	}

	private void FixedUpdate()
	{
		if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
		{
			if (Singleton<GooglePlayManager>.Instance.player.icon != null)
			{
				Texture2D icon = Singleton<GooglePlayManager>.Instance.player.icon;
				if (logo == null)
				{
					logo = Sprite.Create(icon, new Rect(0f, 0f, icon.width, icon.height), new Vector2(0.5f, 0.5f));
				}
				avatar.sprite = logo;
			}
		}
		else
		{
			avatar.sprite = defaulttexture;
		}
		string text = "Connect";
		if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
		{
			text = "Disconnect";
			Button[] connectionDependedntButtons = ConnectionDependedntButtons;
			foreach (Button button in connectionDependedntButtons)
			{
				button.interactable = true;
			}
		}
		else
		{
			Button[] connectionDependedntButtons2 = ConnectionDependedntButtons;
			foreach (Button button2 in connectionDependedntButtons2)
			{
				button2.interactable = false;
			}
			text = ((GooglePlayConnection.State != GPConnectionState.STATE_DISCONNECTED && GooglePlayConnection.State != GPConnectionState.STATE_UNCONFIGURED) ? "Connecting.." : "Connect");
		}
		connectButtonLabel.text = text;
	}

	public void CreateNewSnapshot()
	{
		StartCoroutine(MakeScreenshotAndSaveGameData());
	}

	public void ShowSavedGamesUI()
	{
		int maxNumberOfSavedGamesToShow = 5;
		Singleton<GooglePlaySavedGamesManager>.Instance.ShowSavedGamesUI("See My Saves", maxNumberOfSavedGamesToShow);
	}

	public void LoadSavedGames()
	{
		GooglePlaySavedGamesManager.ActionAvailableGameSavesLoaded += ActionAvailableGameSavesLoaded;
		Singleton<GooglePlaySavedGamesManager>.Instance.LoadAvailableSavedGames();
		SA_StatusBar.text = "Loading saved games.. ";
	}

	private void ActionAvailableGameSavesLoaded(GooglePlayResult res)
	{
		GooglePlaySavedGamesManager.ActionAvailableGameSavesLoaded -= ActionAvailableGameSavesLoaded;
		if (res.IsSucceeded)
		{
			foreach (GP_SnapshotMeta availableGameSafe in Singleton<GooglePlaySavedGamesManager>.Instance.AvailableGameSaves)
			{
				Debug.Log("Meta.Title: " + availableGameSafe.Title);
				Debug.Log("Meta.Description: " + availableGameSafe.Description);
				Debug.Log("Meta.CoverImageUrl): " + availableGameSafe.CoverImageUrl);
				Debug.Log("Meta.LastModifiedTimestamp: " + availableGameSafe.LastModifiedTimestamp);
				Debug.Log("Meta.TotalPlayedTime" + availableGameSafe.TotalPlayedTime);
			}
			if (Singleton<GooglePlaySavedGamesManager>.Instance.AvailableGameSaves.Count > 0)
			{
				GP_SnapshotMeta gP_SnapshotMeta = Singleton<GooglePlaySavedGamesManager>.Instance.AvailableGameSaves[0];
				AndroidDialog androidDialog = AndroidDialog.Create("Load Snapshot?", "Would you like to load " + gP_SnapshotMeta.Title);
				androidDialog.ActionComplete += OnSpanshotLoadDialogComplete;
			}
		}
		else
		{
			AndroidMessage.Create("Fail", "Available Game Saves Load failed");
		}
	}

	private void OnSpanshotLoadDialogComplete(AndroidDialogResult res)
	{
		if (res == AndroidDialogResult.YES)
		{
			GP_SnapshotMeta gP_SnapshotMeta = Singleton<GooglePlaySavedGamesManager>.Instance.AvailableGameSaves[0];
			Singleton<GooglePlaySavedGamesManager>.Instance.LoadSpanshotByName(gP_SnapshotMeta.Title);
		}
	}

	private void ActionNewGameSaveRequest()
	{
		SA_StatusBar.text = "New  Game Save Requested, Creating new save..";
		Debug.Log("New  Game Save Requested, Creating new save..");
		StartCoroutine(MakeScreenshotAndSaveGameData());
		AndroidMessage.Create("Result", "New Game Save Request");
	}

	private void ActionGameSaveLoaded(GP_SpanshotLoadResult result)
	{
		Debug.Log("ActionGameSaveLoaded: " + result.Message);
		if (result.IsSucceeded)
		{
			Debug.Log("Snapshot.Title: " + result.Snapshot.meta.Title);
			Debug.Log("Snapshot.Description: " + result.Snapshot.meta.Description);
			Debug.Log("Snapshot.CoverImageUrl): " + result.Snapshot.meta.CoverImageUrl);
			Debug.Log("Snapshot.LastModifiedTimestamp: " + result.Snapshot.meta.LastModifiedTimestamp);
			Debug.Log("Snapshot.stringData: " + result.Snapshot.stringData);
			Debug.Log("Snapshot.bytes.Length: " + result.Snapshot.bytes.Length);
			AndroidMessage.Create("Snapshot Loaded", "Data: " + result.Snapshot.stringData);
		}
		SA_StatusBar.text = "Games Loaded: " + result.Message;
	}

	private void ActionGameSaveResult(GP_SpanshotLoadResult result)
	{
		GooglePlaySavedGamesManager.ActionGameSaveResult -= ActionGameSaveResult;
		Debug.Log("ActionGameSaveResult: " + result.Message);
		if (result.IsSucceeded)
		{
			SA_StatusBar.text = "Games Saved: " + result.Snapshot.meta.Title;
		}
		else
		{
			SA_StatusBar.text = "Games Save Failed";
		}
		AndroidMessage.Create("Game Save Result", SA_StatusBar.text);
	}

	private void ActionConflict(GP_SnapshotConflict result)
	{
		Debug.Log("Conflict Detected: ");
		GP_Snapshot snapshot = result.Snapshot;
		GP_Snapshot conflictingSnapshot = result.ConflictingSnapshot;
		GP_Snapshot snapshot2 = snapshot;
		if (snapshot.meta.LastModifiedTimestamp < conflictingSnapshot.meta.LastModifiedTimestamp)
		{
			snapshot2 = conflictingSnapshot;
		}
		result.Resolve(snapshot2);
	}

	private void OnPlayerDisconnected()
	{
		SA_StatusBar.text = "Player Disconnected";
		playerLabel.text = "Player Disconnected";
	}

	private void OnPlayerConnected()
	{
		SA_StatusBar.text = "Player Connected";
		playerLabel.text = Singleton<GooglePlayManager>.Instance.player.name;
	}

	private void OnConnectionResult(GooglePlayConnectionResult result)
	{
		SA_StatusBar.text = "ConnectionResul:  " + result.code;
		Debug.Log(result.code.ToString());
	}

	private IEnumerator MakeScreenshotAndSaveGameData()
	{
		yield return new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D Screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
		Screenshot.ReadPixels(new Rect(0f, 0f, width, height), 0, 0);
		Screenshot.Apply();
		long TotalPlayedTime = 20000L;
		string currentSaveName = "TestingSameName";
		string description = "Modified data at: " + DateTime.Now.ToString("MM/dd/yyyy H:mm:ss");
		GooglePlaySavedGamesManager.ActionGameSaveResult += ActionGameSaveResult;
		Singleton<GooglePlaySavedGamesManager>.Instance.CreateNewSnapshot(currentSaveName, description, Screenshot, "some save data, for example you can use JSON or byte array " + UnityEngine.Random.Range(1, 10000), TotalPlayedTime);
		UnityEngine.Object.Destroy(Screenshot);
	}
}
