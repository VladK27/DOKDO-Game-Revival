using System;
using UnityEngine;
using UnityEngine.UI;

namespace Percent.View
{
	public class SlideToFirst : MonoBehaviour
	{
		public CrossPromotion crossPromotion;

		private ScrollRect scrollRect;

		private void Awake()
		{
			CrossPromotion obj = crossPromotion;
			obj.onStateChange = (CrossPromotion.OnStateChange)Delegate.Combine(obj.onStateChange, new CrossPromotion.OnStateChange(onCrossPromotionStateChange));
			scrollRect = GetComponent<ScrollRect>();
		}

		private void onCrossPromotionStateChange(CrossPromotion.Status state)
		{
			if (state == CrossPromotion.Status.SHOW)
			{
				move();
			}
		}

		internal void move()
		{
			scrollRect.normalizedPosition = new Vector2(0f, 1f);
		}
	}
}
