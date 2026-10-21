using System.Collections.Generic;
using System.Text;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class LocalizeManager : MonoBehaviour
{
	public static LocalizeManager Instance;

	public string Language;

	public List<Word> words = new List<Word>();

	private Word basedata = new Word();

	public Dictionary<string, string> localized = new Dictionary<string, string>();

	public int currentLangNum;

	private static int COUNT;

	private int index;

	private void Awake()
	{
		// Only the first LocalizeManager (on the persistent DontDestroyOnLoad GameManager)
		// owns Instance. Duplicate GameManagers from newly loaded scenes are destroyed by
		// GameManager.Awake, so their LocalizeManager must not overwrite Instance (and then
		// clear it in OnDestroy) — otherwise Instance becomes null and ChangeLang silently
		// does nothing.
		index = COUNT;
		COUNT++;
		if (index != 0)
		{
			return;
		}
		Instance = this;
		init();
	}

	private void OnDestroy()
	{
		if (Instance == this)
		{
			Instance = null;
		}
	}

	public void SaveLang()
	{
		ApplyLanguage(Language, true, true);
	}

	public void AddNew()
	{
		words.Add(basedata);
	}

	public void Remove()
	{
		words.RemoveAt(words.Count - 1);
	}

	public void init()
	{
		ApplyLanguage(ObscuredPrefs.GetString("lang", Application.systemLanguage.ToString()), false, false);
	}

	private void ApplyLanguage(string language, bool savePreference, bool refreshVisibleText)
	{
		localized.Clear();
		LoadMonitorTranslations();
		Language = language;
		currentLangNum = 0;
		int num = 7;
		for (int i = 1; i < num; i++)
		{
			Lang lang = (Lang)i;
			string value = lang.ToString();
			if (Language.Equals(value))
			{
				currentLangNum = i;
			}
		}
		if (currentLangNum == 0)
		{
			Language = Lang.English.ToString();
			currentLangNum = (int)Lang.English;
		}
		if (savePreference || !ObscuredPrefs.HasKey("lang"))
		{
			ObscuredPrefs.SetString("lang", Language);
			PlayerPrefs.Save();
		}
		for (int j = 0; j < words.Count; j++)
		{
			localized.Add(words[j].lang[0], words[j].lang[currentLangNum]);
		}
		Debug.Log("[LOCALE] Manager language=" + Language + " index=" + currentLangNum + " entries=" + localized.Count + " saving=" + savePreference);
		if (refreshVisibleText)
		{
			Localization[] labels = FindObjectsOfType<Localization>();
			for (int i = 0; i < labels.Length; i++)
			{
				labels[i].Refresh();
			}
			RandomTip[] tips = FindObjectsOfType<RandomTip>();
			for (int i = 0; i < tips.Length; i++)
			{
				tips[i].RefreshTip();
			}
		}
	}

	private void LoadMonitorTranslations()
	{
		// AD REMOVAL: The developer-promotion tip in Resources/local.txt was replaced with a gameplay hint.
		TextAsset textAsset = Resources.Load<TextAsset>("local");
		if (textAsset == null)
		{
			return;
		}
		string[] lines = textAsset.text.Split('\n');
		for (int i = 0; i < lines.Length; i++)
		{
			string[] columns = ParseCsvRow(lines[i].TrimEnd('\r'));
			if (columns.Length != 7 || !columns[0].StartsWith("monitor_"))
			{
				continue;
			}
			Word word = new Word();
			word.lang = columns;
			int existingIndex = indexOf(columns[0]);
			if (existingIndex >= 0)
			{
				words[existingIndex] = word;
			}
			else
			{
				words.Add(word);
			}
		}
	}

	public static string[] ParseCsvRow(string line)
	{
		List<string> columns = new List<string>();
		StringBuilder value = new StringBuilder();
		bool quoted = false;
		for (int i = 0; i < line.Length; i++)
		{
			char character = line[i];
			if (character == '"')
			{
				if (quoted && i + 1 < line.Length && line[i + 1] == '"')
				{
					value.Append('"');
					i++;
				}
				else
				{
					quoted = !quoted;
				}
			}
			else if (character == ',' && !quoted)
			{
				columns.Add(value.ToString());
				value.Length = 0;
			}
			else
			{
				value.Append(character);
			}
		}
		columns.Add(value.ToString());
		return columns.ToArray();
	}

	public string translate(string id)
	{
		int num = indexOf(id);
		if (num.Equals(-1))
		{
			Debug.LogWarning("Missing localization key: " + id);
			return id;
		}
		return getTranslated(num);
	}

	private int indexOf(string id)
	{
		for (int i = 0; i < words.Count; i++)
		{
			if (words[i].lang[0].Equals(id))
			{
				return i;
			}
		}
		return -1;
	}

	private string getTranslated(int index)
	{
		Word word = words[index];
		return word.lang[currentLangNum];
	}
}
