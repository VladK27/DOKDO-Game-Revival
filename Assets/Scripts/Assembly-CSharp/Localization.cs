using UnityEngine;
using UnityEngine.UI;

public class Localization : MonoBehaviour
{
	public string id;

	private Text label;

	private LocalizeManager LM;

	private void Awake()
	{
		label = GetComponent<Text>();
		LM = LocalizeManager.Instance;
	}

	private void OnEnable()
	{
		Refresh();
	}

	public void Refresh()
	{
		if (label == null)
		{
			label = GetComponent<Text>();
		}
		LM = LocalizeManager.Instance;
		if (LM == null || label == null)
		{
			return;
		}
		label.text = LM.translate(id);
	}
}
