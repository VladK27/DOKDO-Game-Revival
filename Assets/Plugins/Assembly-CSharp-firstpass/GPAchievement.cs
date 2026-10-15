using System;
using UnityEngine;

[Serializable]
public class GPAchievement
{
	public bool IsOpen = true;

	[SerializeField]
	private string _id = string.Empty;

	[SerializeField]
	private string _name = string.Empty;

	[SerializeField]
	private string _description = string.Empty;

	[SerializeField]
	private Texture2D _Texture;

	private int _currentSteps;

	private int _totalSteps;

	private GPAchievementType _type;

	private GPAchievementState _state;

	[Obsolete("id is deprectaed, please use Id instead")]
	public string id
	{
		get
		{
			return Id;
		}
	}

	public string Id
	{
		get
		{
			return _id;
		}
		set
		{
			_id = value;
		}
	}

	[Obsolete("name is deprectaed, please use Name instead")]
	public string name
	{
		get
		{
			return Name;
		}
	}

	public string Name
	{
		get
		{
			return _name;
		}
		set
		{
			_name = value;
		}
	}

	[Obsolete("description is deprectaed, please use Description instead")]
	public string description
	{
		get
		{
			return Description;
		}
	}

	public string Description
	{
		get
		{
			return _description;
		}
		set
		{
			_description = value;
		}
	}

	[Obsolete("currentSteps is deprectaed, please use CurrentSteps instead")]
	public int currentSteps
	{
		get
		{
			return CurrentSteps;
		}
	}

	public int CurrentSteps
	{
		get
		{
			return _currentSteps;
		}
	}

	[Obsolete("totalSteps is deprectaed, please use TotalSteps instead")]
	public int totalSteps
	{
		get
		{
			return TotalSteps;
		}
	}

	public int TotalSteps
	{
		get
		{
			return _totalSteps;
		}
	}

	[Obsolete("type is deprectaed, please use Type instead")]
	public GPAchievementType type
	{
		get
		{
			return Type;
		}
	}

	public GPAchievementType Type
	{
		get
		{
			return _type;
		}
	}

	[Obsolete("state is deprectaed, please use State instead")]
	public GPAchievementState state
	{
		get
		{
			return State;
		}
	}

	public GPAchievementState State
	{
		get
		{
			return _state;
		}
	}

	public Texture2D Texture
	{
		get
		{
			return _Texture;
		}
		set
		{
			_Texture = value;
		}
	}

	public GPAchievement(string id, string name)
	{
		_id = id;
		_name = name;
	}

	public GPAchievement(string aId, string aName, string aDescr, string aCurentSteps, string aTotalSteps, string aState, string aType)
	{
		_id = aId;
		_name = aName;
		_description = aDescr;
		_currentSteps = Convert.ToInt32(aCurentSteps);
		_totalSteps = Convert.ToInt32(aTotalSteps);
		_type = PlayServiceUtil.GetAchievementTypeById(Convert.ToInt32(aType));
		_state = PlayServiceUtil.GetAchievementStateById(Convert.ToInt32(aState));
	}
}
