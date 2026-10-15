using UnityEngine;

public class SA_StatusBar : MonoBehaviour
{
	public TextMesh title;

	public TextMesh shadow;

	public static string text
	{
		get
		{
			SA_StatusBar sA_StatusBar = Object.FindObjectOfType<SA_StatusBar>();
			if (sA_StatusBar == null)
			{
				return string.Empty;
			}
			return sA_StatusBar.title.text;
		}
		set
		{
			SA_StatusBar sA_StatusBar = Object.FindObjectOfType<SA_StatusBar>();
			if (!(sA_StatusBar == null))
			{
				sA_StatusBar.SetText(value);
			}
		}
	}

	public void SetText(string text)
	{
		title.text = text;
		shadow.text = text;
	}
}
