using System;
using System.Collections;
using SA.AndroidNative.DynamicLinks;
using SA.Common.Pattern;
using UnityEngine;

public class SocialSharingTab : FeatureTab
{
	public Texture2D shareTexture;

	private void Awake()
	{
	}

	public void ShareText()
	{
		AndroidSocialGate.OnShareIntentCallback += HandleOnShareIntentCallback;
		AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share https://d45nf.app.goo.gl/QcRv", string.Empty);
	}

	public void RequestDynamicLink()
	{
		Manager instance = Singleton<Manager>.Instance;
		instance.OnShortLinkReceived = (Action<ShortLinkResult>)Delegate.Combine(instance.OnShortLinkReceived, (Action<ShortLinkResult>)((ShortLinkResult result) =>
		{
			if (result.IsSucceeded)
			{
				Debug.Log("[Short Link] " + result.ShortLink);
			}
		}));
		Link.Builder builder = new Link.Builder();
		builder.SetLink("https://game_promo").SetDynamicLinkDomain("d45nf.app.goo.gl").SetAndroidParameters(new Link.AndroidParameters.Builder("com.unionassets.android.plugin.preview").SetMinimumVersion(1).Build())
			.SetIosParameters(new Link.IosParameters.Builder("com.example.ios").SetAppStoreId("123456789").SetMinimumVersion("1.0.1").Build())
			.SetGoogleAnalyticsParameters(new Link.GoogleAnalyticsParameters.Builder().SetSource("preview").SetMedium("social").SetCampaign("example-promo")
				.Build())
			.SetItunesConnectAnalyticsParameters(new Link.ItunesConnectAnalyticsParameters.Builder().SetProviderToken("123456").SetCampaignToken("example-promo").Build())
			.SetSocialMetaTagParameters(new Link.SocialMetaTagParameters.Builder().SetTitle("Example of a Dynamic Link").SetDescription("This link works whether the app is installed or not!").Build());
		Singleton<Manager>.Instance.RequestShortDynamicLink(builder.Build());
	}

	private void HandleOnShareIntentCallback(bool status, string package)
	{
		AndroidSocialGate.OnShareIntentCallback -= HandleOnShareIntentCallback;
		Debug.Log("[HandleOnShareIntentCallback] " + status + " " + package);
	}

	public void ShareScreehshot()
	{
		StartCoroutine(PostScreenshot());
	}

	public void ShareImage()
	{
		AndroidSocialGate.StartShareIntent("Hello Share Intent", "Sharing Hello wolrd image", shareTexture, string.Empty);
	}

	public void TwitterShare()
	{
		AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share", shareTexture, "twi");
	}

	public void ShareMail()
	{
		AndroidSocialGate.SendMail("Hello Share Intent", "This is my text to share <br> <strong> html text </strong> ", "My E-mail Subject", "mail1@gmail.com, mail2@gmail.com", shareTexture);
	}

	public void InstaShare()
	{
		AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share", shareTexture, "insta");
	}

	public void GoogleShare()
	{
		AndroidSocialGate.StartGooglePlusShare("This is my text to share", shareTexture);
	}

	public void ShareFB()
	{
		StartCoroutine(PostFBScreenshot());
	}

	public void ShareWhatsapp()
	{
		StartCoroutine(PostWhatsappScreenshot());
	}

	private IEnumerator PostScreenshot()
	{
		yield return new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0f, 0f, width, height), 0, 0);
		tex.Apply();
		AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share", tex, string.Empty);
		UnityEngine.Object.Destroy(tex);
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
		UnityEngine.Object.Destroy(tex);
	}

	private IEnumerator PostWhatsappScreenshot()
	{
		yield return new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0f, 0f, width, height), 0, 0);
		tex.Apply();
		AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share", tex, "whatsapp");
		UnityEngine.Object.Destroy(tex);
	}
}
