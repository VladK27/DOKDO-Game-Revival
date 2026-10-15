using System.Collections;
using SA.Common.Pattern;
using UnityEngine;
using UnityEngine.UI;

public class FacebookApiTab : FeatureTab
{
	private static bool IsUserInfoLoaded;

	private static bool IsFrindsInfoLoaded;

	private static bool IsAuntificated;

	public Button[] ConnectionDependedntButtons;

	public Text ConnectButtonText;

	public Button ConnectButton;

	public Image Avatar;

	private Sprite AvatarSprite;

	public Text Location;

	public Text Language;

	public Text Mail;

	public Text Name;

	public Text f1Name;

	public Text f2Name;

	public Image f1ProfileLogo;

	public Image f2ProfileLogo;

	private Sprite f1Avatar;

	private Sprite f2Avatar;

	public Texture2D ImageToShare;

	public GameObject friends;

	private int startScore = 555;

	private string UNION_ASSETS_PAGE_ID = "1435528379999137";

	private void Awake()
	{
		SPFacebook.OnInitCompleteAction += OnInit;
		SPFacebook.OnFocusChangedAction += OnFocusChanged;
		SPFacebook.OnAuthCompleteAction += OnAuth;
		SPFacebook.OnPostingCompleteAction += OnPost;
		SPFacebook.OnPlayerScoresRequestCompleteAction += OnPlayerScoreRequestComplete;
		SPFacebook.OnAppScoresRequestCompleteAction += OnAppScoreRequestComplete;
		SPFacebook.OnSubmitScoreRequestCompleteAction += OnSubmitScoreRequestComplete;
		SPFacebook.OnDeleteScoresRequestCompleteAction += OnDeleteScoreRequestComplete;
		Singleton<SPFacebook>.Instance.Init();
		SA_StatusBar.text = "initializing Facebook";
	}

	private void HandleOnRevokePermission(FB_Result result)
	{
		Debug.Log("[HandleOnRevokePermission] result.IsSucceeded: " + result.IsSucceeded + " Responce: " + result.RawData);
	}

	private void FixedUpdate()
	{
		if (IsAuntificated)
		{
			ConnectButtonText.text = "Disconnect";
			Name.text = "Player Connected";
			Button[] connectionDependedntButtons = ConnectionDependedntButtons;
			foreach (Button button in connectionDependedntButtons)
			{
				button.interactable = true;
			}
			if (IsUserInfoLoaded && Singleton<SPFacebook>.Instance.userInfo.GetProfileImage(FB_ProfileImageSize.square) != null)
			{
				if (AvatarSprite == null)
				{
					Texture2D profileImage = Singleton<SPFacebook>.Instance.userInfo.GetProfileImage(FB_ProfileImageSize.square);
					AvatarSprite = Sprite.Create(profileImage, new Rect(0f, 0f, profileImage.width, profileImage.height), new Vector2(0.5f, 0.5f));
					profileImage = null;
				}
				Avatar.sprite = AvatarSprite;
				Name.text = Singleton<SPFacebook>.Instance.userInfo.Name + " aka " + Singleton<SPFacebook>.Instance.userInfo.UserName;
				Location.text = Singleton<SPFacebook>.Instance.userInfo.Location;
				Language.text = Singleton<SPFacebook>.Instance.userInfo.Locale;
			}
			if (IsFrindsInfoLoaded)
			{
				friends.SetActive(true);
				int num = 0;
				if (Singleton<SPFacebook>.Instance.friendsList == null)
				{
					return;
				}
				{
					foreach (FB_UserInfo friends in Singleton<SPFacebook>.Instance.friendsList)
					{
						if (num == 0)
						{
							f1Name.text = friends.Name;
							if (friends.GetProfileImage(FB_ProfileImageSize.square) != null)
							{
								if (f1Avatar == null)
								{
									Texture2D profileImage2 = friends.GetProfileImage(FB_ProfileImageSize.square);
									f1Avatar = Sprite.Create(profileImage2, new Rect(0f, 0f, profileImage2.width, profileImage2.height), new Vector2(0.5f, 0.5f));
									profileImage2 = null;
								}
								f1ProfileLogo.sprite = f1Avatar;
							}
						}
						else
						{
							f2Name.text = friends.Name;
							if (friends.GetProfileImage(FB_ProfileImageSize.square) != null)
							{
								if (f1Avatar == null)
								{
									Texture2D profileImage3 = friends.GetProfileImage(FB_ProfileImageSize.square);
									f2Avatar = Sprite.Create(profileImage3, new Rect(0f, 0f, profileImage3.width, profileImage3.height), new Vector2(0.5f, 0.5f));
									profileImage3 = null;
								}
								f2ProfileLogo.sprite = f2Avatar;
							}
						}
						num++;
					}
					return;
				}
			}
			this.friends.SetActive(false);
		}
		else
		{
			Button[] connectionDependedntButtons2 = ConnectionDependedntButtons;
			foreach (Button button2 in connectionDependedntButtons2)
			{
				button2.interactable = false;
			}
			ConnectButtonText.text = "Connect";
			Name.text = "Player Disconnected";
			this.friends.SetActive(false);
		}
	}

