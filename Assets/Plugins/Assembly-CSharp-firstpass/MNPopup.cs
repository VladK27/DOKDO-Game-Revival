using System;
using System.Collections.Generic;
using UnityEngine;

public class MNPopup
{
	public delegate void MNPopupAction();

	protected Dictionary<string, MNPopupAction> actions = new Dictionary<string, MNPopupAction>();

	protected MNPopupAction dismissCallback;

	protected string title = string.Empty;

	protected string message = string.Empty;

	protected const int MAX_ACTIONS = 3;

	protected const string DISMISS_ACTION = "com.stansassets.action.dismiss";

	public string Title
	{
		get
		{
			return title;
		}
	}

	public string Message
	{
		get
		{
			return message;
		}
	}

	public Dictionary<string, MNPopupAction> Actions
	{
		get
		{
			return actions;
		}
	}

	public MNPopup(string title, string message)
	{
		actions = new Dictionary<string, MNPopupAction>();
		this.title = title;
		this.message = message;
	}

	public void AddAction(string title, MNPopupAction callback)
	{
		if (actions.Count >= 3)
		{
			Debug.LogWarning("Action NOT added! Actions limit exceeded");
		}
		else if (actions.ContainsKey(title))
		{
			Debug.LogWarning("Action NOT added! Action with this Title already exists");
		}
		else
		{
			actions.Add(title, callback);
		}
	}

	public void AddDismissListener(MNPopupAction callback)
	{
		dismissCallback = callback;
	}

	public void Show()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
		{
			MNAndroidAlert mNAndroidAlert = MNAndroidAlert.Create(title, message, actions.Keys);
			mNAndroidAlert.OnComplete = (Action<string>)Delegate.Combine(mNAndroidAlert.OnComplete, new Action<string>(OnPopupCompleted));
			mNAndroidAlert.Show();
			break;
		}
		case RuntimePlatform.IPhonePlayer:
		{
			MNIOSAlert mNIOSAlert = MNIOSAlert.Create(title, message, actions.Keys);
			mNIOSAlert.OnComplete = (Action<string>)Delegate.Combine(mNIOSAlert.OnComplete, new Action<string>(OnPopupCompleted));
			mNIOSAlert.Show();
			break;
		}
		default:
			MNP_Singleton<MNP_EditorTesting>.Instance.ShowPopup(title, message, actions, dismissCallback);
			break;
		}
	}

	private void OnPopupCompleted(string action)
	{
		if (actions.ContainsKey(action))
		{
			actions[action]();
		}
		else if (action.Equals("com.stansassets.action.dismiss") && dismissCallback != null)
		{
			dismissCallback();
		}
	}
}
