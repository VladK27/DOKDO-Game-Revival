public sealed class MNRateUsPopup : MNPopup
{
	private MNPopupAction remindListener;

	private MNPopupAction declineListener;

	private MNPopupAction rateUsListener;

	private string androidAppUrl = string.Empty;

	public MNRateUsPopup(string title, string message, string rateUs, string decline, string remind)
		: base(title, message)
	{
		AddAction(remind, () =>
		{
			if (remindListener != null)
			{
				remindListener();
			}
		});
		AddAction(decline, () =>
		{
			if (declineListener != null)
			{
				declineListener();
			}
		});
		AddAction(rateUs, () =>
		{
			MNAndroidNative.RedirectStoreRatingPage(androidAppUrl);
			if (rateUsListener != null)
			{
				rateUsListener();
			}
		});
	}

	public void SetAppleId(string id)
	{
	}

	public void SetAndroidAppUrl(string appUrl)
	{
		androidAppUrl = appUrl;
	}

	public void AddRateUsListener(MNPopupAction callback)
	{
		rateUsListener = callback;
	}

	public void AddRemindListener(MNPopupAction callback)
	{
		remindListener = callback;
	}

	public void AddDeclineListener(MNPopupAction callback)
	{
		declineListener = callback;
	}
}
