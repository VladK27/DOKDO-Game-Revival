using UnityEngine;
using UnityEngine.SceneManagement;

public class WP8NativePreviewBackButton : WPNFeaturePreview
{
	private string initalSceneName = "scene";

	public static WP8NativePreviewBackButton Create()
	{
		return new GameObject("BackButton").AddComponent<WP8NativePreviewBackButton>();
	}

	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		initalSceneName = SceneManager.GetActiveScene().name;
	}

	private void OnGUI()
	{
		float num = 120f;
		float x = (float)Screen.width - num * 1.2f;
		float y = num * 0.2f;
		if (!SceneManager.GetActiveScene().name.Equals(initalSceneName))
		{
			Color color = GUI.color;
			GUI.color = Color.green;
			if (GUI.Button(new Rect(x, y, num, num * 0.4f), "Back"))
			{
				SceneManager.LoadScene(initalSceneName);
			}
			GUI.color = color;
		}
	}
}
