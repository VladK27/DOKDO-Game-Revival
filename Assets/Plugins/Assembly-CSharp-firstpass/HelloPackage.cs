public class HelloPackage : BasePackage
{
	public HelloPackage()
	{
		initWriter();
		writeFloat(1.1f);
	}

	public override int getId()
	{
		return 2;
	}
}
