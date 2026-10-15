using System;
using SA.Common.Pattern;
using UnityEngine;

public class AN_PlusButton
{
	private int _ButtonId;

	private TextAnchor _anchor = TextAnchor.MiddleCenter;

	private int _x;

	private int _y;

	private bool _IsShowed = true;

	public Action ButtonClicked = () =>
	{
	};

	private static int _nextId;

	public int ButtonId
	{
		get
		{
			return _ButtonId;
		}
	}

	public int x
	{
		get
		{
			return _x;
		}
	}

	public int y
	{
		get
		{
			return _y;
		}
	}

	public bool IsShowed
	{
		get
		{
			return _IsShowed;
		}
	}

	public TextAnchor anchor
	{
		get
		{
			return _anchor;
		}
	}

	public GoogleGravity gravity
	{
		get
		{
			switch (_anchor)
			{
			case TextAnchor.LowerCenter:
				return (GoogleGravity)81;
			case TextAnchor.LowerLeft:
				return GoogleGravity.BOTTOM | GoogleGravity.LEFT;
			case TextAnchor.LowerRight:
				return GoogleGravity.BOTTOM | GoogleGravity.RIGHT;
			case TextAnchor.MiddleCenter:
				return GoogleGravity.CENTER;
			case TextAnchor.MiddleLeft:
				return (GoogleGravity)19;
			case TextAnchor.MiddleRight:
				return (GoogleGravity)21;
			case TextAnchor.UpperCenter:
				return (GoogleGravity)49;
			case TextAnchor.UpperLeft:
				return (GoogleGravity)51;
			case TextAnchor.UpperRight:
				return (GoogleGravity)53;
			default:
				return GoogleGravity.TOP;
			}
		}
	}

	private static int nextId
	{
		get
		{
			_nextId++;
			return _nextId;
		}
	}

	public AN_PlusButton(string url, AN_PlusBtnSize btnSize, AN_PlusBtnAnnotation annotation)
	{
		_ButtonId = nextId;
		AN_PlusButtonProxy.createPlusButton(_ButtonId, url, (int)btnSize, (int)annotation);
		Singleton<AN_PlusButtonsManager>.Instance.RegisterButton(this);
	}

	public void SetGravity(TextAnchor btnAnchor)
	{
		_anchor = btnAnchor;
		AN_PlusButtonProxy.setGravity((int)gravity, _ButtonId);
	}

	public void SetPosition(int btnX, int btnY)
	{
		_x = btnX;
		_y = btnY;
		AN_PlusButtonProxy.setPosition(_x, _y, _ButtonId);
	}

	public void Show()
	{
		_IsShowed = true;
		AN_PlusButtonProxy.show(_ButtonId);
	}

	public void Hide()
	{
		_IsShowed = false;
		AN_PlusButtonProxy.hide(_ButtonId);
	}

	public void Refresh()
	{
		AN_PlusButtonProxy.refresh(_ButtonId);
	}

	public void FireClickAction()
	{
		ButtonClicked();
	}
}
