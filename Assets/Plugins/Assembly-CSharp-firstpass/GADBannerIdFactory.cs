public class GADBannerIdFactory
{
	private static int _nextId;

	public static int nextId
	{
		get
		{
			_nextId++;
			return _nextId;
		}
	}
}
