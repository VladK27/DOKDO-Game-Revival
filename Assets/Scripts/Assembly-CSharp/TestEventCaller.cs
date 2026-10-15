using Percent.Event;
using UnityEngine;

public class TestEventCaller : MonoBehaviour
{
	public PercentTracker tracker;

	private void Start()
	{
		tracker.trigger();
	}
}
