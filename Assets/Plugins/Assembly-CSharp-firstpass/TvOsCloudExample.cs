using SA.Common.Pattern;
using UnityEngine;

public class TvOsCloudExample : MonoBehaviour
{
	private void Start()
	{
		Debug.Log("iCloudManager.Instance.init()");
		iCloudManager.OnCloudDataReceivedAction += OnCloudDataReceivedAction;
		Singleton<iCloudManager>.Instance.setString("Test", "test");
		Singleton<iCloudManager>.Instance.requestDataForKey("Test");
	}

	private void OnCloudDataReceivedAction(iCloudData data)
	{
		Debug.Log("OnCloudDataReceivedAction");
		if (data.IsEmpty)
		{
			Debug.Log(data.key + " / data is empty");
		}
		else
		{
			Debug.Log(data.key + " / " + data.stringValue);
		}
	}
}
