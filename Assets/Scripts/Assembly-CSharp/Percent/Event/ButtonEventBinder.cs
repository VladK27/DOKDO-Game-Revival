using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Percent.Event
{
	public abstract class ButtonEventBinder : MonoBehaviour
	{
		private void Start()
		{
			GetComponent<Button>().onClick.AddListener(onFindMethod());
		}

		protected abstract UnityAction onFindMethod();
	}
}
