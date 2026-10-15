using UnityEngine;

public class WPNFeaturePreview : MonoBehaviour
{
	protected GUIStyle style;

	protected int buttonWidth = 200;

	protected int buttonHeight = 50;

	protected float StartY = 20f;

	protected float StartX = 10f;

	protected float XStartPos = 10f;

	protected float YStartPos = 10f;

	protected float XButtonStep = 220f;

	protected float YButtonStep = 60f;

	protected float YLableStep = 40f;

	protected virtual void InitStyles()
	{
		style = new GUIStyle();
		style.normal.textColor = Color.white;
		style.fontSize = 16;
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperLeft;
		style.wordWrap = true;
	}

	public virtual void Start()
	{
		InitStyles();
	}

	public void UpdateToStartPos()
	{
		StartY = YStartPos;
		StartX = XStartPos;
	}
}
