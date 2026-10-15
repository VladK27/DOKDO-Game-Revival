using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class GA_FREE_JS_Demo06 : MonoBehaviour
{
	[Serializable]
	internal sealed class _0024MoveInTitleGameObjects_0024134 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo06 _0024self__0024135;

			public _0024(GA_FREE_JS_Demo06 self_)
			{
				_0024self__0024135 = self_;
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
					_0024self__0024135.m_Title1.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__0024135.m_Title2.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__0024135.StartCoroutine(_0024self__0024135.MoveInPrimaryButtons());
					GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(_0024self__0024135.m_Canvas, true);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo06 _0024self__0024136;

		public _0024MoveInTitleGameObjects_0024134(GA_FREE_JS_Demo06 self_)
		{
			_0024self__0024136 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024136);
		}
	}

	[Serializable]
	internal sealed class _0024MoveInPrimaryButtons_0024137 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo06 _0024self__0024138;

			public _0024(GA_FREE_JS_Demo06 self_)
			{
				_0024self__0024138 = self_;
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
					_0024self__0024138.m_PrimaryButton1.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__0024138.m_PrimaryButton2.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__0024138.m_PrimaryButton3.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__0024138.m_PrimaryButton4.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__0024138.m_PrimaryButton5.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__0024138.StartCoroutine(_0024self__0024138.EnableAllDemoButtons());
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo06 _0024self__0024139;

		public _0024MoveInPrimaryButtons_0024137(GA_FREE_JS_Demo06 self_)
		{
			_0024self__0024139 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024139);
		}
	}

	[Serializable]
	internal sealed class _0024HideTitleTextMeshes_0024140 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo06 _0024self__0024141;

			public _0024(GA_FREE_JS_Demo06 self_)
			{
				_0024self__0024141 = self_;
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
					_0024self__0024141.m_Title1.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__0024141.m_Title2.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__0024141.m_TopBar.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__0024141.m_BottomBar.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo06 _0024self__0024142;

		public _0024HideTitleTextMeshes_0024140(GA_FREE_JS_Demo06 self_)
		{
			_0024self__0024142 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024142);
		}
	}

	[Serializable]
	internal sealed class _0024EnableAllDemoButtons_0024143 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo06 _0024self__0024144;

			public _0024(GA_FREE_JS_Demo06 self_)
			{
				_0024self__0024144 = self_;
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
					GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(_0024self__0024144.m_Canvas, true);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo06 _0024self__0024145;

		public _0024EnableAllDemoButtons_0024143(GA_FREE_JS_Demo06 self_)
		{
			_0024self__0024145 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024145);
		}
	}

	[Serializable]
	internal sealed class _0024DisableAllButtonsForSeconds_0024146 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal float _0024DisableTime_0024147;

			public _0024(float DisableTime)
			{
				_0024DisableTime_0024147 = DisableTime;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					GUIAnimSystemFREE.Instance.EnableAllButtons(false);
					result = (Yield(2, new WaitForSeconds(_0024DisableTime_0024147)) ? 1 : 0);
					break;
				case 2:
					GUIAnimSystemFREE.Instance.EnableAllButtons(true);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal float _0024DisableTime_0024148;

		public _0024DisableAllButtonsForSeconds_0024146(float DisableTime)
		{
			_0024DisableTime_0024148 = DisableTime;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024DisableTime_0024148);
		}
	}

	public Canvas m_Canvas;

	public GUIAnimFREE m_Title1;

	public GUIAnimFREE m_Title2;

	public GUIAnimFREE m_TopBar;

	public GUIAnimFREE m_BottomBar;

	public GUIAnimFREE m_PrimaryButton1;

	public GUIAnimFREE m_PrimaryButton2;

	public GUIAnimFREE m_PrimaryButton3;

	public GUIAnimFREE m_PrimaryButton4;

	public GUIAnimFREE m_PrimaryButton5;

	public GUIAnimFREE m_SecondaryButton1;

	public GUIAnimFREE m_SecondaryButton2;

	public GUIAnimFREE m_SecondaryButton3;

	public GUIAnimFREE m_SecondaryButton4;

	public GUIAnimFREE m_SecondaryButton5;

	protected bool m_Button1_IsOn;

	protected bool m_Button2_IsOn;

	protected bool m_Button3_IsOn;

	protected bool m_Button4_IsOn;

	protected bool m_Button5_IsOn;

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
		return new _0024MoveInTitleGameObjects_0024134(this).GetEnumerator();
	}

	public virtual IEnumerator MoveInPrimaryButtons()
	{
		return new _0024MoveInPrimaryButtons_0024137(this).GetEnumerator();
	}

	public virtual void HideAllGUIs()
	{
		m_PrimaryButton1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_PrimaryButton2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_PrimaryButton3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_PrimaryButton4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_PrimaryButton5.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		if (m_Button1_IsOn)
		{
			m_SecondaryButton1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		if (m_Button2_IsOn)
		{
			m_SecondaryButton2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		if (m_Button3_IsOn)
		{
			m_SecondaryButton3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		if (m_Button4_IsOn)
		{
			m_SecondaryButton4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		if (m_Button5_IsOn)
		{
			m_SecondaryButton5.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		StartCoroutine(HideTitleTextMeshes());
	}

	public virtual IEnumerator HideTitleTextMeshes()
	{
		return new _0024HideTitleTextMeshes_0024140(this).GetEnumerator();
	}

	public virtual IEnumerator EnableAllDemoButtons()
	{
		return new _0024EnableAllDemoButtons_0024143(this).GetEnumerator();
	}

	public virtual IEnumerator DisableAllButtonsForSeconds(float DisableTime)
	{
		return new _0024DisableAllButtonsForSeconds_0024146(DisableTime).GetEnumerator();
	}

	public virtual void OnButton_1()
	{
		StartCoroutine(DisableAllButtonsForSeconds(0.6f));
		ToggleButton_1();
		if (m_Button2_IsOn)
		{
			ToggleButton_2();
		}
		if (m_Button3_IsOn)
		{
			ToggleButton_3();
		}
		if (m_Button4_IsOn)
		{
			ToggleButton_4();
		}
		if (m_Button5_IsOn)
		{
			ToggleButton_5();
		}
	}

	public virtual void OnButton_2()
	{
		StartCoroutine(DisableAllButtonsForSeconds(0.6f));
		ToggleButton_2();
		if (m_Button1_IsOn)
		{
			ToggleButton_1();
		}
		if (m_Button3_IsOn)
		{
			ToggleButton_3();
		}
		if (m_Button4_IsOn)
		{
			ToggleButton_4();
		}
		if (m_Button5_IsOn)
		{
			ToggleButton_5();
		}
	}

	public virtual void OnButton_3()
	{
		StartCoroutine(DisableAllButtonsForSeconds(0.6f));
		ToggleButton_3();
		if (m_Button1_IsOn)
		{
			ToggleButton_1();
		}
		if (m_Button2_IsOn)
		{
			ToggleButton_2();
		}
		if (m_Button4_IsOn)
		{
			ToggleButton_4();
		}
		if (m_Button5_IsOn)
		{
			ToggleButton_5();
		}
	}

	public virtual void OnButton_4()
	{
		StartCoroutine(DisableAllButtonsForSeconds(0.6f));
		ToggleButton_4();
		if (m_Button1_IsOn)
		{
			ToggleButton_1();
		}
		if (m_Button2_IsOn)
		{
			ToggleButton_2();
		}
		if (m_Button3_IsOn)
		{
			ToggleButton_3();
		}
		if (m_Button5_IsOn)
		{
			ToggleButton_5();
		}
	}

	public virtual void OnButton_5()
	{
		StartCoroutine(DisableAllButtonsForSeconds(0.6f));
		ToggleButton_5();
		if (m_Button1_IsOn)
		{
			ToggleButton_1();
		}
		if (m_Button2_IsOn)
		{
			ToggleButton_2();
		}
		if (m_Button3_IsOn)
		{
			ToggleButton_3();
		}
		if (m_Button4_IsOn)
		{
			ToggleButton_4();
		}
	}

	public virtual void ToggleButton_1()
	{
		m_Button1_IsOn = !m_Button1_IsOn;
		if (m_Button1_IsOn)
		{
			m_SecondaryButton1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			m_SecondaryButton1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}

	public virtual void ToggleButton_2()
	{
		m_Button2_IsOn = !m_Button2_IsOn;
		if (m_Button2_IsOn)
		{
			m_SecondaryButton2.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			m_SecondaryButton2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}

	public virtual void ToggleButton_3()
	{
		m_Button3_IsOn = !m_Button3_IsOn;
		if (m_Button3_IsOn)
		{
			m_SecondaryButton3.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			m_SecondaryButton3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}

	public virtual void ToggleButton_4()
	{
		m_Button4_IsOn = !m_Button4_IsOn;
		if (m_Button4_IsOn)
		{
			m_SecondaryButton4.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			m_SecondaryButton4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}

	public virtual void ToggleButton_5()
	{
		m_Button5_IsOn = !m_Button5_IsOn;
		if (m_Button5_IsOn)
		{
			m_SecondaryButton5.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			m_SecondaryButton5.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}
}
