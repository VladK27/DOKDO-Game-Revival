using System;
using UnityEngine;
using UnityEngine.UI;

namespace Percent.View
{
	public class View : MonoBehaviour
	{
		protected RawImage render;

		protected ViewLifeCycle viewLifeCycle;

		protected RectTransform thisTrans;

		protected Color NOT_VISIBLE = new Color(1f, 1f, 1f, 0f);

		protected virtual void Awake()
		{
			render = GetComponent<RawImage>();
			viewLifeCycle = GameObject.Find("ViewLifeCycle").GetComponent<ViewLifeCycle>();
			ViewLifeCycle obj = viewLifeCycle;
			obj.onStateChange = (ViewLifeCycle.OnStateChange)Delegate.Combine(obj.onStateChange, new ViewLifeCycle.OnStateChange(onViewStateChange));
			thisTrans = GetComponent<RectTransform>();
		}

		protected virtual void Start()
		{
			render.color = NOT_VISIBLE;
		}

		protected virtual void OnEnable()
		{
		}

		internal virtual void onViewStateChange(ViewLifeCycle.Status state)
		{
		}
	}
}