	public void PostWithAuthCheck()
	{
		Singleton<SPFacebook>.Instance.FeedShare(string.Empty, "https://example.com/myapp/?storyID=thelarch", "The Larch", "I thought up a witty tagline about larches", "There are a lot of larch trees around here, aren't there?", "https://example.com/myapp/assets/1/larch.jpg", string.Empty, string.Empty, string.Empty);
	}

	public void PostNativeScreenshot()
	{
		StartCoroutine(PostFBScreenshot());
	}

	public void PostImage()
	{
		AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share", ImageToShare, "facebook.katana");
	}

	private IEnumerator PostFBScreenshot()
	{
		yield return new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0f, 0f, width, height), 0, 0);
		tex.Apply();
		AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share", tex, "facebook.katana");
		Object.Destroy(tex);
	}

	public void Connect()
	{
		if (!IsAuntificated)
		{
			Singleton<SPFacebook>.Instance.Login();
			SA_StatusBar.text = "Log in...";
		}
		else
		{
			LogOut();
			SA_StatusBar.text = "Logged out";
		}
	}

	public void LoadUserData()
	{
		SPFacebook.OnUserDataRequestCompleteAction += OnUserDataLoaded;
		Singleton<SPFacebook>.Instance.LoadUserData();
		SA_StatusBar.text = "Loadin user data..";
	}

	public void PostMessage()
	{
		Singleton<SPFacebook>.Instance.FeedShare(string.Empty, "https://example.com/myapp/?storyID=thelarch", "The Larch", "I thought up a witty tagline about larches", "There are a lot of larch trees around here, aren't there?", "https://example.com/myapp/assets/1/larch.jpg", string.Empty, string.Empty, string.Empty);
		SA_StatusBar.text = "Positng..";
	}

	public void PostScreehShot()
	{
		StartCoroutine(PostScreenshot());
		SA_StatusBar.text = "Positng..";
	}

	public void LoadFriends()
	{
		SPFacebook.OnFriendsDataRequestCompleteAction += OnFriendsDataLoaded;
		int limit = 5;
		Singleton<SPFacebook>.Instance.LoadFrientdsInfo(limit);
		SA_StatusBar.text = "Loading friends..";
	}

	public void AppRequest()
	{
		Singleton<SPFacebook>.Instance.AppRequest("Come play this great game!", null, null, null, null, string.Empty, string.Empty);
	}

	public void LoadScore()
	{
		Singleton<SPFacebook>.Instance.LoadPlayerScores();
	}

	public void LoadAppScores()
	{
		Singleton<SPFacebook>.Instance.LoadAppScores();
	}

	public void SubmitScore()
	{
		startScore++;
		Singleton<SPFacebook>.Instance.SubmitScore(startScore);
	}

	public void DeletePlayerScores()
	{
		Singleton<SPFacebook>.Instance.DeletePlayerScores();
	}

	public void LikePage()
	{
		Application.OpenURL("https://www.facebook.com/unionassets");
	}

	public void CheckLike()
	{
		Debug.Log("[CheckLike]");
		if (Singleton<SPFacebook>.Instance.IsUserLikesPage(Singleton<SPFacebook>.Instance.UserId, UNION_ASSETS_PAGE_ID))
		{
			SA_StatusBar.text = "Current user Likes union assets";
			return;
		}
		SPFacebook.OnLikesListLoadedAction += OnLikesLoaded;
		Singleton<SPFacebook>.Instance.LoadLikes(Singleton<SPFacebook>.Instance.UserId, UNION_ASSETS_PAGE_ID);
	}

	public void ActivateApp()
	{
		SPFacebookAnalytics.ActivateApp();
	}

	public void AchievedLevel()
	{
		SPFacebookAnalytics.AchievedLevel(1);
	}

	public void AddedPaymentInfo()
	{
		SPFacebookAnalytics.AddedPaymentInfo(true);
	}

	public void AddedToCart()
	{
		SPFacebookAnalytics.AddedToCart(54.23f, "HDFU-8452", "shoes");
	}

	public void AddedToWishlist()
	{
		SPFacebookAnalytics.AddedToWishlist(54.23f, "HDFU-8452", "shoes");
	}

	public void CompletedRegistration()
	{
		SPFacebookAnalytics.CompletedRegistration("Email");
	}

	public void InitiatedCheckout()
	{
		SPFacebookAnalytics.InitiatedCheckout(54.23f, 3, "HDFU-8452", "shoes", true);
	}

	public void Purchased()
	{
		SPFacebookAnalytics.Purchased(54.23f, 3, "shoes", "HDFU-8452");
	}

	public void Rated()
	{
		SPFacebookAnalytics.Rated(4, "HDFU-8452", "shoes", 5);
	}

	public void Searched()
	{
		SPFacebookAnalytics.Searched("shoes", "HD", true);
	}

	public void SpentCredits()
	{
		SPFacebookAnalytics.SpentCredits(120f, "shoes", "HDFU-8452");
	}

	public void UnlockedAchievement()
	{
		SPFacebookAnalytics.UnlockedAchievement("ShoeMan");
	}

	public void ViewedContent()
	{
		SPFacebookAnalytics.ViewedContent(54.23f, "shoes", "HDFU-8452");
	}

	private void OnLikesLoaded(FB_Result result)
	{
		Debug.Log("[OnLikesLoaded] result " + result.RawData);
		if (Singleton<SPFacebook>.Instance.IsUserLikesPage(Singleton<SPFacebook>.Instance.UserId, UNION_ASSETS_PAGE_ID))
		{
			SA_StatusBar.text = "Current user Likes union assets";
		}
		else
		{
			SA_StatusBar.text = "Current user does not like union assets";
		}
	}

	private void OnFocusChanged(bool focus)
	{
		Debug.Log("FB OnFocusChanged: " + focus);
		if (!focus)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}

	private void OnUserDataLoaded(FB_Result result)
	{
		SPFacebook.OnUserDataRequestCompleteAction -= OnUserDataLoaded;
		if (result.IsSucceeded)
		{
			SA_StatusBar.text = "User data loaded";
			IsUserInfoLoaded = true;
			Singleton<SPFacebook>.Instance.userInfo.LoadProfileImage(FB_ProfileImageSize.square);
		}
		else
		{
			SA_StatusBar.text = "Opps, user data load failed, something was wrong";
			Debug.Log("Opps, user data load failed, something was wrong");
		}
	}

	private void OnFriendsDataLoaded(FB_Result res)
	{
		SPFacebook.OnFriendsDataRequestCompleteAction -= OnFriendsDataLoaded;
		if (res.Error == null)
		{
			foreach (FB_UserInfo friends in Singleton<SPFacebook>.Instance.friendsList)
			{
				friends.LoadProfileImage(FB_ProfileImageSize.square);
			}
			IsFrindsInfoLoaded = true;
		}
		else
		{
			Debug.Log("Opps, friends data load failed, something was wrong");
		}
	}

	private void OnInit()
	{
		if (Singleton<SPFacebook>.Instance.IsLoggedIn)
		{
			IsAuntificated = true;
		}
		else
		{
			SA_StatusBar.text = "user Login -> fale";
		}
	}

	private void OnAuth(FB_Result result)
	{
		if (Singleton<SPFacebook>.Instance.IsLoggedIn)
		{
			IsAuntificated = true;
			SA_StatusBar.text = "user Login -> true";
		}
		else
		{
			Debug.Log("Failed to log in");
		}
	}

	private void OnPost(FB_PostResult res)
	{
		if (res.IsSucceeded)
		{
			Debug.Log("Posting complete");
			Debug.Log("Posy id: " + res.PostId);
			SA_StatusBar.text = "Posting complete";
		}
		else
		{
			SA_StatusBar.text = "Oops, post failed, something was wrong " + res.Error;
			Debug.Log("Oops, post failed, something was wrong " + res.Error);
		}
	}

	private void OnPlayerScoreRequestComplete(FB_Result result)
	{
		if (result.IsSucceeded)
		{
			string text = "Player has scores in " + Singleton<SPFacebook>.Instance.userScores.Count + " apps\n";
			text = text + "Current Player Score = " + Singleton<SPFacebook>.Instance.GetCurrentPlayerIntScoreByAppId(Singleton<SPFacebook>.Instance.AppId);
			SA_StatusBar.text = text;
		}
		else
		{
			SA_StatusBar.text = result.RawData;
		}
	}

	private void OnAppScoreRequestComplete(FB_Result result)
	{
		if (result.IsSucceeded)
		{
			string text = "Loaded " + Singleton<SPFacebook>.Instance.appScores.Count + " scores results\n";
			text = text + "Current Player Score = " + Singleton<SPFacebook>.Instance.GetScoreByUserId(Singleton<SPFacebook>.Instance.UserId);
			SA_StatusBar.text = text;
		}
		else
		{
			SA_StatusBar.text = result.RawData;
		}
	}

	private void OnSubmitScoreRequestComplete(FB_Result result)
	{
		if (result.IsSucceeded)
		{
			string text = "Score successfully submited\n";
			text = text + "Current Player Score = " + Singleton<SPFacebook>.Instance.GetScoreByUserId(Singleton<SPFacebook>.Instance.UserId);
			SA_StatusBar.text = text;
		}
		else
		{
			SA_StatusBar.text = result.RawData;
		}
	}

	private void OnDeleteScoreRequestComplete(FB_Result result)
	{
		if (result.IsSucceeded)
		{
			string text = "Score successfully deleted\n";
			text = text + "Current Player Score = " + Singleton<SPFacebook>.Instance.GetScoreByUserId(Singleton<SPFacebook>.Instance.UserId);
			SA_StatusBar.text = text;
		}
		else
		{
			SA_StatusBar.text = result.RawData;
		}
	}

	private IEnumerator PostScreenshot()
	{
		yield return new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0f, 0f, width, height), 0, 0);
		tex.Apply();
		Singleton<SPFacebook>.Instance.PostImage("My app ScreehShot", tex);
		Object.Destroy(tex);
	}

	private void LogOut()
	{
		IsUserInfoLoaded = false;
		IsAuntificated = false;
		Singleton<SPFacebook>.Instance.Logout();
	}
}
