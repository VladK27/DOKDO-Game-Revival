using UnityEngine;

public class WP8Message : WP8PopupBase
{
	public static WP8Message Create(string title, string message)
	{
		WP8Message wP8Message = new GameObject("WP8Message").AddComponent<WP8Message>();
		wP8Message.title = title;
		wP8Message.message = message;
		wP8Message.init();
		return wP8Message;
	}

	public void init()
	{
	}

	public void onPopUpCallBack()
	{
		OnComplete(WP8DialogResult.YES);
		Object.Destroy(base.gameObject);
	}
}
