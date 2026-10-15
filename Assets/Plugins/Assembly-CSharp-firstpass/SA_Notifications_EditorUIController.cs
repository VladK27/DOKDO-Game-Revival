using SA.Common.Animation;
using UnityEngine;
using UnityEngine.UI;

public class SA_Notifications_EditorUIController : MonoBehaviour
{
	public Text Title;

	public Text Message;

	public Image[] Icons;

	public SA_UIHightDependence HightDependence;

	private ValuesTween _CurrentTween;

	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		SA_EditorTesting.CheckForEventSystem();
		Canvas component = GetComponent<Canvas>();
		component.sortingOrder = 10001;
	}

	public void ShowNotification(string title, string message, SA_EditorNotificationType type)
	{
		if (_CurrentTween != null)
		{
			_CurrentTween.Stop();
		}
		CancelInvoke("NotificationDelayComplete");
		Title.text = title;
		Message.text = message;
		Image[] icons = Icons;
		foreach (Image image in icons)
		{
			image.gameObject.SetActive(false);
		}
		switch (type)
		{
		case SA_EditorNotificationType.Achievement:
			Icons[0].gameObject.SetActive(true);
			break;
		case SA_EditorNotificationType.Error:
			Icons[1].gameObject.SetActive(true);
			break;
		case SA_EditorNotificationType.Leaderboards:
			Icons[2].gameObject.SetActive(true);
			break;
		case SA_EditorNotificationType.Message:
			Icons[3].gameObject.SetActive(true);
			break;
		}
		Animate(52f, -52f, EaseType.easeOutBack);
		_CurrentTween.OnComplete += HandleOnInTweenComplete;
	}

	private void HandleOnInTweenComplete()
	{
		_CurrentTween = null;
		Invoke("NotificationDelayComplete", 2f);
	}

	private void NotificationDelayComplete()
	{
		Animate(-52f, 58f, EaseType.easeInBack);
		_CurrentTween.OnComplete += HandleOnOutTweenComplete;
	}

	private void HandleOnOutTweenComplete()
	{
		_CurrentTween = null;
	}

	private void HandleOnValueChanged(float pos)
	{
		HightDependence.InitialRect.y = pos;
	}

	private void Animate(float from, float to, EaseType easeType)
	{
		_CurrentTween = ValuesTween.Create();
		_CurrentTween.OnValueChanged += HandleOnValueChanged;
		_CurrentTween.ValueTo(from, to, 0.5f, easeType);
	}
}
