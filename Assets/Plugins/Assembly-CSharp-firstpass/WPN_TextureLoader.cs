using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class WPN_TextureLoader : MonoBehaviour
{
	private string _url;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Texture2D> TextureLoaded__BackingField = delegate
	{
	};

	public event Action<Texture2D> TextureLoaded
	{
		add
		{
			Action<Texture2D> action = TextureLoaded__BackingField;
			Action<Texture2D> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref TextureLoaded__BackingField, (Action<Texture2D>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Texture2D> action = TextureLoaded__BackingField;
			Action<Texture2D> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref TextureLoaded__BackingField, (Action<Texture2D>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static WPN_TextureLoader Create()
	{
		return new GameObject("WPN_TextureLoader").AddComponent<WPN_TextureLoader>();
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
		TextureLoaded__BackingField(www.texture);
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
