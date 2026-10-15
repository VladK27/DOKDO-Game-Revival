using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Toggle))]
public class TintedToggle : MonoBehaviour
{
	[SerializeField]
	private Toggle ToggleButton;

	[SerializeField]
	private Text Label;

	[SerializeField]
	private Color TintColor = Color.white;

	[SerializeField]
	private Color Color;

	private void Awake()
	{
	}

	private void Start()
	{
		Label = GetComponentInChildren<Text>();
		ToggleButton = GetComponent<Toggle>();
		ToggleButton.onValueChanged.AddListener((bool b) =>
		{
			Label.color = ((!b) ? TintColor : Color);
		});
		Label.color = ((!ToggleButton.isOn) ? TintColor : Color);
	}
}
