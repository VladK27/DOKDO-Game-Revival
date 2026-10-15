using UnityEngine;

namespace Percent.View
{
	public class ResponsiveView : View
	{
		public Vector3 verticalInitPos;

		public Vector2 verticalInitSize;

		public Vector3 horizontalInitPos;

		public Vector2 horizontalInitSize;

		public bool respondOnEnable;

		protected override void Awake()
		{
			base.Awake();
		}

		protected override void Start()
		{
			base.Start();
			respond();
		}

		internal virtual void respond()
		{
			switch (Util.getScreenOrientation())
			{
			case ScreenOrientation.VERTICAL:
				thisTrans.localPosition = verticalInitPos;
				thisTrans.sizeDelta = verticalInitSize;
				break;
			case ScreenOrientation.HORIZONTAL:
				thisTrans.localPosition = horizontalInitPos;
				thisTrans.sizeDelta = horizontalInitSize;
				break;
			}
		}

		protected override void OnEnable()
		{
			if (respondOnEnable)
			{
				respond();
			}
		}
	}
}
