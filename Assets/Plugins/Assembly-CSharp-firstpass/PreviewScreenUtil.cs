using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class PreviewScreenUtil : MonoBehaviour
{
	private static PreviewScreenUtil _instance;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action ActionScreenResized__BackingField = () =>
	{
	};

	private int W;

	private int H;

	public static PreviewScreenUtil instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new GameObject("ScreenUtil").AddComponent<PreviewScreenUtil>();
				UnityEngine.Object.DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}

	public event Action ActionScreenResized
	{
		add
		{
			Action action = ActionScreenResized__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionScreenResized__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ActionScreenResized__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionScreenResized__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static bool isInScreenRect(Rect rect, Vector2 point)
	{
		point.y = (float)Screen.height - point.y;
		if (rect.Contains(point))
		{
			return true;
		}
		return false;
	}

	public static Rect getObjectBounds(GameObject obj)
	{
		if (obj.GetComponent<Renderer>() != null)
		{
			return getRendererBounds(obj.GetComponent<Renderer>());
		}
		return default(Rect);
	}

	public static Rect getRendererBounds(Renderer renderer)
	{
		Vector3 vector = Camera.main.WorldToScreenPoint(new Vector3(renderer.bounds.min.x, renderer.bounds.max.y, 0f));
		Vector3 vector2 = Camera.main.WorldToScreenPoint(new Vector3(renderer.bounds.max.x, renderer.bounds.min.y, 0f));
		return new Rect(vector.x, (float)Screen.height - vector.y, vector2.x - vector.x, vector.y - vector2.y);
	}

	private void Awake()
	{
		W = Screen.width;
		H = Screen.height;
	}

	private void FixedUpdate()
	{
		if (W != Screen.width || H != Screen.height)
		{
			W = Screen.width;
			H = Screen.height;
			ActionScreenResized__BackingField();
		}
	}
}
