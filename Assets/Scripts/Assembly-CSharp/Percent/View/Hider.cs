using System;
using UnityEngine;

namespace Percent.View
{
	public class Hider : MonoBehaviour
	{
		public PromotionType type;

		public CrossPromotion crossPromotion;

		private void Awake()
		{
			SessionLifeCycle component = GameObject.Find("SessionLifeCycle").GetComponent<SessionLifeCycle>();
			component.onStateChange = (SessionLifeCycle.OnStateChange)Delegate.Combine(component.onStateChange, new SessionLifeCycle.OnStateChange(onSessionStateChange));
			CrossPromotion obj = crossPromotion;
			obj.onStateChange = (CrossPromotion.OnStateChange)Delegate.Combine(obj.onStateChange, new CrossPromotion.OnStateChange(onCrossPromotionStateChange));
		}

		private void onSessionStateChange(SessionLifeCycle.Status state)
		{
			switch (state)
			{
			case SessionLifeCycle.Status.ACCESS_SUCCESS:
				if (!type.Equals(PromotionData.crossPromotionData.type))
				{
					hide();
				}
				break;
			case SessionLifeCycle.Status.UUID_FAIL:
				hide();
				break;
			case SessionLifeCycle.Status.ACCESS_FAIL:
				hide();
				break;
			}
		}

		private void onCrossPromotionStateChange(CrossPromotion.Status state)
		{
			if (state == CrossPromotion.Status.LOAD_FAIL)
			{
				hide();
			}
		}

		public void hide()
		{
			base.gameObject.SetActive(false);
		}
	}
}
