using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class WP8ProductTemplate
{
	private Texture2D _texture;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Texture2D> ProdcutImageLoaded__BackingField = delegate
	{
	};

	public string Description { get; set; }

	public string ImgURI { get; set; }

	public string Name { get; set; }

	public string ProductId { get; set; }

	public string Price { get; set; }

	public string TransactionID { get; set; }

	public WP8PurchaseProductType Type { get; set; }

	[Obsolete("This property is obsolete. Use 'IsPurchased' property instead")]
	public bool isPurchased
	{
		get
		{
			return IsPurchased;
		}
		set
		{
			IsPurchased = value;
		}
	}

	public bool IsPurchased { get; set; }

	[Obsolete("This property is obsolete. Use 'Texture' property instead")]
	public Texture2D texture
	{
		get
		{
			return _texture;
		}
	}

	public Texture2D Texture
	{
		get
		{
			return _texture;
		}
	}

	public event Action<Texture2D> ProdcutImageLoaded
	{
		add
		{
			Action<Texture2D> action = ProdcutImageLoaded__BackingField;
			Action<Texture2D> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ProdcutImageLoaded__BackingField, (Action<Texture2D>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Texture2D> action = ProdcutImageLoaded__BackingField;
			Action<Texture2D> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ProdcutImageLoaded__BackingField, (Action<Texture2D>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public void LoadProductImage()
	{
		if (_texture != null)
		{
			ProdcutImageLoaded__BackingField(_texture);
			return;
		}
		WPN_TextureLoader wPN_TextureLoader = WPN_TextureLoader.Create();
		wPN_TextureLoader.TextureLoaded += HandleTextureLoaded;
		wPN_TextureLoader.LoadTexture(ImgURI);
	}

	private void HandleTextureLoaded(Texture2D texture)
	{
		_texture = texture;
		ProdcutImageLoaded__BackingField(_texture);
	}
}
