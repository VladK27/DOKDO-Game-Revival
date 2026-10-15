using UnityEngine;

public static class ScreenPlacementExtension
{
	public static void ScreenPlacement(this GameObject target, ScreenPosition position)
	{
		target.transform.DoScreenPlacement(position, Vector2.zero, Camera.main);
	}

	public static void ScreenPlacement(this GameObject target, ScreenPosition position, Camera renderingCamera)
	{
		target.transform.DoScreenPlacement(position, Vector2.zero, renderingCamera);
	}

	public static void ScreenPlacement(this GameObject target, ScreenPosition position, Vector2 pixelsFromEdge)
	{
		target.transform.DoScreenPlacement(position, pixelsFromEdge, Camera.main);
	}

	public static void ScreenPlacement(this GameObject target, ScreenPosition position, Vector2 pixelsFromEdge, Camera renderingCamera)
	{
		target.transform.DoScreenPlacement(position, pixelsFromEdge, renderingCamera);
	}

	public static void ScreenPlacement(this Transform target, ScreenPosition position)
	{
		target.transform.DoScreenPlacement(position, Vector2.zero, Camera.main);
	}

	public static void ScreenPlacement(this Transform target, ScreenPosition position, Camera renderingCamera)
	{
		target.transform.DoScreenPlacement(position, Vector2.zero, renderingCamera);
	}

	public static void ScreenPlacement(this Transform target, ScreenPosition position, Vector2 pixelsFromEdge)
	{
		target.transform.DoScreenPlacement(position, pixelsFromEdge, Camera.main);
	}

	public static void ScreenPlacement(this Transform target, ScreenPosition position, Vector2 pixelsFromEdge, Camera renderingCamera)
	{
		target.transform.DoScreenPlacement(position, pixelsFromEdge, renderingCamera);
	}

	private static void DoScreenPlacement(this Transform target, ScreenPosition position, Vector2 pixelsFromEdge, Camera renderingCamera)
	{
		Vector3 position2 = Vector3.zero;
		float z = 0f - renderingCamera.transform.position.z + target.position.z;
		switch (position)
		{
		case ScreenPosition.UpperLeft:
			position2 = renderingCamera.ScreenToWorldPoint(new Vector3(pixelsFromEdge.x, (float)Screen.height - pixelsFromEdge.y, z));
			break;
		case ScreenPosition.UpperMiddle:
			position2 = renderingCamera.ScreenToWorldPoint(new Vector3((float)(Screen.width / 2) + pixelsFromEdge.x, (float)Screen.height - pixelsFromEdge.y, z));
			break;
		case ScreenPosition.UpperRight:
			position2 = renderingCamera.ScreenToWorldPoint(new Vector3((float)Screen.width - pixelsFromEdge.x, (float)Screen.height - pixelsFromEdge.y, z));
			break;
		case ScreenPosition.Left:
			position2 = renderingCamera.ScreenToWorldPoint(new Vector3(pixelsFromEdge.x, (float)(Screen.height / 2) - pixelsFromEdge.y, z));
			break;
		case ScreenPosition.Middle:
			position2 = renderingCamera.ScreenToWorldPoint(new Vector3((float)(Screen.width / 2) + pixelsFromEdge.x, (float)(Screen.height / 2) - pixelsFromEdge.y, z));
			break;
		case ScreenPosition.Right:
			position2 = renderingCamera.ScreenToWorldPoint(new Vector3((float)Screen.width - pixelsFromEdge.x, (float)(Screen.height / 2) - pixelsFromEdge.y, z));
			break;
		case ScreenPosition.LowerLeft:
			position2 = renderingCamera.ScreenToWorldPoint(new Vector3(pixelsFromEdge.x, pixelsFromEdge.y, z));
			break;
		case ScreenPosition.LowerMiddle:
			position2 = renderingCamera.ScreenToWorldPoint(new Vector3((float)(Screen.width / 2) + pixelsFromEdge.x, pixelsFromEdge.y, z));
			break;
		case ScreenPosition.LowerRight:
			position2 = renderingCamera.ScreenToWorldPoint(new Vector3((float)Screen.width - pixelsFromEdge.x, pixelsFromEdge.y, z));
			break;
		}
		target.transform.position = position2;
	}
}
