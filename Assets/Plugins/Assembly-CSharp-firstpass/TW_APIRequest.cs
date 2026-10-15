using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public abstract class TW_APIRequest : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<TW_APIRequstResult> ActionComplete__BackingField = delegate
	{
	};

	private bool IsFirst = true;

	private string GetParams = string.Empty;

	private string requestUrl;

	private Dictionary<string, string> Headers = new Dictionary<string, string>();

	public event Action<TW_APIRequstResult> ActionComplete
	{
		add
		{
			Action<TW_APIRequstResult> action = ActionComplete__BackingField;
			Action<TW_APIRequstResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionComplete__BackingField, (Action<TW_APIRequstResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<TW_APIRequstResult> action = ActionComplete__BackingField;
			Action<TW_APIRequstResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionComplete__BackingField, (Action<TW_APIRequstResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public void Send()
	{
		if (Singleton<TwitterApplicationOnlyToken>.Instance.currentToken == null)
		{
			Singleton<TwitterApplicationOnlyToken>.Instance.ActionComplete += OnTokenLoaded;
			Singleton<TwitterApplicationOnlyToken>.Instance.RetrieveToken();
		}
		else
		{
			StartCoroutine(Request());
		}
	}

	public void AddParam(string name, int value)
	{
		AddParam(name, value.ToString());
	}

	public void AddParam(string name, string value)
	{
		if (!IsFirst)
		{
			GetParams += "&";
		}
		else
		{
			GetParams += "?";
		}
		GetParams = GetParams + name + "=" + value;
		IsFirst = false;
	}

	protected void SendCompleteResult(TW_APIRequstResult res)
	{
		ActionComplete__BackingField(res);
	}

	protected void SetUrl(string url)
	{
		requestUrl = url;
	}

	private IEnumerator Request()
	{
		requestUrl += GetParams;
		Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
		Headers.Add("Authorization", "Bearer " + Singleton<TwitterApplicationOnlyToken>.Instance.currentToken);
		WWW www = new WWW(requestUrl, null, Headers);
		yield return www;
		if (www.error == null)
		{
			OnResult(www.text);
		}
		else
		{
			ActionComplete__BackingField(new TW_APIRequstResult(false, www.error));
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	protected abstract void OnResult(string data);

	private void OnTokenLoaded()
	{
		Singleton<TwitterApplicationOnlyToken>.Instance.ActionComplete -= OnTokenLoaded;
		if (Singleton<TwitterApplicationOnlyToken>.Instance.currentToken != null)
		{
			StartCoroutine(Request());
		}
		else
		{
			ActionComplete__BackingField(new TW_APIRequstResult(false, "Retirving auth token failed"));
		}
	}
}
