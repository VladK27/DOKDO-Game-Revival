using UnityEngine;

public class GUIAnimatorFREEDemo : MonoBehaviour
{
	private float m_WaitTime = 4f;

	private float m_WaitTimeCount;

	private bool m_ShowMoveInButton = true;

	private void Awake()
	{
		if (base.enabled)
		{
			GUIAnimSystemFREE.Instance.m_GUISpeed = 1f;
			GUIAnimSystemFREE.Instance.m_AutoAnimation = false;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (m_WaitTimeCount > 0f && m_WaitTimeCount <= m_WaitTime)
		{
			m_WaitTimeCount -= Time.deltaTime;
			if (m_WaitTimeCount <= 0f)
			{
				m_WaitTimeCount = 0f;
				m_ShowMoveInButton = !m_ShowMoveInButton;
			}
		}
	}

	private void OnGUI()
	{
		if (!(m_WaitTimeCount <= 0f))
		{
			return;
		}
		Rect position = new Rect((Screen.width - 100) / 2, (Screen.height - 50) / 2, 250f, 50f);
		if (m_ShowMoveInButton)
		{
			if (GUI.Button(position, "Play In-animations then Idle-animations"))
			{
				GUIAnimSystemFREE.Instance.MoveIn(base.transform, true);
				m_WaitTimeCount = m_WaitTime;
			}
		}
		else if (GUI.Button(position, "Play Out-animations"))
		{
			GUIAnimSystemFREE.Instance.MoveOut(base.transform, true);
			m_WaitTimeCount = m_WaitTime;
		}
	}
}
