namespace Percent.Event
{
	public interface ITrackingEventListener
	{
		void onTriggerTrackingEvent<T>(T arg);
	}
}
