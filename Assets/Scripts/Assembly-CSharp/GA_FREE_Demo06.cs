using System.Collections;
using UnityEngine;

public class GA_FREE_Demo06 : MonoBehaviour
{
	public Canvas m_Canvas;

	public GUIAnimFREE m_Title1;

	public GUIAnimFREE m_Title2;

	public GUIAnimFREE m_TopBar;

	public GUIAnimFREE m_BottomBar;

	public GUIAnimFREE m_PrimaryButton1;

	public GUIAnimFREE m_PrimaryButton2;

	public GUIAnimFREE m_PrimaryButton3;

	public GUIAnimFREE m_PrimaryButton4;

	public GUIAnimFREE m_PrimaryButton5;

	public GUIAnimFREE m_SecondaryButton1;

	public GUIAnimFREE m_SecondaryButton2;

	public GUIAnimFREE m_SecondaryButton3;

	public GUIAnimFREE m_SecondaryButton4;

	public GUIAnimFREE m_SecondaryButton5;

	private bool m_Button1_IsOn;

	private bool m_Button2_IsOn;

	private bool m_Button3_IsOn;

	private bool m_Button4_IsOn;

	private bool m_Button5_IsOn;

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
		GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(m_Canvas, true);
	}

	private IEnumerator MoveInPrimaryButtons()
	{
		yield return new WaitForSeconds(1f);
		m_PrimaryButton1.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
		m_PrimaryButton2.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
		m_PrimaryButton3.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
		m_PrimaryButton4.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
		m_PrimaryButton5.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
		StartCoroutine(EnableAllDemoButtons());
	}

	public void HideAllGUIs()
	{
		m_PrimaryButton1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_PrimaryButton2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_PrimaryButton3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_PrimaryButton4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_PrimaryButton5.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		if (m_Button1_IsOn)
		{
			m_SecondaryButton1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		if (m_Button2_IsOn)
		{
			m_SecondaryButton2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		if (m_Button3_IsOn)
		{
			m_SecondaryButton3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		if (m_Button4_IsOn)
		{
			m_SecondaryButton4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		if (m_Button5_IsOn)
		{
			m_SecondaryButton5.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
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

	private IEnumerator DisableAllButtonsForSeconds(float DisableTime)
	{
		GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		yield return new WaitForSeconds(DisableTime);
		GUIAnimSystemFREE.Instance.EnableAllButtons(true);
	}

	public void OnButton_1()
	{
		StartCoroutine(DisableAllButtonsForSeconds(0.6f));
		ToggleButton_1();
		if (m_Button2_IsOn)
		{
			ToggleButton_2();
		}
		if (m_Button3_IsOn)
		{
			ToggleButton_3();
		}
		if (m_Button4_IsOn)
		{
			ToggleButton_4();
		}
		if (m_Button5_IsOn)
		{
			ToggleButton_5();
		}
	}

	public void OnButton_2()
	{
		StartCoroutine(DisableAllButtonsForSeconds(0.6f));
		ToggleButton_2();
		if (m_Button1_IsOn)
		{
			ToggleButton_1();
		}
		if (m_Button3_IsOn)
		{
			ToggleButton_3();
		}
		if (m_Button4_IsOn)
		{
			ToggleButton_4();
		}
		if (m_Button5_IsOn)
		{
			ToggleButton_5();
		}
	}

	public void OnButton_3()
	{
		StartCoroutine(DisableAllButtonsForSeconds(0.6f));
		ToggleButton_3();
		if (m_Button1_IsOn)
		{
			ToggleButton_1();
		}
		if (m_Button2_IsOn)
		{
			ToggleButton_2();
		}
		if (m_Button4_IsOn)
		{
			ToggleButton_4();
		}
		if (m_Button5_IsOn)
		{
			ToggleButton_5();
		}
	}

	public void OnButton_4()
	{
		StartCoroutine(DisableAllButtonsForSeconds(0.6f));
		ToggleButton_4();
		if (m_Button1_IsOn)
		{
			ToggleButton_1();
		}
		if (m_Button2_IsOn)
		{
			ToggleButton_2();
		}
		if (m_Button3_IsOn)
		{
			ToggleButton_3();
		}
		if (m_Button5_IsOn)
		{
			ToggleButton_5();
		}
	}

	public void OnButton_5()
	{
		StartCoroutine(DisableAllButtonsForSeconds(0.6f));
		ToggleButton_5();
		if (m_Button1_IsOn)
		{
			ToggleButton_1();
		}
		if (m_Button2_IsOn)
		{
			ToggleButton_2();
		}
		if (m_Button3_IsOn)
		{
			ToggleButton_3();
		}
		if (m_Button4_IsOn)
		{
			ToggleButton_4();
		}
	}

	private void ToggleButton_1()
	{
		m_Button1_IsOn = !m_Button1_IsOn;
		if (m_Button1_IsOn)
		{
			m_SecondaryButton1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			m_SecondaryButton1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}

	private void ToggleButton_2()
	{
		m_Button2_IsOn = !m_Button2_IsOn;
		if (m_Button2_IsOn)
		{
			m_SecondaryButton2.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			m_SecondaryButton2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}

	private void ToggleButton_3()
	{
		m_Button3_IsOn = !m_Button3_IsOn;
		if (m_Button3_IsOn)
		{
			m_SecondaryButton3.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			m_SecondaryButton3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}

	private void ToggleButton_4()
	{
		m_Button4_IsOn = !m_Button4_IsOn;
		if (m_Button4_IsOn)
		{
			m_SecondaryButton4.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			m_SecondaryButton4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}

	private void ToggleButton_5()
	{
		m_Button5_IsOn = !m_Button5_IsOn;
		if (m_Button5_IsOn)
		{
			m_SecondaryButton5.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			m_SecondaryButton5.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}
}
