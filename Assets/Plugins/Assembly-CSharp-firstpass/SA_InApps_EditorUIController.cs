using System;
using SA.Common.Animation;
using UnityEngine;
using UnityEngine.UI;

public class SA_InApps_EditorUIController : MonoBehaviour
{
	public Text Title;

	public Text Describtion;

	public Text Price;

	public Toggle IsSuccsesPurchase;

	public Image Fader;

	public SA_UIHightDependence HightDependence;

	private ValuesTween _CurrentTween;

	private ValuesTween _FaderTween;

	private Action<bool> _OnComplete;

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		Canvas component = GetComponent<Canvas>();
		component.sortingOrder = 10003;
	}

	public void ShowInAppPopup(string title, string describtion, string price, Action<bool> OnComplete = null)
	{
		if (_CurrentTween != null)
		{
			_CurrentTween.Stop();
		}
		if (_FaderTween != null)
		{
			_FaderTween.Stop();
		}
		_OnComplete = OnComplete;
		Title.text = title;
		Describtion.text = describtion;
		Price.text = price;
		Animate(150f, -300f, EaseType.easeOutBack);
		_CurrentTween.OnComplete += HandleOnInTweenComplete;
		FadeIn();
	}

	public void Close()
	{
		if (_CurrentTween != null)
		{
			_CurrentTween.Stop();
		}
		if (_FaderTween != null)
		{
			_FaderTween.Stop();
		}
		Animate(-300f, 150f, EaseType.easeInBack);
		_CurrentTween.OnComplete += HandleOnOutTweenComplete;
		FadeOut();
		if (_OnComplete != null)
		{
			_OnComplete(IsSuccsesPurchase.isOn);
			_OnComplete = null;
		}
	}

	private void HandleOnInTweenComplete()
	{
		_CurrentTween = null;
	}

	private void HandleOnOutTweenComplete()
	{
		_CurrentTween = null;
	}

	private void HandleOnValueChanged(float pos)
	{
		HightDependence.InitialRect.y = pos;
	}

	private void HandleOnFadeValueChanged(float a)
	{
		Fader.color = new Color(Fader.color.r, Fader.color.g, Fader.color.b, a);
	}

	private void HandleFadeComplete()
	{
		Fader.enabled = false;
	}

	private void FadeIn()
	{
		Fader.enabled = true;
		_FaderTween = ValuesTween.Create();
		_FaderTween.OnValueChanged += HandleOnFadeValueChanged;
		_FaderTween.ValueTo(0f, 0.5f, 0.5f);
	}

	private void FadeOut()
	{
		_FaderTween = ValuesTween.Create();
		_FaderTween.OnValueChanged += HandleOnFadeValueChanged;
		_FaderTween.OnComplete += HandleFadeComplete;
		_FaderTween.ValueTo(0.5f, 0f, 0.5f);
	}

	private void Animate(float from, float to, EaseType easeType)
	{
		_CurrentTween = ValuesTween.Create();
		_CurrentTween.OnValueChanged += HandleOnValueChanged;
		_CurrentTween.ValueTo(from, to, 0.5f, easeType);
	}
}
