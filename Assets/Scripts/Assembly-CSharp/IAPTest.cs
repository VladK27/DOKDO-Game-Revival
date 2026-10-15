using UnityEngine;

public class IAPTest : MonoBehaviour
{
	public string stringToEdit = "http://172.30.1.10:3000";

	public void Awake()
	{
		if (!Debug.isDebugBuild)
		{
			base.gameObject.SetActive(false);
		}
	}

	private void OnGUI()
	{
		GUIStyle gUIStyle = new GUIStyle("button");
		gUIStyle.fontSize = 30;
		stringToEdit = GUI.TextField(new Rect(10f, 10f, 500f, 100f), stringToEdit, gUIStyle);
		if (GUI.Button(new Rect(10f, 120f, 500f, 100f), "적용", gUIStyle))
		{
			ThirdPartyManager.instance.DevUrl = stringToEdit;
		}
	}
}
