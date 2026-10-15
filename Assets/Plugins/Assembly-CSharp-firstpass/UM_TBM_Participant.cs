using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class UM_TBM_Participant
{
	private string _Id;

	private string _Playerid = string.Empty;

	private string _DisplayName = string.Empty;

	private UM_TBM_Outcome _Outcome;

	private UM_TBM_ParticipantStatus _Status;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Texture2D> BigPhotoLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Texture2D> SmallPhotoLoaded__BackingField = delegate
	{
	};

	private GK_TBM_Participant _GK_Participan;

	private GP_Participant _GP_Participant;

	public string Id
	{
		get
		{
			return _Id;
		}
	}

	public string Playerid
	{
		get
		{
			return _Playerid;
		}
	}

	public string DisplayName
	{
		get
		{
			return _DisplayName;
		}
	}

	public UM_TBM_Outcome Outcome
	{
		get
		{
			return _Outcome;
		}
	}

	public UM_TBM_ParticipantStatus Status
	{
		get
		{
			return _Status;
		}
	}

	public bool IsPlayerDefined
	{
		get
		{
			if (_Playerid.Equals(string.Empty))
			{
				return false;
			}
			return true;
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

	public UM_TBM_Participant(GK_TBM_Participant participant)
	{
		_GK_Participan = participant;
		_Id = _GK_Participan.PlayerId;
		_Playerid = _GK_Participan.PlayerId;
		if (participant.Player != null)
		{
			_DisplayName = _GK_Participan.Player.Alias;
			_GK_Participan.Player.OnPlayerPhotoLoaded += HandleOnPlayerPhotoLoaded;
		}
		switch (_GK_Participan.Status)
		{
		case GK_TurnBasedParticipantStatus.Active:
			_Status = UM_TBM_ParticipantStatus.Active;
			break;
		case GK_TurnBasedParticipantStatus.Declined:
			_Status = UM_TBM_ParticipantStatus.Declined;
			break;
		case GK_TurnBasedParticipantStatus.Done:
			_Status = UM_TBM_ParticipantStatus.Done;
			break;
		case GK_TurnBasedParticipantStatus.Invited:
			_Status = UM_TBM_ParticipantStatus.Invited;
			break;
		case GK_TurnBasedParticipantStatus.Unknown:
		case GK_TurnBasedParticipantStatus.Matching:
			_Status = UM_TBM_ParticipantStatus.Unknown;
			break;
		}
		switch (_GK_Participan.MatchOutcome)
		{
		case GK_TurnBasedMatchOutcome.Won:
			_Outcome = UM_TBM_Outcome.Won;
			break;
		case GK_TurnBasedMatchOutcome.Lost:
			_Outcome = UM_TBM_Outcome.Lost;
			break;
		case GK_TurnBasedMatchOutcome.Tied:
			_Outcome = UM_TBM_Outcome.Tied;
			break;
		case GK_TurnBasedMatchOutcome.Quit:
			_Outcome = UM_TBM_Outcome.Disconnected;
			break;
		default:
			_Outcome = UM_TBM_Outcome.None;
			break;
		}
	}

	public UM_TBM_Participant(GP_Participant participant)
	{
		_GP_Participant = participant;
		_Id = _GP_Participant.id;
		_Playerid = _GP_Participant.playerId;
		_DisplayName = _GP_Participant.DisplayName;
		_GP_Participant.BigPhotoLoaded += HandleBigPhotoLoaded;
		_GP_Participant.SmallPhotoLoaded += HandleSmallPhotoLoaded;
		switch (_GP_Participant.Status)
		{
		case GP_RTM_ParticipantStatus.STATUS_JOINED:
			_Status = UM_TBM_ParticipantStatus.Active;
			break;
		case GP_RTM_ParticipantStatus.STATUS_DECLINED:
		case GP_RTM_ParticipantStatus.STATUS_LEFT:
			_Status = UM_TBM_ParticipantStatus.Declined;
			break;
		case GP_RTM_ParticipantStatus.STATUS_FINISHED:
			_Status = UM_TBM_ParticipantStatus.Done;
			break;
		case GP_RTM_ParticipantStatus.STATUS_INVITED:
			_Status = UM_TBM_ParticipantStatus.Invited;
			break;
		case GP_RTM_ParticipantStatus.STATUS_NOT_INVITED_YET:
		case GP_RTM_ParticipantStatus.STATUS_UNRESPONSIVE:
			_Status = UM_TBM_ParticipantStatus.Unknown;
			break;
		}
		_Outcome = UM_TBM_Outcome.None;
		if (_GP_Participant.Result != null)
		{
			switch (_GP_Participant.Result.Result)
			{
			case GP_TBM_ParticipantResult.MATCH_RESULT_WIN:
				_Outcome = UM_TBM_Outcome.Won;
				break;
			case GP_TBM_ParticipantResult.MATCH_RESULT_LOSS:
				_Outcome = UM_TBM_Outcome.Lost;
				break;
			case GP_TBM_ParticipantResult.MATCH_RESULT_TIE:
				_Outcome = UM_TBM_Outcome.Tied;
				break;
			case GP_TBM_ParticipantResult.MATCH_RESULT_DISCONNECT:
				_Outcome = UM_TBM_Outcome.Disconnected;
				break;
			default:
				_Outcome = UM_TBM_Outcome.None;
				break;
			}
		}
	}

	public void LoadBigPhoto()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			_GP_Participant.LoadBigPhoto();
			break;
		case RuntimePlatform.IPhonePlayer:
			if (_GK_Participan.Player != null)
			{
				_GK_Participan.Player.LoadPhoto(GK_PhotoSize.GKPhotoSizeNormal);
			}
			break;
		}
	}

	public void LoadSmallPhoto()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			_GP_Participant.LoadSmallPhoto();
			break;
		case RuntimePlatform.IPhonePlayer:
			if (_GK_Participan.Player != null)
			{
				_GK_Participan.Player.LoadPhoto(GK_PhotoSize.GKPhotoSizeSmall);
			}
			break;
		}
	}

	private void HandleOnPlayerPhotoLoaded(GK_UserPhotoLoadResult res)
	{
		if (res.IsSucceeded)
		{
			if (res.Size == GK_PhotoSize.GKPhotoSizeSmall)
			{
				SmallPhotoLoaded__BackingField(res.Photo);
			}
			else
			{
				BigPhotoLoaded__BackingField(res.Photo);
			}
		}
	}

	private void HandleSmallPhotoLoaded(Texture2D photo)
	{
		SmallPhotoLoaded__BackingField(photo);
	}

	private void HandleBigPhotoLoaded(Texture2D photo)
	{
		BigPhotoLoaded__BackingField(photo);
	}
}
