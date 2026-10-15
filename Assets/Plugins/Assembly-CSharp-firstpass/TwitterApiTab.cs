using System.Collections;
using SA.Common.Pattern;
using UnityEngine;
using UnityEngine.UI;

public class TwitterApiTab : FeatureTab
{
	private static bool IsUserInfoLoaded;

	private static bool IsAuthenticated;

	public Texture2D ImageToShare;

	public Text ConnectButtonText;

	public Button ConnectButton;

	public Image Avatar;

	public Text Location;

	public Text Language;

	public Text Status;

	public Text Name;

	public Button[] AuthDependedButtons;

	private Sprite AvatarSprite;

	private void Awake()
	{
		Singleton<AndroidTwitterManager>.Instance.OnTwitterInitedAction += OnTwitterInitedAction;
		Singleton<AndroidTwitterManager>.Instance.OnPostingCompleteAction += OnPostingCompleteAction;
		Singleton<AndroidTwitterManager>.Instance.OnUserDataRequestCompleteAction += OnUserDataRequestCompleteAction;
		Singleton<AndroidTwitterManager>.Instance.OnAuthCompleteAction += OnAuthCompleteAction;
		Singleton<AndroidTwitterManager>.Instance.Init();
	}

	private void FixedUpdate()
	{
		if (IsAuthenticated)
		{
			ConnectButtonText.text = "Disconnect";
			Name.text = "Player Connected";
			Button[] authDependedButtons = AuthDependedButtons;
			foreach (Button button in authDependedButtons)
			{
				button.interactable = true;
			}
			if (!IsUserInfoLoaded)
			{
				return;
			}
			if (Singleton<AndroidTwitterManager>.Instance.userInfo.profile_image != null)
			{
				if (AvatarSprite == null)
				{
					AvatarSprite = Sprite.Create(Singleton<AndroidTwitterManager>.Instance.userInfo.profile_image, new Rect(0f, 0f, Singleton<AndroidTwitterManager>.Instance.userInfo.profile_image.width, Singleton<AndroidTwitterManager>.Instance.userInfo.profile_image.height), new Vector2(0.5f, 0.5f));
				}
				Avatar.sprite = AvatarSprite;
			}
			Name.text = Singleton<AndroidTwitterManager>.Instance.userInfo.name + " aka " + Singleton<AndroidTwitterManager>.Instance.userInfo.screen_name;
			Location.text = Singleton<AndroidTwitterManager>.Instance.userInfo.location;
			Language.text = Singleton<AndroidTwitterManager>.Instance.userInfo.lang;
			Status.text = Singleton<AndroidTwitterManager>.Instance.userInfo.status.text;
		}
		else
		{
			Button[] authDependedButtons2 = AuthDependedButtons;
			foreach (Button button2 in authDependedButtons2)
			{
				button2.interactable = false;
			}
			ConnectButtonText.text = "Connect";
			Name.text = "Player Disconnected";
		}
	}

	public void Connect()
	{
		if (!IsAuthenticated)
		{
			Singleton<AndroidTwitterManager>.Instance.AuthenticateUser();
		}
		else
		{
			LogOut();
		}
	}

	public void PostWithAuthCheck()
	{
		Singleton<AndroidTwitterManager>.Instance.PostWithAuthCheck("Hello, I'am posting this from my app");
	}

	public void PostNativeScreenshot()
	{
		StartCoroutine(PostTWScreenshot());
	}

	public void PostMSG()
	{
		AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share", "twi");
	}

	public void PostImage()
	{
		AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share", ImageToShare, "twi");
	}

