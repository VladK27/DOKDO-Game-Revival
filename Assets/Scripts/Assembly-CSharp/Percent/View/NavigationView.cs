using System;
using Percent.Tween;
using UnityEngine;

namespace Percent.View
{
	public class NavigationView : View
	{
		internal int index;

		private PercentTween showScaleTweener;

		private PercentTween showColorTweener;

		private PercentTween slideTweener;

		internal readonly int TERM = 82;

		private static int instanceNum;

		protected override void Awake()
		{
			base.Awake();
			index = ++instanceNum;
		}

		protected override void Start()
		{
			base.Start();
			initialize();
		}

		private void initialize()
		{
			initScrollView();
			initAnimation();
		}

		private void initScrollView()
		{
			CenterOnChild component = GameObject.Find("TypeSlide").GetComponent<CenterOnChild>();
			component.onChangeCenterContent = (CenterOnChild.OnChangeCenterContent)Delegate.Combine(component.onChangeCenterContent, new CenterOnChild.OnChangeCenterContent(onChangeCenterContent));
			thisTrans.SetParent(GameObject.Find("Navigation").transform);
			thisTrans.localPosition = getPosOfIndex();
		}

		private Vector2 getPosOfIndex()
		{
			int num = 0;
			int num2 = -809;
			int num3 = PromotionData.crossPromotionData.resourceUrl.Length;
			num = (num3 - 1) * TERM / 2 - (index - 1) * TERM;
			return new Vector2(num, num2);
		}

		private void initAnimation()
		{
			showScaleTweener = GetComponent<ScaleTween>();
			PercentTween[] components = GetComponents<ColorTween>();
			showColorTweener = components[0];
			slideTweener = components[1];
			setShowTweenSequece();
		}

		private void setShowTweenSequece()
		{
			float num = 0.7f;
			float num2 = 0.1f;
			int num3 = PromotionData.crossPromotionData.resourceUrl.Length;
			float startDelay = num + (float)(num3 - index) * num2;
			showScaleTweener.startDelay = startDelay;
			showColorTweener.startDelay = startDelay;
		}

		internal void onChangeCenterContent(int indexOfCenter, int indexOfLastCenter)
		{
			int obj = -1;
			if (!indexOfLastCenter.Equals(obj))
			{
				if (indexOfCenter.Equals(index))
				{
					slideTweener.play();
				}
				else if (indexOfLastCenter.Equals(index))
				{
					slideTweener.playReverse();
				}
			}
		}

		internal override void onViewStateChange(ViewLifeCycle.Status state)
		{
			switch (state)
			{
			case ViewLifeCycle.Status.LOAD_SUCCESS:
				if (!PromotionData.crossPromotionData.type.Equals(PromotionType.SLIDE))
				{
					ViewLifeCycle obj2 = viewLifeCycle;
					obj2.onStateChange = (ViewLifeCycle.OnStateChange)Delegate.Remove(obj2.onStateChange, new ViewLifeCycle.OnStateChange(onViewStateChange));
					base.gameObject.SetActive(false);
				}
				break;
			case ViewLifeCycle.Status.LOAD_FAIL:
			{
				ViewLifeCycle obj = viewLifeCycle;
				obj.onStateChange = (ViewLifeCycle.OnStateChange)Delegate.Remove(obj.onStateChange, new ViewLifeCycle.OnStateChange(onViewStateChange));
				base.gameObject.SetActive(false);
				break;
			}
			case ViewLifeCycle.Status.SHOW:
				if (showScaleTweener == null)
				{
					initAnimation();
				}
				showScaleTweener.play();
				if (isFirstNavigation())
				{
					showColorTweener.onEnd = onShowAnimationEnd;
				}
				showColorTweener.play();
				break;
			case ViewLifeCycle.Status.HIDE:
				showScaleTweener.startDelay = 0f;
				showScaleTweener.playReverse();
				showColorTweener.startDelay = 0f;
				break;
			}
		}

		private bool isFirstNavigation()
		{
			int obj = PromotionData.crossPromotionData.resourceUrl.Length;
			if (index.Equals(obj))
			{
				return true;
			}
			return false;
		}

		public void onShowAnimationEnd()
		{
			render.color = Color.white;
		}
	}
}
