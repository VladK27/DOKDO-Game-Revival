using UnityEngine;

public class CFX_Demo_GTButton : MonoBehaviour
{
	public Color NormalColor = new Color32(128, 128, 128, 128);

	public Color HoverColor = new Color32(128, 128, 128, 128);

	public string Callback;

	public GameObject Receiver;

	private Rect CollisionRect;

	private bool Over;

	private void Awake()
	{
		CollisionRect = GetComponent<CompatGUITexture>().GetScreenRect(Camera.main);
	}

	private void Update()
	{
		if (CollisionRect.Contains(Input.mousePosition))
		{
			GetComponent<CompatGUITexture>().color = HoverColor;
			if (Input.GetMouseButtonDown(0))
			{
				OnClick();
			}
		}
		else
		{
			GetComponent<CompatGUITexture>().color = NormalColor;
		}
	}

	private void OnClick()
	{
		Receiver.SendMessage(Callback);
	}
}