	private IEnumerator PostTWScreenshot()
	{
		yield return new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0f, 0f, width, height), 0, 0);
		tex.Apply();
		AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share", tex, "twi");
		Object.Destroy(tex);
	}

	public void LoadUserData()
	{
		Singleton<AndroidTwitterManager>.Instance.LoadUserData();
	}

	private void Test()
	{
		TW_OAuthAPIRequest tW_OAuthAPIRequest = TW_OAuthAPIRequest.Create();
		tW_OAuthAPIRequest.Send("https://api.twitter.com/1.1/statuses/home_timeline.json");
		tW_OAuthAPIRequest.OnResult += OnResult;
	}

	private void OnResult(TW_APIRequstResult result)
	{
		Debug.Log("Is Request Succeeded: " + result.IsSucceeded);
		Debug.Log("Responce data:");
		Debug.Log(result.responce);
	}

	public void PostMessage()
	{
		Singleton<AndroidTwitterManager>.Instance.Post("Hello, I'am posting this from my app");
	}

	public void PostScreehShot()
	{
		StartCoroutine(PostScreenshot());
	}

	private void OnUserDataRequestCompleteAction(TWResult result)
	{
		if (result.IsSucceeded)
		{
			IsUserInfoLoaded = true;
			Singleton<AndroidTwitterManager>.Instance.userInfo.LoadProfileImage();
			Singleton<AndroidTwitterManager>.Instance.userInfo.LoadBackgroundImage();
		}
		else
		{
			Debug.Log("Opps, user data load failed, something was wrong");
		}
	}

	private void OnPostingCompleteAction(TWResult result)
	{
		if (result.IsSucceeded)
		{
			Debug.Log("Congrats. You just posted something to Twitter!");
		}
		else
		{
			Debug.Log("Oops! Posting failed. Something went wrong.");
		}
	}

	private void OnAuthCompleteAction(TWResult result)
	{
		if (result.IsSucceeded)
		{
			OnAuth();
		}
	}

	private void OnTwitterInitedAction(TWResult result)
	{
		if (Singleton<AndroidTwitterManager>.Instance.IsAuthed)
		{
			OnAuth();
		}
	}

	private void OnAuth()
	{
		IsAuthenticated = true;
	}

	public void RetrieveTimeLine()
	{
		TW_UserTimeLineRequest tW_UserTimeLineRequest = TW_UserTimeLineRequest.Create();
		tW_UserTimeLineRequest.ActionComplete += OnTimeLineRequestComplete;
		tW_UserTimeLineRequest.AddParam("screen_name", "unity3d");
		tW_UserTimeLineRequest.AddParam("count", "1");
		tW_UserTimeLineRequest.Send();
	}

	public void UserLookUpRequest()
	{
		TW_UsersLookUpRequest tW_UsersLookUpRequest = TW_UsersLookUpRequest.Create();
		tW_UsersLookUpRequest.ActionComplete += OnLookUpRequestComplete;
		tW_UsersLookUpRequest.AddParam("screen_name", "unity3d");
		tW_UsersLookUpRequest.Send();
	}

	public void FriedsidsRequest()
	{
		TW_FriendsIdsRequest tW_FriendsIdsRequest = TW_FriendsIdsRequest.Create();
		tW_FriendsIdsRequest.ActionComplete += OnIdsLoaded;
		tW_FriendsIdsRequest.AddParam("screen_name", "unity3d");
		tW_FriendsIdsRequest.Send();
	}

	public void FollowersidsRequest()
	{
		TW_FollowersIdsRequest tW_FollowersIdsRequest = TW_FollowersIdsRequest.Create();
		tW_FollowersIdsRequest.ActionComplete += OnIdsLoaded;
		tW_FollowersIdsRequest.AddParam("screen_name", "unity3d");
		tW_FollowersIdsRequest.Send();
	}

	public void TweetSearch()
	{
		TW_SearchTweetsRequest tW_SearchTweetsRequest = TW_SearchTweetsRequest.Create();
		tW_SearchTweetsRequest.ActionComplete += OnSearchRequestComplete;
		tW_SearchTweetsRequest.AddParam("q", "@noradio");
		tW_SearchTweetsRequest.AddParam("count", "1");
		tW_SearchTweetsRequest.Send();
	}

	private void OnIdsLoaded(TW_APIRequstResult result)
	{
		if (result.IsSucceeded)
		{
			AN_PoupsProxy.showMessage("Ids Request Succeeded", "Totals ids loaded: " + result.ids.Count);
			Debug.Log(result.ids.Count);
		}
		else
		{
			Debug.Log(result.responce);
			AN_PoupsProxy.showMessage("Ids Request Failed", result.responce);
		}
	}

	private void OnLookUpRequestComplete(TW_APIRequstResult result)
	{
		if (result.IsSucceeded)
		{
			string text = "User Id: ";
			text += result.users[0].id;
			text += "\n";
			text = text + "User Name:" + result.users[0].name;
			AN_PoupsProxy.showMessage("User Info Loaded", text);
			Debug.Log(text);
		}
		else
		{
			Debug.Log(result.responce);
			AN_PoupsProxy.showMessage("User Info Failed", result.responce);
		}
	}

	private void OnSearchRequestComplete(TW_APIRequstResult result)
	{
		if (result.IsSucceeded)
		{
			string text = "Tweet text:\n";
			text += result.tweets[0].text;
			AN_PoupsProxy.showMessage("Tweet Search Request Succeeded", text);
			Debug.Log(text);
		}
		else
		{
			Debug.Log(result.responce);
			AN_PoupsProxy.showMessage("Tweet Search Request Failed", result.responce);
		}
	}

	private void OnTimeLineRequestComplete(TW_APIRequstResult result)
	{
		if (result.IsSucceeded)
		{
			string text = "Last Tweet text:\n";
			text += result.tweets[0].text;
			AN_PoupsProxy.showMessage("Time Line Request Succeeded", text);
			Debug.Log(text);
		}
		else
		{
			Debug.Log(result.responce);
			AN_PoupsProxy.showMessage("Time Line Request Failed", result.responce);
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
		Singleton<AndroidTwitterManager>.Instance.Post("My app ScreehShot", tex);
		Object.Destroy(tex);
	}

	private void LogOut()
	{
		IsUserInfoLoaded = false;
		IsAuthenticated = false;
		Singleton<AndroidTwitterManager>.Instance.LogOut();
	}
}
