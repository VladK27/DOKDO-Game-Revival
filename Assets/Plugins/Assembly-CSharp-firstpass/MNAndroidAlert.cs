using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MNAndroidAlert : MonoBehaviour
{
	private string title = string.Empty;

	private string message = string.Empty;

	private IEnumerable<string> actions;

	public Action<string> OnComplete = delegate
	{
	};

	public static MNAndroidAlert Create(string title, string message, IEnumerable<string> actions)
	{
		MNAndroidAlert mNAndroidAlert = new GameObject("AndroidPopUp").AddComponent<MNAndroidAlert>();
		mNAndroidAlert.title = title;
		mNAndroidAlert.message = message;
		mNAndroidAlert.actions = actions;
		return mNAndroidAlert;
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
		MNAndroidNative.showMessage(title, message, stringBuilder.ToString(), MNP_PlatformSettings.Instance.AndroidDialogTheme);
	}

	public void onPopUpCallBack(string result)
	{
		OnComplete(result);
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
