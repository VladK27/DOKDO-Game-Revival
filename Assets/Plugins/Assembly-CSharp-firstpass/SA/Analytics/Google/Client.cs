using System.Text;
using UnityEngine;

namespace SA.Analytics.Google
{
	public class Client
	{
		private const string PROTOCOL_VERSION = "v=1";

		private const string HTTP_URL = "http://www.google-analytics.com/collect";

		private const string HTTPS_URL = "https://ssl.google-analytics.com/collect";

		public string TrackingID;

		public string ClientID;

		public string AppName;

		public string AppVersion;

		private string DefaultHitData;

		private StringBuilder builder = new StringBuilder(256, 8192);

		private HitType currentHitType;

		private string DataSendUrl;

		private string lastScreenName = string.Empty;

		private string userId = string.Empty;

		public string AnalyticsHost
		{
			get
			{
				return DataSendUrl;
			}
		}

		public string LastScreenName
		{
			get
			{
				return lastScreenName;
			}
		}

		public string UserId
		{
			get
			{
				return userId;
			}
		}

		public Client(string anonymousClientID)
		{
			ClientID = EscapeString(anonymousClientID, true);
			AppName = EscapeString(GA_Settings.Instance.AppName, true);
			AppVersion = EscapeString(GA_Settings.Instance.AppVersion, true);
			if (GA_Settings.Instance.UseHTTPS)
			{
				DataSendUrl = "https://ssl.google-analytics.com/collect";
			}
			else
			{
				DataSendUrl = "http://www.google-analytics.com/collect";
			}
			GenerateHeaders(GA_Settings.Instance.GetCurentProfile().TrackingID);
		}

		public void GenerateHeaders(string trackingId)
		{
			TrackingID = EscapeString(trackingId);
			builder.Length = 0;
			builder.Append("v=1");
			builder.Append("&tid=");
			builder.Append(TrackingID);
			builder.Append("&cid=");
			builder.Append(ClientID);
			builder.Append("&an=");
			builder.Append(AppName);
			builder.Append("&av=");
			builder.Append(AppVersion);
			if (!string.IsNullOrEmpty(UserId))
			{
				builder.Append("&uid=");
				builder.Append(UserId);
			}
			DefaultHitData = builder.ToString();
			builder.Length = 0;
		}

		public void SetAnonymizeIP()
		{
			builder.Append("&aip=0");
		}

		public void SetQueueTime(int time)
		{
			builder.Append("&qt=");
			builder.Append(time);
		}

		public void StartSession()
		{
			builder.Append("&sc=start");
		}

		public void EndSession()
		{
			builder.Append("&sc=end");
		}

		public void IPOverride(string ip)
		{
			AppendData("uip", ip);
		}

		public void UserAgentOverride(string userAgent)
		{
			AppendData("ua", userAgent);
		}

		public void SetDocumentReferrer(string url)
		{
			AppendData("dr", url, "Document Referrer", 2048);
		}

		public void SetCampaignName(string name)
		{
			AppendData("cn", name, "Campaign Name", 100);
		}

		public void SetCampaignSource(string source)
		{
			AppendData("cs", source, "Campaign Source", 100);
		}

		public void SetCampaignMedium(string medium)
		{
			AppendData("cm", medium, "Campaign Medium", 50);
		}

		public void AddCampaignKeyword(string keyword)
		{
			AppendData("ck", keyword, "Campaign Keyword", 500);
		}

		public void SetCampaignContent(string content)
		{
			AppendData("cc", content, "Campaign Content", 500);
		}

		public void SetCampaignID(string id)
		{
			AppendData("ci", id, "Campaign ID", 500);
		}

		public void SetGoogleAdWordsID(string id)
		{
			AppendData("gclid", id);
		}

		public void SetGoogleDisplayAdsID(string id)
		{
			AppendData("dclid", id);
		}

