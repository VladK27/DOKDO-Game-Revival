using UnityEngine;
using UnityEngine.UI;

namespace Percent.View
{
	public class CanvasScaler : MonoBehaviour
	{
		private void Awake()
		{
			setCanvasScalerByMode();
		}

		private void setCanvasScalerByMode()
		{
			float num = 1242f;
			float num2 = 2208f;
			UnityEngine.UI.CanvasScaler component = GetComponent<UnityEngine.UI.CanvasScaler>();
			if (Util.getScreenOrientation().Equals(ScreenOrientation.VERTICAL))
			{
				component.referenceResolution = new Vector2(num, num2);
			}
			else
			{
				component.referenceResolution = new Vector2(num2, num);
			}
		}
	}
}
