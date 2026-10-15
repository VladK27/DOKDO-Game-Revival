using UnityEngine;

public class OpenURL : MonoBehaviour
{
	public string url;

	private void Start()
	{
	}

	public void openurl()
	{
		Application.OpenURL(url);
	}
}
