using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MNIOSAlert : MonoBehaviour
{
	private string title = string.Empty;

	private string message = string.Empty;

	private IEnumerable<string> actions;

	public Action<string> OnComplete = delegate
	{
	};

	public static MNIOSAlert Create(string title, string message, IEnumerable<string> actions)
	{
		MNIOSAlert mNIOSAlert = new GameObject("IOSPopUp").AddComponent<MNIOSAlert>();
		mNIOSAlert.title = title;
		mNIOSAlert.message = message;
		mNIOSAlert.actions = actions;
		return mNIOSAlert;
	}

	public void Show()
	{
		StringBuilder stringBuilder = new StringBuilder();
		IEnumerator<string> enumerator = actions.GetEnumerator();
		if (enumerator.MoveNext())
		{
			stringBuilder.Append(enumerator.Current);
		}
		while (enumerator.MoveNext())
		{
			stringBuilder.Append("|");
			stringBuilder.Append(enumerator.Current);
		}
		MNIOSNative.showMessage(title, message, stringBuilder.ToString());
	}

	public void onPopUpCallBack(string result)
	{
		OnComplete(result);
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
