using UnityEngine;

public class GameBillingManagerExample : MonoBehaviour
{
	private static bool _isInited;

	public const string COINS_ITEM = "small_coins_bag";

	public const string COINS_BOOST = "coins_bonus";

	private static bool ListnersAdded;

	public static bool isInited
	{
		get
		{
			return _isInited;
		}
	}

	public static void init()
	{
		if (!ListnersAdded)
		{
			AndroidInAppPurchaseManager.Client.AddProduct("small_coins_bag");
			AndroidInAppPurchaseManager.Client.AddProduct("coins_bonus");
			AndroidInAppPurchaseManager.ActionProductPurchased += OnProductPurchased;
			AndroidInAppPurchaseManager.ActionProductConsumed += OnProductConsumed;
			AndroidInAppPurchaseManager.ActionBillingSetupFinished += OnBillingConnected;
			AndroidInAppPurchaseManager.Client.Connect();
			ListnersAdded = true;
		}
	}

	public static void purchase(string SKU)
	{
		AndroidInAppPurchaseManager.Client.Purchase(SKU);
	}

	public static void consume(string SKU)
	{
		AndroidInAppPurchaseManager.Client.Consume(SKU);
	}

	private static void OnProcessingPurchasedProduct(GooglePurchaseTemplate purchase)
	{
		switch (purchase.SKU)
		{
		case "small_coins_bag":
			consume("small_coins_bag");
			break;
		case "coins_bonus":
			GameDataExample.EnableCoinsBoost();
			break;
		}
	}

	private static void OnProcessingConsumeProduct(GooglePurchaseTemplate purchase)
	{
		string sKU = purchase.SKU;
		if (sKU != null && sKU == "small_coins_bag")
		{
			GameDataExample.AddCoins(100);
		}
	}

	private static void OnProductPurchased(BillingResult result)
	{
		if (result.IsSuccess)
		{
			OnProcessingPurchasedProduct(result.Purchase);
		}
		else
		{
			AndroidMessage.Create("Product Purchase Failed", result.Response + " " + result.Message);
		}
		Debug.Log("Purchased Responce: " + result.Response + " " + result.Message);
	}

	private static void OnProductConsumed(BillingResult result)
	{
		if (result.IsSuccess)
		{
			OnProcessingConsumeProduct(result.Purchase);
		}
		else
		{
			AndroidMessage.Create("Product Cousume Failed", result.Response + " " + result.Message);
		}
		Debug.Log("Cousume Responce: " + result.Response + " " + result.Message);
	}

	private static void OnBillingConnected(BillingResult result)
	{
		AndroidInAppPurchaseManager.ActionBillingSetupFinished -= OnBillingConnected;
		if (result.IsSuccess)
		{
			AndroidInAppPurchaseManager.ActionRetrieveProducsFinished += OnRetrieveProductsFinised;
			AndroidInAppPurchaseManager.Client.RetrieveProducDetails();
		}
		AndroidMessage.Create("Connection Responce", result.Response + " " + result.Message);
		Debug.Log("Connection Responce: " + result.Response + " " + result.Message);
	}

	private static void OnRetrieveProductsFinised(BillingResult result)
	{
		AndroidInAppPurchaseManager.ActionRetrieveProducsFinished -= OnRetrieveProductsFinised;
		if (result.IsSuccess)
		{
			UpdateStoreData();
			_isInited = true;
		}
		else
		{
			AndroidMessage.Create("Connection Responce", result.Response + " " + result.Message);
		}
	}

	private static void UpdateStoreData()
	{
		foreach (GoogleProductTemplate product in AndroidInAppPurchaseManager.Client.Inventory.Products)
		{
			Debug.Log("Loaded product: " + product.Title);
		}
		if (AndroidInAppPurchaseManager.Client.Inventory.IsProductPurchased("small_coins_bag"))
		{
			consume("small_coins_bag");
		}
		if (AndroidInAppPurchaseManager.Client.Inventory.IsProductPurchased("coins_bonus"))
		{
			GameDataExample.EnableCoinsBoost();
		}
	}
}
