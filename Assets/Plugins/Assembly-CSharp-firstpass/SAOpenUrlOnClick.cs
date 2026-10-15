using UnityEngine;

public class SAOpenUrlOnClick : SAOnClickAction
{
	public string url;

	protected override void OnClick()
	{
		Application.OpenURL(url);
	}
}