		public void SetUserId(string id)
		{
			userId = EscapeString(id, true);
			builder.Append("&userId=");
			builder.Append(userId);
		}

		public void SetScreenResolution(int width, int height)
		{
			builder.Append("&sr=");
			builder.Append(width);
			builder.Append('x');
			builder.Append(height);
		}

		public void SetViewportSize(int width, int height)
		{
			builder.Append("&vp=");
			builder.Append(width);
			builder.Append('x');
			builder.Append(height);
		}

		public void SetDocumentEncoding(string encoding)
		{
			AppendData("de", encoding);
		}

		public void SetScreenColors(string colorsBuffer)
		{
			AppendData("sd", colorsBuffer);
		}

		public void SetUserLanguage(string lang)
		{
			AppendData("ul", lang);
		}

		public void SetJavaEnablede(bool isEnabled)
		{
			string val = "0";
			if (isEnabled)
			{
				val = "1";
			}
			AppendData("je", val);
		}

		public void SetFlashVersion(string version)
		{
			AppendData("fl", version);
		}

		public void SetHitType(HitType hit)
		{
			AppendData("t", hit.ToString().ToLower());
		}

		public void SetNoInteractionHit()
		{
			builder.Append("&ni=1");
		}

		public void SetDocumentlocationURL(string url)
		{
			AppendData("dl", url, "Document location URL", 2048);
		}

		public void SetDocumentHostName(string host)
		{
			AppendData("dh", host, "Document Host Name", 100);
		}

		public void SetDocumentPath(string path)
		{
			AppendData("dp", path, "Document Path", 2048);
		}

		public void SetDocumentTitle(string title)
		{
			AppendData("dt", title, "Document Title", 1500);
		}

		public void SetScreenName(string name)
		{
			lastScreenName = name;
			AppendData("cd", name, "Screen Name", 2048);
		}

		public void SetLinkID(string id)
		{
			AppendData("linkid", id);
		}

		public void SetApplicationName(string name)
		{
			AppendData("an", name, "Application Name", 100);
		}

		public void SetApplicationVersion(string version)
		{
			AppendData("av", version, "Application Version", 100);
		}

		public void SetApplicationInstallerID(string identifier)
		{
			AppendData("aiid", identifier, "Application Installer ID", 150);
		}

		public void SetEventCategory(string ec)
		{
			AppendData("ec", ec, "Event Category", 150, HitType.EVENT);
		}

		public void SetEventAction(string ea)
		{
			AppendData("ea", ea, "Event Action", 500, HitType.EVENT);
		}

		public void SetEventLabel(string el)
		{
			AppendData("el", el, "Event Label", 500, HitType.EVENT);
		}

		public void SetEventValue(int val)
		{
			AppendData("ev", val.ToString(), string.Empty, 0, HitType.EVENT);
		}

		public void SetTransactionID(string ti)
		{
			AppendData("ti", ti, "Transaction ID", 500, HitType.TRANSACTION, HitType.ITEM);
		}

		public void SetTransactionAffiliation(string ta)
		{
			AppendData("ta", ta, "Transaction Affiliation", 500, HitType.TRANSACTION);
		}

		public void SetTransactionRevenue(float tr)
		{
			AppendData("tr", FloatToCurrency(tr), string.Empty, 0, HitType.TRANSACTION);
		}

		public void SetTransactionShipping(float ts)
		{
			AppendData("ts", FloatToCurrency(ts), string.Empty, 0, HitType.TRANSACTION);
		}

		public void SetTransactionTax(float tt)
		{
			AppendData("tt", FloatToCurrency(tt), string.Empty, 0, HitType.TRANSACTION);
		}

		public void SetTransactionCouponCode(string tcc)
		{
			AppendData("tcc", tcc);
		}

		public void SetItemName(string name)
		{
			AppendData("in", name, "Item Name", 500, HitType.ITEM);
		}

		public void SetItemPrice(float ip)
		{
			AppendData("ip", FloatToCurrency(ip), string.Empty, 0, HitType.ITEM);
		}

