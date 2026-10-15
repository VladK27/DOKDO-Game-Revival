using Facebook.Unity;
using UnityEngine;

public class FB_GrapRequest_V7
{
	private SPFacebook.FB_Delegate _Callback;

	public FB_GrapRequest_V7(string query, HttpMethod method, SPFacebook.FB_Delegate callback)
	{
		_Callback = callback;
		FB.API(query, method, GraphCallback);
	}

	public FB_GrapRequest_V7(string query, HttpMethod method, SPFacebook.FB_Delegate callback, WWWForm form)
	{
		_Callback = callback;
		FB.API(query, method, GraphCallback, form);
	}

	private void GraphCallback(IGraphResult result)
	{
		FB_Result result2 = ((result != null) ? new FB_Result(result.RawResult, result.Error) : new FB_Result(string.Empty, "Null Response"));
		_Callback(result2);
	}
}
