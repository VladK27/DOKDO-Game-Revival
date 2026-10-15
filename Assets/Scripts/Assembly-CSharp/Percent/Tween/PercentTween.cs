using System.Collections;
using UnityEngine;

namespace Percent.Tween
{
	public abstract class PercentTween : MonoBehaviour
	{
		internal delegate void OnTweenStart();

		internal delegate void OnTweenEnd();

		public AnimationCurve animCurve;

		public float duration;

		public float startDelay;

		public bool ignoreTimeScale = true;

		internal OnTweenStart onStart;

		internal OnTweenEnd onEnd;

		protected bool isCoroutineStart;

		protected float originalStartDelay;

		private float timeSinceStartTween;

		internal void play()
		{
			if (!isCoroutineStart)
			{
				StartCoroutine(tween());
			}
		}

		internal virtual void playReverse()
		{
		}

		internal void stop()
		{
			if (isCoroutineStart)
			{
				StopCoroutine(tween());
			}
		}

		internal abstract void resetToBegining();

		internal abstract void toEndFrame();

		private IEnumerator tween()
		{
			isCoroutineStart = true;
			timeSinceStartTween = 0f;
			if (ignoreTimeScale)
			{
				if (startDelay > 0f)
				{
					yield return new WaitForSecondsRealtime(startDelay);
				}
				else if (startDelay > 0f)
				{
					yield return new WaitForSeconds(startDelay);
				}
			}
			onTweenStart();
			if (onStart != null)
			{
				onStart();
			}
			while (getProgress() <= 1f)
			{
				onTween();
				yield return null;
			}
			onTweenEnd();
			if (onEnd != null)
			{
				onEnd();
			}
			isCoroutineStart = false;
		}

		protected abstract void onTweenStart();

		protected abstract void onTween();

		protected abstract void onTweenEnd();

		protected float getProgress()
		{
			if (ignoreTimeScale)
			{
				timeSinceStartTween += Time.unscaledDeltaTime;
			}
			else
			{
				timeSinceStartTween += Time.deltaTime;
			}
			return timeSinceStartTween / duration;
		}
	}
}
