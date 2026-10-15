using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

namespace SA.Common.Animation
{
	public class ValuesTween : MonoBehaviour
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action OnComplete__BackingField = () =>
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<float> OnValueChanged__BackingField = delegate
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<Vector3> OnVectorValueChanged__BackingField = delegate
		{
		};

		public bool DestoryGameObjectOnComplete = true;

		private float FinalFloatValue;

		private Vector3 FinalVectorValue;

		public event Action OnComplete
		{
			add
			{
				Action action = OnComplete__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnComplete__BackingField, (Action)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action action = OnComplete__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnComplete__BackingField, (Action)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public event Action<float> OnValueChanged
		{
			add
			{
				Action<float> action = OnValueChanged__BackingField;
				Action<float> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnValueChanged__BackingField, (Action<float>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<float> action = OnValueChanged__BackingField;
				Action<float> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnValueChanged__BackingField, (Action<float>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public event Action<Vector3> OnVectorValueChanged
		{
			add
			{
				Action<Vector3> action = OnVectorValueChanged__BackingField;
				Action<Vector3> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnVectorValueChanged__BackingField, (Action<Vector3>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<Vector3> action = OnVectorValueChanged__BackingField;
				Action<Vector3> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnVectorValueChanged__BackingField, (Action<Vector3>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public static ValuesTween Create()
		{
			return new GameObject("SA.Common.Animation.ValuesTween").AddComponent<ValuesTween>();
		}

		private void Update()
		{
			OnValueChanged__BackingField(base.transform.position.x);
			OnVectorValueChanged__BackingField(base.transform.position);
		}

		public void ValueTo(float from, float to, float time, EaseType easeType = EaseType.linear)
		{
			Vector3 position = base.transform.position;
			position.x = from;
			base.transform.position = position;
			FinalFloatValue = to;
			SA_iTween.MoveTo(base.gameObject, SA_iTween.Hash("x", to, "time", time, "easeType", easeType.ToString(), "oncomplete", "onTweenComplete", "oncompletetarget", base.gameObject));
		}

		public void VectorTo(Vector3 from, Vector3 to, float time, EaseType easeType = EaseType.linear)
		{
			base.transform.position = from;
			FinalVectorValue = to;
			SA_iTween.MoveTo(base.gameObject, SA_iTween.Hash("position", to, "time", time, "easeType", easeType.ToString(), "oncomplete", "onTweenComplete", "oncompletetarget", base.gameObject));
		}

		public void ScaleTo(Vector3 from, Vector3 to, float time, EaseType easeType = EaseType.linear)
		{
			base.transform.localScale = from;
			FinalVectorValue = to;
			SA_iTween.ScaleTo(base.gameObject, SA_iTween.Hash("scale", to, "time", time, "easeType", easeType.ToString(), "oncomplete", "onTweenComplete", "oncompletetarget", base.gameObject));
		}

		public void VectorToS(Vector3 from, Vector3 to, float speed, EaseType easeType = EaseType.linear)
		{
			base.transform.position = from;
			FinalVectorValue = to;
			SA_iTween.MoveTo(base.gameObject, SA_iTween.Hash("position", to, "speed", speed, "easeType", easeType.ToString(), "oncomplete", "onTweenComplete", "oncompletetarget", base.gameObject));
		}

		public void Stop()
		{
			SA_iTween.Stop(base.gameObject);
			UnityEngine.Object.Destroy(base.gameObject);
		}

		private void onTweenComplete()
		{
			OnValueChanged__BackingField(FinalFloatValue);
			OnVectorValueChanged__BackingField(FinalVectorValue);
			OnComplete__BackingField();
			if (DestoryGameObjectOnComplete)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else
			{
				UnityEngine.Object.Destroy(this);
			}
		}
	}
}
