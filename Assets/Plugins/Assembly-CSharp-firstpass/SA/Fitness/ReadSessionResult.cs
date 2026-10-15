using System.Collections.Generic;
using SA.Common.Models;

namespace SA.Fitness
{
	public class ReadSessionResult : Result
	{
		private List<Session> sessions = new List<Session>();

		private int id;

		public int Id
		{
			get
			{
				return id;
			}
		}

		public List<Session> Sessions
		{
			get
			{
				return sessions;
			}
		}

		public ReadSessionResult(int id)
		{
			this.id = id;
		}

		public ReadSessionResult(int id, int resultCode, string message)
			: base(new Error(resultCode, message))
		{
			this.id = id;
		}

		public void AddSession(Session session)
		{
			sessions.Add(session);
		}
	}
}
