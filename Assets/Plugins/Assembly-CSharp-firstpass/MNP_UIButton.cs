using UnityEngine;
using UnityEngine.UI;

public class MNP_UIButton : MonoBehaviour
{
	public Button Button;

	public Text Title;

	private void Start()
	{
	}

	public void SetText(string text)
	{
		Title.text = text;
	}
}
