using System.Collections;
using UnityEngine;

public class GA_FREE_Demo08 : MonoBehaviour
{
	public Canvas m_Canvas;

	public GUIAnimFREE m_Title1;

	public GUIAnimFREE m_Title2;

	public GUIAnimFREE m_TopBar;

	public GUIAnimFREE m_BottomBar;

	public GUIAnimFREE m_CenterButtons;

	public GUIAnimFREE m_Button1;

	public GUIAnimFREE m_Button2;

	public GUIAnimFREE m_Button3;

	public GUIAnimFREE m_Button4;

	public GUIAnimFREE m_Bar1;

	public GUIAnimFREE m_Bar2;

	public GUIAnimFREE m_Bar3;

	public GUIAnimFREE m_Bar4;

	private bool m_Bar1_IsOn;

	private bool m_Bar2_IsOn;

	private bool m_Bar3_IsOn;

	private bool m_Bar4_IsOn;

	private void Awake()
	{
		if (base.enabled)
		{
			GUIAnimSystemFREE.Instance.m_AutoAnimation = false;
		}
	}

	private void Start()
	{
		m_TopBar.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_BottomBar.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(MoveInTitleGameObjects());
		GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(m_Canvas, false);
	}

	private void Update()
	{
	}

	private IEnumerator MoveInTitleGameObjects()
	{
		yield return new WaitForSeconds(1f);
		m_Title1.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
		m_Title2.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
		StartCoroutine(MoveInPrimaryButtons());
	}

	private IEnumerator MoveInPrimaryButtons()
	{
		yield return new WaitForSeconds(1f);
		m_CenterButtons.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(EnableAllDemoButtons());
	}

	public void HideAllGUIs()
	{
		m_CenterButtons.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		if (m_Bar1_IsOn)
		{
			m_Bar1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		if (m_Bar2_IsOn)
		{
			m_Bar2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		if (m_Bar3_IsOn)
		{
			m_Bar3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		if (m_Bar4_IsOn)
		{
			m_Bar4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		StartCoroutine(HideTitleTextMeshes());
	}

	private IEnumerator HideTitleTextMeshes()
	{
		yield return new WaitForSeconds(1f);
		m_Title1.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
		m_Title2.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
		m_TopBar.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_BottomBar.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}

	private IEnumerator EnableAllDemoButtons()
	{
		yield return new WaitForSeconds(1f);
		GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(m_Canvas, true);
	}

	private IEnumerator DisableButtonForSeconds(GameObject GO, float DisableTime)
	{
		GUIAnimSystemFREE.Instance.EnableButton(GO.transform, false);
		yield return new WaitForSeconds(DisableTime);
		GUIAnimSystemFREE.Instance.EnableButton(GO.transform, true);
	}

	public void OnButton_1()
	{
		ToggleBar1();
		if (m_Bar2_IsOn)
		{
			ToggleBar2();
		}
		if (m_Bar3_IsOn)
		{
			ToggleBar3();
		}
		if (m_Bar4_IsOn)
		{
			ToggleBar4();
		}
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 0.75f));
	}

	public void OnButton_2()
	{
		ToggleBar2();
		if (m_Bar1_IsOn)
		{
			ToggleBar1();
		}
		if (m_Bar3_IsOn)
		{
			ToggleBar3();
		}
		if (m_Bar4_IsOn)
		{
			ToggleBar4();
		}
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 0.75f));
	}

	public void OnButton_3()
	{
		ToggleBar3();
		if (m_Bar1_IsOn)
		{
			ToggleBar1();
		}
		if (m_Bar2_IsOn)
		{
			ToggleBar2();
		}
		if (m_Bar4_IsOn)
		{
			ToggleBar4();
		}
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 0.75f));
	}

	public void OnButton_4()
	{
		ToggleBar4();
		if (m_Bar1_IsOn)
		{
			ToggleBar1();
		}
		if (m_Bar2_IsOn)
		{
			ToggleBar2();
		}
		if (m_Bar3_IsOn)
		{
			ToggleBar3();
		}
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 0.75f));
	}

	private void ToggleBar1()
	{
		m_Bar1_IsOn = !m_Bar1_IsOn;
		if (m_Bar1_IsOn)
		{
			m_Bar1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			m_Bar1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}

	private void ToggleBar2()
	{
		m_Bar2_IsOn = !m_Bar2_IsOn;
		if (m_Bar2_IsOn)
		{
			m_Bar2.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			m_Bar2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}

	private void ToggleBar3()
	{
		m_Bar3_IsOn = !m_Bar3_IsOn;
		if (m_Bar3_IsOn)
		{
			m_Bar3.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			m_Bar3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}

	private void ToggleBar4()
	{
		m_Bar4_IsOn = !m_Bar4_IsOn;
		if (m_Bar4_IsOn)
		{
			m_Bar4.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			m_Bar4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}
}
