using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class TwitterPostingTask : AsyncTask
{
	private string _status = string.Empty;

	private Texture2D _texture;

	private TwitterManagerInterface _controller;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<TWResult> ActionComplete__BackingField = delegate
	{
	};

	public event Action<TWResult> ActionComplete
	{
		add
		{
			Action<TWResult> action = ActionComplete__BackingField;
			Action<TWResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionComplete__BackingField, (Action<TWResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<TWResult> action = ActionComplete__BackingField;
			Action<TWResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionComplete__BackingField, (Action<TWResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static TwitterPostingTask Cretae()
	{
		return new GameObject("TwitterPositngTask").AddComponent<TwitterPostingTask>();
	}

	public void Post(string status, Texture2D texture, TwitterManagerInterface controller)
	{
		_status = status;
		_texture = texture;
		_controller = controller;
		if (_controller.IsInited)
		{
			OnTWInited(null);
			return;
		}
		_controller.OnTwitterInitedAction += OnTWInited;
		_controller.Init();
	}

	private void OnTWInited(TWResult result)
	{
		_controller.OnTwitterInitedAction -= OnTWInited;
		if (_controller.IsAuthed)
		{
			OnTWAuth(new TWResult(true, "Auth Success"));
			return;
		}
		_controller.OnAuthCompleteAction += OnTWAuth;
		_controller.AuthenticateUser();
	}

	private void OnTWAuth(TWResult result)
	{
		_controller.OnAuthCompleteAction -= OnTWAuth;
		if (result.IsSucceeded)
		{
			_controller.OnPostingCompleteAction += OnPost;
			if (_texture != null)
			{
				_controller.Post(_status, _texture);
			}
			else
			{
				_controller.Post(_status);
			}
		}
		else
		{
			TWResult obj = new TWResult(false, "Auth Failed");
			ActionComplete__BackingField(obj);
		}
	}

	private void OnPost(TWResult res)
	{
		_controller.OnPostingCompleteAction -= OnPost;
		ActionComplete__BackingField(res);
	}
}
