using System.Collections.Generic;
using UnityEngine;

public class MNP_EditorTesting : MNP_Singleton<MNP_EditorTesting>
{
	private MNP_EditorUIController UiController;

	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("MNP_EditorTestingUI"));
		gameObject.transform.SetParent(base.transform);
		UiController = gameObject.GetComponent<MNP_EditorUIController>();
		UiController.Hide();
	}

	private void Start()
	{
	}

	public void ShowPopup(string title, string message, Dictionary<string, MNPopup.MNPopupAction> actions, MNPopup.MNPopupAction dismiss)
	{
		UiController.SetTitle(title);
		UiController.SetMessage(message);
		UiController.SetActions(actions, dismiss);
		UiController.Show();
	}
}
