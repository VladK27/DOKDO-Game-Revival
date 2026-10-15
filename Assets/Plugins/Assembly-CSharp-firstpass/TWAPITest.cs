using UnityEngine;

public class TWAPITest : MonoBehaviour
{
	private void Start()
	{
		TW_OAuthAPIRequest tW_OAuthAPIRequest = TW_OAuthAPIRequest.Create();
		tW_OAuthAPIRequest.AddParam("count", 1);
		tW_OAuthAPIRequest.Send("https://api.twitter.com/1.1/statuses/home_timeline.json");
		tW_OAuthAPIRequest.OnResult += OnResult;
	}

	private void OnResult(TW_APIRequstResult result)
	{
		Debug.Log("Is Request Succeeded: " + result.IsSucceeded);
		Debug.Log("Responce data:");
		Debug.Log(result.responce);
	}
}
