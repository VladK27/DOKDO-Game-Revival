using SA.Common.Models;
using SA.Common.Pattern;
using SA.IOSNative.StoreKit;

public class PaymentManagerExample
{
	public const string SMALL_PACK = "your.product.id1.here";

	public const string NC_PACK = "your.product.id2.here";

	public string lastTransactionProdudctId = string.Empty;

	private static bool IsInitialized;

	public static void init()
	{
		if (!IsInitialized)
		{
			Singleton<PaymentManager>.Instance.AddProductId("your.product.id1.here");
			Singleton<PaymentManager>.Instance.AddProductId("your.product.id2.here");
			PaymentManager.OnVerificationComplete += HandleOnVerificationComplete;
			PaymentManager.OnStoreKitInitComplete += OnStoreKitInitComplete;
			PaymentManager.OnTransactionComplete += OnTransactionComplete;
			PaymentManager.OnRestoreComplete += OnRestoreComplete;
			IsInitialized = true;
			Singleton<PaymentManager>.Instance.LoadStore();
		}
	}

	public static void buyItem(string productId)
	{
		Singleton<PaymentManager>.Instance.BuyProduct(productId);
	}

	private static void UnlockProducts(string productIdentifier)
	{
		switch (productIdentifier)
		{
		}
		Singleton<PaymentManager>.Instance.FinishTransaction(productIdentifier);
	}

	private static void OnTransactionComplete(PurchaseResult result)
	{
		ISN_Logger.Log("OnTransactionComplete: " + result.ProductIdentifier);
		ISN_Logger.Log("OnTransactionComplete: state: " + result.State);
		switch (result.State)
		{
		case PurchaseState.Purchased:
		case PurchaseState.Restored:
			UnlockProducts(result.ProductIdentifier);
			break;
		case PurchaseState.Failed:
			ISN_Logger.Log("Transaction failed with error, code: " + result.Error.Code);
			ISN_Logger.Log("Transaction failed with error, description: " + result.Error.Message);
			break;
		}
		if (result.State == PurchaseState.Failed)
		{
			IOSNativePopUpManager.showMessage("Transaction Failed", "Error code: " + result.Error.Code + "\nError description:" + result.Error.Message);
		}
		else
		{
			IOSNativePopUpManager.showMessage("Store Kit Response", "product " + result.ProductIdentifier + " state: " + result.State);
		}
	}

	private static void OnRestoreComplete(RestoreResult res)
	{
		if (res.IsSucceeded)
		{
			IOSNativePopUpManager.showMessage("Success", "Restore Compleated");
		}
		else
		{
			IOSNativePopUpManager.showMessage("Error: " + res.Error.Code, res.Error.Message);
		}
	}

	private static void HandleOnVerificationComplete(VerificationResponse response)
	{
		IOSNativePopUpManager.showMessage("Verification", "Transaction verification status: " + response.Status);
		ISN_Logger.Log("ORIGINAL JSON: " + response.OriginalJSON);
	}

	private static void OnStoreKitInitComplete(Result result)
	{
		if (result.IsSucceeded)
		{
			int num = 0;
			foreach (Product product in Singleton<PaymentManager>.Instance.Products)
			{
				if (product.IsAvailable)
				{
					num++;
				}
			}
			IOSNativePopUpManager.showMessage("StoreKit Init Succeeded", "Available products count: " + num);
			ISN_Logger.Log("StoreKit Init Succeeded Available products count: " + num);
		}
		else
		{
			IOSNativePopUpManager.showMessage("StoreKit Init Failed", "Error code: " + result.Error.Code + "\nError description:" + result.Error.Message);
		}
	}
}
