using System;
using System.Text;
using UnityEngine;

[Serializable]
public class AmazonProductTemplate
{
	public bool IsOpen = true;

	[SerializeField]
	private string _LocalizedPrice = "0.99 $";

	[SerializeField]
	private long _PriceAmountMicros = 990000L;

	[SerializeField]
	private string _PriceCurrencyCode = "USD";

	[SerializeField]
	private string _sku = string.Empty;

	[SerializeField]
	private AMN_InAppType _productType;

	[SerializeField]
	private string _price = string.Empty;

	[SerializeField]
	private string _title = string.Empty;

	[SerializeField]
	private string _description = string.Empty;

	[SerializeField]
	private string _smallIconUrl = string.Empty;

	[SerializeField]
	private Texture2D _Texture;

	public string Sku
	{
		get
		{
			return _sku;
		}
		set
		{
			_sku = value;
		}
	}

	public AMN_InAppType ProductType
	{
		get
		{
			return _productType;
		}
		set
		{
			_productType = value;
		}
	}

	public string Price
	{
		get
		{
			return _price;
		}
	}

	public string Title
	{
		get
		{
			return _title;
		}
		set
		{
			_title = value;
		}
	}

	public string Description
	{
		get
		{
			return _description;
		}
		set
		{
			_description = value;
		}
	}

	public string SmallIconUrl
	{
		get
		{
			return _smallIconUrl;
		}
	}

	public Texture2D Texture
	{
		get
		{
			return _Texture;
		}
		set
		{
			_Texture = value;
		}
	}

	public string PriceCurrencyCode
	{
		get
		{
			return _PriceCurrencyCode;
		}
		set
		{
			_PriceCurrencyCode = value;
		}
	}

	public string LocalizedPrice
	{
		get
		{
			return _LocalizedPrice;
		}
		set
		{
			_LocalizedPrice = value;
		}
	}

	public long PriceAmountMicros
	{
		get
		{
			return _PriceAmountMicros;
		}
		set
		{
			_PriceAmountMicros = value;
		}
	}

	private static bool isFloatChar(char c)
	{
		return (c >= '0' && c <= '9') || c == '.';
	}

	private void SetPriceFromLocalizedPrice()
	{
		StringBuilder stringBuilder = new StringBuilder();
		StringBuilder stringBuilder2 = new StringBuilder();
		char[] array = LocalizedPrice.ToCharArray();
		foreach (char c in array)
		{
			if (isFloatChar(c))
			{
				stringBuilder2.Append(c);
			}
			else
			{
				stringBuilder.Append(c);
			}
		}
		float result;
		if (float.TryParse(stringBuilder2.ToString(), out result))
		{
			_price = result.ToString();
			_PriceCurrencyCode = PriceCurrencyCode.ToString().Trim();
		}
	}
}
