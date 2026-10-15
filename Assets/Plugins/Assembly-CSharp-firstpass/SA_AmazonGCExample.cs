using System.Collections.Generic;
using UnityEngine;

public class SA_AmazonGCExample : MonoBehaviour
{
	public DefaultPreviewButton[] buttons;

	public SA_Label playerLabel;

	public SA_Label playerID;

	public SA_Label alias;

	private AMN_WWWTextureLoader loader;

	private Texture2D image;

	public GameObject avatar;

	private List<GC_Achievement> achievements;

	private List<GC_Leaderboard> leaderboards;

	private bool isInitialized;

	private long leaderboard_progress = 100L;

	private float achieve_progress = 150f;

	private string achieve_id = "first_achievement";

	private string leaderboard_id = "first_leaderboard";

	private void Start()
	{
		loader = AMN_WWWTextureLoader.Create();
		DisableButtons();
		AMN_Singleton<SA_AmazonGameCircleManager>.Instance.OnInitializeResult += OnInitializeResult;
		AMN_Singleton<SA_AmazonGameCircleManager>.Instance.OnRequestPlayerDataReceived += OnRequestPlayerDataReceived;
		AMN_Singleton<SA_AmazonGameCircleManager>.Instance.OnRequestAchievementsReceived += OnRequestAchievementsReceived;
		AMN_Singleton<SA_AmazonGameCircleManager>.Instance.OnUpdateAchievementReceived += OnUpdateAchievementReceived;
		AMN_Singleton<SA_AmazonGameCircleManager>.Instance.OnRequestLeaderboardsReceived += OnRequestLeaderboardsReceived;
		AMN_Singleton<SA_AmazonGameCircleManager>.Instance.OnSubmitLeaderboardReceived += OnSubmitLeaderboardReceived;
	}

	private void OnGUI()
	{
		if (isInitialized)
		{
			EnableButtons();
		}
	}

	private void OnInitializeResult(AMN_InitializeResult result)
	{
		if (result.isSuccess)
		{
			isInitialized = true;
			playerLabel.text = "Player Connected";
			SA_StatusBar.text = "Amazon connected";
		}
		else
		{
			playerLabel.text = "Player Disconnected";
			SA_StatusBar.text = "Amazon disconnected with error " + result.Error;
		}
	}

	private void OnRequestPlayerDataReceived(AMN_RequestPlayerDataResult result)
	{
		if (result.isSuccess)
		{
			GC_Player player = result.Player;
			playerID.text = "PlayerID:  " + player.PlayerId;
			alias.text = "Alias:  " + player.Name;
			LoadAvatar(player.AvatarUrl);
		}
		else
		{
			playerID.text = "PlayerID: none";
			alias.text = "Alias: none";
		}
	}

	private void OnProfileImageLoaded(Texture2D texture)
	{
		loader.OnLoad -= OnProfileImageLoaded;
		if (texture != null)
		{
			image = texture;
		}
		avatar.GetComponent<Renderer>().material.mainTexture = image;
	}

	private void OnRequestAchievementsReceived(AMN_RequestAchievementsResult result)
	{
		if (result.isSuccess)
		{
			achievements = result.AchievementList;
			Debug.Log("Printing Achievements list, total items: " + achievements.Count);
			foreach (GC_Achievement achievement in achievements)
			{
				Debug.Log(achievement.Identifier);
				Debug.Log(achievement.Description);
				Debug.Log(achievement.PointValue);
				Debug.Log(achievement.DateUnlocked);
			}
			SA_StatusBar.text = "OnRequest Achievements success!";
		}
		else
		{
			SA_StatusBar.text = "OnRequest Achievements Failed with error " + result.Error;
		}
	}

