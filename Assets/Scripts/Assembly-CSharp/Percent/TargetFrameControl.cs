using System;
using UnityEngine;

namespace Percent
{
	public class TargetFrameControl : MonoBehaviour
	{
		public CrossPromotion crossPromotion;

		private int gameTargetFrame;

		private void Awake()
		{
			CrossPromotion obj = crossPromotion;
			obj.onStateChange = (CrossPromotion.OnStateChange)Delegate.Combine(obj.onStateChange, new CrossPromotion.OnStateChange(onCrossPromotionStateChange));
		}

		private void Start()
		{
			gameTargetFrame = Application.targetFrameRate;
			Application.targetFrameRate = Config.VALUE_TARGET_FRAME_RATE;
		}

		internal void onCrossPromotionStateChange(CrossPromotion.Status state)
		{
			if (state == CrossPromotion.Status.HIDE)
			{
				Invoke("returnFrameRate", 2f);
			}
		}

		private void returnFrameRate()
		{
			Application.targetFrameRate = gameTargetFrame;
		}
	}
}
