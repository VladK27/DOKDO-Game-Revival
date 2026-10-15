using System;

public class SA_EditorInApps
{
	private static SA_InApps_EditorUIController _EditorUI;

	private static SA_InApps_EditorUIController EditorUI
	{
		get
		{
			return _EditorUI;
		}
	}

	public static void ShowInAppPopup(string title, string describtion, string price, Action<bool> OnComplete = null)
	{
		EditorUI.ShowInAppPopup(title, describtion, price, OnComplete);
	}
}
