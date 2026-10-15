using UnityEngine;
using UnityEngine.Events;

namespace Percent.Event
{
	public class XButtonEventBinder : ButtonEventBinder
	{
		protected override UnityAction onFindMethod()
		{
			GameObject gameObject = GameObject.Find("CrossPromotion");
			CrossPromotionUIEventHandler component = gameObject.GetComponent<CrossPromotionUIEventHandler>();
			return component.onHide;
		}
	}
}
