using Percent;
using UnityEngine;

public class TestCaller : MonoBehaviour
{
	private void Start()
	{
		CrossPromotion.showAds();
	}

	public void showGDPRWindow()
	{
		CrossPromotion.showPrivacyWindow();
	}
}
