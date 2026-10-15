using System.Collections;
using Percent.Event;
using Percent.Http;
using Percent.Tween;
using UnityEngine;
using UnityEngine.UI;

namespace Percent.Badge
{
	public class Badge : HttpsClient
	{
		public bool refreshOnEnable = true;

		private BadgeData pickedBadge;

		private RawImage targetRenderer;

		private ImageTool imageTool;

		private ColorTween showTween;

		private ClickTracker clickTracker;

		private static readonly int VALUE_MAX_LOAD_TEXTURE_RETRY = 20;

		private bool isLocalCache;

		private void Awake()
		{
			targetRenderer = GetComponent<RawImage>();
			imageTool = new ImageTool();
			showTween = GetComponent<ColorTween>();
			clickTracker = base.gameObject.AddComponent<ClickTracker>();
		}

		private void OnEnable()
		{
			if (refreshOnEnable)
			{
				StartCoroutine(pollingData());
			}
		}

		private void OnDisable()
		{
			showTween.resetToBegining();
		}

		private IEnumerator pollingData()
		{
			int retryCount = 0;
			do
			{
				retryCount++;
				yield return null;
			}
			while (!isLoadReady(retryCount));
			ifBadgePoolIsNullThenLoadOldPool();
			if (!PromotionData.isBadgePoolNull())
			{
				pickRandomBadgeUp();
				loadTexture();
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}

		private bool isLoadReady(int retryCount)
		{
			if (retryCount >= VALUE_MAX_LOAD_TEXTURE_RETRY || !PromotionData.isBadgePoolNull())
			{
				return true;
			}
			return false;
		}

		private void ifBadgePoolIsNullThenLoadOldPool()
		{
			if (PromotionData.isBadgePoolNull())
			{
				loadBadgePoolFromLocalCache();
			}
		}

		private void loadBadgePoolFromLocalCache()
		{
			PromotionData.badgePool = BadgePoolCacher.load();
		}

		private void pickRandomBadgeUp()
		{
			int max = PromotionData.badgePool.Length;
			int num = Random.Range(0, max);
			pickedBadge.gameId = PromotionData.badgePool[num].gameId;
			pickedBadge.iconUrl = PromotionData.badgePool[num].iconUrl;
			pickedBadge.storeUrl = PromotionData.badgePool[num].storeUrl;
		}

		private void loadTexture()
		{
			if (!imageTool.isCacheExist(HttpsClient.resourceIdOf(pickedBadge.iconUrl)))
			{
				sendTextureRequest(pickedBadge.iconUrl);
			}
			else
			{
				tryToLoadCacheIfFailSendRequest();
			}
		}

		private void tryToLoadCacheIfFailSendRequest()
		{
			string resourceId = HttpsClient.resourceIdOf(pickedBadge.iconUrl);
			Texture2D texture2D = imageTool.loadTextureFromCache(resourceId);
			if (texture2D == null)
			{
				Logger.error("Load badge cache texture from local is FAIL. Try to download via https : " + pickedBadge.iconUrl);
				sendTextureRequest(pickedBadge.iconUrl);
			}
			else
			{
				onTextureResponseSuccess(texture2D);
				isLocalCache = true;
			}
		}

		internal override void onTextureResponseSuccess(Texture2D texture)
		{
			targetRenderer.texture = texture;
			targetRenderer.color = Color.white;
			showTween.play();
			saveCacheTextureToLocal(texture);
			if (!isLocalCache)
			{
				BadgePoolCacher.save();
			}
			base.onTextureResponseSuccess(texture);
		}

		private void saveCacheTextureToLocal(Texture2D texture)
		{
			string resourceId = HttpsClient.resourceIdOf(pickedBadge.iconUrl);
			if (!imageTool.isCacheExist(resourceId))
			{
				imageTool.saveTexture(resourceId, texture);
			}
		}

		public void onClick()
		{
			Util.goMarket(pickedBadge.storeUrl);
			clickTracker.trigger(PromotionType.BADGE, pickedBadge.gameId);
		}
	}
}
