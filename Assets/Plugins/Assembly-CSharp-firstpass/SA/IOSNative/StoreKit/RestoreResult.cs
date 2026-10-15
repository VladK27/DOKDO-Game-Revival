using SA.Common.Models;

namespace SA.IOSNative.StoreKit
{
	public class RestoreResult : Result
	{
		public TransactionErrorCode TransactionErrorCode
		{
			get
			{
				if (_Error != null)
				{
					return (TransactionErrorCode)_Error.Code;
				}
				return TransactionErrorCode.SKErrorNone;
			}
		}

		public RestoreResult(Error e)
			: base(e)
		{
		}

		public RestoreResult()
		{
		}
	}
}
