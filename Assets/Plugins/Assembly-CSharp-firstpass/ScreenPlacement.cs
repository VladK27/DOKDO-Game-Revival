using UnityEngine;

public class ScreenPlacement : MonoBehaviour
{
	public ScreenPosition position;

	public Vector2 pixelOffset;

	public bool persents;

	public bool calulateStartOnly = true;

	public Transform boundsTransform;

	private Vector2 actualOffset = default(Vector2);

	private ScreenOrientation orinetation;

	private void Start()
	{
		if (calulateStartOnly)
		{
			placementCalculation();
		}
	}

	private void FixedUpdate()
	{
		if (!calulateStartOnly)
		{
			placementCalculation();
		}
	}

	public void placementCalculation()
	{
		base.transform.ScreenPlacement(position, pixelOffset);
		Transform transform = ((!(boundsTransform == null)) ? boundsTransform : base.transform);
		Vector3 vector = Camera.main.WorldToScreenPoint(new Vector3(transform.GetComponent<Renderer>().bounds.min.x, transform.GetComponent<Renderer>().bounds.max.y, 0f));
		Vector3 vector2 = Camera.main.WorldToScreenPoint(new Vector3(transform.GetComponent<Renderer>().bounds.max.x, transform.GetComponent<Renderer>().bounds.min.y, 0f));
		Rect rect = new Rect(vector.x, (float)Screen.height - vector.y, vector2.x - vector.x, vector.y - vector2.y);
		float num = 0f;
		float num2 = 0f;
		if (persents)
		{
			num = (float)(Screen.width / 100) * pixelOffset.x;
			num2 = (float)(Screen.height / 100) * pixelOffset.y;
		}
		else
		{
			num = pixelOffset.x;
			num2 = pixelOffset.y;
		}
		switch (position)
		{
		case ScreenPosition.Right:
			actualOffset.x = num + rect.width / 2f;
			break;
		case ScreenPosition.UpperRight:
			actualOffset.x = num + rect.width / 2f;
			actualOffset.y = num2 + rect.height / 2f;
			break;
		case ScreenPosition.LowerRight:
			actualOffset.x = num + rect.width / 2f;
			actualOffset.y = num2 + rect.height / 2f;
			break;
		case ScreenPosition.Left:
			actualOffset.x = num + rect.width / 2f;
			break;
		case ScreenPosition.LowerLeft:
			actualOffset.x = num + rect.width / 2f;
			actualOffset.y = num2 + rect.height / 2f;
			break;
		case ScreenPosition.UpperLeft:
			actualOffset.x = num + rect.width / 2f;
			actualOffset.y = num2 + rect.height / 2f;
			break;
		case ScreenPosition.UpperMiddle:
			actualOffset.y = num2 + rect.height / 2f;
			break;
		case ScreenPosition.LowerMiddle:
			actualOffset.y = num2 + rect.height / 2f;
			break;
		}
		base.transform.ScreenPlacement(position, actualOffset);
	}
}
