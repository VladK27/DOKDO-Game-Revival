using System;
using UnityEngine;

public interface GoogleMobileAdBanner
{
	int id { get; }

	int width { get; }

	int height { get; }

	bool IsLoaded { get; }

	bool IsOnScreen { get; }

	bool ShowOnLoad { get; set; }

	GADBannerSize size { get; }

	TextAnchor anchor { get; }

	Action<GoogleMobileAdBanner> OnLoadedAction { get; set; }

	Action<GoogleMobileAdBanner> OnFailedLoadingAction { get; set; }

	Action<GoogleMobileAdBanner> OnOpenedAction { get; set; }

	Action<GoogleMobileAdBanner> OnClosedAction { get; set; }

	Action<GoogleMobileAdBanner> OnLeftApplicationAction { get; set; }

	void Hide();

	void Show();

	void Refresh();

	void SetBannerPosition(int x, int y);

	void SetBannerPosition(TextAnchor anchor);
}
