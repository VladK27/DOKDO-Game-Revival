using System.Collections;
using System.Collections.Generic;
using SA.Common.Pattern;
using UnityEngine;

namespace SA.Analytics.Google
{
	public class CacheQueue : Singleton<CacheQueue>
	{
		private bool IsWorking;

		private CachedRequest _CurrentRequest;

		private List<CachedRequest> _CurrentQueue;

		public void Run()
		{
			if (!IsWorking)
			{
				IsWorking = true;
				_CurrentQueue = RequestCache.CurrenCachedRequests;
				if (_CurrentQueue.Count == 0)
				{
					Stop();
					return;
				}
				_CurrentRequest = _CurrentQueue[0];
				StartCoroutine(Send(_CurrentRequest));
			}
		}

		private void Stop()
		{
			IsWorking = false;
			_CurrentRequest = null;
			_CurrentQueue = null;
		}

		private void Continue()
		{
			_CurrentQueue.Remove(_CurrentRequest);
			if (_CurrentQueue.Count == 0)
			{
				RequestCache.Clear();
				Stop();
			}
			else
			{
				RequestCache.CacheRequests(_CurrentQueue);
				_CurrentRequest = _CurrentQueue[0];
				StartCoroutine(Send(_CurrentRequest));
			}
		}

		private IEnumerator Send(CachedRequest request)
		{
			string HitRequest = request.RequestBody;
			if (GA_Settings.Instance.IsQueueTimeEnabled)
			{
				HitRequest = HitRequest + "&qt" + request.Delay;
			}
			WWW www = Manager.SendSkipCache(HitRequest);
			yield return www;
			if (www.error != null)
			{
				Stop();
			}
			else
			{
				yield return new WaitForSeconds(2f);
				Continue();
			}
			yield return null;
		}
	}
}
