using SA.Common.Pattern;
using UnityEngine;

public class GK_MatchUpdateTask : MonoBehaviour
{
	private GK_TBM_Controller _controller;

	private string _matchId = string.Empty;

	public static void Create(string matchId, GK_TBM_Controller controller)
	{
		GK_MatchUpdateTask gK_MatchUpdateTask = new GameObject("GK_MatchUpdateTask").AddComponent<GK_MatchUpdateTask>();
		gK_MatchUpdateTask.LoadMatchInfo(matchId, controller);
	}

	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}

	public void LoadMatchInfo(string matchId, GK_TBM_Controller controller)
	{
		_matchId = matchId;
		_controller = controller;
		GameCenter_TBM.ActionMatchInfoLoaded += GameCenter_TBM_ActionMatchInfoLoaded;
		Singleton<GameCenter_TBM>.Instance.LoadMatch(_matchId);
	}

	private void GameCenter_TBM_ActionMatchInfoLoaded(GK_TBM_LoadMatchResult res)
	{
		GameCenter_TBM.ActionMatchInfoLoaded -= GameCenter_TBM_ActionMatchInfoLoaded;
		_controller.SendMatchUpdateEvent(res, res.Match);
		Object.Destroy(base.gameObject);
	}
}
