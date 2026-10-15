using SA.Common.Pattern;

public class SALoadedSceneOnClick : SAOnClickAction
{
	public string levelName;

	protected override void OnClick()
	{
		Singleton<SALevelLoader>.Instance.LoadLevel(levelName);
	}
}
