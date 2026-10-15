using System.Collections.Generic;

public class AndroidInventory
{
	private Dictionary<string, GooglePurchaseTemplate> _purchases;

	public List<GooglePurchaseTemplate> Purchases
	{
		get
		{
			return new List<GooglePurchaseTemplate>(_purchases.Values);
		}
	}

	public List<GoogleProductTemplate> Products
	{
		get
		{
			return AndroidNativeSettings.Instance.InAppProducts;
		}
	}

	public AndroidInventory()
	{
		_purchases = new Dictionary<string, GooglePurchaseTemplate>();
	}

	public void addPurchase(GooglePurchaseTemplate purchase)
	{
		if (_purchases.ContainsKey(purchase.SKU))
		{
			_purchases[purchase.SKU] = purchase;
		}
		else
		{
			_purchases.Add(purchase.SKU, purchase);
		}
	}

	public void removePurchase(GooglePurchaseTemplate purchase)
	{
		if (_purchases.ContainsKey(purchase.SKU))
		{
			_purchases.Remove(purchase.SKU);
		}
	}

	public bool IsProductPurchased(string SKU)
	{
		if (_purchases.ContainsKey(SKU))
		{
			GooglePurchaseTemplate purchaseDetails = GetPurchaseDetails(SKU);
			if (purchaseDetails.State == GooglePurchaseState.PURCHASED)
			{
				return true;
			}
			return false;
		}
		return false;
	}

	public GoogleProductTemplate GetProductDetails(string SKU)
	{
		foreach (GoogleProductTemplate product in Products)
		{
			if (product.SKU.Equals(SKU))
			{
				return product;
			}
		}
		GoogleProductTemplate googleProductTemplate = new GoogleProductTemplate();
		googleProductTemplate.SKU = SKU;
		return googleProductTemplate;
	}

	public GooglePurchaseTemplate GetPurchaseDetails(string SKU)
	{
		if (_purchases.ContainsKey(SKU))
		{
			return _purchases[SKU];
		}
		return null;
	}
}
