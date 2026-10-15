using SA.Common.Pattern;
using UnityEngine;

public class CloudKitUseExample : BaseIOSFeaturePreview
{
	private void OnGUI()
	{
		UpdateToStartPos();
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Cloud Kit", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Create Record"))
		{
			CK_RecordID id = new CK_RecordID("1");
			CK_Record cK_Record = new CK_Record(id, "Post");
			cK_Record.SetObject("PostText", "Sample point of interest");
			cK_Record.SetObject("PostTitle", "My favorite point of interest");
			CK_Database privateDB = Singleton<ISN_CloudKit>.Instance.PrivateDB;
			privateDB.SaveRecrod(cK_Record);
			privateDB.ActionRecordSaved += Database_ActionRecordSaved;
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Delete Record"))
		{
			CK_RecordID recordId = new CK_RecordID("1");
			CK_Database privateDB2 = Singleton<ISN_CloudKit>.Instance.PrivateDB;
			privateDB2.DeleteRecordWithID(recordId);
			privateDB2.ActionRecordDeleted += Database_ActionRecordDeleted;
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Fetch Record"))
		{
			CK_RecordID recordId2 = new CK_RecordID("1");
			CK_Database privateDB3 = Singleton<ISN_CloudKit>.Instance.PrivateDB;
			privateDB3.FetchRecordWithID(recordId2);
			privateDB3.ActionRecordFetchComplete += Database_ActionRecordFetchComplete;
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Fetch And Update"))
		{
			CK_RecordID recordId3 = new CK_RecordID("1");
			CK_Database privateDB4 = Singleton<ISN_CloudKit>.Instance.PrivateDB;
			privateDB4.FetchRecordWithID(recordId3);
			privateDB4.ActionRecordFetchComplete += Database_ActionRecordFetchForUpdateComplete;
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Perform Query"))
		{
			CK_Database privateDB5 = Singleton<ISN_CloudKit>.Instance.PrivateDB;
			CK_Query query = new CK_Query("TRUEPREDICATE", "Post");
			privateDB5.ActionQueryComplete += Database_ActionQueryComplete;
			privateDB5.PerformQuery(query);
		}
	}

	private void Database_ActionQueryComplete(CK_QueryResult res)
	{
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("Database_ActionQueryComplete, total records found: " + res.Records.Count);
			{
				foreach (CK_Record record in res.Records)
				{
					Debug.Log(record.Id.Name);
					ISN_Logger.Log("Post Title: " + record.GetObject("PostTitle"));
				}
				return;
			}
		}
		ISN_Logger.Log("Database_ActionRecordFetchComplete, Error: " + res.Error.Code + " / " + res.Error.Message);
	}

	private void Database_ActionRecordFetchComplete(CK_RecordResult res)
	{
		res.Database.ActionRecordFetchComplete -= Database_ActionRecordFetchComplete;
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("Database_ActionRecordFetchComplete:");
			ISN_Logger.Log("Post Title: " + res.Record.GetObject("PostTitle"));
			return;
		}
		ISN_Logger.Log("Database_ActionRecordFetchComplete, Error: " + res.Error.Code + " / " + res.Error.Message);
	}

	private void Database_ActionRecordFetchForUpdateComplete(CK_RecordResult res)
	{
		res.Database.ActionRecordFetchComplete -= Database_ActionRecordFetchForUpdateComplete;
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("Database_ActionRecordFetchComplete:");
			ISN_Logger.Log("Post Title: " + res.Record.GetObject("PostTitle"));
			ISN_Logger.Log("Updatting Title: ");
			CK_Record record = res.Record;
			record.SetObject("PostTitle", "My favorite point of interest - updated");
			Singleton<ISN_CloudKit>.Instance.PrivateDB.SaveRecrod(record);
			Singleton<ISN_CloudKit>.Instance.PrivateDB.ActionRecordSaved += Database_ActionRecordSaved;
		}
		else
		{
			ISN_Logger.Log("Database_ActionRecordFetchComplete, Error: " + res.Error.Code + " / " + res.Error.Message);
		}
	}

	private void Database_ActionRecordDeleted(CK_RecordDeleteResult res)
	{
		res.Database.ActionRecordDeleted -= Database_ActionRecordDeleted;
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("Database_ActionRecordDeleted, Success: ");
			return;
		}
		ISN_Logger.Log("Database_ActionRecordDeleted, Error: " + res.Error.Code + " / " + res.Error.Message);
	}

	private void Database_ActionRecordSaved(CK_RecordResult res)
	{
		res.Database.ActionRecordSaved -= Database_ActionRecordSaved;
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("Database_ActionRecordSaved:");
			ISN_Logger.Log("Post Title: " + res.Record.GetObject("PostTitle"));
			return;
		}
		ISN_Logger.Log("Database_ActionRecordSaved, Error: " + res.Error.Code + " / " + res.Error.Message);
	}
}
