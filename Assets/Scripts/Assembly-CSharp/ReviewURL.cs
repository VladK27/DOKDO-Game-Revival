using UnityEngine;

public class ReviewURL : MonoBehaviour
{
	public string Android_Package;

	public string iOS_AppID;

	public void openurl()
	{
		Application.OpenURL("https://play.google.com/store/apps/details?id=" + Android_Package);
	}
}
