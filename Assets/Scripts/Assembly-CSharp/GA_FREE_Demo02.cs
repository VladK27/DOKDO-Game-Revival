using System.Collections;
using UnityEngine;

public class GA_FREE_Demo02 : MonoBehaviour
{
	public Canvas m_Canvas;

	public GUIAnimFREE m_Title1;

	public GUIAnimFREE m_Title2;

	public GUIAnimFREE m_TopBar;

	public GUIAnimFREE m_BottomBar;

	public GUIAnimFREE m_Dialog;

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
		StartCoroutine(ShowDialog());
	}

	private IEnumerator ShowDialog()
	{
		yield return new WaitForSeconds(1f);
		m_Dialog.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(EnableAllDemoButtons());
	}

	public void HideAllGUIs()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
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

	public void OnButton_UpperEdge()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.UpperScreenEdge));
	}

	public void OnButton_LeftEdge()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.LeftScreenEdge));
	}

	public void OnButton_RightEdge()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableAllButtonsForSeconds(2f));
		StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.RightScreenEdge));
	}

	public void OnButton_BottomEdge()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableAllButtonsForSeconds(2f));
		StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.BottomScreenEdge));
	}

	public void OnButton_UpperLeft()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableAllButtonsForSeconds(2f));
		StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.UpperLeft));
	}

	public void OnButton_UpperRight()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableAllButtonsForSeconds(2f));
		StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.UpperRight));
	}

	public void OnButton_BottomLeft()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableAllButtonsForSeconds(2f));
		StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.BottomLeft));
	}

	public void OnButton_BottomRight()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableAllButtonsForSeconds(2f));
		StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.BottomRight));
	}

	public void OnButton_Center()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableAllButtonsForSeconds(2f));
		StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.MiddleCenter));
	}

	private IEnumerator DialogMoveIn(GUIAnimFREE.ePosMove PosMoveIn)
	{
		yield return new WaitForSeconds(1.5f);
		switch (PosMoveIn)
		{
		case GUIAnimFREE.ePosMove.UpperScreenEdge:
			m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.UpperScreenEdge;
			m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
			break;
		case GUIAnimFREE.ePosMove.LeftScreenEdge:
			m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.LeftScreenEdge;
			m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
			break;
		case GUIAnimFREE.ePosMove.RightScreenEdge:
			m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.RightScreenEdge;
			m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
			break;
		case GUIAnimFREE.ePosMove.BottomScreenEdge:
			m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.BottomScreenEdge;
			m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
			break;
		case GUIAnimFREE.ePosMove.UpperLeft:
			m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.UpperLeft;
			m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
			break;
		case GUIAnimFREE.ePosMove.UpperRight:
			m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.UpperRight;
			m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
			break;
		case GUIAnimFREE.ePosMove.BottomLeft:
			m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.BottomLeft;
			m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
			break;
		case GUIAnimFREE.ePosMove.BottomRight:
			m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.BottomRight;
			m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
			break;
		default:
			m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.MiddleCenter;
			m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
			break;
		}
		m_Dialog.Reset();
		m_Dialog.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}
}
