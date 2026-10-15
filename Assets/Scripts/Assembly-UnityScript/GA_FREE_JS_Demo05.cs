using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class GA_FREE_JS_Demo05 : MonoBehaviour
{
	[Serializable]
	internal sealed class _0024MoveInTitleGameObjects_0024105 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo05 _0024self__0024106;

			public _0024(GA_FREE_JS_Demo05 self_)
			{
				_0024self__0024106 = self_;
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
					_0024self__0024106.m_Title1.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__0024106.m_Title2.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__0024106.StartCoroutine(_0024self__0024106.MoveInPrimaryButtons());
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo05 _0024self__0024107;

		public _0024MoveInTitleGameObjects_0024105(GA_FREE_JS_Demo05 self_)
		{
			_0024self__0024107 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024107);
		}
	}

	[Serializable]
	internal sealed class _0024MoveInPrimaryButtons_0024108 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo05 _0024self__0024109;

			public _0024(GA_FREE_JS_Demo05 self_)
			{
				_0024self__0024109 = self_;
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
					_0024self__0024109.m_Dialog1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__0024109.m_Dialog2.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__0024109.m_Dialog3.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__0024109.m_Dialog4.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__0024109.StartCoroutine(_0024self__0024109.EnableAllDemoButtons());
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo05 _0024self__0024110;

		public _0024MoveInPrimaryButtons_0024108(GA_FREE_JS_Demo05 self_)
		{
			_0024self__0024110 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024110);
		}
	}

	[Serializable]
	internal sealed class _0024HideTitleTextMeshes_0024111 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo05 _0024self__0024112;

			public _0024(GA_FREE_JS_Demo05 self_)
			{
				_0024self__0024112 = self_;
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
					_0024self__0024112.m_Title1.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__0024112.m_Title2.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__0024112.m_TopBar.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__0024112.m_BottomBar.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo05 _0024self__0024113;

		public _0024HideTitleTextMeshes_0024111(GA_FREE_JS_Demo05 self_)
		{
			_0024self__0024113 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024113);
		}
	}

	[Serializable]
	internal sealed class _0024EnableAllDemoButtons_0024114 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo05 _0024self__0024115;

			public _0024(GA_FREE_JS_Demo05 self_)
			{
				_0024self__0024115 = self_;
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
					GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(_0024self__0024115.m_Canvas, true);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo05 _0024self__0024116;

		public _0024EnableAllDemoButtons_0024114(GA_FREE_JS_Demo05 self_)
		{
			_0024self__0024116 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024116);
		}
	}

	[Serializable]
	internal sealed class _0024DisableButtonForSeconds_0024117 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GameObject _0024GO_0024118;

			internal float _0024DisableTime_0024119;

			public _0024(GameObject GO, float DisableTime)
			{
				_0024GO_0024118 = GO;
				_0024DisableTime_0024119 = DisableTime;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					GUIAnimSystemFREE.Instance.EnableButton(_0024GO_0024118.transform, false);
					result = (Yield(2, new WaitForSeconds(_0024DisableTime_0024119)) ? 1 : 0);
					break;
				case 2:
					GUIAnimSystemFREE.Instance.EnableButton(_0024GO_0024118.transform, true);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GameObject _0024GO_0024120;

		internal float _0024DisableTime_0024121;

		public _0024DisableButtonForSeconds_0024117(GameObject GO, float DisableTime)
		{
			_0024GO_0024120 = GO;
			_0024DisableTime_0024121 = DisableTime;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024GO_0024120, _0024DisableTime_0024121);
		}
	}

	[Serializable]
	internal sealed class _0024Dialog1_MoveIn_0024122 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo05 _0024self__0024123;

			public _0024(GA_FREE_JS_Demo05 self_)
			{
				_0024self__0024123 = self_;
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
					_0024self__0024123.m_Dialog1.ResetAllChildren();
					_0024self__0024123.m_Dialog1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo05 _0024self__0024124;

		public _0024Dialog1_MoveIn_0024122(GA_FREE_JS_Demo05 self_)
		{
			_0024self__0024124 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024124);
		}
	}

	[Serializable]
	internal sealed class _0024Dialog2_MoveIn_0024125 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo05 _0024self__0024126;

			public _0024(GA_FREE_JS_Demo05 self_)
			{
				_0024self__0024126 = self_;
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
					_0024self__0024126.m_Dialog2.ResetAllChildren();
					_0024self__0024126.m_Dialog2.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo05 _0024self__0024127;

		public _0024Dialog2_MoveIn_0024125(GA_FREE_JS_Demo05 self_)
		{
			_0024self__0024127 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024127);
		}
	}

	[Serializable]
	internal sealed class _0024Dialog3_MoveIn_0024128 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo05 _0024self__0024129;

			public _0024(GA_FREE_JS_Demo05 self_)
			{
				_0024self__0024129 = self_;
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
					_0024self__0024129.m_Dialog3.ResetAllChildren();
					_0024self__0024129.m_Dialog3.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo05 _0024self__0024130;

		public _0024Dialog3_MoveIn_0024128(GA_FREE_JS_Demo05 self_)
		{
			_0024self__0024130 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024130);
		}
	}

	[Serializable]
	internal sealed class _0024Dialog4_MoveIn_0024131 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo05 _0024self__0024132;

			public _0024(GA_FREE_JS_Demo05 self_)
			{
				_0024self__0024132 = self_;
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
					_0024self__0024132.m_Dialog4.ResetAllChildren();
					_0024self__0024132.m_Dialog4.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo05 _0024self__0024133;

		public _0024Dialog4_MoveIn_0024131(GA_FREE_JS_Demo05 self_)
		{
			_0024self__0024133 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024133);
		}
	}

	public Canvas m_Canvas;

	public GUIAnimFREE m_Title1;

	public GUIAnimFREE m_Title2;

	public GUIAnimFREE m_TopBar;

	public GUIAnimFREE m_BottomBar;

	public GUIAnimFREE m_Dialog1;

	public GUIAnimFREE m_Dialog2;

	public GUIAnimFREE m_Dialog3;

	public GUIAnimFREE m_Dialog4;

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
		return new _0024MoveInTitleGameObjects_0024105(this).GetEnumerator();
	}

	public virtual IEnumerator MoveInPrimaryButtons()
	{
		return new _0024MoveInPrimaryButtons_0024108(this).GetEnumerator();
	}

	public virtual void HideAllGUIs()
	{
		m_Dialog1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Dialog2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Dialog3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Dialog4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(HideTitleTextMeshes());
	}

	public virtual IEnumerator HideTitleTextMeshes()
	{
		return new _0024HideTitleTextMeshes_0024111(this).GetEnumerator();
	}

	public virtual IEnumerator EnableAllDemoButtons()
	{
		return new _0024EnableAllDemoButtons_0024114(this).GetEnumerator();
	}

	public virtual IEnumerator DisableButtonForSeconds(GameObject GO, float DisableTime)
	{
		return new _0024DisableButtonForSeconds_0024117(GO, DisableTime).GetEnumerator();
	}

	public virtual void OnButton_Dialog1()
	{
		m_Dialog1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableButtonForSeconds(m_Dialog1.gameObject, 2.5f));
		StartCoroutine(Dialog1_MoveIn());
	}

	public virtual void OnButton_Dialog2()
	{
		m_Dialog2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableButtonForSeconds(m_Dialog2.gameObject, 2.5f));
		StartCoroutine(Dialog2_MoveIn());
	}

	public virtual void OnButton_Dialog3()
	{
		m_Dialog3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableButtonForSeconds(m_Dialog3.gameObject, 2.5f));
		StartCoroutine(Dialog3_MoveIn());
	}

	public virtual void OnButton_Dialog4()
	{
		m_Dialog4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableButtonForSeconds(m_Dialog4.gameObject, 2.5f));
		StartCoroutine(Dialog4_MoveIn());
	}

	public virtual void OnButton_MoveOutAllDialogs()
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

	public virtual IEnumerator Dialog1_MoveIn()
	{
		return new _0024Dialog1_MoveIn_0024122(this).GetEnumerator();
	}

	public virtual IEnumerator Dialog2_MoveIn()
	{
		return new _0024Dialog2_MoveIn_0024125(this).GetEnumerator();
	}

	public virtual IEnumerator Dialog3_MoveIn()
	{
		return new _0024Dialog3_MoveIn_0024128(this).GetEnumerator();
	}

	public virtual IEnumerator Dialog4_MoveIn()
	{
		return new _0024Dialog4_MoveIn_0024131(this).GetEnumerator();
	}
}
