using SA.Common.Models;

public class CK_RecordDeleteResult : Result
{
	private CK_RecordID _RecordID;

	private CK_Database _Database;

	public CK_Database Database
	{
		get
		{
			return _Database;
		}
	}

	public CK_RecordID RecordID
	{
		get
		{
			return _RecordID;
		}
	}

	public CK_RecordDeleteResult(int recordId)
	{
		_RecordID = CK_RecordID.GetRecordIdByInternalId(recordId);
	}

	public CK_RecordDeleteResult(string errorData)
		: base(new Error(errorData))
	{
	}

	public void SetDatabase(CK_Database database)
	{
		_Database = database;
	}
}
