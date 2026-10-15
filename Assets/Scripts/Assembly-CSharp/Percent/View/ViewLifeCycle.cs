using System;
using UnityEngine;

namespace Percent.View
{
	public class ViewLifeCycle : MonoBehaviour
	{
		public enum Status
		{
			INIT = 0,
			LOAD_SUCCESS = 1,
			LOAD_FAIL = 2,
			SHOW = 3,
			HIDE = 4
		}

		internal delegate void OnStateChange(Status state);

		public CrossPromotion crossPromotion;

		private Status state;

		internal OnStateChange onStateChange;

		private Status State
		{
			set
			{
				state = value;
				if (onStateChange != null)
				{
					onStateChange(state);
				}
			}
		}

		private void Awake()
		{
			CrossPromotion obj = crossPromotion;
			obj.onStateChange = (CrossPromotion.OnStateChange)Delegate.Combine(obj.onStateChange, new CrossPromotion.OnStateChange(onCrossPromotionStateChange));
		}

		private void Start()
		{
			initialize();
		}

		private void initialize()
		{
			State = Status.INIT;
		}

		private void onCrossPromotionStateChange(CrossPromotion.Status state)
		{
			switch (state)
			{
			case CrossPromotion.Status.LOAD_SUCCESS:
				State = Status.LOAD_SUCCESS;
				break;
			case CrossPromotion.Status.LOAD_FAIL:
				State = Status.LOAD_FAIL;
				break;
			case CrossPromotion.Status.SHOW:
				State = Status.SHOW;
				break;
			case CrossPromotion.Status.HIDE:
				State = Status.HIDE;
				break;
			}
		}
	}
}
