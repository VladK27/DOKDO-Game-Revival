using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class SA_UIWidthDependence : MonoBehaviour
{
	private RectTransform _rect;

	public bool KeepRatioInEdiotr;

	public bool CaclulcateOnlyOntStart;

	public Rect InitialRect = default(Rect);

	[HideInInspector]
	public Rect InitialScreen = default(Rect);

	public RectTransform rect
	{
		get
		{
			if (_rect == null)
			{
				_rect = GetComponent<RectTransform>();
			}
			return _rect;
		}
	}

	private void Awake()
	{
		if (Application.isPlaying)
		{
			ApplyTransformation();
		}
	}

	private void Update()
	{
		if (!Application.isPlaying)
		{
			if (!KeepRatioInEdiotr)
			{
				InitialRect = new Rect(rect.anchoredPosition.x, rect.anchoredPosition.y, rect.rect.width, rect.rect.height);
				InitialScreen = new Rect(0f, 0f, Screen.width, Screen.height);
				rect.hideFlags = HideFlags.None;
			}
			else
			{
				ApplyTransformation();
				rect.hideFlags = HideFlags.NotEditable;
			}
		}
		else if (!CaclulcateOnlyOntStart)
		{
			ApplyTransformation();
		}
	}

	public void ApplyTransformation()
	{
		float num = InitialScreen.width / InitialRect.width;
		float num2 = InitialRect.height / InitialRect.width;
		float num3 = (float)Screen.width / num;
		float num4 = num3 * num2;
		float num5 = InitialRect.y / InitialRect.height;
		float num6 = InitialRect.x / InitialRect.width;
		float y = num4 * num5;
		float x = num3 * num6;
		rect.anchoredPosition = new Vector2(x, y);
		rect.sizeDelta = new Vector2(num3, num4);
	}

	private void OnDetroy()
	{
		rect.hideFlags = HideFlags.None;
	}
}
