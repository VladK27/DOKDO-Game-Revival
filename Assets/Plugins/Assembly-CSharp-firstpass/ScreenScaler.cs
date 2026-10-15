using UnityEngine;

public class ScreenScaler : MonoBehaviour
{
	public bool calulateStartOnly = true;

	public float persentsY = 100f;

	private float _scaleFactorY = 1f;

	private float _xScaleDiff;

	private void Awake()
	{
		_scaleFactorY = base.transform.localScale.y;
		_xScaleDiff = base.transform.localScale.x / base.transform.localScale.y;
		if (calulateStartOnly)
		{
			placementCalculation();
		}
	}

	private void Update()
	{
		if (!calulateStartOnly)
		{
			placementCalculation();
		}
	}

	public void placementCalculation()
	{
		float num = (float)Screen.height / 100f * persentsY;
		Rect objectBounds = PreviewScreenUtil.getObjectBounds(base.gameObject);
		if (objectBounds.height < num)
		{
			while (objectBounds.height < num)
			{
				objectBounds = PreviewScreenUtil.getObjectBounds(base.gameObject);
				base.transform.localScale = new Vector3(_scaleFactorY * _xScaleDiff, _scaleFactorY, 0f);
				_scaleFactorY += 0.1f;
			}
		}
		else
		{
			while (objectBounds.height > num)
			{
				objectBounds = PreviewScreenUtil.getObjectBounds(base.gameObject);
				base.transform.localScale = new Vector3(base.transform.localScale.x - base.transform.localScale.x * 0.1f, base.transform.localScale.y - base.transform.localScale.y * 0.1f, base.transform.localScale.z);
			}
		}
	}
}
