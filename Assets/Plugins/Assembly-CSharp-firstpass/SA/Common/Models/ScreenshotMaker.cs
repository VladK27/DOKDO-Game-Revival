using System;
using System.Collections;
using UnityEngine;

namespace SA.Common.Models
{
	public class ScreenshotMaker : MonoBehaviour
	{
		public Action<Texture2D> OnScreenshotReady = delegate
		{
		};

		public static ScreenshotMaker Create()
		{
			return new GameObject("ScreenshotMaker").AddComponent<ScreenshotMaker>();
		}

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public void GetScreenshot()
		{
			StartCoroutine(SaveScreenshot());
		}

		private IEnumerator SaveScreenshot()
		{
			yield return new WaitForEndOfFrame();
			int width = Screen.width;
			int height = Screen.height;
			Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
			tex.ReadPixels(new Rect(0f, 0f, width, height), 0, 0);
			tex.Apply();
			if (OnScreenshotReady != null)
			{
				OnScreenshotReady(tex);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
