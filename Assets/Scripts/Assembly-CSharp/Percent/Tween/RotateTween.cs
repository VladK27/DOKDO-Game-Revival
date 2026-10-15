using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Percent.Tween
{
	public class RotateTween : MonoBehaviour
	{
		public float rotationSpeed;

		private RectTransform thisTrans;

		private Image image;

		private IEnumerator rotateCoroutine;

		private void Awake()
		{
			thisTrans = GetComponent<RectTransform>();
			image = GetComponent<Image>();
			rotateCoroutine = rotate();
		}

		public void enableRotation(bool isEnable)
		{
			if (isEnable)
			{
				StartCoroutine(rotateCoroutine);
				image.color = Color.white;
			}
			else
			{
				StopCoroutine(rotateCoroutine);
				image.color = Color.clear;
			}
		}

		private IEnumerator rotate()
		{
			while (true)
			{
				thisTrans.Rotate(0f, 0f, rotationSpeed);
				yield return null;
			}
		}
	}
}
