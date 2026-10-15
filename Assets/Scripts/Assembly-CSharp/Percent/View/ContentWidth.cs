using System;
using UnityEngine;

namespace Percent.View
{
	public class ContentWidth : MonoBehaviour
	{
		private RectTransform thisTrans;

		private ViewLifeCycle viewLifeCycle;

		private void Awake()
		{
			thisTrans = GetComponent<RectTransform>();
			viewLifeCycle = GameObject.Find("ViewLifeCycle").GetComponent<ViewLifeCycle>();
			ViewLifeCycle obj = viewLifeCycle;
			obj.onStateChange = (ViewLifeCycle.OnStateChange)Delegate.Combine(obj.onStateChange, new ViewLifeCycle.OnStateChange(onViewStateChange));
		}

		private void onViewStateChange(ViewLifeCycle.Status state)
		{
			if (state == ViewLifeCycle.Status.LOAD_SUCCESS)
			{
				if (!PromotionData.crossPromotionData.type.Equals(PromotionType.SLIDE))
				{
					ViewLifeCycle obj = viewLifeCycle;
					obj.onStateChange = (ViewLifeCycle.OnStateChange)Delegate.Remove(obj.onStateChange, new ViewLifeCycle.OnStateChange(onViewStateChange));
				}
				else if (PromotionData.crossPromotionData.type.Equals(PromotionType.SLIDE))
				{
					setWidthBySlideViewNum();
				}
			}
		}

		private void setWidthBySlideViewNum()
		{
			int num = PromotionData.crossPromotionData.resourceUrl.Length;
			int num2 = SlideView.TERM * num + 50;
			thisTrans.sizeDelta = new Vector2(num2, 300f);
		}
	}
}
