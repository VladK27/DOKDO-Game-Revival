using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Percent.Tween
{
	public class TextColorTween : PercentTween
	{
		public Color fromColor;

		public Color toColor;

		public UnityEvent endTweenEvent;

		private Text text;

		private Color deltaColor;

		private bool isSwaped;

		private void Awake()
		{
			text = GetComponent<Text>();
			originalStartDelay = startDelay;
		}

		protected override void onTweenStart()
		{
			deltaColor = fromColor - toColor;
		}

		protected override void onTween()
		{
			Color color = fromColor - deltaColor * animCurve.Evaluate(getProgress());
			text.color = color;
		}

		protected override void onTweenEnd()
		{
			text.color = toColor;
			if (isSwaped)
			{
				swapFromAndToColor();
				isSwaped = false;
			}
			if (endTweenEvent != null)
			{
				endTweenEvent.Invoke();
			}
			startDelay = originalStartDelay;
		}

		internal override void resetToBegining()
		{
			text.color = fromColor;
		}

		internal override void toEndFrame()
		{
			text.color = toColor;
		}

		internal override void playReverse()
		{
			if (!isSwaped)
			{
				swapFromAndToColor();
				isSwaped = true;
				play();
			}
		}

		private void swapFromAndToColor()
		{
			Color color = fromColor;
			fromColor = toColor;
			toColor = color;
		}

		public void playTween()
		{
			play();
		}
	}
}
