using UnityEngine;
using UnityEngine.SceneManagement;

namespace Percent
{
	public class SceneChecker : MonoBehaviour
	{
		private int levelOfCallShow = -1;

		private int currentLevel = -2;

		private void Start()
		{
			SceneManager.sceneLoaded += OnLevelWasLoaded_V_5_4_0;
		}

		internal bool isShowableScene()
		{
			if (didCalledShow() && isCurrentSceneSameWithCallingShowScene())
			{
				return true;
			}
			return false;
		}

		private bool didCalledShow()
		{
			if (levelOfCallShow.Equals(-1))
			{
				return false;
			}
			return true;
		}

		private bool isCurrentSceneSameWithCallingShowScene()
		{
			if (levelOfCallShow.Equals(currentLevel))
			{
				return true;
			}
			return false;
		}

		internal void saveCalledShowScene()
		{
			levelOfCallShow = currentLevel;
		}

		private void OnLevelWasLoaded_V_5_4_0(Scene scene, LoadSceneMode mode)
		{
			currentLevel = scene.buildIndex;
		}
	}
}
