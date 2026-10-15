using SA.Common.Pattern;
using UnityEngine;

public class ImmersiveMode : Singleton<ImmersiveMode>
{
	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}

	public void EnableImmersiveMode()
	{
		AN_ImmersiveModeProxy.enableImmersiveMode();
	}
}
