using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class GA_FREE_JS_Demo02 : MonoBehaviour
{
	[Serializable]
	internal sealed class _0024MoveInTitleGameObjects_002426 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo02 _0024self__002427;

			public _0024(GA_FREE_JS_Demo02 self_)
			{
				_0024self__002427 = self_;
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
					_0024self__002427.m_Title1.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__002427.m_Title2.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__002427.StartCoroutine(_0024self__002427.ShowDialog());
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo02 _0024self__002428;

		public _0024MoveInTitleGameObjects_002426(GA_FREE_JS_Demo02 self_)
		{
			_0024self__002428 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002428);
		}
	}

	[Serializable]
	internal sealed class _0024ShowDialog_002429 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo02 _0024self__002430;

			public _0024(GA_FREE_JS_Demo02 self_)
			{
				_0024self__002430 = self_;
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
					_0024self__002430.m_Dialog.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__002430.StartCoroutine(_0024self__002430.EnableAllDemoButtons());
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo02 _0024self__002431;

		public _0024ShowDialog_002429(GA_FREE_JS_Demo02 self_)
		{
			_0024self__002431 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002431);
		}
	}

	[Serializable]
	internal sealed class _0024HideTitleTextMeshes_002432 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo02 _0024self__002433;

			public _0024(GA_FREE_JS_Demo02 self_)
			{
				_0024self__002433 = self_;
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
					_0024self__002433.m_Title1.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__002433.m_Title2.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
					_0024self__002433.m_TopBar.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					_0024self__002433.m_BottomBar.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo02 _0024self__002434;

		public _0024HideTitleTextMeshes_002432(GA_FREE_JS_Demo02 self_)
		{
			_0024self__002434 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002434);
		}
	}

	[Serializable]
	internal sealed class _0024EnableAllDemoButtons_002435 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GA_FREE_JS_Demo02 _0024self__002436;

			public _0024(GA_FREE_JS_Demo02 self_)
			{
				_0024self__002436 = self_;
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
					GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(_0024self__002436.m_Canvas, true);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GA_FREE_JS_Demo02 _0024self__002437;

		public _0024EnableAllDemoButtons_002435(GA_FREE_JS_Demo02 self_)
		{
			_0024self__002437 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002437);
		}
	}

	[Serializable]
	internal sealed class _0024DisableAllButtonsForSeconds_002438 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal float _0024DisableTime_002439;

			public _0024(float DisableTime)
			{
				_0024DisableTime_002439 = DisableTime;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					GUIAnimSystemFREE.Instance.EnableAllButtons(false);
					result = (Yield(2, new WaitForSeconds(_0024DisableTime_002439)) ? 1 : 0);
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

		internal float _0024DisableTime_002440;

		public _0024DisableAllButtonsForSeconds_002438(float DisableTime)
		{
			_0024DisableTime_002440 = DisableTime;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024DisableTime_002440);
		}
	}

	[Serializable]
	internal sealed class _0024DialogMoveIn_002441 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GUIAnimFREE.ePosMove _0024_0024switch_00243_002442;

			internal GUIAnimFREE.ePosMove _0024PosMoveIn_002443;

			internal GA_FREE_JS_Demo02 _0024self__002444;

			public _0024(GUIAnimFREE.ePosMove PosMoveIn, GA_FREE_JS_Demo02 self_)
			{
				_0024PosMoveIn_002443 = PosMoveIn;
				_0024self__002444 = self_;
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
					_0024_0024switch_00243_002442 = _0024PosMoveIn_002443;
					if (_0024_0024switch_00243_002442 == GUIAnimFREE.ePosMove.UpperScreenEdge)
					{
						_0024self__002444.m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.UpperScreenEdge;
						_0024self__002444.m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
					}
					else if (_0024_0024switch_00243_002442 == GUIAnimFREE.ePosMove.LeftScreenEdge)
					{
						_0024self__002444.m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.LeftScreenEdge;
						_0024self__002444.m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
					}
					else if (_0024_0024switch_00243_002442 == GUIAnimFREE.ePosMove.RightScreenEdge)
					{
						_0024self__002444.m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.RightScreenEdge;
						_0024self__002444.m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
					}
					else if (_0024_0024switch_00243_002442 == GUIAnimFREE.ePosMove.BottomScreenEdge)
					{
						_0024self__002444.m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.BottomScreenEdge;
						_0024self__002444.m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
					}
					else if (_0024_0024switch_00243_002442 == GUIAnimFREE.ePosMove.UpperLeft)
					{
						_0024self__002444.m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.UpperLeft;
						_0024self__002444.m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
					}
					else if (_0024_0024switch_00243_002442 == GUIAnimFREE.ePosMove.UpperRight)
					{
						_0024self__002444.m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.UpperRight;
						_0024self__002444.m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
					}
					else if (_0024_0024switch_00243_002442 == GUIAnimFREE.ePosMove.BottomLeft)
					{
						_0024self__002444.m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.BottomLeft;
						_0024self__002444.m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
					}
					else if (_0024_0024switch_00243_002442 == GUIAnimFREE.ePosMove.BottomRight)
					{
						_0024self__002444.m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.BottomRight;
						_0024self__002444.m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
					}
					else if (_0024_0024switch_00243_002442 != GUIAnimFREE.ePosMove.MiddleCenter)
					{
						_0024self__002444.m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.MiddleCenter;
						_0024self__002444.m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
					}
					_0024self__002444.m_Dialog.Reset();
					_0024self__002444.m_Dialog.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal GUIAnimFREE.ePosMove _0024PosMoveIn_002445;

		internal GA_FREE_JS_Demo02 _0024self__002446;

		public _0024DialogMoveIn_002441(GUIAnimFREE.ePosMove PosMoveIn, GA_FREE_JS_Demo02 self_)
		{
			_0024PosMoveIn_002445 = PosMoveIn;
			_0024self__002446 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024PosMoveIn_002445, _0024self__002446);
		}
	}

	public Canvas m_Canvas;

	public GUIAnimFREE m_Title1;

	public GUIAnimFREE m_Title2;

	public GUIAnimFREE m_TopBar;

	public GUIAnimFREE m_BottomBar;

	public GUIAnimFREE m_Dialog;

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
		return new _0024MoveInTitleGameObjects_002426(this).GetEnumerator();
	}

	public virtual IEnumerator ShowDialog()
	{
		return new _0024ShowDialog_002429(this).GetEnumerator();
	}

	public virtual void HideAllGUIs()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(HideTitleTextMeshes());
	}

	public virtual IEnumerator HideTitleTextMeshes()
	{
		return new _0024HideTitleTextMeshes_002432(this).GetEnumerator();
	}

	public virtual IEnumerator EnableAllDemoButtons()
	{
		return new _0024EnableAllDemoButtons_002435(this).GetEnumerator();
	}

	public virtual IEnumerator DisableAllButtonsForSeconds(float DisableTime)
	{
		return new _0024DisableAllButtonsForSeconds_002438(DisableTime).GetEnumerator();
	}

	public virtual void OnButton_UpperEdge()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.UpperScreenEdge));
	}

	public virtual void OnButton_LeftEdge()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.LeftScreenEdge));
	}

	public virtual void OnButton_RightEdge()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableAllButtonsForSeconds(2f));
		StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.RightScreenEdge));
	}

	public virtual void OnButton_BottomEdge()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableAllButtonsForSeconds(2f));
		StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.BottomScreenEdge));
	}

	public virtual void OnButton_UpperLeft()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableAllButtonsForSeconds(2f));
		StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.UpperLeft));
	}

	public virtual void OnButton_UpperRight()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableAllButtonsForSeconds(2f));
		StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.UpperRight));
	}

	public virtual void OnButton_BottomLeft()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableAllButtonsForSeconds(2f));
		StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.BottomLeft));
	}

	public virtual void OnButton_BottomRight()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableAllButtonsForSeconds(2f));
		StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.BottomRight));
	}

	public virtual void OnButton_Center()
	{
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		StartCoroutine(DisableAllButtonsForSeconds(2f));
		StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.MiddleCenter));
	}

	public virtual IEnumerator DialogMoveIn(GUIAnimFREE.ePosMove PosMoveIn)
	{
		return new _0024DialogMoveIn_002441(PosMoveIn, this).GetEnumerator();
	}
}
