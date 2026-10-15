using UnityEngine;

public class WPN_Singletone<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;

	public static T instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(T)) as T;
				if (_instance == null)
				{
					_instance = new GameObject(typeof(T).Name).AddComponent<T>();
				}
			}
			return _instance;
		}
	}

	public static bool HasInstance
	{
		get
		{
			if (_instance == null)
			{
				return false;
			}
			return true;
		}
	}
}