		public void SetItemQuantity(int iq)
		{
			AppendData("iq", iq.ToString(), string.Empty, 0, HitType.ITEM);
		}

		public void SetItemCode(string ic)
		{
			AppendData("ic", ic, "Item Code", 500, HitType.ITEM);
		}

		public void SetItemCategory(string iv)
		{
			AppendData("iv", iv, "Item Category", 500, HitType.ITEM);
		}

		public void SetCurrencyCode(string cu)
		{
			AppendData("cu", cu, "Currency Code", 10, HitType.ITEM, HitType.TRANSACTION);
		}

		public void SetProductSKU(int productIndex, string sku)
		{
			AppendData("pr" + productIndex + "id", sku, "Product SKU", 500);
		}

		public void SetSetProductName(int productIndex, string name)
		{
			AppendData("pr" + productIndex + "nm", name, "Product Name", 500);
		}

		public void SetProductBrand(int productIndex, string brand)
		{
			AppendData("pr" + productIndex + "br", brand, "Product Brand ", 500);
		}

		public void SetProductCategory(int productIndex, string category)
		{
			AppendData("pr" + productIndex + "ca", category, "Product Category ", 500);
		}

		public void SetProductVariant(int productIndex, string variant)
		{
			AppendData("pr" + productIndex + "va", variant, "Product Variant ", 500);
		}

		public void SetProductPrice(int productIndex, float prise)
		{
			AppendData("pr" + productIndex + "pr", FloatToCurrency(prise));
		}

		public void SetProductQuantity(int productIndex, int quantit)
		{
			AppendData("pr" + productIndex + "qt", quantit.ToString());
		}

		public void SetProductCouponCode(int productIndex, string couponCode)
		{
			AppendData("pr" + productIndex + "cc", couponCode, "Product Coupon Code", 500);
		}

		public void SetProductPosition(int productIndex, int pos)
		{
			AppendData("pr" + productIndex + "ps", pos.ToString());
		}

		public void SetProductCustomDimension(int productIndex, int index, string val)
		{
			AppendData("pr" + productIndex + "cd" + index, val);
		}

		public void SetProductCustomMetric(int productIndex, int index, int metric)
		{
			AppendData("pr" + productIndex + "cm" + index, metric.ToString());
		}

		public void SetProductAction(string pa)
		{
			AppendData("pa", pa);
		}

		public void SetProductActionList(string val)
		{
			AppendData("pal", val);
		}

		public void SetCheckoutStep(int cos)
		{
			AppendData("cos", cos.ToString());
		}

		public void SetCheckoutStepOption(string col)
		{
			AppendData("col", col);
		}

		public void SetSocialNetwork(string sn)
		{
			AppendData("sn", sn, "Social Network", 50, HitType.SOCIAL);
		}

		public void SetSocialAction(string action)
		{
			AppendData("sa", action, "Social Action", 50, HitType.SOCIAL);
		}

		public void SetSocialActionTarget(string target)
		{
			AppendData("st", target, "Social Action Target", 2048, HitType.SOCIAL);
		}

		public void SetUserTimingCategory(string category)
		{
			AppendData("utc", category, "User Timing Category", 150, HitType.TIMING);
		}

		public void SetUserTimingVariableName(string name)
		{
			AppendData("utv", name, "User Timing Variable Name", 500, HitType.TIMING);
		}

		public void SetUserTimingTime(int time)
		{
			AppendData("utt", time.ToString(), "User Timing Time", 0, HitType.TIMING);
		}

		public void SetUserTimingLabel(string label)
		{
			AppendData("utl", label, "User Timing Label", 500, HitType.TIMING);
		}

		public void SetPageLoadTime(int time)
		{
			AppendData("plt", time.ToString(), "Page Load Time", 0, HitType.TIMING);
		}

