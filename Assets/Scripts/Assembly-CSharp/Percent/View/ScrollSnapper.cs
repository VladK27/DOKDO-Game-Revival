using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Percent.View
{
	public class ScrollSnapper : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IEventSystemHandler
	{
		private RectTransform contentTrans;

		private int numOfContent;

		private ViewLifeCycle viewLifeCycle;

		private Vector2[] posOnContent;

		private int targetIndex;

		private bool isSnapEnable;

		private readonly int SNAP_SPEED = 15;

		private readonly int LAST_SNAP = 20;

		private void Awake()
		{
			contentTrans = GetComponent<ScrollRect>().content;
			viewLifeCycle = GameObject.Find("ViewLifeCycle").GetComponent<ViewLifeCycle>();
			ViewLifeCycle obj = viewLifeCycle;
			obj.onStateChange = (ViewLifeCycle.OnStateChange)Delegate.Combine(obj.onStateChange, new ViewLifeCycle.OnStateChange(onViewStateChange));
			CenterOnChild component = GameObject.Find("TypeSlide").GetComponent<CenterOnChild>();
			component.onChangeCenterContent = (CenterOnChild.OnChangeCenterContent)Delegate.Combine(component.onChangeCenterContent, new CenterOnChild.OnChangeCenterContent(onChangeCenterContent));
		}

		private void Update()
		{
			if (isSnapEnable)
			{
				contentTrans.anchoredPosition = Vector2.Lerp(contentTrans.anchoredPosition, posOnContent[targetIndex], Time.unscaledDeltaTime * (float)SNAP_SPEED);
				if (Vector2.Distance(contentTrans.anchoredPosition, posOnContent[targetIndex]) < (float)LAST_SNAP)
				{
					contentTrans.anchoredPosition = posOnContent[targetIndex];
					isSnapEnable = false;
				}
			}
		}

		internal void onViewStateChange(ViewLifeCycle.Status state)
		{
			switch (state)
			{
			case ViewLifeCycle.Status.LOAD_SUCCESS:
				if (!PromotionData.crossPromotionData.type.Equals(PromotionType.SLIDE))
				{
					ViewLifeCycle obj = viewLifeCycle;
					obj.onStateChange = (ViewLifeCycle.OnStateChange)Delegate.Remove(obj.onStateChange, new ViewLifeCycle.OnStateChange(onViewStateChange));
				}
				break;
			case ViewLifeCycle.Status.SHOW:
				setContentPosOnContainer();
				break;
			}
		}

		private void setContentPosOnContainer()
		{
			numOfContent = PromotionData.crossPromotionData.resourceUrl.Length;
			posOnContent = new Vector2[numOfContent];
			int num = (SlideView.TERM * numOfContent + 50) / 2;
			int num2 = (SlideView.TERM * numOfContent + 50) / numOfContent;
			for (int i = 0; i < numOfContent; i++)
			{
				posOnContent[i] = new Vector2(num - num2 * i, 0f);
			}
			targetIndex = 0;
		}

		internal void onChangeCenterContent(int indexOfCenter, int indexOfLastCenter)
		{
			targetIndex = numOfContent - indexOfCenter;
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			isSnapEnable = false;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			isSnapEnable = true;
		}
	}
}
