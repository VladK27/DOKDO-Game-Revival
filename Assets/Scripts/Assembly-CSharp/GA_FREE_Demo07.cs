using System.Collections;
using UnityEngine;

public class GA_FREE_Demo07 : MonoBehaviour
{
	public Canvas m_Canvas;

	public GUIAnimFREE m_Title1;

	public GUIAnimFREE m_Title2;

	public GUIAnimFREE m_TopBar;

	public GUIAnimFREE m_BottomBar;

	public GUIAnimFREE m_Dialog;

	public GUIAnimFREE m_DialogButtons;

	public GUIAnimFREE m_Button1;

	public GUIAnimFREE m_Button2;

	public GUIAnimFREE m_Button3;

	public GUIAnimFREE m_Button4;

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
		m_Dialog.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_DialogButtons.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Button1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Button2.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Button3.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Button4.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(EnableAllDemoButtons());
	}

	public void HideAllGUIs()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_DialogButtons.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Button1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Button2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Button3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Button4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
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
		MoveButtonsOut();
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 2f));
		StartCoroutine(SetButtonMove(GUIAnimFREE.ePosMove.UpperScreenEdge, GUIAnimFREE.ePosMove.UpperScreenEdge));
	}

	public void OnButton_2()
	{
		MoveButtonsOut();
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 2f));
		StartCoroutine(SetButtonMove(GUIAnimFREE.ePosMove.LeftScreenEdge, GUIAnimFREE.ePosMove.LeftScreenEdge));
	}

	public void OnButton_3()
	{
		MoveButtonsOut();
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 2f));
		StartCoroutine(SetButtonMove(GUIAnimFREE.ePosMove.RightScreenEdge, GUIAnimFREE.ePosMove.RightScreenEdge));
	}

	public void OnButton_4()
	{
		MoveButtonsOut();
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 2f));
		StartCoroutine(SetButtonMove(GUIAnimFREE.ePosMove.BottomScreenEdge, GUIAnimFREE.ePosMove.BottomScreenEdge));
	}

	public void OnDialogButton()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_DialogButtons.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableButtonForSeconds(m_DialogButtons.gameObject, 2f));
		StartCoroutine(DialogMoveIn());
	}

	private void MoveButtonsOut()
	{
		m_Button1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Button2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Button3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Button4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}

	private IEnumerator SetButtonMove(GUIAnimFREE.ePosMove PosMoveIn, GUIAnimFREE.ePosMove PosMoveOut)
	{
		yield return new WaitForSeconds(2f);
		m_Button1.m_MoveIn.MoveFrom = PosMoveIn;
		m_Button1.Reset();
		m_Button1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Button2.m_MoveIn.MoveFrom = PosMoveIn;
		m_Button2.Reset();
		m_Button2.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Button3.m_MoveIn.MoveFrom = PosMoveIn;
		m_Button3.Reset();
		m_Button3.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Button4.m_MoveIn.MoveFrom = PosMoveIn;
		m_Button4.Reset();
		m_Button4.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}

	private IEnumerator DialogMoveIn()
	{
		yield return new WaitForSeconds(1.5f);
		m_Dialog.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_DialogButtons.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}
}