		public void SetDNSTime(int time)
		{
			AppendData("dns", time.ToString(), "DNS Time", 0, HitType.TIMING);
		}

		public void SetPageDownloadTime(int time)
		{
			AppendData("pdt", time.ToString(), string.Empty, 0, HitType.TIMING);
		}

		public void SetRedirectResponseTime(int time)
		{
			AppendData("rrt", time.ToString(), string.Empty, 0, HitType.TIMING);
		}

		public void SetTCPConnectTime(int time)
		{
			AppendData("tcp", time.ToString(), string.Empty, 0, HitType.TIMING);
		}

		public void SetServerResponseTime(int time)
		{
			AppendData("srt", time.ToString(), string.Empty, 0, HitType.TIMING);
		}

		public void SetPromotionID(int index, string id)
		{
			AppendData("promo" + index + "id", id);
		}

		public void SetPromotionName(int index, string nm)
		{
			AppendData("promo" + index + "nm", nm);
		}

		public void SetPromotionCreative(int index, string cr)
		{
			AppendData("promo" + index + "cr", cr);
		}

		public void SetPromotionPosition(int index, string ps)
		{
			AppendData("promo" + index + "ps", ps);
		}

		public void SetPromotionAction(string promoa)
		{
			AppendData("promoa", promoa);
		}

		public void SetExceptionDescription(string description)
		{
			if (description.Length > 149)
			{
				Debug.Log("Exception Description is too big, trimmed to 150 characters");
				description = description.Substring(0, 149);
			}
			AppendData("exd", description, "Exception Description", 150, HitType.EXCEPTION);
		}

		public void SetIsFatalException(bool isFatal)
		{
			string val = "0";
			if (isFatal)
			{
				val = "1";
			}
			AppendData("exf", val, string.Empty, 0, HitType.EXCEPTION);
		}

		public void SetNonInteractionFlag()
		{
			AppendData("ni", "1");
		}

		public void SetCustomDimension(int index, string value)
		{
			AppendData("cd" + index, value, "Custom Dimension", 150);
		}

		public void SetCustomMetric(int index, int value)
		{
			AppendData("cm" + index, value.ToString());
		}

		public void SetExperimentID(string id)
		{
			AppendData("xid", id, "Experiment ID", 40);
		}

		public void SetExperimentVariant(string variant)
		{
			AppendData("xvar", variant);
		}

		public void SendPageHit(string host, string page, string title, string description = "", string linkId = "")
		{
			CreateHit(HitType.PAGEVIEW);
			SetDocumentHostName(host);
			SetDocumentPath(page);
			SetDocumentTitle(title);
			if (!description.Equals(string.Empty))
			{
				SetScreenName(description);
			}
			if (!linkId.Equals(string.Empty))
			{
				SetLinkID(linkId);
			}
			Send();
		}

		public void SendEventHit(string category, string action, string label = "", int val = -1, bool trackLevelName = false)
		{
			CreateHit(HitType.EVENT);
			SetEventCategory(category);
			SetEventAction(action);
			if (trackLevelName)
			{
				SetScreenName(GA_Settings.Instance.LevelPrefix + Manager.LoadedLevelName + GA_Settings.Instance.LevelPostfix);
			}
			if (!label.Equals(string.Empty))
			{
				SetEventLabel(label);
			}
			if (val != -1)
			{
				SetEventValue(val);
			}
			Send();
		}

		public void SendTransactionHit(string id, string affiliation = "", string currencyCode = "", float revenue = 0f, float shipping = 0f, float tax = 0f)
		{
			CreateHit(HitType.TRANSACTION);
			SetTransactionID(id);
			if (!string.IsNullOrEmpty(affiliation))
			{
				SetTransactionAffiliation(affiliation);
			}
			if (!string.IsNullOrEmpty(currencyCode))
			{
				SetCurrencyCode(currencyCode);
			}
			if (revenue != 0f)
			{
				SetTransactionRevenue(revenue);
			}
			if (shipping != 0f)
			{
				SetTransactionShipping(shipping);
			}
			if (tax != 0f)
			{
				SetTransactionTax(tax);
			}
			Send();
		}

