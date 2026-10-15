using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Percent.View
{
	public class CenterOnChild : MonoBehaviour, IDragHandler, IEventSystemHandler
	{
		public delegate void OnChangeCenterContent(int indexOfCenter, int indexOfLastCenter);

		public OnChangeCenterContent onChangeCenterContent;

		private ScrollRect scrollRect;

		private RectTransform contentContainer;

		private RectTransform[] contents = new RectTransform[0];

		private ViewLifeCycle viewLifeCycle;

		private Vector2 scrollDir;

		private int lastIndex = -1;

		private readonly float CENTER_FIX = 540f;

		private void Awake()
		{
			scrollRect = GetComponent<ScrollRect>();
			scrollRect.onValueChanged.AddListener(onValueChanged);
			contentContainer = scrollRect.content;
			viewLifeCycle = GameObject.Find("ViewLifeCycle").GetComponent<ViewLifeCycle>();
			ViewLifeCycle obj = viewLifeCycle;
			obj.onStateChange = (ViewLifeCycle.OnStateChange)Delegate.Combine(obj.onStateChange, new ViewLifeCycle.OnStateChange(onViewStateChange));
		}

		public void OnDrag(PointerEventData data)
		{
			scrollDir = data.delta.normalized;
		}

		internal void onValueChanged(Vector2 dir)
		{
			int num = 0;
			float num2 = float.MaxValue;
			float num3 = contentContainer.localPosition.x + scrollDir.x * CENTER_FIX;
			for (int i = 1; i < contents.Length; i++)
			{
				if (Mathf.Abs(contents[i].localPosition.x + num3) < num2)
				{
					num = i;
					num2 = contents[i].localPosition.x + num3;
				}
			}
			if (!lastIndex.Equals(num))
			{
				if (onChangeCenterContent != null)
				{
					onChangeCenterContent(num, lastIndex);
				}
				lastIndex = num;
			}
		}

		internal void onViewStateChange(ViewLifeCycle.Status state)
		{
			if (state != ViewLifeCycle.Status.LOAD_SUCCESS)
			{
				return;
			}
			if (!PromotionData.crossPromotionData.type.Equals(PromotionType.SLIDE))
			{
				ViewLifeCycle obj = viewLifeCycle;
				obj.onStateChange = (ViewLifeCycle.OnStateChange)Delegate.Remove(obj.onStateChange, new ViewLifeCycle.OnStateChange(onViewStateChange));
			}
			else if (PromotionData.crossPromotionData.type.Equals(PromotionType.SLIDE))
			{
				SlideView[] componentsInChildren = GameObject.Find("Content").GetComponentsInChildren<SlideView>();
				int num = PromotionData.crossPromotionData.resourceUrl.Length;
				contents = new RectTransform[num + 1];
				for (int i = 1; i <= componentsInChildren.Length; i++)
				{
					contents[i] = componentsInChildren[i - 1].GetComponent<RectTransform>();
				}
			}
		}
	}
}
