using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class SA_Ad_EditorUIController : MonoBehaviour
{
	public GameObject VideoPanel;

	public GameObject InterstitialPanel;

	public Image[] AppIcons;

	public Text[] AppNames;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<bool> OnCloseVideo__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnVideoLeftApplication__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<bool> OnCloseInterstitial__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnInterstitialLeftApplication__BackingField = () =>
	{
	};

	public event Action<bool> OnCloseVideo
	{
		add
		{
			Action<bool> action = OnCloseVideo__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnCloseVideo__BackingField, (Action<bool>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<bool> action = OnCloseVideo__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnCloseVideo__BackingField, (Action<bool>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OnVideoLeftApplication
	{
		add
		{
			Action action = OnVideoLeftApplication__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnVideoLeftApplication__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnVideoLeftApplication__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnVideoLeftApplication__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<bool> OnCloseInterstitial
	{
		add
		{
			Action<bool> action = OnCloseInterstitial__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnCloseInterstitial__BackingField, (Action<bool>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<bool> action = OnCloseInterstitial__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnCloseInterstitial__BackingField, (Action<bool>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OnInterstitialLeftApplication
	{
		add
		{
			Action action = OnInterstitialLeftApplication__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialLeftApplication__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnInterstitialLeftApplication__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialLeftApplication__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		SA_EditorTesting.CheckForEventSystem();
		Canvas component = GetComponent<Canvas>();
		component.sortingOrder = 10001;
	}

	private void Start()
	{
	}

	public void InterstitialClick()
	{
		OnInterstitialLeftApplication__BackingField();
	}

	public void VideoClick()
	{
		OnVideoLeftApplication__BackingField();
	}

	public void ShowInterstitialAd()
	{
		base.gameObject.SetActive(true);
		InterstitialPanel.SetActive(true);
	}

	public void ShowVideoAd()
	{
		base.gameObject.SetActive(true);
		VideoPanel.SetActive(true);
	}

	public void CloseInterstitial()
	{
		base.gameObject.SetActive(false);
		InterstitialPanel.SetActive(false);
		OnCloseInterstitial__BackingField(true);
	}

	public void CloseVideo()
	{
		base.gameObject.SetActive(false);
		VideoPanel.SetActive(false);
		OnCloseVideo__BackingField(true);
	}
}
