using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MNP_EditorUIController : MonoBehaviour
{
	[SerializeField]
	private GameObject Root;

	[SerializeField]
	private Text Title;

	[SerializeField]
	private Text Message;

	[SerializeField]
	private MNP_UIButton[] UIButtons;

	private MNPopup.MNPopupAction dismiss;

	private bool isActive;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void Hide()
	{
		isActive = false;
		Root.SetActive(false);
		for (int i = 0; i < UIButtons.Length; i++)
		{
			UIButtons[i].gameObject.SetActive(false);
		}
	}

	public void SetTitle(string title)
	{
		if (!isActive)
		{
			Title.text = title;
		}
	}

	public void SetMessage(string message)
	{
		if (!isActive)
		{
			Message.text = message;
		}
	}

	public void SetActions(Dictionary<string, MNPopup.MNPopupAction> actions, MNPopup.MNPopupAction dismissAction)
	{
		if (isActive)
		{
			return;
		}
		int num = 0;
		dismiss = dismissAction;
		foreach (KeyValuePair<string, MNPopup.MNPopupAction> action in actions)
		{
			UIButtons[num].Title.text = action.Key;
			MNPopup.MNPopupAction a = action.Value.Clone() as MNPopup.MNPopupAction;
			UIButtons[num].Button.onClick.AddListener(() =>
			{
				a();
				for (int i = 0; i < UIButtons.Length; i++)
				{
					UIButtons[i].Button.onClick.RemoveAllListeners();
				}
				Hide();
			});
			UIButtons[num].gameObject.SetActive(true);
			num++;
		}
	}

	public void Show()
	{
		isActive = true;
		Root.SetActive(true);
	}

	public void OnDismiss()
	{
		if (dismiss != null)
		{
			Hide();
			dismiss();
			dismiss = null;
		}
	}
}
