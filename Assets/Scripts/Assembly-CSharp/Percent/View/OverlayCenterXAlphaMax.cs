using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Percent.View
{
	public class OverlayCenterXAlphaMax : MonoBehaviour
	{
		public RawImage[] target;

		public float alphaMaxDistance = 300f;

		public float alphaZeroDistance = 600f;

		public float fixedCenterX;

		private RectTransform thisTrans;

		private float centerX;

		private float distanceFromCenter;

		private void Awake()
		{
			thisTrans = GetComponent<RectTransform>();
		}

		private void Start()
		{
			centerX = (float)(Screen.width / 2) + fixedCenterX;
		}

		internal void startAnimation()
		{
			StartCoroutine(animate());
		}

		private IEnumerator animate()
		{
			while (true)
			{
				distanceFromCenter = Mathf.Abs(centerX - thisTrans.position.x);
				float alpha = (isAlphaZeroDistance() ? 0f : ((!isAlphaMaxDistance()) ? (1f - (distanceFromCenter - alphaMaxDistance) / (alphaZeroDistance - alphaMaxDistance)) : 1f));
				for (int i = 0; i < target.Length; i++)
				{
					target[i].color = new Color(target[i].color.r, target[i].color.g, target[i].color.b, alpha);
				}
				yield return null;
			}
		}

		private bool isAlphaZeroDistance()
		{
			if (distanceFromCenter >= alphaZeroDistance)
			{
				return true;
			}
			return false;
		}

		private bool isAlphaMaxDistance()
		{
			if (distanceFromCenter <= alphaMaxDistance)
			{
				return true;
			}
			return false;
		}
	}
}
