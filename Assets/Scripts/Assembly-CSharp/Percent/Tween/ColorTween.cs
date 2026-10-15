using System;
using UnityEngine;
using UnityEngine.UI;

namespace Percent.Tween
{
	public class ColorTween : PercentTween
	{
		public Color fromColor;

		public Color toColor;

		public bool cascadeColor;

		public bool hideOnCompletion;

		private RawImage rawImage;

		private Image image;

		private Color deltaColor;

		private bool isSwaped;

		private void Awake()
		{
			rawImage = GetComponent<RawImage>();
			if (rawImage == null)
			{
				image = GetComponent<Image>();
			}
			if (hideOnCompletion)
			{
				onEnd = (OnTweenEnd)Delegate.Combine(onEnd, new OnTweenEnd(deactivateSelf));
			}
			originalStartDelay = startDelay;
		}

		private void deactivateSelf()
		{
			resetToBegining();
			base.gameObject.SetActive(false);
		}

		protected override void onTweenStart()
		{
			deltaColor = fromColor - toColor;
		}

		protected override void onTween()
		{
			Color color = fromColor - deltaColor * animCurve.Evaluate(getProgress());
			if ((bool)rawImage)
			{
				rawImage.color = color;
			}
			else
			{
				image.color = color;
			}
			if (!cascadeColor)
			{
				return;
			}
			foreach (Transform item in base.transform)
			{
				RawImage component = item.GetComponent<RawImage>();
				if ((bool)component)
				{
					component.color = color;
				}
				Image component2 = item.GetComponent<Image>();
				if ((bool)component2)
				{
					component2.color = color;
				}
			}
		}

		protected override void onTweenEnd()
		{
			if ((bool)rawImage)
			{
				rawImage.color = toColor;
			}
			else
			{
				image.color = toColor;
			}
			if (cascadeColor)
			{
				foreach (Transform item in base.transform)
				{
					RawImage component = item.GetComponent<RawImage>();
					if ((bool)component)
					{
						component.color = toColor;
					}
					Image component2 = item.GetComponent<Image>();
					if ((bool)component2)
					{
						component2.color = toColor;
					}
				}
			}
			if (isSwaped)
			{
				swapFromAndToColor();
				isSwaped = false;
			}
			startDelay = originalStartDelay;
		}

		internal override void resetToBegining()
		{
			if ((bool)rawImage)
			{
				rawImage.color = fromColor;
			}
			else
			{
				image.color = fromColor;
			}
		}

		internal override void toEndFrame()
		{
			if ((bool)rawImage)
			{
				rawImage.color = toColor;
			}
			else
			{
				image.color = toColor;
			}
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
