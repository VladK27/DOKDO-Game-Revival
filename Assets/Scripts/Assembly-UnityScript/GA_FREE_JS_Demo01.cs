using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class GA_FREE_JS_Demo01 : MonoBehaviour
{
	[Serializable]
	internal sealed class _0024MoveInTitleGameObjects_00249 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo01 _0024self__002410;

			public _0024(GA_FREE_JS_Demo01 self_)
			{
				_0024self__002410 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					result = (Yield(2, new WaitForSeconds(1f)) ? 1 : 0);
					break;
				case 2:
					_0024self__002410.m_Title1.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__002410.m_Title2.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__002410.StartCoroutine(_0024self__002410.MoveInPrimaryButtons());
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo01 _0024self__002411;

		public _0024MoveInTitleGameObjects_00249(GA_FREE_JS_Demo01 self_)
		{
			_0024self__002411 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002411);
		}
	}

	[Serializable]
	internal sealed class _0024MoveInPrimaryButtons_002412 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo01 _0024self__002413;

			public _0024(GA_FREE_JS_Demo01 self_)
			{
				_0024self__002413 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					result = (Yield(2, new WaitForSeconds(1f)) ? 1 : 0);
					break;
				case 2:
					_0024self__002413.m_TopLeft_A.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__002413.m_BottomLeft_A.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__002413.m_RightBar_A.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__002413.StartCoroutine(_0024self__002413.EnableAllDemoButtons());
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo01 _0024self__002414;

		public _0024MoveInPrimaryButtons_002412(GA_FREE_JS_Demo01 self_)
		{
			_0024self__002414 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002414);
		}
	}

	[Serializable]
	internal sealed class _0024HideTitleTextMeshes_002415 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo01 _0024self__002416;

			public _0024(GA_FREE_JS_Demo01 self_)
			{
				_0024self__002416 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					result = (Yield(2, new WaitForSeconds(1f)) ? 1 : 0);
					break;
				case 2:
					_0024self__002416.m_Title1.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__002416.m_Title2.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__002416.m_TopBar.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__002416.m_BottomBar.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo01 _0024self__002417;

		public _0024HideTitleTextMeshes_002415(GA_FREE_JS_Demo01 self_)
		{
			_0024self__002417 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002417);
		}
	}

	[Serializable]
	internal sealed class _0024EnableAllDemoButtons_002418 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo01 _0024self__002419;

			public _0024(GA_FREE_JS_Demo01 self_)
			{
				_0024self__002419 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					result = (Yield(2, new WaitForSeconds(1f)) ? 1 : 0);
					break;
				case 2:
					GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(_0024self__002419.m_Canvas, true);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo01 _0024self__002420;

		public _0024EnableAllDemoButtons_002418(GA_FREE_JS_Demo01 self_)
		{
			_0024self__002420 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002420);
		}
	}

	[Serializable]
	internal sealed class _0024DisableButtonForSeconds_002421 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GameObject _0024GO_002422;

			internal float _0024DisableTime_002423;

			public _0024(GameObject GO, float DisableTime)
			{
				_0024GO_002422 = GO;
				_0024DisableTime_002423 = DisableTime;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					GUIAnimSystemFREE.Instance.EnableButton(_0024GO_002422.transform, false);
					result = (Yield(2, new WaitForSeconds(_0024DisableTime_002423)) ? 1 : 0);
					break;
				case 2:
					GUIAnimSystemFREE.Instance.EnableButton(_0024GO_002422.transform, true);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GameObject _0024GO_002424;

		internal float _0024DisableTime_002425;

		public _0024DisableButtonForSeconds_002421(GameObject GO, float DisableTime)
		{
			_0024GO_002424 = GO;
			_0024DisableTime_002425 = DisableTime;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024GO_002424, _0024DisableTime_002425);
		}
	}

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

	protected bool m_TopLeft_IsOn;

	protected bool m_BottomLeft_IsOn;

	protected bool m_RightBar_IsOn;

	public virtual void Awake()
	{
		if (enabled)
		{
			GUIAnimSystemFREE.Instance.m_AutoAnimation = false;
		}
	}

	public virtual void Start()
	{
		m_TopBar.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_BottomBar.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(MoveInTitleGameObjects());
		GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(m_Canvas, false);
	}

	public virtual void Update()
	{
	}

	public virtual IEnumerator MoveInTitleGameObjects()
	{
		return new _0024MoveInTitleGameObjects_00249(this).GetEnumerator();
	}

	public virtual IEnumerator MoveInPrimaryButtons()
	{
		return new _0024MoveInPrimaryButtons_002412(this).GetEnumerator();
	}

	public virtual void HideAllGUIs()
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

	public virtual IEnumerator HideTitleTextMeshes()
	{
		return new _0024HideTitleTextMeshes_002415(this).GetEnumerator();
	}

	public virtual IEnumerator EnableAllDemoButtons()
	{
		return new _0024EnableAllDemoButtons_002418(this).GetEnumerator();
	}

	public virtual IEnumerator DisableButtonForSeconds(GameObject GO, float DisableTime)
	{
		return new _0024DisableButtonForSeconds_002421(GO, DisableTime).GetEnumerator();
	}

	public virtual void OnButton_TopLeft()
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

	public virtual void OnButton_BottomLeft()
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

	public virtual void OnButton_RightBar()
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

	public virtual void ToggleTopLeft()
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

	public virtual void ToggleBottomLeft()
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

	public virtual void ToggleRightBar()
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
