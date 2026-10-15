using UnityEngine;
using UnityEngine.UI;

public class GE_OrbitCameraUI : MonoBehaviour
{
	private Toggle m_ToggleYaw;

	private Toggle m_TogglePitch;

	private Toggle m_ToggleZoom;

	private Toggle m_ToggleHelp;

	private Toggle m_ToggleDetails;

	private Button m_PinchZoom;

	private Button m_VScrollZoom;

	private GUIAnimFREE m_PanelSettings;

	private GUIAnimFREE m_ButtonSettings;

	private GUIAnimFREE m_PanelHelp1;

	private GUIAnimFREE m_PanelHelp2;

	private GUIAnimFREE m_PanelDetails;

	private GE_OrbitCamera m_GE_OrbitCamera;

	private void Start()
	{
		if (base.enabled)
		{
			GUIAnimSystemFREE.Instance.m_GUISpeed = 1f;
			GUIAnimSystemFREE.Instance.m_AutoAnimation = false;
		}
		m_GE_OrbitCamera = Object.FindObjectOfType<GE_OrbitCamera>();
		GameObject gameObject = GameObject.Find("Toggle Invert X");
		if (gameObject != null)
		{
			m_ToggleYaw = gameObject.GetComponent<Toggle>();
		}
		gameObject = GameObject.Find("Toggle Invert Y");
		if (gameObject != null)
		{
			m_TogglePitch = gameObject.GetComponent<Toggle>();
		}
		gameObject = GameObject.Find("Toggle Invert Zoom");
		if (gameObject != null)
		{
			m_ToggleZoom = gameObject.GetComponent<Toggle>();
		}
		gameObject = GameObject.Find("Toggle Help");
		if (gameObject != null)
		{
			m_ToggleHelp = gameObject.GetComponent<Toggle>();
		}
		gameObject = GameObject.Find("Toggle Details");
		if (gameObject != null)
		{
			m_ToggleDetails = gameObject.GetComponent<Toggle>();
		}
		gameObject = GameObject.Find("Button Pinch Zoom");
		if (gameObject != null)
		{
			m_PinchZoom = gameObject.GetComponent<Button>();
		}
		gameObject = GameObject.Find("Button V-Scroll Zoom");
		if (gameObject != null)
		{
			m_VScrollZoom = gameObject.GetComponent<Button>();
		}
		gameObject = GameObject.Find("Panel Settings");
		if (gameObject != null)
		{
			m_PanelSettings = gameObject.GetComponent<GUIAnimFREE>();
		}
		gameObject = GameObject.Find("Button Settings");
		if (gameObject != null)
		{
			m_ButtonSettings = gameObject.GetComponent<GUIAnimFREE>();
		}
		if (m_ButtonSettings != null)
		{
			m_ButtonSettings.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
		}
		gameObject = GameObject.Find("Panel Help1");
		if (gameObject != null)
		{
			m_PanelHelp1 = gameObject.GetComponent<GUIAnimFREE>();
		}
		gameObject = GameObject.Find("Panel Help2");
		if (gameObject != null)
		{
			m_PanelHelp2 = gameObject.GetComponent<GUIAnimFREE>();
		}
		gameObject = GameObject.Find("Panel Details");
		if (gameObject != null)
		{
			m_PanelDetails = gameObject.GetComponent<GUIAnimFREE>();
		}
		if (m_ToggleHelp != null && m_ToggleHelp.isOn)
		{
			if (m_PanelHelp1 != null)
			{
				m_PanelHelp1.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
			}
			if (m_PanelHelp2 != null)
			{
				m_PanelHelp2.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
			}
		}
		if (m_ToggleDetails != null && m_PanelDetails != null && m_ToggleDetails.isOn && m_PanelDetails != null)
		{
			m_PanelDetails.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
		}
		if (m_GE_OrbitCamera != null)
		{
			if (m_ToggleYaw != null)
			{
				m_ToggleYaw.isOn = m_GE_OrbitCamera.m_XInvert;
			}
			if (m_TogglePitch != null)
			{
				m_TogglePitch.isOn = m_GE_OrbitCamera.m_YInvert;
			}
			if (m_ToggleZoom != null)
			{
				m_ToggleZoom.isOn = m_GE_OrbitCamera.m_ZoomInvert;
			}
		}
		if (m_ToggleHelp != null)
		{
			m_ToggleHelp.isOn = true;
		}
		if (m_ToggleDetails != null)
		{
			m_ToggleDetails.isOn = true;
		}
		if (m_PinchZoom != null)
		{
			m_PinchZoom.interactable = false;
		}
		if (m_VScrollZoom != null)
		{
			m_VScrollZoom.interactable = true;
		}
	}

	private void Update()
	{
	}

	public void OnToggle_InvertX()
	{
		if (m_ToggleYaw != null && m_GE_OrbitCamera != null)
		{
			m_GE_OrbitCamera.m_XInvert = m_ToggleYaw.isOn;
		}
	}

	public void OnToggle_InvertY()
	{
		if (m_TogglePitch != null && m_GE_OrbitCamera != null)
		{
			m_GE_OrbitCamera.m_YInvert = m_TogglePitch.isOn;
		}
	}

	public void OnToggle_InvertZoom()
	{
		if (m_ToggleZoom != null && m_GE_OrbitCamera != null)
		{
			m_GE_OrbitCamera.m_ZoomInvert = m_ToggleZoom.isOn;
		}
	}

	public void OnToggle_Help()
	{
		if (!(m_ToggleHelp != null))
		{
			return;
		}
		if (m_ToggleHelp.isOn)
		{
			if (m_PanelHelp1 != null)
			{
				m_PanelHelp1.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
			}
			if (m_PanelHelp2 != null)
			{
				m_PanelHelp2.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
			}
		}
		else
		{
			if (m_PanelHelp1 != null)
			{
				m_PanelHelp1.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
			}
			if (m_PanelHelp2 != null)
			{
				m_PanelHelp2.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
			}
		}
	}

	public void OnToggle_Details()
	{
		if (m_ToggleDetails != null && m_PanelDetails != null)
		{
			if (m_ToggleDetails.isOn)
			{
				m_PanelDetails.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
			}
			else
			{
				m_PanelDetails.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
			}
		}
	}

	public void OnButton_PinchZoom()
	{
		if (m_PinchZoom != null)
		{
			m_PinchZoom.interactable = !m_PinchZoom.interactable;
		}
		if (m_VScrollZoom != null)
		{
			m_VScrollZoom.interactable = !m_VScrollZoom.interactable;
		}
	}

	public void OnButton_VScrollZoom()
	{
		if (m_PinchZoom != null)
		{
			m_PinchZoom.interactable = !m_PinchZoom.interactable;
		}
		if (m_VScrollZoom != null)
		{
			m_VScrollZoom.interactable = !m_VScrollZoom.interactable;
		}
	}

	public void OnButton_Settings()
	{
		if (m_PanelSettings != null)
		{
			if (m_PanelSettings.transform.localScale == new Vector3(0f, 0f, 0f))
			{
				m_PanelSettings.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
			}
			else
			{
				m_PanelSettings.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
			}
		}
	}
}
