using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class IOSSocialManager : Singleton<IOSSocialManager>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnFacebookPostStart__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnTwitterPostStart__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnInstagramPostStart__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<Result> OnFacebookPostResult__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<Result> OnTwitterPostResult__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<Result> OnInstagramPostResult__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<Result> OnMailResult__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<TextMessageComposeResult> OnTextMessageResult__BackingField = delegate
	{
	};

	public static event Action OnFacebookPostStart
	{
		add
		{
			Action action = OnFacebookPostStart__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnFacebookPostStart__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnFacebookPostStart__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnFacebookPostStart__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action OnTwitterPostStart
	{
		add
		{
			Action action = OnTwitterPostStart__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnTwitterPostStart__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnTwitterPostStart__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnTwitterPostStart__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action OnInstagramPostStart
	{
		add
		{
			Action action = OnInstagramPostStart__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInstagramPostStart__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnInstagramPostStart__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInstagramPostStart__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<Result> OnFacebookPostResult
	{
		add
		{
			Action<Result> action = OnFacebookPostResult__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnFacebookPostResult__BackingField, (Action<Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Result> action = OnFacebookPostResult__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnFacebookPostResult__BackingField, (Action<Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<Result> OnTwitterPostResult
	{
		add
		{
			Action<Result> action = OnTwitterPostResult__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnTwitterPostResult__BackingField, (Action<Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Result> action = OnTwitterPostResult__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnTwitterPostResult__BackingField, (Action<Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<Result> OnInstagramPostResult
	{
		add
		{
			Action<Result> action = OnInstagramPostResult__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInstagramPostResult__BackingField, (Action<Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Result> action = OnInstagramPostResult__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInstagramPostResult__BackingField, (Action<Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<Result> OnMailResult
	{
		add
		{
			Action<Result> action = OnMailResult__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnMailResult__BackingField, (Action<Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Result> action = OnMailResult__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnMailResult__BackingField, (Action<Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<TextMessageComposeResult> OnTextMessageResult
	{
		add
		{
			Action<TextMessageComposeResult> action = OnTextMessageResult__BackingField;
			Action<TextMessageComposeResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnTextMessageResult__BackingField, (Action<TextMessageComposeResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<TextMessageComposeResult> action = OnTextMessageResult__BackingField;
			Action<TextMessageComposeResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnTextMessageResult__BackingField, (Action<TextMessageComposeResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void ShareMedia(string text, Texture2D texture = null)
	{
	}

	public void TwitterPost(string text, string url = null, Texture2D texture = null)
	{
		OnTwitterPostStart__BackingField();
	}

	public void TwitterPostGif(string text, string url)
	{
	}

	public void FacebookPost(string text, string url = null, Texture2D texture = null)
	{
		OnFacebookPostStart__BackingField();
	}

	public void InstagramPost(Texture2D texture)
	{
		InstagramPost(texture, string.Empty);
	}

	public void InstagramPost(Texture2D texture, string message)
	{
		OnInstagramPostStart__BackingField();
	}

	public void WhatsAppShareText(string message)
	{
	}

	public void WhatsAppShareImage(Texture2D texture)
	{
	}

	public void SendMail(string subject, string body, string recipients)
	{
		SendMail(subject, body, recipients, null);
	}

	public void SendMail(string subject, string body, string recipients, Texture2D texture)
	{
		if (!(texture == null))
		{
		}
	}

	public void SendTextMessage(string body, string recepient, Action<TextMessageComposeResult> callback)
	{
		List<string> list = new List<string>();
		list.Add(recepient);
		SendTextMessage(body, list, callback);
	}

	public void SendTextMessage(string body, List<string> recepients, Action<TextMessageComposeResult> callback)
	{
		OnTextMessageResult += callback;
	}

	private void OnTextMessageComposeResult(string data)
	{
		int obj = Convert.ToInt32(data);
		OnTextMessageResult__BackingField((TextMessageComposeResult)obj);
		OnTextMessageResult__BackingField = delegate
		{
		};
	}

	private void OnTwitterPostFailed()
	{
		Result obj = new Result(new Error());
		OnTwitterPostResult__BackingField(obj);
	}

	private void OnTwitterPostSuccess()
	{
		Result obj = new Result();
		OnTwitterPostResult__BackingField(obj);
	}

	private void OnFacebookPostFailed()
	{
		Result obj = new Result(new Error());
		OnFacebookPostResult__BackingField(obj);
	}

	private void OnFacebookPostSuccess()
	{
		Result obj = new Result();
		OnFacebookPostResult__BackingField(obj);
	}

	private void OnMailFailed()
	{
		Result obj = new Result(new Error());
		OnMailResult__BackingField(obj);
	}

	private void OnMailSuccess()
	{
		Result obj = new Result();
		OnMailResult__BackingField(obj);
	}

	private void OnInstaPostSuccess()
	{
		Result obj = new Result();
		OnInstagramPostResult__BackingField(obj);
	}

	private void OnInstaPostFailed(string data)
	{
		int code = Convert.ToInt32(data);
		Error error = new Error(code, "Posting Failed");
		Result obj = new Result(error);
		OnInstagramPostResult__BackingField(obj);
	}
}
