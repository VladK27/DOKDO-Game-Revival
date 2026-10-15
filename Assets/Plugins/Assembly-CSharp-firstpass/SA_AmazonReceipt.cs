public class SA_AmazonReceipt
{
	private string _sku = string.Empty;

	private string _productType = string.Empty;

	private string _receiptId = string.Empty;

	private long _purchaseDate;

	private long _cancelDate;

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

	public string ReceiptId
	{
		get
		{
			return _receiptId;
		}
	}

	public long PurchaseDate
	{
		get
		{
			return _purchaseDate;
		}
	}

	public long CancelDate
	{
		get
		{
			return _cancelDate;
		}
	}
}
