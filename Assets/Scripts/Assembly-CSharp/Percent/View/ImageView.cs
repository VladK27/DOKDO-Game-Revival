using System;
using Percent.Http;
using Percent.Tween;
using UnityEngine;

namespace Percent.View
{
	public class ImageView : ResponsiveView
	{
		private PercentTween showScaleTweener;

		private PercentTween showColorTweener;

		private TextureLoader textureLoader;

		private TextureLifeCycle textureLifeCycle;

		protected override void Awake()
		{
			base.Awake();
			showScaleTweener = GetComponent<ScaleTween>();
			showColorTweener = GetComponent<ColorTween>();
			textureLoader = GetComponent<TextureLoader>();
			textureLifeCycle = GameObject.Find("TextureLifeCycle").GetComponent<TextureLifeCycle>();
			TextureLifeCycle obj = textureLifeCycle;
			obj.onStateChange = (TextureLifeCycle.OnStateChange)Delegate.Combine(obj.onStateChange, new TextureLifeCycle.OnStateChange(onTextureStateChange));
		}

		internal override void onViewStateChange(ViewLifeCycle.Status state)
		{
			switch (state)
			{
			case ViewLifeCycle.Status.LOAD_SUCCESS:
				if (!PromotionData.crossPromotionData.type.Equals(PromotionType.IMAGE))
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
				showScaleTweener.play();
				showColorTweener.play();
				render.raycastTarget = true;
				break;
			case ViewLifeCycle.Status.HIDE:
				showScaleTweener.startDelay = 0f;
				showScaleTweener.onEnd = onAnimationEnd;
				showScaleTweener.playReverse();
				showColorTweener.startDelay = 0f;
				showColorTweener.playReverse();
				render.raycastTarget = false;
				break;
			}
		}

		internal void onTextureStateChange(TextureLifeCycle.Status state)
		{
			if (state == TextureLifeCycle.Status.LOAD)
			{
				if (!PromotionData.crossPromotionData.type.Equals(PromotionType.IMAGE))
				{
					TextureLifeCycle obj = textureLifeCycle;
					obj.onStateChange = (TextureLifeCycle.OnStateChange)Delegate.Remove(obj.onStateChange, new TextureLifeCycle.OnStateChange(onTextureStateChange));
				}
				else if (PromotionData.crossPromotionData.type.Equals(PromotionType.IMAGE))
				{
					string[] resourceUrl = PromotionData.crossPromotionData.resourceUrl;
					textureLoader.render(resourceUrl[0], textureLifeCycle.onEachTextureLoad);
				}
			}
		}

		public void onAnimationEnd()
		{
			deactivateWholeView();
		}

		private void deactivateWholeView()
		{
			GameObject gameObject = GameObject.Find("TypeImage");
			gameObject.GetComponent<Hider>().hide();
		}
	}
}
