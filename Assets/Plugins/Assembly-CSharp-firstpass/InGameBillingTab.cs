using UnityEngine;
using UnityEngine.UI;

public class InGameBillingTab : FeatureTab
{
	[SerializeField]
	private GameObject[] objectToEnbaleOnInit;

	[SerializeField]
	private Button[] initBoundButtons;

	public Text coinsLabel;

	public Text boostLabel;

	private void Awake()
	{
		GameBillingManagerExample.init();
	}

	private void FixedUpdate()
	{
		coinsLabel.text = "Total Coins: " + GameDataExample.coins;
		if (GameDataExample.IsBoostPurchased)
		{
			boostLabel.text = "Boost Enabled";
		}
		else
		{
			boostLabel.text = "Boost Disabled";
		}
		if (GameBillingManagerExample.isInited)
		{
			GameObject[] array = objectToEnbaleOnInit;
			foreach (GameObject gameObject in array)
			{
				gameObject.SetActive(true);
			}
			Button[] array2 = initBoundButtons;
			foreach (Button button in array2)
			{
				button.interactable = true;
			}
		}
		else
		{
			Button[] array3 = initBoundButtons;
			foreach (Button button2 in array3)
			{
				button2.interactable = false;
			}
		}
	}

	public void AddCoins()
	{
		GameBillingManagerExample.purchase("small_coins_bag");
	}

	public void Boost()
	{
		GameBillingManagerExample.purchase("coins_bonus");
	}
}
