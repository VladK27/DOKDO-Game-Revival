using System;
using Percent.Tween;

namespace Percent.View
{
	public class BackgroundView : View
	{
		public PromotionType type;

		private PercentTween tweener;

		protected override void Awake()
		{
			base.Awake();
			tweener = GetComponent<ColorTween>();
		}

		internal override void onViewStateChange(ViewLifeCycle.Status state)
		{
			switch (state)
			{
			case ViewLifeCycle.Status.INIT:
				render.raycastTarget = false;
				break;
			case ViewLifeCycle.Status.LOAD_SUCCESS:
				if (!PromotionData.crossPromotionData.type.Equals(type))
				{
					ViewLifeCycle obj2 = viewLifeCycle;
					obj2.onStateChange = (ViewLifeCycle.OnStateChange)Delegate.Remove(obj2.onStateChange, new ViewLifeCycle.OnStateChange(onViewStateChange));
					base.gameObject.SetActive(false);
				}
				break;
			case ViewLifeCycle.Status.LOAD_FAIL:
			{
				ViewLifeCycle obj = viewLifeCycle;
				obj.onStateChange = (ViewLifeCycle.OnStateChange)Delegate.Remove(obj.onStateChange, new ViewLifeCycle.OnStateChange(onViewStateChange));
				base.gameObject.SetActive(false);
				break;
			}
			case ViewLifeCycle.Status.SHOW:
				render.raycastTarget = true;
				tweener.play();
				break;
			case ViewLifeCycle.Status.HIDE:
				render.raycastTarget = false;
				tweener.playReverse();
				break;
			}
		}
	}
}
