public class AMN_PurchaseResponse : AMN_Result
{
	private string _requestId = string.Empty;

	private string _userId = string.Empty;

	private string _marketplace = string.Empty;

	private string _receiptId = string.Empty;

	private long _cancelDate;

	private long _purchaseDate;

	private string _sku = string.Empty;

	private string _productType = string.Empty;

	private string _status = string.Empty;

	public string RequestId
	{
		get
		{
			return _requestId;
		}
	}

	public string UserId
	{
		get
		{
			return _userId;
		}
	}

	public string Marketplace
	{
		get
		{
			return _marketplace;
		}
	}

	public string ReceiptId
	{
		get
		{
			return _receiptId;
		}
	}

	public long CancelDate
	{
		get
		{
			return _cancelDate;
		}
	}

	public long PurchaseDatee
	{
		get
		{
			return _purchaseDate;
		}
	}

	public string Sku
	{
		get
		{
			return _sku;
		}
	}

	public string ProductType
	{
		get
		{
			return _productType;
		}
	}

	public string Status
	{
		get
		{
			return _status;
		}
	}

	public AMN_PurchaseResponse()
		: base(true)
	{
	}
}
