using System;
using UnityEngine;

namespace Percent.View
{
	public class SlideViewGenerator : MonoBehaviour
	{
		public GameObject prefabSlideView;

		private TextureLifeCycle textureLifeCycle;

		private void Awake()
		{
			textureLifeCycle = GameObject.Find("TextureLifeCycle").GetComponent<TextureLifeCycle>();
			TextureLifeCycle obj = textureLifeCycle;
			obj.onStateChange = (TextureLifeCycle.OnStateChange)Delegate.Combine(obj.onStateChange, new TextureLifeCycle.OnStateChange(onTextureStateChange));
		}

		internal void onTextureStateChange(TextureLifeCycle.Status state)
		{
			if (state == TextureLifeCycle.Status.LOAD)
			{
				if (!PromotionData.crossPromotionData.type.Equals(PromotionType.SLIDE))
				{
					TextureLifeCycle obj = textureLifeCycle;
					obj.onStateChange = (TextureLifeCycle.OnStateChange)Delegate.Remove(obj.onStateChange, new TextureLifeCycle.OnStateChange(onTextureStateChange));
				}
				else if (PromotionData.crossPromotionData.type.Equals(PromotionType.SLIDE))
				{
					generate();
				}
			}
		}

		private void generate()
		{
			int num = PromotionData.crossPromotionData.resourceUrl.Length;
			for (int i = 0; i < num; i++)
			{
				UnityEngine.Object.Instantiate(prefabSlideView, Vector3.zero, Quaternion.identity);
			}
		}
	}
}
