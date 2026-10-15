using UnityEngine;

public class WP8RateUsPopUp : WP8PopupBase
{
	public static WP8RateUsPopUp Create(string title, string message)
	{
		WP8RateUsPopUp wP8RateUsPopUp = new GameObject("WP8RateUsPopUp").AddComponent<WP8RateUsPopUp>();
		wP8RateUsPopUp.title = title;
		wP8RateUsPopUp.message = message;
		wP8RateUsPopUp.init();
		return wP8RateUsPopUp;
	}

	public void init()
	{
	}

	public void OnOkDel()
	{
	}

	public void OnCancelDel()
	{
		OnComplete(WP8DialogResult.DECLINED);
		Object.Destroy(base.gameObject);
	}
}
