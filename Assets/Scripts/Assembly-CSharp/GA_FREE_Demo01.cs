using System.Collections;
using UnityEngine;

public class GA_FREE_Demo01 : MonoBehaviour
{
	public Canvas m_Canvas;

	public GUIAnimFREE m_Title1;

	public GUIAnimFREE m_Title2;

	public GUIAnimFREE m_TopBar;

	public GUIAnimFREE m_BottomBar;

	public GUIAnimFREE m_TopLeft_A;

	public GUIAnimFREE m_TopLeft_B;

	public GUIAnimFREE m_BottomLeft_A;

	public GUIAnimFREE m_BottomLeft_B;

	public GUIAnimFREE m_RightBar_A;

	public GUIAnimFREE m_RightBar_B;

	public GUIAnimFREE m_RightBar_C;

	private bool m_TopLeft_IsOn;

	private bool m_BottomLeft_IsOn;

	private bool m_RightBar_IsOn;

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
		m_TopLeft_A.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
		m_BottomLeft_A.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
		m_RightBar_A.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
		StartCoroutine(EnableAllDemoButtons());
	}

	public void HideAllGUIs()
	{
		m_TopLeft_A.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_BottomLeft_A.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_RightBar_A.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
		if (m_TopLeft_IsOn)
		{
			m_TopLeft_B.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		if (m_BottomLeft_IsOn)
		{
			m_BottomLeft_B.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		if (m_RightBar_IsOn)
		{
			m_RightBar_B.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
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

	public void OnButton_TopLeft()
	{
		StartCoroutine(DisableButtonForSeconds(m_TopLeft_A.gameObject, 0.3f));
		StartCoroutine(DisableButtonForSeconds(m_RightBar_A.gameObject, 0.6f));
		StartCoroutine(DisableButtonForSeconds(m_RightBar_C.gameObject, 0.6f));
		StartCoroutine(DisableButtonForSeconds(m_BottomLeft_A.gameObject, 0.3f));
		ToggleTopLeft();
		if (m_BottomLeft_IsOn)
		{
			ToggleBottomLeft();
		}
		if (m_RightBar_IsOn)
		{
			ToggleRightBar();
		}
	}

	public void OnButton_BottomLeft()
	{
		StartCoroutine(DisableButtonForSeconds(m_TopLeft_A.gameObject, 0.3f));
		StartCoroutine(DisableButtonForSeconds(m_RightBar_A.gameObject, 0.6f));
		StartCoroutine(DisableButtonForSeconds(m_RightBar_C.gameObject, 0.6f));
		StartCoroutine(DisableButtonForSeconds(m_BottomLeft_A.gameObject, 0.3f));
		ToggleBottomLeft();
		if (m_TopLeft_IsOn)
		{
			ToggleTopLeft();
		}
		if (m_RightBar_IsOn)
		{
			ToggleRightBar();
		}
	}

	public void OnButton_RightBar()
	{
		StartCoroutine(DisableButtonForSeconds(m_TopLeft_A.gameObject, 0.3f));
		StartCoroutine(DisableButtonForSeconds(m_RightBar_A.gameObject, 0.6f));
		StartCoroutine(DisableButtonForSeconds(m_RightBar_C.gameObject, 0.6f));
		StartCoroutine(DisableButtonForSeconds(m_BottomLeft_A.gameObject, 0.3f));
		ToggleRightBar();
		if (m_TopLeft_IsOn)
		{
			ToggleTopLeft();
		}
		if (m_BottomLeft_IsOn)
		{
			ToggleBottomLeft();
		}
	}

	private void ToggleTopLeft()
	{
		m_TopLeft_IsOn = !m_TopLeft_IsOn;
		if (m_TopLeft_IsOn)
		{
			m_TopLeft_B.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			m_TopLeft_B.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}

	private void ToggleBottomLeft()
	{
		m_BottomLeft_IsOn = !m_BottomLeft_IsOn;
		if (m_BottomLeft_IsOn)
		{
			m_BottomLeft_B.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			m_BottomLeft_B.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}

	private void ToggleRightBar()
	{
		m_RightBar_IsOn = !m_RightBar_IsOn;
		if (m_RightBar_IsOn)
		{
			m_RightBar_A.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
			m_RightBar_B.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			m_RightBar_A.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
			m_RightBar_B.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}
}
