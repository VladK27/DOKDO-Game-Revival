using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

namespace SA.Common.Models
{
	public class WWWTextureLoader : MonoBehaviour
	{
		public static Dictionary<string, Texture2D> LocalCache = new Dictionary<string, Texture2D>();

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

		public static WWWTextureLoader Create()
		{
			return new GameObject("WWWTextureLoader").AddComponent<WWWTextureLoader>();
		}

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public void LoadTexture(string url)
		{
			_url = url;
			if (LocalCache.ContainsKey(_url))
			{
				OnLoad__BackingField(LocalCache[_url]);
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else
			{
				StartCoroutine(LoadCoroutin());
			}
		}

		private IEnumerator LoadCoroutin()
		{
			WWW www = new WWW(_url);
			yield return www;
			if (www.error == null)
			{
				UpdateLocalCache(_url, www.texture);
				OnLoad__BackingField(www.texture);
			}
			else
			{
				OnLoad__BackingField(null);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}

		private static void UpdateLocalCache(string url, Texture2D image)
		{
			if (!LocalCache.ContainsKey(url))
			{
				LocalCache.Add(url, image);
			}
		}
	}
}
