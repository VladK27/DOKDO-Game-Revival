using System;
using UnityEngine;

namespace SA.Common.Models
{
	public class Invoker : MonoBehaviour
	{
		private Action _callback;

		public static Invoker Create(string name)
		{
			return new GameObject("SA.Common.Models.Invoker: " + name).AddComponent<Invoker>();
		}

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public void StartInvoke(Action callback, float time)
		{
			_callback = callback;
			Invoke("TimeOut", time);
		}

		private void TimeOut()
		{
			if (_callback != null)
			{
				_callback();
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
