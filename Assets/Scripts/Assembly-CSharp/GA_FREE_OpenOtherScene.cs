using UnityEngine;

public class GA_FREE_OpenOtherScene : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void ButtonOpenDemoScene1()
	{
		GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("GA FREE - Demo01 (960x600px)", 1.5f);
		base.gameObject.SendMessage("HideAllGUIs");
	}

	public void ButtonOpenDemoScene2()
	{
		GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("GA FREE - Demo02 (960x600px)", 1.5f);
		base.gameObject.SendMessage("HideAllGUIs");
	}

	public void ButtonOpenDemoScene3()
	{
		GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("GA FREE - Demo03 (960x600px)", 1.5f);
		base.gameObject.SendMessage("HideAllGUIs");
	}

	public void ButtonOpenDemoScene4()
	{
		GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("GA FREE - Demo04 (960x600px)", 1.5f);
		base.gameObject.SendMessage("HideAllGUIs");
	}

	public void ButtonOpenDemoScene5()
	{
		GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("GA FREE - Demo05 (960x600px)", 1.5f);
		base.gameObject.SendMessage("HideAllGUIs");
	}

	public void ButtonOpenDemoScene6()
	{
		GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("GA FREE - Demo06 (960x600px)", 1.5f);
		base.gameObject.SendMessage("HideAllGUIs");
	}

	public void ButtonOpenDemoScene7()
	{
		GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("GA FREE - Demo07 (960x600px)", 1.5f);
		base.gameObject.SendMessage("HideAllGUIs");
	}

	public void ButtonOpenDemoScene8()
	{
		GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("GA FREE - Demo08 (960x600px)", 1.5f);
		base.gameObject.SendMessage("HideAllGUIs");
	}
}
