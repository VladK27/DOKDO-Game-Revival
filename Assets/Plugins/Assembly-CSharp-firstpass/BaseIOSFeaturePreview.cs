using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseIOSFeaturePreview : MonoBehaviour
{
	protected GUIStyle style;

	protected int buttonWidth = 200;

	protected int buttonHeight = 75;

	protected float StartY = 20f;

	protected float StartX = 10f;

	protected float XStartPos = 10f;

	protected float YStartPos = 10f;

	protected float XButtonStep = 220f;

	protected float YButtonStep = 90f;

	protected float YLableStep = 50f;

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

	public void LoadLevel(string levelName)
	{
		SceneManager.LoadScene(levelName);
	}
}
