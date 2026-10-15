using SA.Common.Models;

public class SK_RequestCapabilitieResult : Result
{
	private SK_CloudServiceCapability _Capability;

	public SK_CloudServiceCapability Capability
	{
		get
		{
			return _Capability;
		}
	}

	public SK_RequestCapabilitieResult(SK_CloudServiceCapability capability)
	{
		_Capability = capability;
	}

	public SK_RequestCapabilitieResult(string errorData)
		: base(new Error(errorData))
	{
	}
}
