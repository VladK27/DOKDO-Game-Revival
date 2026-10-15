using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

namespace SA.Common.Models
{
	public class PrefabAsyncLoader : MonoBehaviour
	{
		private string PrefabPath;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<GameObject> ObjectLoadedAction__BackingField = delegate
		{
		};

		public event Action<GameObject> ObjectLoadedAction
		{
			add
			{
				Action<GameObject> action = ObjectLoadedAction__BackingField;
				Action<GameObject> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref ObjectLoadedAction__BackingField, (Action<GameObject>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<GameObject> action = ObjectLoadedAction__BackingField;
				Action<GameObject> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref ObjectLoadedAction__BackingField, (Action<GameObject>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public static PrefabAsyncLoader Create()
		{
			return new GameObject("PrefabAsyncLoader").AddComponent<PrefabAsyncLoader>();
		}

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public void LoadAsync(string name)
		{
			PrefabPath = name;
			StartCoroutine(Load());
		}

		private IEnumerator Load()
		{
			ResourceRequest request = Resources.LoadAsync(PrefabPath);
			yield return request;
			if (request.asset == null)
			{
				UnityEngine.Debug.LogWarning("Prefab not found at path: " + PrefabPath);
				ObjectLoadedAction__BackingField(null);
			}
			else
			{
				GameObject obj = UnityEngine.Object.Instantiate(request.asset) as GameObject;
				ObjectLoadedAction__BackingField(obj);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
