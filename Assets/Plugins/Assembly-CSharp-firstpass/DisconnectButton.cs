using SA.Common.Pattern;
using UnityEngine;

public class DisconnectButton : MonoBehaviour
{
	private float w;

	private float h;

	private Rect r = default(Rect);

	private void Start()
	{
		w = (float)Screen.width * 0.2f;
		h = (float)Screen.height * 0.1f;
		r.x = w * 0.1f;
		r.y = h * 0.1f;
		r.width = w;
		r.height = h;
	}

	private void OnGUI()
	{
		if (GUI.Button(r, "Disconnect"))
		{
			Singleton<GameCenter_RTM>.Instance.Disconnect();
		}
	}
}
