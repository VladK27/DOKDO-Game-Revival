using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ImportLocalizationAll
{
	public static void Run()
	{
		string[] scenes =
		{
			"Assets/0.my/Scenes/Main.unity",
			"Assets/0.my/Scenes/Intro.unity",
			"Assets/0.my/Scenes/load.unity"
		};
		foreach (string path in scenes)
		{
			var scene = EditorSceneManager.OpenScene(path);
			DataImporter importer = Object.FindObjectOfType<DataImporter>();
			if (importer == null)
			{
				Debug.LogError("[IMPORT] No DataImporter in " + path);
				continue;
			}
			importer.ImportLocalization();
			EditorSceneManager.MarkSceneDirty(scene);
			EditorSceneManager.SaveScene(scene);
			Debug.Log("[IMPORT] saved " + path);
		}
		AssetDatabase.SaveAssets();
	}
}
