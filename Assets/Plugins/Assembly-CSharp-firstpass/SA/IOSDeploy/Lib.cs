using System;

namespace SA.IOSDeploy
{
	[Serializable]
	public class Lib
	{
		public bool IsOpen;

		public iOSLibrary Type;

		public bool IsOptional;

		public string Name
		{
			get
			{
				return ISD_LibHandler.stringValueOf(Type);
			}
		}

		public Lib(iOSLibrary lib)
		{
			Type = lib;
		}
	}
}
