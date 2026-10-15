public class ObjectCreatePackage : BasePackage
{
	public ObjectCreatePackage(float x, float y)
	{
		initWriter();
		writeFloat(x);
		writeFloat(y);
	}

	public override int getId()
	{
		return 1;
	}
}
