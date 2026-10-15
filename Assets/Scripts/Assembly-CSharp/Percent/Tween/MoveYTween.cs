using UnityEngine;

namespace Percent.Tween
{
	public class MoveYTween : PercentTween
	{
		public float targetLocalY;

		private RectTransform thisTrans;

		private float distanceY;

		private float startY;

		private bool isSwaped;

		private void Awake()
		{
			thisTrans = GetComponent<RectTransform>();
			originalStartDelay = startDelay;
		}

		protected override void onTweenStart()
		{
			startY = thisTrans.localPosition.y;
			distanceY = startY - targetLocalY;
		}

		protected override void onTween()
		{
			float y = startY - distanceY * animCurve.Evaluate(getProgress());
			thisTrans.localPosition = new Vector3(thisTrans.localPosition.x, y, thisTrans.localPosition.z);
		}

		protected override void onTweenEnd()
		{
			thisTrans.localPosition = new Vector3(thisTrans.localPosition.x, targetLocalY, thisTrans.localPosition.z);
			if (isSwaped)
			{
				swapFromAndToPosition();
				isSwaped = false;
			}
			startDelay = originalStartDelay;
		}

		internal override void resetToBegining()
		{
			thisTrans.localPosition = new Vector3(thisTrans.localPosition.x, startY, thisTrans.localPosition.z);
		}

		internal override void toEndFrame()
		{
			thisTrans.localPosition = new Vector3(thisTrans.localPosition.x, targetLocalY, thisTrans.localPosition.z);
		}

		internal override void playReverse()
		{
			if (!isSwaped)
			{
				swapFromAndToPosition();
				isSwaped = true;
				play();
			}
		}

		private void swapFromAndToPosition()
		{
			targetLocalY = startY;
			startY = base.transform.localPosition.y;
		}
	}
}
