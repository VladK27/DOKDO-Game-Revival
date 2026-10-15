using UnityEngine;

public class WP8Dialog : WP8PopupBase
{
	public static WP8Dialog Create(string title, string message)
	{
		WP8Dialog wP8Dialog = new GameObject("WP8Dialog").AddComponent<WP8Dialog>();
		wP8Dialog.title = title;
		wP8Dialog.message = message;
		wP8Dialog.init();
		return wP8Dialog;
	}

	public void init()
	{
	}

	public void OnOkDel()
	{
		OnComplete(WP8DialogResult.YES);
		Object.Destroy(base.gameObject);
	}

	public void OnCancelDel()
	{
		OnComplete(WP8DialogResult.NO);
		Object.Destroy(base.gameObject);
	}
}