		public void SendItemHit(string transactionId, string name, string SKU, float price, int quantity = 1, string category = "", string currencyCode = "")
		{
			CreateHit(HitType.ITEM);
			SetTransactionID(transactionId);
			SetItemName(name);
			SetItemCode(SKU);
			SetItemPrice(price);
			SetItemQuantity(quantity);
			if (!string.IsNullOrEmpty(category))
			{
				SetItemCategory(category);
			}
			if (!string.IsNullOrEmpty(currencyCode))
			{
				SetCurrencyCode(currencyCode);
			}
			Send();
		}

		public void SendSocialHit(string action, string network, string target)
		{
			CreateHit(HitType.SOCIAL);
			SetSocialAction(action);
			SetSocialNetwork(network);
			SetSocialActionTarget(target);
			Send();
		}

		public void SendExceptionHit(string description, bool IsFatal = false)
		{
			CreateHit(HitType.EXCEPTION);
			SetExceptionDescription(description);
			if (IsFatal)
			{
				SetIsFatalException(IsFatal);
			}
			Send();
		}

		public void SendUserTimingHit(string category, string variable, int time, string label)
		{
			CreateHit(HitType.TIMING);
			SetUserTimingCategory(category);
			SetUserTimingVariableName(variable);
			SetUserTimingTime(time);
			SetUserTimingLabel(label);
			Send();
		}

		public void SendScreenHit(string screenName)
		{
			CreateHit(HitType.APPVIEW);
			SetScreenName(screenName);
			Send();
		}

		public void CreateHit(HitType hit)
		{
			currentHitType = hit;
			builder.Length = 0;
			builder.Append(DefaultHitData);
			SetHitType(hit);
		}

		public void Send()
		{
			if (!GA_Settings.Instance.IsDisabled)
			{
				builder.Append("&z=");
				builder.Append(Random.Range(0, int.MaxValue) ^ 0xD60);
				string request = builder.ToString();
				builder.Length = 0;
				Manager.Send(request);
			}
		}

		public WWW GenerateWWW(byte[] data)
		{
			return new WWW(DataSendUrl, data);
		}

		public void AppendData(string protocolToken, string val)
		{
			AppendData(protocolToken, val, string.Empty, 0);
		}

		public void AppendData(string protocolToken, string val, string action, int maxSize, params HitType[] supportedHitTypes)
		{
			if (supportedHitTypes.Length > 0)
			{
				bool flag = false;
				foreach (HitType hitType in supportedHitTypes)
				{
					if (hitType == currentHitType)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					Debug.LogWarning("Google Analytics: " + action + " not supported for hit type  " + currentHitType);
					return;
				}
			}
			string text = EscapeString(val);
			builder.Append("&");
			builder.Append(protocolToken);
			builder.Append("=");
			builder.Append(text);
			if (maxSize > 0)
			{
				CheckDataLength(action, text, maxSize);
			}
		}

		private string FloatToCurrency(float val)
		{
			return val.ToString("n2");
		}

		private void CheckDataLength(string action, string data, int maxLength)
		{
			if (string.IsNullOrEmpty(data))
			{
				Debug.LogWarning("Google Analytics: " + action + " data is null");
			}
			else if (data.Length > maxLength)
			{
				Debug.LogWarning("Google Analytics: " + action + " is too long, max size " + maxLength + " bytes (" + data + ")");
			}
		}

		private string EscapeString(string original)
		{
			return EscapeString(original, false);
		}

		private string EscapeString(string original, bool forced)
		{
			if (forced)
			{
				return WWW.EscapeURL(original);
			}
			if (GA_Settings.Instance.StringEscaping)
			{
				return WWW.EscapeURL(original);
			}
			return original;
		}
	}
}
