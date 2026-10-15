using UnityEngine;

namespace Percent.Tween
{
	public class ScaleTween : PercentTween
	{
		public Vector3 fromScale;

		public Vector3 toScale;

		private RectTransform targetTrans;

		private Vector3 deltaScale;

		private bool isSwaped;

		public bool playOnStart;

		public bool repositionAtStart;

		public Vector3 startPosition = Vector3.zero;

		private void Awake()
		{
			targetTrans = GetComponent<RectTransform>();
			if (playOnStart)
			{
				play();
			}
			originalStartDelay = startDelay;
		}

		protected override void onTweenStart()
		{
			deltaScale = fromScale - toScale;
			if (repositionAtStart)
			{
				targetTrans.localPosition = startPosition;
				repositionAtStart = false;
			}
		}

		protected override void onTween()
		{
			Vector3 localScale = fromScale - deltaScale * animCurve.Evaluate(getProgress());
			targetTrans.localScale = localScale;
		}

		protected override void onTweenEnd()
		{
			targetTrans.localScale = toScale;
			if (isSwaped)
			{
				swapFromAndToScale();
				isSwaped = false;
			}
			startDelay = originalStartDelay;
		}

		internal override void resetToBegining()
		{
			targetTrans.localScale = fromScale;
		}

		internal override void toEndFrame()
		{
			targetTrans.localScale = toScale;
		}

		internal override void playReverse()
		{
			if (!isSwaped)
			{
				swapFromAndToScale();
				isSwaped = true;
				play();
			}
		}

		private void swapFromAndToScale()
		{
			Vector3 vector = fromScale;
			fromScale = toScale;
			toScale = vector;
		}

		public void playTween()
		{
			isCoroutineStart = false;
			play();
		}

		public void playReverseTween()
		{
			startDelay = 0f;
			playReverse();
		}
	}
}
