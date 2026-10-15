using System.Text;
using UnityEngine;

public class WPN_BillingManagerExample : MonoBehaviour
{
	public const string YOUR_DURABLE_PRODUCT_ID_CONSTANT = "item2";

	public const string YOUR_CONSUMABLE_PRODUCT_ID_CONSTANT = "item1";

	private static bool _IsInited = false;

	public static string _status = string.Empty;

	public static bool IsInited
	{
		get
		{
			return _IsInited;
		}
	}

	public static void Init()
	{
		WP8InAppPurchasesManager.OnInitComplete += HandleOnInitComplete;
		WP8InAppPurchasesManager.OnPurchaseFinished += HandleOnPurchaseFinished;
		WP8InAppPurchasesManager.Instance.Init();
	}

	public static void Purchase(string productId)
	{
		WP8InAppPurchasesManager.Instance.Purchase(productId);
	}

	private static void HandleOnPurchaseFinished(WP8PurchseResponce responce)
	{
		if (responce.IsSuccses)
		{
			WP8Dialog.Create("Purchase Succse", "Product: " + responce.ProductId);
		}
		else
		{
			WP8Dialog.Create("Purchase Failed", "Product: " + responce.ProductId);
		}
	}

	private static void HandleOnInitComplete(WP8_InAppsInitResult result)
	{
		if (result.IsFailed)
		{
			_status = "[Billing Init] Status" + result.IsSucceeded;
			return;
		}
		_IsInited = true;
		WP8InAppPurchasesManager.OnInitComplete -= HandleOnInitComplete;
		StringBuilder stringBuilder = new StringBuilder();
		foreach (WP8ProductTemplate product in WP8InAppPurchasesManager.Instance.Products)
		{
			if (product.Type == WP8PurchaseProductType.Durable && product.IsPurchased)
			{
				Debug.Log("Product " + product.Name + " is purchased");
			}
			stringBuilder.AppendLine(string.Format("[PRODUCT] {0} {1} {2} {3}", product.ProductId, product.Name, product.Type.ToString(), product.Price));
		}
		_status = stringBuilder.ToString();
		WP8Dialog.Create("market Initted", "Total products avaliable: " + WP8InAppPurchasesManager.Instance.Products.Count);
	}
}
