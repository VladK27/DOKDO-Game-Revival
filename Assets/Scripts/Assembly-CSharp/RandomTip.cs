using UnityEngine;
using UnityEngine.UI;

public class RandomTip : MonoBehaviour
{
	public Text label;

	public string[] Tipids;

	private void OnEnable()
	{
		RefreshTip();
	}

	public void RefreshTip()
	{
		if (label == null || Tipids == null || Tipids.Length == 0 || LocalizeManager.Instance == null)
		{
			return;
		}
		string id = Tipids[Random.Range(0, Tipids.Length)];
		label.text = "! : " + LocalizeManager.Instance.translate(id);
	}
}
