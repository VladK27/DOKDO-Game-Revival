using System.Collections;
using Boomlagoon.JSON;
using UnityEngine;
using UnityEngine.Events;

namespace Percent.Http
{
	public class HttpsClient : MonoBehaviour
	{
		internal UnityAction<bool> onGETResponse;

		internal UnityAction<bool> onTextureResponse;

		private string url;

		internal void sendTextureRequest(string url)
		{
			StartCoroutine(sendTextureRequestCoroutine(url));
		}

		internal IEnumerator sendTextureRequestCoroutine(string url)
		{
			WWW www = new WWW(url);
			yield return www;
			if (!isRequestError(www))
			{
				if (isRequestSuccess(www))
				{
					onTextureResponseSuccess(www.texture);
				}
				else if (isStatus301MovedPermanently(www))
				{
					onTextureResponseSuccess(www.texture);
				}
				else if (isStatus302MovedTemporarily(www))
				{
					onTextureResponseSuccess(www.texture);
				}
				else
				{
					Logger.error("Https Texture response FAIL " + getStatusCode(www));
					onTextureResponseFail();
				}
			}
			else
			{
				Logger.error("Https Texture request FAIL : " + www.error);
				onTextureResponseFail();
				yield return null;
			}
			www.Dispose();
		}

		internal virtual void onTextureResponseSuccess(Texture2D texture)
		{
			if (onTextureResponse != null)
			{
				onTextureResponse(true);
			}
		}

		internal virtual void onTextureResponseFail()
		{
			if (onTextureResponse != null)
			{
				onTextureResponse(false);
			}
		}

		internal void sendGETRequest(string url, bool isResponseJson = false)
		{
			this.url = url;
			StartCoroutine(sendGETRequestCoroutine(isResponseJson));
		}

		internal IEnumerator sendGETRequestCoroutine(bool isResponseJson = false)
		{
			WWW www = new WWW(url);
			yield return www;
			if (!isRequestError(www))
			{
				if (isRequestSuccess(www))
				{
					if (isResponseJson)
					{
						onGETResponseSuccess(JSONObject.Parse(www.text));
					}
					else
					{
						onGETResponseSuccess();
					}
				}
				else if (isStatus301MovedPermanently(www))
				{
					if (isResponseJson)
					{
						onGETResponseSuccess(JSONObject.Parse(www.text));
					}
					else
					{
						onGETResponseSuccess();
					}
				}
				else if (isStatus302MovedTemporarily(www))
				{
					if (isResponseJson)
					{
						onGETResponseSuccess(JSONObject.Parse(www.text));
					}
					else
					{
						onGETResponseSuccess();
					}
				}
				else
				{
					Logger.error("Https GET response FAIL " + getStatusCode(www));
					onGETResponseFail();
				}
			}
			else
			{
				Logger.error("Https GET request FAIL : " + www.error);
				onGETResponseFail();
			}
			www.Dispose();
		}

		internal virtual void onGETResponseSuccess()
		{
			if (onGETResponse != null)
			{
				onGETResponse(true);
			}
		}

		internal virtual void onGETResponseSuccess(JSONObject json)
		{
			if (onGETResponse != null)
			{
				onGETResponse(true);
			}
		}

		internal virtual void onGETResponseFail()
		{
			if (onGETResponse != null)
			{
				onGETResponse(false);
			}
		}

		internal static string resolve(string url)
		{
			return PercentHttpConfig.HOST + url;
		}

		internal static void addResource<T>(ref string url, T resource)
		{
			url = url + PercentHttpConfig.SLASH + resource;
		}

		internal static bool isRequestError(WWW www)
		{
			if (www.error == null)
			{
				return false;
			}
			return true;
		}

		internal static bool isRequestSuccess(WWW www)
		{
			if (isStatus200OK(www))
			{
				return true;
			}
			return false;
		}

		private static bool isStatus200OK(WWW www)
		{
			if (getStatusCode(www).Contains("200"))
			{
				return true;
			}
			return false;
		}

		internal static bool isStatus301MovedPermanently(WWW www)
		{
			if (getStatusCode(www).Contains("301"))
			{
				return true;
			}
			return false;
		}

		internal static bool isStatus302MovedTemporarily(WWW www)
		{
			if (getStatusCode(www).Contains("302"))
			{
				return true;
			}
			return false;
		}

		private static string getStatusCode(WWW www)
		{
			string value;
			www.responseHeaders.TryGetValue(PercentHttpConfig.RES_STATUS, out value);
			return value;
		}

		private static string getLocation(WWW www)
		{
			string value;
			www.responseHeaders.TryGetValue(PercentHttpConfig.RES_LOCATION, out value);
			return value;
		}

		internal static string resourceIdOf(string url)
		{
			int num = url.LastIndexOf(PercentHttpConfig.SLASH);
			return url.Substring(num + 1);
		}

		internal static string finalRedirectionUrlOf(string weirdUrl)
		{
			int num = weirdUrl.LastIndexOf(PercentHttpConfig.COMMA);
			return (!num.Equals(-1)) ? weirdUrl.Substring(num + 1) : weirdUrl;
		}
	}
}
