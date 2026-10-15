using System.Collections;
using UnityEngine;

public class GA_FREE_Demo05 : MonoBehaviour
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
		StartCoroutine(MoveInPrimaryButtons());
	}

	private IEnumerator MoveInPrimaryButtons()
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
		m_Dialog1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableButtonForSeconds(m_Dialog1.gameObject, 2.5f));
		StartCoroutine(Dialog1_MoveIn());
	}

	public void OnButton_Dialog2()
	{
		m_Dialog2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableButtonForSeconds(m_Dialog2.gameObject, 2.5f));
		StartCoroutine(Dialog2_MoveIn());
	}

	public void OnButton_Dialog3()
	{
		m_Dialog3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableButtonForSeconds(m_Dialog3.gameObject, 2.5f));
		StartCoroutine(Dialog3_MoveIn());
	}

	public void OnButton_Dialog4()
	{
		m_Dialog4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableButtonForSeconds(m_Dialog4.gameObject, 2.5f));
		StartCoroutine(Dialog4_MoveIn());
	}

	public void OnButton_MoveOutAllDialogs()
	{
		StartCoroutine(DisableButtonForSeconds(m_Dialog1.gameObject, 2.5f));
		StartCoroutine(DisableButtonForSeconds(m_Dialog2.gameObject, 2.5f));
		StartCoroutine(DisableButtonForSeconds(m_Dialog3.gameObject, 2.5f));
		StartCoroutine(DisableButtonForSeconds(m_Dialog4.gameObject, 2.5f));
		m_Dialog1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Dialog2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Dialog3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Dialog4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(Dialog1_MoveIn());
		StartCoroutine(Dialog2_MoveIn());
		StartCoroutine(Dialog3_MoveIn());
		StartCoroutine(Dialog4_MoveIn());
	}

	private IEnumerator Dialog1_MoveIn()
	{
		yield return new WaitForSeconds(1.5f);
		m_Dialog1.ResetAllChildren();
		m_Dialog1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}

	private IEnumerator Dialog2_MoveIn()
	{
		yield return new WaitForSeconds(1.5f);
		m_Dialog2.ResetAllChildren();
		m_Dialog2.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}

	private IEnumerator Dialog3_MoveIn()
	{
		yield return new WaitForSeconds(1.5f);
		m_Dialog3.ResetAllChildren();
		m_Dialog3.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}

	private IEnumerator Dialog4_MoveIn()
	{
		yield return new WaitForSeconds(1.5f);
		m_Dialog4.ResetAllChildren();
		m_Dialog4.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}
}
