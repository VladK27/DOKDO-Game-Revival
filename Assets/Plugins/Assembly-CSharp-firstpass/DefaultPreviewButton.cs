using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class DefaultPreviewButton : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action ActionClick__BackingField = () =>
	{
	};

	public Texture normalTexture;

	public Texture pressedTexture;

	public Texture disabledTexture;

	public Texture selectedTexture;

	private Texture normalTex;

	public AudioClip sound;

	public AudioClip disabledsound;

	private bool IsDisabled;

	public string text
	{
		get
		{
			TextMesh componentInChildren = base.gameObject.GetComponentInChildren<TextMesh>();
			return componentInChildren.text;
		}
		set
		{
			TextMesh[] componentsInChildren = base.gameObject.GetComponentsInChildren<TextMesh>();
			TextMesh[] array = componentsInChildren;
			foreach (TextMesh textMesh in array)
			{
				textMesh.text = value;
			}
		}
	}

	public event Action ActionClick
	{
		add
		{
			Action action = ActionClick__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionClick__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ActionClick__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionClick__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		if (GetComponent<AudioSource>() == null)
		{
			base.gameObject.AddComponent<AudioSource>();
			GetComponent<AudioSource>().clip = sound;
			GetComponent<AudioSource>().Stop();
		}
		GetComponent<Renderer>().material = new Material(GetComponent<Renderer>().material);
		normalTex = normalTexture;
	}

	public void Select()
	{
		normalTexture = selectedTexture;
		OnTimeoutPress();
	}

	public void Unselect()
	{
		normalTexture = normalTex;
		OnTimeoutPress();
	}

	public void DisabledButton()
	{
		if (disabledTexture != null)
		{
			GetComponent<Renderer>().material.mainTexture = disabledTexture;
		}
		IsDisabled = true;
	}

	public void EnabledButton()
	{
		if (disabledTexture != null)
		{
			GetComponent<Renderer>().material.mainTexture = normalTexture;
		}
		IsDisabled = false;
	}

	private void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hitInfo, float.PositiveInfinity) && hitInfo.transform.gameObject == base.gameObject)
		{
			OnClick();
		}
	}

	protected virtual void OnClick()
	{
		if (IsDisabled)
		{
			GetComponent<AudioSource>().PlayOneShot(disabledsound);
			return;
		}
		GetComponent<AudioSource>().PlayOneShot(sound);
		GetComponent<Renderer>().material.mainTexture = pressedTexture;
		ActionClick__BackingField();
		CancelInvoke("OnTimeoutPress");
		Invoke("OnTimeoutPress", 0.1f);
	}

	private void OnTimeoutPress()
	{
		GetComponent<Renderer>().material.mainTexture = normalTexture;
	}
}
