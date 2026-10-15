using UnityEngine;

public class FeatureTab : MonoBehaviour
{
	public void Show()
	{
		base.gameObject.SetActive(true);
	}

	public void Hide()
	{
		base.gameObject.SetActive(false);
	}
}
