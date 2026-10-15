using UnityEngine;

public abstract class SAOnClickAction : MonoBehaviour
{
	private void Awake()
	{
		DefaultPreviewButton component = GetComponent<DefaultPreviewButton>();
		if (component != null)
		{
			component.ActionClick += OnClick;
		}
	}

	protected abstract void OnClick();
}
