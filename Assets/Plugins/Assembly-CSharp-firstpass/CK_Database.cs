using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

public class CK_Database
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<CK_RecordResult> ActionRecordSaved__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<CK_RecordResult> ActionRecordFetchComplete__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<CK_RecordDeleteResult> ActionRecordDeleted__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<CK_QueryResult> ActionQueryComplete__BackingField = delegate
	{
	};

	private static Dictionary<int, CK_Database> _Databases = new Dictionary<int, CK_Database>();

	private int _InternalId;

	public int InternalId
	{
		get
		{
			return _InternalId;
		}
	}

	public event Action<CK_RecordResult> ActionRecordSaved
	{
		add
		{
			Action<CK_RecordResult> action = ActionRecordSaved__BackingField;
			Action<CK_RecordResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRecordSaved__BackingField, (Action<CK_RecordResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<CK_RecordResult> action = ActionRecordSaved__BackingField;
			Action<CK_RecordResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRecordSaved__BackingField, (Action<CK_RecordResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<CK_RecordResult> ActionRecordFetchComplete
	{
		add
		{
			Action<CK_RecordResult> action = ActionRecordFetchComplete__BackingField;
			Action<CK_RecordResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRecordFetchComplete__BackingField, (Action<CK_RecordResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<CK_RecordResult> action = ActionRecordFetchComplete__BackingField;
			Action<CK_RecordResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRecordFetchComplete__BackingField, (Action<CK_RecordResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<CK_RecordDeleteResult> ActionRecordDeleted
	{
		add
		{
			Action<CK_RecordDeleteResult> action = ActionRecordDeleted__BackingField;
			Action<CK_RecordDeleteResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRecordDeleted__BackingField, (Action<CK_RecordDeleteResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<CK_RecordDeleteResult> action = ActionRecordDeleted__BackingField;
			Action<CK_RecordDeleteResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRecordDeleted__BackingField, (Action<CK_RecordDeleteResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<CK_QueryResult> ActionQueryComplete
	{
		add
		{
			Action<CK_QueryResult> action = ActionQueryComplete__BackingField;
			Action<CK_QueryResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionQueryComplete__BackingField, (Action<CK_QueryResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<CK_QueryResult> action = ActionQueryComplete__BackingField;
			Action<CK_QueryResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionQueryComplete__BackingField, (Action<CK_QueryResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public CK_Database(int internalId)
	{
		_InternalId = internalId;
		_Databases.Add(_InternalId, this);
	}

	public void SaveRecrod(CK_Record record)
	{
		record.UpdateRecord();
		ISN_CloudKit.SaveRecord(_InternalId, record.Internal_Id);
	}

	public void FetchRecordWithID(CK_RecordID recordId)
	{
		ISN_CloudKit.FetchRecord(_InternalId, recordId.Internal_Id);
	}

	public void DeleteRecordWithID(CK_RecordID recordId)
	{
		ISN_CloudKit.DeleteRecord(_InternalId, recordId.Internal_Id);
	}

	public void PerformQuery(CK_Query query)
	{
		ISN_CloudKit.PerformQuery(_InternalId, query.Predicate, query.RecordType);
	}

	public static CK_Database GetDatabaseByInternalId(int id)
	{
		return _Databases[id];
	}

	public void FireSaveRecordResult(CK_RecordResult result)
	{
		result.SetDatabase(this);
		ActionRecordSaved__BackingField(result);
	}

	public void FireFetchRecordResult(CK_RecordResult result)
	{
		result.SetDatabase(this);
		ActionRecordFetchComplete__BackingField(result);
	}

	public void FireDeleteRecordResult(CK_RecordDeleteResult result)
	{
		result.SetDatabase(this);
		ActionRecordDeleted__BackingField(result);
	}

	public void FireQueryCompleteResult(CK_QueryResult result)
	{
		result.SetDatabase(this);
		ActionQueryComplete__BackingField(result);
	}
}
