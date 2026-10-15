using SA.Common.Pattern;
using UnityEngine;

public class ConnectionButton : MonoBehaviour
{
	private float w;

	private float h;

	private Rect r = default(Rect);

	private void Start()
	{
		w = (float)Screen.width * 0.2f;
		h = (float)Screen.height * 0.1f;
		r.x = ((float)Screen.width - w) / 2f;
		r.y = ((float)Screen.height - h) / 2f;
		r.width = w;
		r.height = h;
	}

	private void OnGUI()
	{
		if (GUI.Button(r, "Find Match"))
		{
			Singleton<GameCenter_RTM>.Instance.FindMatch(2, 2, string.Empty);
		}
	}
}
