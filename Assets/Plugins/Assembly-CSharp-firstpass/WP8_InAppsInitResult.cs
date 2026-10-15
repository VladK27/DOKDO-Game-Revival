using System;
using System.Collections.Generic;

public class WP8_InAppsInitResult : WP8_Result
{
	[Obsolete("This property is deprecated. Use 'Products' property instead")]
	public List<WP8ProductTemplate> products
	{
		get
		{
			return Products;
		}
	}

	public List<WP8ProductTemplate> Products
	{
		get
		{
			return WP8InAppPurchasesManager.Instance.Products;
		}
	}

	public WP8_InAppsInitResult(int statusCode)
		: base(statusCode == 0)
	{
	}
}
