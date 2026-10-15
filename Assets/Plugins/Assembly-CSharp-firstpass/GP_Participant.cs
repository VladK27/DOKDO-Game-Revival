using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Util;
using UnityEngine;

public class GP_Participant
{
	private string _id;

	private string _playerid;

	private string _HiResImageUrl;

	private string _IconImageUrl;

	private string _DisplayName;

	private GP_ParticipantResult _result;

	private GP_RTM_ParticipantStatus _status = GP_RTM_ParticipantStatus.STATUS_UNRESPONSIVE;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Texture2D> BigPhotoLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Texture2D> SmallPhotoLoaded__BackingField = delegate
	{
	};

	private Texture2D _SmallPhoto;

	private Texture2D _BigPhoto;

	public Texture2D SmallPhoto
	{
		get
		{
			return _SmallPhoto;
		}
	}

	public Texture2D BigPhoto
	{
		get
		{
			return _BigPhoto;
		}
	}

	public string id
	{
		get
		{
			return _id;
		}
	}

	public string playerId
	{
		get
		{
			return _playerid;
		}
	}

	public string HiResImageUrl
	{
		get
		{
			return _HiResImageUrl;
		}
	}

	public string IconImageUrl
	{
		get
		{
			return _IconImageUrl;
		}
	}

	public string DisplayName
	{
		get
		{
			return _DisplayName;
		}
	}

	public GP_RTM_ParticipantStatus Status
	{
		get
		{
			return _status;
		}
	}

	public GP_ParticipantResult Result
	{
		get
		{
			return _result;
		}
	}

	public event Action<Texture2D> BigPhotoLoaded
	{
		add
		{
			Action<Texture2D> action = BigPhotoLoaded__BackingField;
			Action<Texture2D> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BigPhotoLoaded__BackingField, (Action<Texture2D>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Texture2D> action = BigPhotoLoaded__BackingField;
			Action<Texture2D> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BigPhotoLoaded__BackingField, (Action<Texture2D>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<Texture2D> SmallPhotoLoaded
	{
		add
		{
			Action<Texture2D> action = SmallPhotoLoaded__BackingField;
			Action<Texture2D> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SmallPhotoLoaded__BackingField, (Action<Texture2D>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Texture2D> action = SmallPhotoLoaded__BackingField;
			Action<Texture2D> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SmallPhotoLoaded__BackingField, (Action<Texture2D>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public GP_Participant(string uid, string playerUid, string stat, string hiResImg, string IconImg, string Name)
	{
		_id = uid;
		_playerid = playerUid;
		_status = (GP_RTM_ParticipantStatus)Convert.ToInt32(stat);
		_HiResImageUrl = hiResImg;
		_IconImageUrl = IconImg;
		_DisplayName = Name;
	}

	public void SetResult(GP_ParticipantResult r)
	{
		_result = r;
	}

	public void LoadBigPhoto()
	{
		Loader.LoadWebTexture(_HiResImageUrl, HandheBigPhotoLoaed);
	}

	public void LoadSmallPhoto()
	{
		Loader.LoadWebTexture(_IconImageUrl, HandheSmallPhotoLoaed);
	}

	private void HandheBigPhotoLoaed(Texture2D tex)
	{
		if (this != null)
		{
			_BigPhoto = tex;
			BigPhotoLoaded__BackingField(_BigPhoto);
		}
	}

	private void HandheSmallPhotoLoaed(Texture2D tex)
	{
		if (this != null)
		{
			_SmallPhoto = tex;
			SmallPhotoLoaded__BackingField(_SmallPhoto);
		}
	}
}
