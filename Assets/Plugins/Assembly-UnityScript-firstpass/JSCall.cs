using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class JSCall : MonoBehaviour
{
	public virtual void Start()
	{
		SendMessage("InitGameCenter");
	}

	public virtual void OnGUI()
	{
		if (GUI.Button(new Rect(10f, 10f, 150f, 40f), "Submit Score"))
		{
			SendMessage("SubmitScore", 100);
		}
		if (GUI.Button(new Rect(10f, 60f, 150f, 40f), "Submit Achievement"))
		{
			UnityScript.Lang.Array array = new UnityScript.Lang.Array();
			array.push("20.2");
			array.push("your.achievement.id1.here");
			SendMessage("SubmitAchievement", array.join("|"));
		}
	}

	public virtual void Main()
	{
	}
}
