using System;
using UnityEngine;

public class GC_Achievement
{
	public bool IsOpen = true;

	[SerializeField]
	private string _title = string.Empty;

	[SerializeField]
	private string _id = string.Empty;

	[SerializeField]
	private string _description = string.Empty;

	[SerializeField]
	private float _progress;

	[SerializeField]
	private int _pointValue;

	[SerializeField]
	private bool _isHidden;

	[SerializeField]
	private bool _isUnlocked;

	[SerializeField]
	private int _position;

	private DateTime _dateUnlocked = DateTime.Now;

	[SerializeField]
	private Texture2D _Texture;

	public string Title
	{
		get
		{
			return _title;
		}
		set
		{
			_title = value;
		}
	}

	public string Identifier
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

	public float Progress
	{
		get
		{
			return _progress;
		}
	}

	public int PointValue
	{
		get
		{
			return _pointValue;
		}
	}

	public bool IsHidden
	{
		get
		{
			return _isHidden;
		}
	}

	public bool IsUnlocked
	{
		get
		{
			return _isUnlocked;
		}
	}

	public int Position
	{
		get
		{
			return _position;
		}
	}

	public DateTime DateUnlocked
	{
		get
		{
			return _dateUnlocked;
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

	public GC_Achievement()
	{
		_title = "New Achievement";
	}
}
