using System;
using UnityEngine;

[Serializable]
public class GA_FREE_JS_OpenOtherScene : MonoBehaviour
{
	public virtual void Start()
	{
	}

	public virtual void Update()
	{
	}

	public virtual void ButtonOpenDemoScene1()
	{
		GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("GA FREE JS - Demo01 (960x600px)", 1.5f);
		gameObject.SendMessage("HideAllGUIs");
	}

	public virtual void ButtonOpenDemoScene2()
	{
		GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("GA FREE JS - Demo02 (960x600px)", 1.5f);
		gameObject.SendMessage("HideAllGUIs");
	}

	public virtual void ButtonOpenDemoScene3()
	{
		GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("GA FREE JS - Demo03 (960x600px)", 1.5f);
		gameObject.SendMessage("HideAllGUIs");
	}

	public virtual void ButtonOpenDemoScene4()
	{
		GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("GA FREE JS - Demo04 (960x600px)", 1.5f);
		gameObject.SendMessage("HideAllGUIs");
	}

	public virtual void ButtonOpenDemoScene5()
	{
		GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("GA FREE JS - Demo05 (960x600px)", 1.5f);
		gameObject.SendMessage("HideAllGUIs");
	}

	public virtual void ButtonOpenDemoScene6()
	{
		GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("GA FREE JS - Demo06 (960x600px)", 1.5f);
		gameObject.SendMessage("HideAllGUIs");
	}

	public virtual void ButtonOpenDemoScene7()
	{
		GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("GA FREE JS - Demo07 (960x600px)", 1.5f);
		gameObject.SendMessage("HideAllGUIs");
	}

	public virtual void ButtonOpenDemoScene8()
	{
		GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("GA FREE JS - Demo08 (960x600px)", 1.5f);
		gameObject.SendMessage("HideAllGUIs");
	}
}
