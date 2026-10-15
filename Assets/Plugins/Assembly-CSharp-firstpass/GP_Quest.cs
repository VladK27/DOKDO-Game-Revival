using SA.Common.Util;
using UnityEngine;

public class GP_Quest
{
	public string Id;

	public string Name;

	public string Description;

	public string IconImageUrl;

	public string BannerImageUrl;

	public GP_QuestState state;

	public long LastUpdatedTimestamp;

	public long AcceptedTimestamp;

	public long EndTimestamp;

	public byte[] RewardData;

	public long CurrentProgress;

	public long TargetProgress;

	private Texture2D _icon;

	private Texture2D _banner;

	public Texture2D icon
	{
		get
		{
			return _icon;
		}
	}

	public Texture2D banner
	{
		get
		{
			return _banner;
		}
	}

	public void LoadIcon()
	{
		if (!(icon != null))
		{
			Loader.LoadWebTexture(IconImageUrl, OnIconLoaded);
		}
	}

	public void LoadBanner()
	{
		if (!(icon != null))
		{
			Loader.LoadWebTexture(BannerImageUrl, OnBannerLoaded);
		}
	}

	private void OnBannerLoaded(Texture2D tex)
	{
		if (this != null)
		{
			_banner = tex;
		}
	}

	private void OnIconLoaded(Texture2D tex)
	{
		if (this != null)
		{
			_icon = tex;
		}
	}
}
