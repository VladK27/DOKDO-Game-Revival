using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Models;
using UnityEngine;

public class GC_Player
{
	private string _playerId = string.Empty;

	private string _name = string.Empty;

	private string _avatarUrl = string.Empty;

	private Texture2D _avatar;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Texture2D> AvatarLoaded__BackingField = delegate
	{
	};

	public string PlayerId
	{
		get
		{
			return _playerId;
		}
	}

	public string Name
	{
		get
		{
			return _name;
		}
	}

	public string AvatarUrl
	{
		get
		{
			return _avatarUrl;
		}
	}

	public Texture2D Avatar
	{
		get
		{
			return _avatar;
		}
	}

	public event Action<Texture2D> AvatarLoaded
	{
		add
		{
			Action<Texture2D> action = AvatarLoaded__BackingField;
			Action<Texture2D> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref AvatarLoaded__BackingField, (Action<Texture2D>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Texture2D> action = AvatarLoaded__BackingField;
			Action<Texture2D> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref AvatarLoaded__BackingField, (Action<Texture2D>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public void LoadAvatar()
	{
		if (_avatar != null)
		{
			AvatarLoaded__BackingField(_avatar);
			return;
		}
		UnityEngine.Debug.Log("Amazon Player Avatar Started to Load!");
		WWWTextureLoader wWWTextureLoader = WWWTextureLoader.Create();
		wWWTextureLoader.OnLoad += OnProfileImageLoaded;
		wWWTextureLoader.LoadTexture(_avatarUrl);
	}

	private void OnProfileImageLoaded(Texture2D texture)
	{
		UnityEngine.Debug.Log("Amazon Player OnProfileImageLoaded" + texture);
		_avatar = texture;
		AvatarLoaded__BackingField(_avatar);
	}
}
