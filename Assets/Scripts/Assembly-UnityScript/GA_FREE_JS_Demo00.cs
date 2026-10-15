using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GA_FREE_JS_Demo00 : MonoBehaviour
{
	[Serializable]
	internal sealed class _0024ShowText_00245 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal GameObject _0024go_00246;

			internal float _0024Delay_00247;

			public _0024(float Delay)
			{
				_0024Delay_00247 = Delay;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					_0024go_00246 = GameObject.Find("Panel (Middle Center)");
					if ((bool)_0024go_00246)
					{
						GUIAnimSystemFREE.Instance.MoveIn(_0024go_00246.transform, true);
					}
					result = (Yield(2, new WaitForSeconds(3f)) ? 1 : 0);
					break;
				case 2:
					if ((bool)_0024go_00246)
					{
						GUIAnimSystemFREE.Instance.MoveOut(_0024go_00246.transform, true);
					}
					result = (Yield(3, new WaitForSeconds(_0024Delay_00247 / 2f)) ? 1 : 0);
					break;
				case 3:
					SceneManager.LoadScene("GA FREE JS - Demo01 (960x600px)");
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal float _0024Delay_00248;

		public _0024ShowText_00245(float Delay)
		{
			_0024Delay_00248 = Delay;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024Delay_00248);
		}
	}

	public virtual void Awake()
	{
		if (enabled)
		{
			GUIAnimSystemFREE.Instance.m_AutoAnimation = false;
		}
	}

	public virtual void Start()
	{
		StartCoroutine(ShowText(1f));
	}

	public virtual void Update()
	{
	}

	public virtual IEnumerator ShowText(float Delay)
	{
		return new _0024ShowText_00245(Delay).GetEnumerator();
	}
}
