using System;
using SA.IOSNative.StoreKit;
using UnityEngine;

[Serializable]
public class UM_InAppProduct
{
	public bool IsOpen;

	public string id = "new_product";

	public UM_InAppType Type;

	public PriceTier PriceTier;

	public Texture2D Texture;

	public string DisplayName = "New Product";

	public string Description = string.Empty;

	public string IOSId = string.Empty;

	public string AndroidId = string.Empty;

	public string WP8Id = string.Empty;

	public string AmazonId = string.Empty;

	private Product _IOSTemplate = new Product();

	private WP8ProductTemplate _WP8Template = new WP8ProductTemplate();

	private GoogleProductTemplate _AndroidTemplate = new GoogleProductTemplate();

	private AmazonProductTemplate _AmazonTemplate = new AmazonProductTemplate();

	private UM_InAppProductTemplate _template = new UM_InAppProductTemplate();

	private bool _isTemplateSet;

	public WP8ProductTemplate WP8Template
	{
		get
		{
			return _WP8Template;
		}
	}

	public Product IOSTemplate
	{
		get
		{
			return _IOSTemplate;
		}
	}

	public GoogleProductTemplate AndroidTemplate
	{
		get
		{
			return _AndroidTemplate;
		}
	}

	public AmazonProductTemplate AmazonTemplate
	{
		get
		{
			return _AmazonTemplate;
		}
	}

	public UM_InAppProductTemplate template
	{
		get
		{
			return _template;
		}
	}

	public bool IsConsumable
	{
		get
		{
			if (Type == UM_InAppType.Consumable)
			{
				return true;
			}
			return false;
		}
	}

	public string LocalizedPrice
	{
		get
		{
			if (!_isTemplateSet)
			{
				return GetPriceByTier() + " $";
			}
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				if (UltimateMobileSettings.Instance.PlatformEngine.Equals(UM_PlatformDependencies.Android))
				{
					return _AndroidTemplate.LocalizedPrice;
				}
				return _AmazonTemplate.LocalizedPrice;
			case RuntimePlatform.IPhonePlayer:
				return _IOSTemplate.LocalizedPrice;
			case RuntimePlatform.MetroPlayerX86:
			case RuntimePlatform.MetroPlayerX64:
			case RuntimePlatform.MetroPlayerARM:
				return _WP8Template.Price;
			default:
				return GetPriceByTier() + " $";
			}
		}
	}

	public string CurrencyCode
	{
		get
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				if (UltimateMobileSettings.Instance.PlatformEngine.Equals(UM_PlatformDependencies.Android))
				{
					return (!_isTemplateSet) ? "USD" : _AndroidTemplate.PriceCurrencyCode;
				}
				return (!_isTemplateSet) ? "USD" : _AmazonTemplate.PriceCurrencyCode;
			case RuntimePlatform.IPhonePlayer:
				return (!_isTemplateSet) ? "USD" : _IOSTemplate.CurrencyCode;
			default:
				return string.Empty;
			}
		}
	}

	public long PriceAmountMicros
	{
		get
		{
			if (!_isTemplateSet)
			{
				return Convert.ToInt64(GetPriceByTier() * 1000000f);
			}
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				if (UltimateMobileSettings.Instance.PlatformEngine.Equals(UM_PlatformDependencies.Android))
				{
					return _AndroidTemplate.PriceAmountMicros;
				}
				return Convert.ToInt64(GetPriceByTier() * 1000000f);
			case RuntimePlatform.IPhonePlayer:
				return _IOSTemplate.PriceInMicros;
			case RuntimePlatform.MetroPlayerX86:
			case RuntimePlatform.MetroPlayerX64:
			case RuntimePlatform.MetroPlayerARM:
				return Convert.ToInt64(GetPriceByTier() * 1000000f);
			default:
				return Convert.ToInt64(GetPriceByTier() * 1000000f);
			}
		}
	}

	public string ActualPrice
	{
		get
		{
			return ActualPriceValue.ToString();
		}
	}

	public float ActualPriceValue
	{
		get
		{
			return (float)PriceAmountMicros / 1000000f;
		}
	}

	public bool IsPurchased
	{
		get
		{
			return UM_InAppPurchaseManager.Client.IsProductPurchased(this);
		}
	}

	public string Title
	{
		get
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				if (UltimateMobileSettings.Instance.PlatformEngine.Equals(UM_PlatformDependencies.Android))
				{
					return _AndroidTemplate.Title;
				}
				return _AmazonTemplate.Title;
			case RuntimePlatform.IPhonePlayer:
				return _IOSTemplate.DisplayName;
			case RuntimePlatform.MetroPlayerX86:
			case RuntimePlatform.MetroPlayerX64:
			case RuntimePlatform.MetroPlayerARM:
				return _WP8Template.Name;
			default:
				return string.Empty;
			}
		}
	}

	public void SetTemplate(WP8ProductTemplate tpl)
	{
		_WP8Template = tpl;
		_template = new UM_InAppProductTemplate();
		_template.id = tpl.ProductId;
		_template.title = tpl.Name;
		_template.description = tpl.Description;
		_template.price = tpl.Price;
		DisplayName = tpl.Name;
		Description = tpl.Description;
		_isTemplateSet = true;
	}

	public void SetTemplate(Product tpl)
	{
		_IOSTemplate = tpl;
		_template = new UM_InAppProductTemplate();
		_template.id = tpl.Id;
		_template.title = tpl.DisplayName;
		_template.description = tpl.Description;
		_template.price = tpl.Price.ToString();
		DisplayName = tpl.DisplayName;
		Description = tpl.Description;
		_isTemplateSet = true;
	}

	public void SetTemplate(GoogleProductTemplate tpl)
	{
		_AndroidTemplate = tpl;
		_template = new UM_InAppProductTemplate();
		_template.id = tpl.SKU;
		_template.title = tpl.Title;
		_template.description = tpl.Description;
		_template.price = tpl.Price.ToString();
		DisplayName = tpl.Title;
		Description = tpl.Description;
		_isTemplateSet = true;
	}

	public void SetTemplate(AmazonProductTemplate tpl)
	{
		_AmazonTemplate = tpl;
		_template = new UM_InAppProductTemplate();
		_template.id = tpl.Sku;
		_template.title = tpl.Title;
		_template.description = tpl.Description;
		_template.price = tpl.Price.ToString();
		DisplayName = tpl.Title;
		Description = tpl.Description;
		_isTemplateSet = true;
	}

	private float GetPriceByTier()
	{
		int priceTier = (int)PriceTier;
		return (float)priceTier + 1f - 0.01f;
	}

	public override string ToString()
	{
		return string.Format("[UM_InAppProduct: template={0}, Title={1}, Description={2}, Price={3}, WP8Template={4}, IOSTemplate={5}, AndroidTemplate={6}, AndroidTemplate={7}]", template, DisplayName, Description, LocalizedPrice, WP8Template, IOSTemplate, AndroidTemplate, AmazonTemplate);
	}
}
