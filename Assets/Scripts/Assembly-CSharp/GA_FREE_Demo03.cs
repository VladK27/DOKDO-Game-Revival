using System.Collections;
using UnityEngine;

public class GA_FREE_Demo03 : MonoBehaviour
{
	public Canvas m_Canvas;

	public GUIAnimFREE m_Title1;

	public GUIAnimFREE m_Title2;

	public GUIAnimFREE m_TopBar;

	public GUIAnimFREE m_BottomBar;

	public GUIAnimFREE m_Dialog1;

	public GUIAnimFREE m_Dialog2;

	public GUIAnimFREE m_Dialog3;

	public GUIAnimFREE m_Dialog4;

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
		StartCoroutine(ShowDialogs());
	}

	private IEnumerator ShowDialogs()
	{
		yield return new WaitForSeconds(1f);
		m_Dialog1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Dialog2.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Dialog3.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Dialog4.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(EnableAllDemoButtons());
	}

	public void HideAllGUIs()
	{
		m_Dialog1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Dialog2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Dialog3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Dialog4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
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

	public void OnButton_Dialog1()
	{
		StartCoroutine(DisableButtonForSeconds(m_Dialog1.gameObject, 1f));
		m_Dialog1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(Dialog1_MoveIn());
	}

	public void OnButton_Dialog2()
	{
		StartCoroutine(DisableButtonForSeconds(m_Dialog2.gameObject, 1f));
		m_Dialog2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(Dialog2_MoveIn());
	}

	public void OnButton_Dialog3()
	{
		StartCoroutine(DisableButtonForSeconds(m_Dialog3.gameObject, 1f));
		m_Dialog3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(Dialog3_MoveIn());
	}

	public void OnButton_Dialog4()
	{
		StartCoroutine(DisableButtonForSeconds(m_Dialog4.gameObject, 1f));
		m_Dialog4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(Dialog4_MoveIn());
	}

	public void OnButton_MoveOutAllDialogs()
	{
		if (!m_Dialog1.m_MoveOut.Began && !m_Dialog1.m_MoveOut.Done)
		{
			m_Dialog1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
			StartCoroutine(Dialog1_MoveIn());
		}
		if (!m_Dialog2.m_MoveOut.Began && !m_Dialog2.m_MoveOut.Done)
		{
			m_Dialog2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
			StartCoroutine(Dialog2_MoveIn());
		}
		if (!m_Dialog3.m_MoveOut.Began && !m_Dialog3.m_MoveOut.Done)
		{
			m_Dialog3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
			StartCoroutine(Dialog3_MoveIn());
		}
		if (!m_Dialog4.m_MoveOut.Began && !m_Dialog4.m_MoveOut.Done)
		{
			m_Dialog4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
			StartCoroutine(Dialog4_MoveIn());
		}
	}

	private IEnumerator Dialog1_MoveIn()
	{
		yield return new WaitForSeconds(1.5f);
		m_Dialog1.Reset();
		m_Dialog1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}

	private IEnumerator Dialog2_MoveIn()
	{
		yield return new WaitForSeconds(1.5f);
		m_Dialog2.Reset();
		m_Dialog2.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}

	private IEnumerator Dialog3_MoveIn()
	{
		yield return new WaitForSeconds(1.5f);
		m_Dialog3.Reset();
		m_Dialog3.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}

	private IEnumerator Dialog4_MoveIn()
	{
		yield return new WaitForSeconds(1.5f);
		m_Dialog4.Reset();
		m_Dialog4.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}
}
