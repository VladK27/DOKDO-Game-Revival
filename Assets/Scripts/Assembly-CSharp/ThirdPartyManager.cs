using UnityEngine;

// Revival build: all ad / purchase / social integrations were removed.
// This component is kept as a no-op hub so the existing scene reference and the
// remaining callers (SendAdsEvent, IslandMenu, Panel_Store, AssiStore, ...) stay valid.
public class ThirdPartyManager : MonoBehaviour
{
	public static ThirdPartyManager instance;

	public string DevUrl = "";

	private void Awake()
	{
		if (instance != null)
		{
			return;
		}
		instance = this;
		Debug.Log("ThirdPartyManager: ads/purchases/social disabled (no-op)");
	}

	public void ShowInterstitialAds()
	{
	}

	public void ShowVideoAds(RewardType type)
	{
	}

	public void BuyProduct(string product)
	{
	}

	public void Restore()
	{
	}
}
