using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class GA_FREE_JS_Demo04 : MonoBehaviour
{
	[Serializable]
	internal sealed class _0024MoveInTitleGameObjects_002476 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo04 _0024self__002477;

			public _0024(GA_FREE_JS_Demo04 self_)
			{
				_0024self__002477 = self_;
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
					_0024self__002477.m_Title1.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__002477.m_Title2.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__002477.StartCoroutine(_0024self__002477.MoveInPrimaryButtons());
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo04 _0024self__002478;

		public _0024MoveInTitleGameObjects_002476(GA_FREE_JS_Demo04 self_)
		{
			_0024self__002478 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002478);
		}
	}

	[Serializable]
	internal sealed class _0024MoveInPrimaryButtons_002479 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo04 _0024self__002480;

			public _0024(GA_FREE_JS_Demo04 self_)
			{
				_0024self__002480 = self_;
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
					_0024self__002480.m_Dialog1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__002480.m_Dialog2.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__002480.m_Dialog3.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__002480.m_Dialog4.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__002480.StartCoroutine(_0024self__002480.EnableAllDemoButtons());
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo04 _0024self__002481;

		public _0024MoveInPrimaryButtons_002479(GA_FREE_JS_Demo04 self_)
		{
			_0024self__002481 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002481);
		}
	}

	[Serializable]
	internal sealed class _0024HideTitleTextMeshes_002482 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo04 _0024self__002483;

			public _0024(GA_FREE_JS_Demo04 self_)
			{
				_0024self__002483 = self_;
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
					_0024self__002483.m_Title1.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__002483.m_Title2.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__002483.m_TopBar.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__002483.m_BottomBar.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo04 _0024self__002484;

		public _0024HideTitleTextMeshes_002482(GA_FREE_JS_Demo04 self_)
		{
			_0024self__002484 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002484);
		}
	}

	[Serializable]
	internal sealed class _0024EnableAllDemoButtons_002485 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo04 _0024self__002486;

			public _0024(GA_FREE_JS_Demo04 self_)
			{
				_0024self__002486 = self_;
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
					GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(_0024self__002486.m_Canvas, true);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo04 _0024self__002487;

		public _0024EnableAllDemoButtons_002485(GA_FREE_JS_Demo04 self_)
		{
			_0024self__002487 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002487);
		}
	}

	[Serializable]
	internal sealed class _0024DisableButtonForSeconds_002488 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GameObject _0024GO_002489;

			internal float _0024DisableTime_002490;

			public _0024(GameObject GO, float DisableTime)
			{
				_0024GO_002489 = GO;
				_0024DisableTime_002490 = DisableTime;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					GUIAnimSystemFREE.Instance.EnableButton(_0024GO_002489.transform, false);
					result = (Yield(2, new WaitForSeconds(_0024DisableTime_002490)) ? 1 : 0);
					break;
				case 2:
					GUIAnimSystemFREE.Instance.EnableButton(_0024GO_002489.transform, true);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GameObject _0024GO_002491;

		internal float _0024DisableTime_002492;

		public _0024DisableButtonForSeconds_002488(GameObject GO, float DisableTime)
		{
			_0024GO_002491 = GO;
			_0024DisableTime_002492 = DisableTime;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024GO_002491, _0024DisableTime_002492);
		}
	}

	[Serializable]
	internal sealed class _0024Dialog1_MoveIn_002493 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo04 _0024self__002494;

			public _0024(GA_FREE_JS_Demo04 self_)
			{
				_0024self__002494 = self_;
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
					_0024self__002494.m_Dialog1.ResetAllChildren();
					_0024self__002494.m_Dialog1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo04 _0024self__002495;

		public _0024Dialog1_MoveIn_002493(GA_FREE_JS_Demo04 self_)
		{
			_0024self__002495 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002495);
		}
	}

	[Serializable]
	internal sealed class _0024Dialog2_MoveIn_002496 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo04 _0024self__002497;

			public _0024(GA_FREE_JS_Demo04 self_)
			{
				_0024self__002497 = self_;
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
					_0024self__002497.m_Dialog2.ResetAllChildren();
					_0024self__002497.m_Dialog2.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo04 _0024self__002498;

		public _0024Dialog2_MoveIn_002496(GA_FREE_JS_Demo04 self_)
		{
			_0024self__002498 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002498);
		}
	}

	[Serializable]
	internal sealed class _0024Dialog3_MoveIn_002499 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo04 _0024self__0024100;

			public _0024(GA_FREE_JS_Demo04 self_)
			{
				_0024self__0024100 = self_;
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
					_0024self__0024100.m_Dialog3.ResetAllChildren();
					_0024self__0024100.m_Dialog3.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo04 _0024self__0024101;

		public _0024Dialog3_MoveIn_002499(GA_FREE_JS_Demo04 self_)
		{
			_0024self__0024101 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024101);
		}
	}

	[Serializable]
	internal sealed class _0024Dialog4_MoveIn_0024102 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo04 _0024self__0024103;

			public _0024(GA_FREE_JS_Demo04 self_)
			{
				_0024self__0024103 = self_;
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
					_0024self__0024103.m_Dialog4.ResetAllChildren();
					_0024self__0024103.m_Dialog4.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo04 _0024self__0024104;

		public _0024Dialog4_MoveIn_0024102(GA_FREE_JS_Demo04 self_)
		{
			_0024self__0024104 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024104);
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
		return new _0024MoveInTitleGameObjects_002476(this).GetEnumerator();
	}

	public virtual IEnumerator MoveInPrimaryButtons()
	{
		return new _0024MoveInPrimaryButtons_002479(this).GetEnumerator();
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
		return new _0024HideTitleTextMeshes_002482(this).GetEnumerator();
	}

	public virtual IEnumerator EnableAllDemoButtons()
	{
		return new _0024EnableAllDemoButtons_002485(this).GetEnumerator();
	}

	public virtual IEnumerator DisableButtonForSeconds(GameObject GO, float DisableTime)
	{
		return new _0024DisableButtonForSeconds_002488(GO, DisableTime).GetEnumerator();
	}

	public virtual void OnButton_Dialog1()
	{
		m_Dialog1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableButtonForSeconds(m_Dialog1.gameObject, 2f));
		StartCoroutine(Dialog1_MoveIn());
	}

	public virtual void OnButton_Dialog2()
	{
		m_Dialog2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableButtonForSeconds(m_Dialog2.gameObject, 2f));
		StartCoroutine(Dialog2_MoveIn());
	}

	public virtual void OnButton_Dialog3()
	{
		m_Dialog3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableButtonForSeconds(m_Dialog3.gameObject, 2f));
		StartCoroutine(Dialog3_MoveIn());
	}

	public virtual void OnButton_Dialog4()
	{
		m_Dialog4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableButtonForSeconds(m_Dialog4.gameObject, 2f));
		StartCoroutine(Dialog4_MoveIn());
	}

	public virtual void OnButton_MoveOutAllDialogs()
	{
		StartCoroutine(DisableButtonForSeconds(m_Dialog1.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Dialog2.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Dialog3.gameObject, 2f));
		StartCoroutine(DisableButtonForSeconds(m_Dialog4.gameObject, 2f));
		if (!m_Dialog1.m_MoveOut.Began && !m_Dialog1.m_MoveOut.Done)
		{
			m_Dialog1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
			StartCoroutine(Dialog1_MoveIn());
		}
		if (!m_Dialog2.m_MoveOut.Began && !m_Dialog2.m_MoveOut.Done)
		{
			m_Dialog2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
			StartCoroutine(Dialog2_MoveIn());
		}
		if (!m_Dialog3.m_MoveOut.Began && !m_Dialog3.m_MoveOut.Done)
		{
			m_Dialog3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
			StartCoroutine(Dialog3_MoveIn());
		}
		if (!m_Dialog4.m_MoveOut.Began && !m_Dialog4.m_MoveOut.Done)
		{
			m_Dialog4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
			StartCoroutine(Dialog4_MoveIn());
		}
	}

	public virtual IEnumerator Dialog1_MoveIn()
	{
		return new _0024Dialog1_MoveIn_002493(this).GetEnumerator();
	}

	public virtual IEnumerator Dialog2_MoveIn()
	{
		return new _0024Dialog2_MoveIn_002496(this).GetEnumerator();
	}

	public virtual IEnumerator Dialog3_MoveIn()
	{
		return new _0024Dialog3_MoveIn_002499(this).GetEnumerator();
	}

	public virtual IEnumerator Dialog4_MoveIn()
	{
		return new _0024Dialog4_MoveIn_0024102(this).GetEnumerator();
	}
}
