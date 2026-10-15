using System;
using Percent.Http;
using Percent.Tween;
using UnityEngine;
using UnityEngine.UI;

namespace Percent.View
{
	public class SlideView : View
	{
		internal int index;

		internal static readonly int TERM = 1200;

		private static int instanceNum;

		private PercentTween showScaleTweener;

		private PercentTween showColorTweener;

		private readonly Vector2 VIEW_SIZE = new Vector2(1000f, 1500f);

		protected override void Awake()
		{
			base.Awake();
			index = ++instanceNum;
		}

		protected override void Start()
		{
			base.Start();
			initialize();
			renderTexture();
		}

		private void initialize()
		{
			initScrollView();
			initAnimation();
			initButton();
		}

		private void initScrollView()
		{
			CenterOnChild component = GameObject.Find("TypeSlide").GetComponent<CenterOnChild>();
			component.onChangeCenterContent = (CenterOnChild.OnChangeCenterContent)Delegate.Combine(component.onChangeCenterContent, new CenterOnChild.OnChangeCenterContent(onChangeCenterContent));
			thisTrans.SetParent(GameObject.Find("Content").transform);
			thisTrans.sizeDelta = VIEW_SIZE;
			thisTrans.localPosition = getPosOfIndex();
		}

		private Vector2 getPosOfIndex()
		{
			int num = 0;
			int num2 = 0;
			int num3 = PromotionData.crossPromotionData.resourceUrl.Length;
			num = (num3 - 1) * TERM / 2 - (index - 1) * TERM;
			return new Vector2(num, num2);
		}

		internal void onChangeCenterContent(int indexOfCenter, int indexOfLastCenter)
		{
			int obj = -1;
			if (indexOfLastCenter.Equals(obj))
			{
			}
		}

		private void initAnimation()
		{
			showScaleTweener = GetComponent<ScaleTween>();
			showColorTweener = GetComponents<ColorTween>()[0];
		}

		private void initButton()
		{
			GameObject gameObject = GameObject.Find("CrossPromotion");
			CrossPromotionUIEventHandler component = gameObject.GetComponent<CrossPromotionUIEventHandler>();
			GetComponent<Button>().onClick.AddListener(component.onSlideTypeClick);
		}

		private void renderTexture()
		{
			GameObject gameObject = GameObject.Find("TextureLifeCycle");
			TextureLifeCycle component = gameObject.GetComponent<TextureLifeCycle>();
			int num = PromotionData.crossPromotionData.resourceUrl.Length;
			string imageUrl = PromotionData.crossPromotionData.resourceUrl[num - index];
			GetComponent<TextureLoader>().render(imageUrl, component.onEachTextureLoad);
		}

		internal override void onViewStateChange(ViewLifeCycle.Status state)
		{
			switch (state)
			{
			case ViewLifeCycle.Status.LOAD_SUCCESS:
				if (!PromotionData.crossPromotionData.type.Equals(PromotionType.SLIDE))
				{
					ViewLifeCycle obj = viewLifeCycle;
					obj.onStateChange = (ViewLifeCycle.OnStateChange)Delegate.Remove(obj.onStateChange, new ViewLifeCycle.OnStateChange(onViewStateChange));
					base.gameObject.SetActive(false);
				}
				break;
			case ViewLifeCycle.Status.LOAD_FAIL:
			{
				ViewLifeCycle obj2 = viewLifeCycle;
				obj2.onStateChange = (ViewLifeCycle.OnStateChange)Delegate.Remove(obj2.onStateChange, new ViewLifeCycle.OnStateChange(onViewStateChange));
				base.gameObject.SetActive(false);
				break;
			}
			case ViewLifeCycle.Status.SHOW:
			{
				int num = 1;
				GetComponentsInChildren<ScaleTween>()[num].onEnd = onShowAnimationEnd;
				if (showScaleTweener == null)
				{
					initAnimation();
				}
				showScaleTweener.play();
				showColorTweener.play();
				render.raycastTarget = true;
				break;
			}
			case ViewLifeCycle.Status.HIDE:
				showScaleTweener.startDelay = 0f;
				showScaleTweener.onEnd = onHideAnimationEnd;
				showScaleTweener.playReverse();
				showColorTweener.startDelay = 0f;
				render.raycastTarget = false;
				break;
			}
		}

		public void onShowAnimationEnd()
		{
			GetComponent<OverlayCenterXAlphaMax>().startAnimation();
		}

		public void onHideAnimationEnd()
		{
			int num = --instanceNum;
			if (num.Equals(0))
			{
				deactivateWholeView();
			}
		}

		private void deactivateWholeView()
		{
			GameObject gameObject = GameObject.Find("TypeSlide");
			gameObject.GetComponent<Hider>().hide();
		}
	}
}
