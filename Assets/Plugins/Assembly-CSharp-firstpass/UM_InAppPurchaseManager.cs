using System;
using System.Collections.Generic;
using SA.IOSNative.StoreKit;
using UnityEngine;

public class UM_InAppPurchaseManager
{
	private const string PREFS_KEY = "UM_InAppPurchaseManager";

	public static UM_InAppClient _Client;

	[Obsolete("Instance is deprectaed, please use Client instead")]
	public static UM_InAppClient Instance
	{
		get
		{
			return Client;
		}
	}

	[Obsolete("instance is deprectaed, please use Client instead")]
	public static UM_InAppClient instance
	{
		get
		{
			return Client;
		}
	}

	public static UM_InAppClient Client
	{
		get
		{
			if (_Client == null)
			{
				switch (Application.platform)
				{
				case RuntimePlatform.IPhonePlayer:
					_Client = new UM_IOS_InAppClient();
					break;
				case RuntimePlatform.Android:
					if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
					{
						_Client = new UM_Amazon_InAppClient();
					}
					else
					{
						_Client = new UM_Android_InAppClient();
					}
					break;
				case RuntimePlatform.MetroPlayerX86:
				case RuntimePlatform.MetroPlayerX64:
				case RuntimePlatform.MetroPlayerARM:
					_Client = new UM_WP8_InAppClient();
					break;
				default:
					if (Application.isEditor && UltimateMobileSettings.Instance.Is_InApps_EditorTestingEnabled)
					{
						_Client = new UM_Editor_InAppClient();
					}
					else
					{
						_Client = new UM_Disabled_InAppClient();
					}
					break;
				}
				_Client.OnPurchaseFinished += ClientPurchaseFinishedHadnler;
			}
			return _Client;
		}
	}

	public static List<UM_InAppProduct> InAppProducts
	{
		get
		{
			return UltimateMobileSettings.Instance.InAppProducts;
		}
	}

	public static UM_InAppProduct GetProductById(string id)
	{
		return UltimateMobileSettings.Instance.GetProductById(id);
	}

	public static UM_InAppProduct GetProductByIOSId(string id)
	{
		return UltimateMobileSettings.Instance.GetProductByIOSId(id);
	}

	public static UM_InAppProduct GetProductByAndroidId(string id)
	{
		return UltimateMobileSettings.Instance.GetProductByAndroidId(id);
	}

	public static UM_InAppProduct GetProductByAmazonId(string id)
	{
		return UltimateMobileSettings.Instance.GetProductByAmazonId(id);
	}

	public static UM_InAppProduct GetProductByWp8Id(string id)
	{
		return UltimateMobileSettings.Instance.GetProductByWp8Id(id);
	}

	public static bool IsLocalPurchaseRecordExists(UM_InAppProduct product)
	{
		if (product == null)
		{
			return false;
		}
		if (product.IsConsumable)
		{
			return false;
		}
		return PlayerPrefs.HasKey("UM_InAppPurchaseManager" + product.id);
	}

	public static bool IsLocalPurchaseRecordExists(string productId)
	{
		if (string.IsNullOrEmpty(productId))
		{
			return false;
		}
		return PlayerPrefs.HasKey("UM_InAppPurchaseManager" + productId);
	}

	public static void SaveNonConsumableItemPurchaseInfo(UM_InAppProduct product)
	{
		PlayerPrefs.SetInt("UM_InAppPurchaseManager" + product.id, 1);
	}

	public static void UpdatePlatfromsInAppSettings()
	{
		IOSNativeSettings.Instance.InAppProducts.Clear();
		AndroidNativeSettings.Instance.InAppProducts.Clear();
		AmazonNativeSettings.Instance.InAppProducts.Clear();
		foreach (UM_InAppProduct inAppProduct in UltimateMobileSettings.Instance.InAppProducts)
		{
			AddToISNSettings(inAppProduct);
			AddToANSettings(inAppProduct);
			AddToAMMSettings(inAppProduct);
		}
	}

	private static void AddToANSettings(UM_InAppProduct prod)
	{
		if (!prod.AndroidId.Equals(string.Empty))
		{
			GoogleProductTemplate googleProductTemplate = new GoogleProductTemplate();
			googleProductTemplate.SKU = prod.AndroidId;
			googleProductTemplate.Title = prod.DisplayName;
			googleProductTemplate.Description = prod.Description;
			googleProductTemplate.Texture = prod.Texture;
			googleProductTemplate.LocalizedPrice = prod.LocalizedPrice;
			if (prod.Type == UM_InAppType.Consumable)
			{
				googleProductTemplate.ProductType = AN_InAppType.Consumable;
			}
			if (prod.Type == UM_InAppType.NonConsumable)
			{
				googleProductTemplate.ProductType = AN_InAppType.NonConsumable;
			}
			AndroidNativeSettings.Instance.InAppProducts.Add(googleProductTemplate);
		}
	}

	private static void AddToISNSettings(UM_InAppProduct prod)
	{
		if (!prod.IOSId.Equals(string.Empty))
		{
			Product product = new Product();
			product.Id = prod.IOSId;
			product.Description = prod.Description;
			product.DisplayName = prod.DisplayName;
			product.PriceTier = prod.PriceTier;
			product.Texture = prod.Texture;
			product.IsOpen = false;
			if (prod.Type == UM_InAppType.Consumable)
			{
				product.Type = ProductType.Consumable;
			}
			if (prod.Type == UM_InAppType.NonConsumable)
			{
				product.Type = ProductType.NonConsumable;
			}
			IOSNativeSettings.Instance.InAppProducts.Add(product);
		}
	}

	private static void AddToAMMSettings(UM_InAppProduct prod)
	{
		if (!prod.AmazonId.Equals(string.Empty))
		{
			AmazonProductTemplate amazonProductTemplate = new AmazonProductTemplate();
			amazonProductTemplate.Sku = prod.AmazonId;
			amazonProductTemplate.Description = prod.Description;
			amazonProductTemplate.Title = prod.DisplayName;
			amazonProductTemplate.LocalizedPrice = prod.LocalizedPrice;
			amazonProductTemplate.Texture = prod.Texture;
			amazonProductTemplate.IsOpen = false;
			if (prod.Type == UM_InAppType.Consumable)
			{
				amazonProductTemplate.ProductType = AMN_InAppType.CONSUMABLE;
			}
			if (prod.Type == UM_InAppType.NonConsumable)
			{
				amazonProductTemplate.ProductType = AMN_InAppType.ENTITLED;
			}
			AmazonNativeSettings.Instance.InAppProducts.Add(amazonProductTemplate);
		}
	}

	private static void ClientPurchaseFinishedHadnler(UM_PurchaseResult result)
	{
		if (!result.product.IsConsumable && result.isSuccess)
		{
			SaveNonConsumableItemPurchaseInfo(result.product);
		}
	}
}
