using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class AMN_WWWTextureLoader : MonoBehaviour
{
	private string _url;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Texture2D> OnLoad__BackingField = delegate
	{
	};

	public event Action<Texture2D> OnLoad
	{
		add
		{
			Action<Texture2D> action = OnLoad__BackingField;
			Action<Texture2D> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLoad__BackingField, (Action<Texture2D>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Texture2D> action = OnLoad__BackingField;
			Action<Texture2D> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLoad__BackingField, (Action<Texture2D>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static AMN_WWWTextureLoader Create()
	{
		return new GameObject("WWWTextureLoader").AddComponent<AMN_WWWTextureLoader>();
	}

	public void LoadTexture(string url)
	{
		_url = url;
		StartCoroutine(LoadCoroutin());
	}

	private IEnumerator LoadCoroutin()
	{
		WWW www = new WWW(_url);
		yield return www;
		if (www.error == null)
		{
			OnLoad__BackingField(www.texture);
		}
		else
		{
			OnLoad__BackingField(null);
		}
	}
}
