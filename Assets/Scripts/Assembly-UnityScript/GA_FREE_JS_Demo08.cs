using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class GA_FREE_JS_Demo08 : MonoBehaviour
{
	[Serializable]
	internal sealed class _0024MoveInTitleGameObjects_0024174 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo08 _0024self__0024175;

			public _0024(GA_FREE_JS_Demo08 self_)
			{
				_0024self__0024175 = self_;
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
					_0024self__0024175.m_Title1.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__0024175.m_Title2.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__0024175.StartCoroutine(_0024self__0024175.MoveInPrimaryButtons());
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo08 _0024self__0024176;

		public _0024MoveInTitleGameObjects_0024174(GA_FREE_JS_Demo08 self_)
		{
			_0024self__0024176 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024176);
		}
	}

	[Serializable]
	internal sealed class _0024MoveInPrimaryButtons_0024177 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo08 _0024self__0024178;

			public _0024(GA_FREE_JS_Demo08 self_)
			{
				_0024self__0024178 = self_;
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
					_0024self__0024178.m_CenterButtons.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__0024178.StartCoroutine(_0024self__0024178.EnableAllDemoButtons());
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo08 _0024self__0024179;

		public _0024MoveInPrimaryButtons_0024177(GA_FREE_JS_Demo08 self_)
		{
			_0024self__0024179 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024179);
		}
	}

	[Serializable]
	internal sealed class _0024HideTitleTextMeshes_0024180 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo08 _0024self__0024181;

			public _0024(GA_FREE_JS_Demo08 self_)
			{
				_0024self__0024181 = self_;
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
					_0024self__0024181.m_Title1.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__0024181.m_Title2.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__0024181.m_TopBar.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__0024181.m_BottomBar.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo08 _0024self__0024182;

		public _0024HideTitleTextMeshes_0024180(GA_FREE_JS_Demo08 self_)
		{
			_0024self__0024182 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024182);
		}
	}

	[Serializable]
	internal sealed class _0024EnableAllDemoButtons_0024183 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo08 _0024self__0024184;

			public _0024(GA_FREE_JS_Demo08 self_)
			{
				_0024self__0024184 = self_;
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
					GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(_0024self__0024184.m_Canvas, true);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo08 _0024self__0024185;

		public _0024EnableAllDemoButtons_0024183(GA_FREE_JS_Demo08 self_)
		{
			_0024self__0024185 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024185);
		}
	}

	[Serializable]
	internal sealed class _0024DisableButtonForSeconds_0024186 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GameObject _0024GO_0024187;

			internal float _0024DisableTime_0024188;

			public _0024(GameObject GO, float DisableTime)
			{
				_0024GO_0024187 = GO;
				_0024DisableTime_0024188 = DisableTime;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					GUIAnimSystemFREE.Instance.EnableButton(_0024GO_0024187.transform, false);
					result = (Yield(2, new WaitForSeconds(_0024DisableTime_0024188)) ? 1 : 0);
					break;
				case 2:
					GUIAnimSystemFREE.Instance.EnableButton(_0024GO_0024187.transform, true);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GameObject _0024GO_0024189;

		internal float _0024DisableTime_0024190;

		public _0024DisableButtonForSeconds_0024186(GameObject GO, float DisableTime)
		{
			_0024GO_0024189 = GO;
			_0024DisableTime_0024190 = DisableTime;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024GO_0024189, _0024DisableTime_0024190);
		}
	}

	public Canvas m_Canvas;

	public GUIAnimFREE m_Title1;

	public GUIAnimFREE m_Title2;

	public GUIAnimFREE m_TopBar;

	public GUIAnimFREE m_BottomBar;

	public GUIAnimFREE m_CenterButtons;

	public GUIAnimFREE m_Button1;

	public GUIAnimFREE m_Button2;

	public GUIAnimFREE m_Button3;

	public GUIAnimFREE m_Button4;

	public GUIAnimFREE m_Bar1;

	public GUIAnimFREE m_Bar2;

	public GUIAnimFREE m_Bar3;

	public GUIAnimFREE m_Bar4;

	protected bool m_Bar1_IsOn;

	protected bool m_Bar2_IsOn;

	protected bool m_Bar3_IsOn;

	protected bool m_Bar4_IsOn;

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
		return new _0024MoveInTitleGameObjects_0024174(this).GetEnumerator();
	}

	public virtual IEnumerator MoveInPrimaryButtons()
	{
		return new _0024MoveInPrimaryButtons_0024177(this).GetEnumerator();
	}

	public virtual void HideAllGUIs()
	{
		m_CenterButtons.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		if (m_Bar1_IsOn)
		{
			m_Bar1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		if (m_Bar2_IsOn)
		{
			m_Bar2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		if (m_Bar3_IsOn)
		{
			m_Bar3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		if (m_Bar4_IsOn)
		{
			m_Bar4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		StartCoroutine(HideTitleTextMeshes());
	}

	public virtual IEnumerator HideTitleTextMeshes()
	{
		return new _0024HideTitleTextMeshes_0024180(this).GetEnumerator();
	}

	public virtual IEnumerator EnableAllDemoButtons()
	{
		return new _0024EnableAllDemoButtons_0024183(this).GetEnumerator();
	}

	public virtual IEnumerator DisableButtonForSeconds(GameObject GO, float DisableTime)
	{
		return new _0024DisableButtonForSeconds_0024186(GO, DisableTime).GetEnumerator();
	}

	public virtual void OnButton_1()
	{
		ToggleBar1();
		if (m_Bar2_IsOn)
		{
			ToggleBar2();
		}
		if (m_Bar3_IsOn)
		{
			ToggleBar3();
		}
		if (m_Bar4_IsOn)
		{
			ToggleBar4();
		}
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 0.75f));
	}

	public virtual void OnButton_2()
	{
		ToggleBar2();
		if (m_Bar1_IsOn)
		{
			ToggleBar1();
		}
		if (m_Bar3_IsOn)
		{
			ToggleBar3();
		}
		if (m_Bar4_IsOn)
		{
			ToggleBar4();
		}
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 0.75f));
	}

	public virtual void OnButton_3()
	{
		ToggleBar3();
		if (m_Bar1_IsOn)
		{
			ToggleBar1();
		}
		if (m_Bar2_IsOn)
		{
			ToggleBar2();
		}
		if (m_Bar4_IsOn)
		{
			ToggleBar4();
		}
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 0.75f));
	}

	public virtual void OnButton_4()
	{
		ToggleBar4();
		if (m_Bar1_IsOn)
		{
			ToggleBar1();
		}
		if (m_Bar2_IsOn)
		{
			ToggleBar2();
		}
		if (m_Bar3_IsOn)
		{
			ToggleBar3();
		}
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 0.75f));
	}

	public virtual void ToggleBar1()
	{
		m_Bar1_IsOn = !m_Bar1_IsOn;
		if (m_Bar1_IsOn)
		{
			m_Bar1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			m_Bar1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}

	public virtual void ToggleBar2()
	{
		m_Bar2_IsOn = !m_Bar2_IsOn;
		if (m_Bar2_IsOn)
		{
			m_Bar2.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			m_Bar2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}

	public virtual void ToggleBar3()
	{
		m_Bar3_IsOn = !m_Bar3_IsOn;
		if (m_Bar3_IsOn)
		{
			m_Bar3.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			m_Bar3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}

	public virtual void ToggleBar4()
	{
		m_Bar4_IsOn = !m_Bar4_IsOn;
		if (m_Bar4_IsOn)
		{
			m_Bar4.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			m_Bar4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}
}
