using Percent;
using UnityEngine;

public class PrivacyButton : MonoBehaviour
{
	private void Start()
	{
		base.transform.gameObject.SetActive(CrossPromotion.isEURegion());
	}

	public void showPrivacy()
	{
		CrossPromotion.showPrivacyWindow();
	}
}
