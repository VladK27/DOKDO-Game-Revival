using SA.Common.Pattern;
using UnityEngine;

public class GameCenter : MonoBehaviour
{
	public void OpenLeader()
	{
		Singleton<UM_GameServiceManager>.Instance.ShowLeaderBoardsUI();
	}

	public void OpenAchieve()
	{
		Singleton<UM_GameServiceManager>.Instance.ShowAchievementsUI();
	}
}
