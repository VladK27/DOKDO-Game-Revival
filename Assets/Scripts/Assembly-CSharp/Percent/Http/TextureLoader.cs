using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Percent.Http
{
	public class TextureLoader : HttpsClient
	{
		private RawImage targetRenderer;

		private ImageTool imageTool;

		private string imageUrl;

		private void Awake()
		{
			targetRenderer = GetComponent<RawImage>();
			imageTool = new ImageTool();
		}

		internal void render(string imageUrl, UnityAction<bool> onResponse)
		{
			this.imageUrl = imageUrl;
			onTextureResponse = onResponse;
			if (!imageTool.isCacheExist(HttpsClient.resourceIdOf(imageUrl)))
			{
				sendTextureRequest(imageUrl);
			}
			else
			{
				tryToLoadCacheIfFailSendRequest(imageUrl);
			}
		}

		private void tryToLoadCacheIfFailSendRequest(string imageUrl)
		{
			Texture2D texture2D = imageTool.loadTextureFromCache(HttpsClient.resourceIdOf(imageUrl));
			if (texture2D == null)
			{
				Logger.error("Load Cache texture from local is FAIL. Try to download via https : " + imageUrl);
				sendTextureRequest(imageUrl);
			}
			else
			{
				onTextureResponseSuccess(texture2D);
			}
		}

		internal override void onTextureResponseSuccess(Texture2D texture)
		{
			targetRenderer.texture = texture;
			saveCacheTextureToLocal(texture);
			base.onTextureResponseSuccess(texture);
		}

		private void saveCacheTextureToLocal(Texture2D texture)
		{
			string resourceId = HttpsClient.resourceIdOf(imageUrl);
			if (!imageTool.isCacheExist(resourceId))
			{
				imageTool.saveTexture(resourceId, texture);
			}
		}
	}
}
