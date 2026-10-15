using UnityEngine;
using UnityEngine.SceneManagement;

public class IOSNativePreviewBackButton : BaseIOSFeaturePreview
{
	private string initialSceneName = "scene";

	public string loadedLevelName
	{
		get
		{
			return SceneManager.GetActiveScene().name;
		}
	}

	public static IOSNativePreviewBackButton Create()
	{
		return new GameObject("BackButton").AddComponent<IOSNativePreviewBackButton>();
	}

	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		initialSceneName = loadedLevelName;
	}

	private void OnGUI()
	{
		float num = 120f;
		float x = (float)Screen.width - num * 1.2f;
		float y = num * 0.2f;
		if (!loadedLevelName.Equals(initialSceneName))
		{
			Color color = GUI.color;
			GUI.color = Color.green;
			if (GUI.Button(new Rect(x, y, num, num * 0.4f), "Back"))
			{
				LoadLevel(initialSceneName);
			}
			GUI.color = color;
		}
	}
}
