using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Toggle))]
public class FeatureSelector : MonoBehaviour
{
	[SerializeField]
	private Toggle Selector;

	[SerializeField]
	private FeatureTab Tab;

	private void Start()
	{
		Selector = GetComponent<Toggle>();
		Selector.onValueChanged.AddListener((bool b) =>
		{
			if (b)
			{
				Tab.Show();
			}
			else
			{
				Tab.Hide();
			}
		});
	}
}
