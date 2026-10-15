using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class GA_FREE_JS_Demo07 : MonoBehaviour
{
	[Serializable]
	internal sealed class _0024MoveInTitleGameObjects_0024149 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo07 _0024self__0024150;

			public _0024(GA_FREE_JS_Demo07 self_)
			{
				_0024self__0024150 = self_;
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
					_0024self__0024150.m_Title1.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__0024150.m_Title2.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__0024150.StartCoroutine(_0024self__0024150.MoveInPrimaryButtons());
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo07 _0024self__0024151;

		public _0024MoveInTitleGameObjects_0024149(GA_FREE_JS_Demo07 self_)
		{
			_0024self__0024151 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024151);
		}
	}

	[Serializable]
	internal sealed class _0024MoveInPrimaryButtons_0024152 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo07 _0024self__0024153;

			public _0024(GA_FREE_JS_Demo07 self_)
			{
				_0024self__0024153 = self_;
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
					_0024self__0024153.m_Dialog.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__0024153.m_DialogButtons.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__0024153.m_Button1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__0024153.m_Button2.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__0024153.m_Button3.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__0024153.m_Button4.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__0024153.StartCoroutine(_0024self__0024153.EnableAllDemoButtons());
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo07 _0024self__0024154;

		public _0024MoveInPrimaryButtons_0024152(GA_FREE_JS_Demo07 self_)
		{
			_0024self__0024154 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024154);
		}
	}

	[Serializable]
	internal sealed class _0024HideTitleTextMeshes_0024155 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo07 _0024self__0024156;

			public _0024(GA_FREE_JS_Demo07 self_)
			{
				_0024self__0024156 = self_;
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
					_0024self__0024156.m_Title1.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__0024156.m_Title2.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__0024156.m_TopBar.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__0024156.m_BottomBar.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo07 _0024self__0024157;

		public _0024HideTitleTextMeshes_0024155(GA_FREE_JS_Demo07 self_)
		{
			_0024self__0024157 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024157);
		}
	}

	[Serializable]
	internal sealed class _0024EnableAllDemoButtons_0024158 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo07 _0024self__0024159;

			public _0024(GA_FREE_JS_Demo07 self_)
			{
				_0024self__0024159 = self_;
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
					GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(_0024self__0024159.m_Canvas, true);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo07 _0024self__0024160;

		public _0024EnableAllDemoButtons_0024158(GA_FREE_JS_Demo07 self_)
		{
			_0024self__0024160 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024160);
		}
	}

	[Serializable]
	internal sealed class _0024DisableButtonForSeconds_0024161 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GameObject _0024GO_0024162;

			internal float _0024DisableTime_0024163;

			public _0024(GameObject GO, float DisableTime)
			{
				_0024GO_0024162 = GO;
				_0024DisableTime_0024163 = DisableTime;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					GUIAnimSystemFREE.Instance.EnableButton(_0024GO_0024162.transform, false);
					result = (Yield(2, new WaitForSeconds(_0024DisableTime_0024163)) ? 1 : 0);
					break;
				case 2:
					GUIAnimSystemFREE.Instance.EnableButton(_0024GO_0024162.transform, true);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GameObject _0024GO_0024164;

		internal float _0024DisableTime_0024165;

		public _0024DisableButtonForSeconds_0024161(GameObject GO, float DisableTime)
		{
			_0024GO_0024164 = GO;
			_0024DisableTime_0024165 = DisableTime;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024GO_0024164, _0024DisableTime_0024165);
		}
	}

	[Serializable]
	internal sealed class _0024SetButtonMove_0024166 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GUIAnimFREE.ePosMove _0024PosMoveIn_0024167;

			internal GA_FREE_JS_Demo07 _0024self__0024168;

			public _0024(GUIAnimFREE.ePosMove PosMoveIn, GA_FREE_JS_Demo07 self_)
			{
				_0024PosMoveIn_0024167 = PosMoveIn;
				_0024self__0024168 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					result = (Yield(2, new WaitForSeconds(2f)) ? 1 : 0);
					break;
				case 2:
					_0024self__0024168.m_Button1.m_MoveIn.MoveFrom = _0024PosMoveIn_0024167;
					_0024self__0024168.m_Button1.Reset();
					_0024self__0024168.m_Button1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__0024168.m_Button2.m_MoveIn.MoveFrom = _0024PosMoveIn_0024167;
					_0024self__0024168.m_Button2.Reset();
					_0024self__0024168.m_Button2.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__0024168.m_Button3.m_MoveIn.MoveFrom = _0024PosMoveIn_0024167;
					_0024self__0024168.m_Button3.Reset();
					_0024self__0024168.m_Button3.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__0024168.m_Button4.m_MoveIn.MoveFrom = _0024PosMoveIn_0024167;
					_0024self__0024168.m_Button4.Reset();
					_0024self__0024168.m_Button4.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GUIAnimFREE.ePosMove _0024PosMoveIn_0024169;

		internal GA_FREE_JS_Demo07 _0024self__0024170;

		public _0024SetButtonMove_0024166(GUIAnimFREE.ePosMove PosMoveIn, GA_FREE_JS_Demo07 self_)
		{
			_0024PosMoveIn_0024169 = PosMoveIn;
			_0024self__0024170 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024PosMoveIn_0024169, _0024self__0024170);
		}
	}

	[Serializable]
	internal sealed class _0024DialogMoveIn_0024171 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo07 _0024self__0024172;

			public _0024(GA_FREE_JS_Demo07 self_)
			{
				_0024self__0024172 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					result = (Yield(2, new WaitForSeconds(1.5f)) ? 1 : 0);
					break;
				case 2:
					_0024self__0024172.m_Dialog.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__0024172.m_DialogButtons.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo07 _0024self__0024173;

		public _0024DialogMoveIn_0024171(GA_FREE_JS_Demo07 self_)
		{
			_0024self__0024173 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024173);
		}
	}

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
		return new _0024MoveInTitleGameObjects_0024149(this).GetEnumerator();
	}

	public virtual IEnumerator MoveInPrimaryButtons()
	{
		return new _0024MoveInPrimaryButtons_0024152(this).GetEnumerator();
	}

	public virtual void HideAllGUIs()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_DialogButtons.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Button1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Button2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Button3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Button4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(HideTitleTextMeshes());
	}

	public virtual IEnumerator HideTitleTextMeshes()
	{
		return new _0024HideTitleTextMeshes_0024155(this).GetEnumerator();
	}

	public virtual IEnumerator EnableAllDemoButtons()
	{
		return new _0024EnableAllDemoButtons_0024158(this).GetEnumerator();
	}

	public virtual IEnumerator DisableButtonForSeconds(GameObject GO, float DisableTime)
	{
		return new _0024DisableButtonForSeconds_0024161(GO, DisableTime).GetEnumerator();
	}

	public virtual void OnButton_1()
	{
		MoveButtonsOut();
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 2f));
		StartCoroutine(SetButtonMove(GUIAnimFREE.ePosMove.UpperScreenEdge, GUIAnimFREE.ePosMove.UpperScreenEdge));
	}

	public virtual void OnButton_2()
	{
		MoveButtonsOut();
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 2f));
		StartCoroutine(SetButtonMove(GUIAnimFREE.ePosMove.LeftScreenEdge, GUIAnimFREE.ePosMove.LeftScreenEdge));
	}

	public virtual void OnButton_3()
	{
		MoveButtonsOut();
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 2f));
		StartCoroutine(SetButtonMove(GUIAnimFREE.ePosMove.RightScreenEdge, GUIAnimFREE.ePosMove.RightScreenEdge));
	}

	public virtual void OnButton_4()
	{
		MoveButtonsOut();
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 2f));
		StartCoroutine(SetButtonMove(GUIAnimFREE.ePosMove.BottomScreenEdge, GUIAnimFREE.ePosMove.BottomScreenEdge));
	}

	public virtual void OnDialogButton()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_DialogButtons.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableButtonForSeconds(m_DialogButtons.gameObject, 2f));
		StartCoroutine(DialogMoveIn());
	}

	public virtual void MoveButtonsOut()
	{
		m_Button1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Button2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Button3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Button4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}

	public virtual IEnumerator SetButtonMove(GUIAnimFREE.ePosMove PosMoveIn, GUIAnimFREE.ePosMove PosMoveOut)
	{
		return new _0024SetButtonMove_0024166(PosMoveIn, this).GetEnumerator();
	}

	public virtual IEnumerator DialogMoveIn()
	{
		return new _0024DialogMoveIn_0024171(this).GetEnumerator();
	}
}
