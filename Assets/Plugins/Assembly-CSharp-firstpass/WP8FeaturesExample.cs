using UnityEngine;
using UnityEngine.SceneManagement;

public class WP8FeaturesExample : WPNFeaturePreview
{
	public static WP8NativePreviewBackButton back;

	private void Awake()
	{
		if (back == null)
		{
			back = WP8NativePreviewBackButton.Create();
		}
	}

	private void OnGUI()
	{
		UpdateToStartPos();
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Basic Features", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "In App Purchasing"))
		{
			SceneManager.LoadScene("In_AppPurchases");
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Native Pop Ups"))
		{
			SceneManager.LoadScene("PopUpExample");
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Multimedia"))
		{
			SceneManager.LoadScene("Multimedia");
		}
	}
}
