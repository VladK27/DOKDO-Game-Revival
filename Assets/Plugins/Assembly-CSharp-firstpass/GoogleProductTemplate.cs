using System;
using UnityEngine;

[Serializable]
public class GoogleProductTemplate
{
	public const long DEFAULT_PRICE_AMOUNT_MICROS = 990000L;

	public bool IsOpen = true;

	public string SKU = string.Empty;

	private string _OriginalJson = string.Empty;

	[SerializeField]
	private string _LocalizedPrice = "0.99 $";

	[SerializeField]
	private long _PriceAmountMicros = 990000L;

	[SerializeField]
	private string _PriceCurrencyCode = "USD";

	[SerializeField]
	private string _Description = string.Empty;

	[SerializeField]
	private string _Title = "New Product";

	[SerializeField]
	private Texture2D _Texture;

	[SerializeField]
	private AN_InAppType _ProductType;

	public string OriginalJson
	{
		get
		{
			return _OriginalJson;
		}
		set
		{
			_OriginalJson = value;
		}
	}

	public float Price
	{
		get
		{
			return (float)_PriceAmountMicros / 1000000f;
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

	public string Description
	{
		get
		{
			return _Description;
		}
		set
		{
			_Description = value;
		}
	}

	public string Title
	{
		get
		{
			return _Title;
		}
		set
		{
			_Title = value;
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

	public AN_InAppType ProductType
	{
		get
		{
			return _ProductType;
		}
		set
		{
			_ProductType = value;
		}
	}
}