	private void OnRequestLeaderboardsReceived(AMN_RequestLeaderboardsResult result)
	{
		if (result.isSuccess)
		{
			leaderboards = result.LeaderboardsList;
			Debug.Log("Printing Leaderboards list, total items: " + leaderboards.Count);
			foreach (GC_Leaderboard leaderboard in leaderboards)
			{
				Debug.Log(leaderboard.Identifier);
				Debug.Log(leaderboard.Title);
				Debug.Log(leaderboard.Description);
				Debug.Log(leaderboard.ImageUrl);
			}
			SA_StatusBar.text = "OnRequest Leaderboards success!";
		}
		else
		{
			SA_StatusBar.text = "OnRequest Leaderboards Failed with error " + result.Error;
		}
	}

	private void OnUpdateAchievementReceived(AMN_UpdateAchievementResult result)
	{
		if (result.isSuccess)
		{
			SA_StatusBar.text = "OnUpdate Achievement Completed for id " + result.AchievementID;
		}
		else
		{
			SA_StatusBar.text = "OnUpdate Achievement Failed for id " + result.AchievementID + result.Error;
		}
	}

	private void OnSubmitLeaderboardReceived(AMN_SubmitLeaderboardResult result)
	{
		if (result.isSuccess)
		{
			SA_StatusBar.text = "OnSubmit Leaderboard Completed for id " + result.LeaderboardID;
		}
		else
		{
			SA_StatusBar.text = "OnSubmit Leaderboard Failed for id " + result.LeaderboardID + result.Error;
		}
	}

	private void InitializeAmazon()
	{
		if (AMN_Singleton<SA_AmazonGameCircleManager>.Instance.IsInitialized)
		{
			SA_StatusBar.text = "Disconnecting from Amazon Service";
			AMN_Singleton<SA_AmazonGameCircleManager>.Instance.Disconnect();
		}
		else
		{
			SA_StatusBar.text = "Connecting to Amazon";
			AMN_Singleton<SA_AmazonGameCircleManager>.Instance.Connect();
		}
	}

	private void DisableButtons()
	{
		DefaultPreviewButton[] array = buttons;
		foreach (DefaultPreviewButton defaultPreviewButton in array)
		{
			defaultPreviewButton.DisabledButton();
		}
	}

	private void EnableButtons()
	{
		DefaultPreviewButton[] array = buttons;
		foreach (DefaultPreviewButton defaultPreviewButton in array)
		{
			defaultPreviewButton.EnabledButton();
		}
	}

	private void ShowGCOverlay()
	{
		AMN_Singleton<SA_AmazonGameCircleManager>.Instance.ShowGCOverlay();
	}

	private void ShowSignInPage()
	{
		AMN_Singleton<SA_AmazonGameCircleManager>.Instance.ShowSignInPage();
	}

	private void RetrieveLocalPlayer()
	{
		AMN_Singleton<SA_AmazonGameCircleManager>.Instance.RetrieveLocalPlayer();
	}

	private void ShowAchievementsOverlay()
	{
		AMN_Singleton<SA_AmazonGameCircleManager>.Instance.ShowAchievementsOverlay();
	}

	private void RequestAchievements()
	{
		AMN_Singleton<SA_AmazonGameCircleManager>.Instance.RequestAchievements();
	}

	private void LoadAvatar(string url)
	{
		loader.OnLoad += OnProfileImageLoaded;
		loader.LoadTexture(url);
	}

	private void UpdateAchievementProgress()
	{
		AMN_Singleton<SA_AmazonGameCircleManager>.Instance.UpdateAchievementProgress(achieve_id, achieve_progress);
	}

	private void ShowLeaderboardsOverlay()
	{
		AMN_Singleton<SA_AmazonGameCircleManager>.Instance.ShowLeaderboardsOverlay();
	}

	private void RequestLeaderboards()
	{
		AMN_Singleton<SA_AmazonGameCircleManager>.Instance.RequestLeaderboards();
	}

	private void SubmitLeaderBoardProgress()
	{
		AMN_Singleton<SA_AmazonGameCircleManager>.Instance.SubmitLeaderBoardProgress(leaderboard_id, leaderboard_progress++);
	}
}
