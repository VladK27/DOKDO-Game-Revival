using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class GA_FREE_JS_Demo03 : MonoBehaviour
{
	[Serializable]
	internal sealed class _0024MoveInTitleGameObjects_002447 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo03 _0024self__002448;

			public _0024(GA_FREE_JS_Demo03 self_)
			{
				_0024self__002448 = self_;
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
					_0024self__002448.m_Title1.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__002448.m_Title2.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__002448.StartCoroutine(_0024self__002448.ShowDialogs());
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo03 _0024self__002449;

		public _0024MoveInTitleGameObjects_002447(GA_FREE_JS_Demo03 self_)
		{
			_0024self__002449 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002449);
		}
	}

	[Serializable]
	internal sealed class _0024ShowDialogs_002450 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo03 _0024self__002451;

			public _0024(GA_FREE_JS_Demo03 self_)
			{
				_0024self__002451 = self_;
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
					_0024self__002451.m_Dialog1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__002451.m_Dialog2.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__002451.m_Dialog3.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__002451.m_Dialog4.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__002451.StartCoroutine(_0024self__002451.EnableAllDemoButtons());
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo03 _0024self__002452;

		public _0024ShowDialogs_002450(GA_FREE_JS_Demo03 self_)
		{
			_0024self__002452 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002452);
		}
	}

	[Serializable]
	internal sealed class _0024HideTitleTextMeshes_002453 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo03 _0024self__002454;

			public _0024(GA_FREE_JS_Demo03 self_)
			{
				_0024self__002454 = self_;
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
					_0024self__002454.m_Title1.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__002454.m_Title2.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__002454.m_TopBar.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__002454.m_BottomBar.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo03 _0024self__002455;

		public _0024HideTitleTextMeshes_002453(GA_FREE_JS_Demo03 self_)
		{
			_0024self__002455 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002455);
		}
	}

	[Serializable]
	internal sealed class _0024EnableAllDemoButtons_002456 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo03 _0024self__002457;

			public _0024(GA_FREE_JS_Demo03 self_)
			{
				_0024self__002457 = self_;
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
					GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(_0024self__002457.m_Canvas, true);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo03 _0024self__002458;

		public _0024EnableAllDemoButtons_002456(GA_FREE_JS_Demo03 self_)
		{
			_0024self__002458 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002458);
		}
	}

	[Serializable]
	internal sealed class _0024DisableButtonForSeconds_002459 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GameObject _0024GO_002460;

			internal float _0024DisableTime_002461;

			public _0024(GameObject GO, float DisableTime)
			{
				_0024GO_002460 = GO;
				_0024DisableTime_002461 = DisableTime;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					GUIAnimSystemFREE.Instance.EnableButton(_0024GO_002460.transform, false);
					result = (Yield(2, new WaitForSeconds(_0024DisableTime_002461)) ? 1 : 0);
					break;
				case 2:
					GUIAnimSystemFREE.Instance.EnableButton(_0024GO_002460.transform, true);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GameObject _0024GO_002462;

		internal float _0024DisableTime_002463;

		public _0024DisableButtonForSeconds_002459(GameObject GO, float DisableTime)
		{
			_0024GO_002462 = GO;
			_0024DisableTime_002463 = DisableTime;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024GO_002462, _0024DisableTime_002463);
		}
	}

	[Serializable]
	internal sealed class _0024Dialog1_MoveIn_002464 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo03 _0024self__002465;

			public _0024(GA_FREE_JS_Demo03 self_)
			{
				_0024self__002465 = self_;
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
					_0024self__002465.m_Dialog1.Reset();
					_0024self__002465.m_Dialog1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo03 _0024self__002466;

		public _0024Dialog1_MoveIn_002464(GA_FREE_JS_Demo03 self_)
		{
			_0024self__002466 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002466);
		}
	}

	[Serializable]
	internal sealed class _0024Dialog2_MoveIn_002467 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo03 _0024self__002468;

			public _0024(GA_FREE_JS_Demo03 self_)
			{
				_0024self__002468 = self_;
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
					_0024self__002468.m_Dialog2.Reset();
					_0024self__002468.m_Dialog2.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo03 _0024self__002469;

		public _0024Dialog2_MoveIn_002467(GA_FREE_JS_Demo03 self_)
		{
			_0024self__002469 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002469);
		}
	}

	[Serializable]
	internal sealed class _0024Dialog3_MoveIn_002470 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo03 _0024self__002471;

			public _0024(GA_FREE_JS_Demo03 self_)
			{
				_0024self__002471 = self_;
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
					_0024self__002471.m_Dialog3.Reset();
					_0024self__002471.m_Dialog3.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo03 _0024self__002472;

		public _0024Dialog3_MoveIn_002470(GA_FREE_JS_Demo03 self_)
		{
			_0024self__002472 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002472);
		}
	}

	[Serializable]
	internal sealed class _0024Dialog4_MoveIn_002473 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo03 _0024self__002474;

			public _0024(GA_FREE_JS_Demo03 self_)
			{
				_0024self__002474 = self_;
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
					_0024self__002474.m_Dialog4.Reset();
					_0024self__002474.m_Dialog4.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo03 _0024self__002475;

		public _0024Dialog4_MoveIn_002473(GA_FREE_JS_Demo03 self_)
		{
			_0024self__002475 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002475);
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
		return new _0024MoveInTitleGameObjects_002447(this).GetEnumerator();
	}

	public virtual IEnumerator ShowDialogs()
	{
		return new _0024ShowDialogs_002450(this).GetEnumerator();
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
		return new _0024HideTitleTextMeshes_002453(this).GetEnumerator();
	}

	public virtual IEnumerator EnableAllDemoButtons()
	{
		return new _0024EnableAllDemoButtons_002456(this).GetEnumerator();
	}

	public virtual IEnumerator DisableButtonForSeconds(GameObject GO, float DisableTime)
	{
		return new _0024DisableButtonForSeconds_002459(GO, DisableTime).GetEnumerator();
	}

	public virtual void OnButton_Dialog1()
	{
		StartCoroutine(DisableButtonForSeconds(m_Dialog1.gameObject, 1f));
		m_Dialog1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(Dialog1_MoveIn());
	}

	public virtual void OnButton_Dialog2()
	{
		StartCoroutine(DisableButtonForSeconds(m_Dialog2.gameObject, 1f));
		m_Dialog2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(Dialog2_MoveIn());
	}

	public virtual void OnButton_Dialog3()
	{
		StartCoroutine(DisableButtonForSeconds(m_Dialog3.gameObject, 1f));
		m_Dialog3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(Dialog3_MoveIn());
	}

	public virtual void OnButton_Dialog4()
	{
		StartCoroutine(DisableButtonForSeconds(m_Dialog4.gameObject, 1f));
		m_Dialog4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(Dialog4_MoveIn());
	}

	public virtual void OnButton_MoveOutAllDialogs()
	{
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
		return new _0024Dialog1_MoveIn_002464(this).GetEnumerator();
	}

	public virtual IEnumerator Dialog2_MoveIn()
	{
		return new _0024Dialog2_MoveIn_002467(this).GetEnumerator();
	}

	public virtual IEnumerator Dialog3_MoveIn()
	{
		return new _0024Dialog3_MoveIn_002470(this).GetEnumerator();
	}

	public virtual IEnumerator Dialog4_MoveIn()
	{
		return new _0024Dialog4_MoveIn_002473(this).GetEnumerator();
	}
}
