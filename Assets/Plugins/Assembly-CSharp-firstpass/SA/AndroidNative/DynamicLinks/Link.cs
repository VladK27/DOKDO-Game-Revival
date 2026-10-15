using System.Collections.Generic;

namespace SA.AndroidNative.DynamicLinks
{
	public class Link
	{
		public class SocialMetaTagParameters
		{
			public class Builder
			{
				private SocialMetaTagParameters param;

				public Builder()
				{
					param = new SocialMetaTagParameters();
				}

				public Builder SetTitle(string title)
				{
					param.title = title;
					return this;
				}

				public Builder SetDescription(string description)
				{
					param.description = description;
					return this;
				}

				public Builder SetImageUrl(string url)
				{
					param.imageUrl = url;
					return this;
				}

				public SocialMetaTagParameters Build()
				{
					return param;
				}
			}

			private string title = string.Empty;

			private string description = string.Empty;

			private string imageUrl = string.Empty;

			public string Title
			{
				get
				{
					return title;
				}
			}

			public string Description
			{
				get
				{
					return description;
				}
			}

			public string ImageUrl
			{
				get
				{
					return imageUrl;
				}
			}

			private SocialMetaTagParameters()
			{
			}

			public Dictionary<string, object> Serialize()
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("title", title);
				dictionary.Add("description", description);
				dictionary.Add("image_url", imageUrl);
				return dictionary;
			}
		}

		public class ItunesConnectAnalyticsParameters
		{
			public class Builder
			{
				private ItunesConnectAnalyticsParameters param;

				public Builder()
				{
					param = new ItunesConnectAnalyticsParameters();
				}

				public Builder SetProviderToken(string token)
				{
					param.providerToken = token;
					return this;
				}

				public Builder SetAffiliateToken(string token)
				{
					param.affiliateToken = token;
					return this;
				}

				public Builder SetCampaignToken(string token)
				{
					param.campaignToken = token;
					return this;
				}

				public ItunesConnectAnalyticsParameters Build()
				{
					return param;
				}
			}

			private string providerToken = string.Empty;

			private string affiliateToken = string.Empty;

			private string campaignToken = string.Empty;

			public string CampaignToken
			{
				get
				{
					return campaignToken;
				}
			}

			public string AffiliateToken
			{
				get
				{
					return affiliateToken;
				}
			}

			public string ProviderToken
			{
				get
				{
					return providerToken;
				}
			}

			private ItunesConnectAnalyticsParameters()
			{
			}

			public Dictionary<string, object> Serialize()
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("provider_token", providerToken);
				dictionary.Add("affiliate_token", affiliateToken);
				dictionary.Add("campaign_token", campaignToken);
				return dictionary;
			}
		}

		public class GoogleAnalyticsParameters
		{
			public class Builder
			{
				private GoogleAnalyticsParameters param;

				public Builder()
				{
					param = new GoogleAnalyticsParameters();
				}

				public Builder SetSource(string source)
				{
					param.source = source;
					return this;
				}

				public Builder SetMedium(string medium)
				{
					param.medium = medium;
					return this;
				}

				public Builder SetCampaign(string campaign)
				{
					param.campaign = campaign;
					return this;
				}

				public Builder SetTerm(string term)
				{
					param.term = term;
					return this;
				}

				public Builder SetContent(string content)
				{
					param.content = content;
					return this;
				}

				public GoogleAnalyticsParameters Build()
				{
					return param;
				}
			}

			private string source = string.Empty;

			private string medium = string.Empty;

			private string campaign = string.Empty;

			private string term = string.Empty;

			private string content = string.Empty;

			public string Content
			{
				get
				{
					return content;
				}
			}

			public string Term
			{
				get
				{
					return term;
				}
			}

			public string Campaign
			{
				get
				{
					return campaign;
				}
			}

			public string Medium
			{
				get
				{
					return medium;
				}
			}

			public string Source
			{
				get
				{
					return source;
				}
			}

			private GoogleAnalyticsParameters()
			{
			}

			public Dictionary<string, object> Serialize()
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("source", source);
				dictionary.Add("medium", medium);
				dictionary.Add("campaign", campaign);
				dictionary.Add("term", term);
				dictionary.Add("content", content);
				return dictionary;
			}
		}

		public class IosParameters
		{
			public class Builder
			{
				private IosParameters param;

				public Builder(string appBundle)
				{
					param = new IosParameters();
					param.appBundle = appBundle;
				}

				public Builder SetFallbackUrl(string url)
				{
					param.fallbackUrl = url;
					return this;
				}

				public Builder SetCustomScheme(string scheme)
				{
					param.customScheme = scheme;
					return this;
				}

				public Builder SetIpadFallbackUrl(string ipadFallbackUrl)
				{
					param.ipadFallbackUrl = ipadFallbackUrl;
					return this;
				}

				public Builder SetIpadBundleId(string ipadBundleId)
				{
					param.ipadBundleId = ipadBundleId;
					return this;
				}

				public Builder SetAppStoreId(string appStoreId)
				{
					param.appStoreId = appStoreId;
					return this;
				}

				public Builder SetMinimumVersion(string minVersion)
				{
					param.minimumVersion = minVersion;
					return this;
				}

				public IosParameters Build()
				{
					return param;
				}
			}

			private string appBundle = string.Empty;

			private string appStoreId = string.Empty;

			private string minimumVersion = string.Empty;

			private string ipadBundleId = string.Empty;

			private string ipadFallbackUrl = string.Empty;

			private string fallbackUrl = string.Empty;

			private string customScheme = string.Empty;

			public string AppStoreId
			{
				get
				{
					return appStoreId;
				}
			}

			public string AppBundle
			{
				get
				{
					return appBundle;
				}
			}

			public string MinimumVersion
			{
				get
				{
					return minimumVersion;
				}
			}

			public string IpadBundleId
			{
				get
				{
					return ipadBundleId;
				}
			}

			public string IpadFallbackUrl
			{
				get
				{
					return ipadFallbackUrl;
				}
			}

			public string FallbackUrl
			{
				get
				{
					return fallbackUrl;
				}
			}

			public string CustomScheme
			{
				get
				{
					return customScheme;
				}
			}

			private IosParameters()
			{
			}

			public Dictionary<string, object> Serialize()
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("app_bundle", appBundle);
				dictionary.Add("appstore_id", appStoreId);
				dictionary.Add("minimum_version", minimumVersion);
				dictionary.Add("ipad_bundle_id", ipadBundleId);
				dictionary.Add("ipad_fallback_id", ipadFallbackUrl);
				dictionary.Add("fallback_url", fallbackUrl);
				dictionary.Add("custom_scheme", customScheme);
				return dictionary;
			}
		}

		public class AndroidParameters
		{
			public class Builder
			{
				private AndroidParameters param;

				public Builder(string appBundle)
				{
					param = new AndroidParameters();
					param.appBundle = appBundle;
				}

				public Builder SetFallbackUrl(string url)
				{
					param.fallbackUrl = url;
					return this;
				}

				public Builder SetMinimumVersion(int minVersion)
				{
					param.minimumVersion = minVersion;
					return this;
				}

				public AndroidParameters Build()
				{
					return param;
				}
			}

			private string appBundle = string.Empty;

			private string fallbackUrl = string.Empty;

			private int minimumVersion = 1;

			public string FallbackUrl
			{
				get
				{
					return fallbackUrl;
				}
			}

			public int MinimumVersion
			{
				get
				{
					return minimumVersion;
				}
			}

			public string AppBundle
			{
				get
				{
					return appBundle;
				}
			}

			private AndroidParameters()
			{
			}

			public Dictionary<string, object> Serialize()
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("app_bundle", appBundle);
				dictionary.Add("fallback_url", fallbackUrl);
				dictionary.Add("minimum_version", minimumVersion);
				return dictionary;
			}
		}

		public class Builder
		{
			private Link link;

			public Builder()
			{
				link = new Link();
			}

			public Builder SetLink(string url)
			{
				link.url = url;
				return this;
			}

			public Builder SetDynamicLinkDomain(string domain)
			{
				link.domain = domain;
				return this;
			}

			public Builder SetAndroidParameters(AndroidParameters param)
			{
				link.androidParams = param;
				return this;
			}

			public Builder SetIosParameters(IosParameters param)
			{
				link.iosParams = param;
				return this;
			}

			public Builder SetGoogleAnalyticsParameters(GoogleAnalyticsParameters param)
			{
				link.googleAnalyticsParams = param;
				return this;
			}

			public Builder SetItunesConnectAnalyticsParameters(ItunesConnectAnalyticsParameters param)
			{
				link.itunesAnalyticsParams = param;
				return this;
			}

			public Builder SetSocialMetaTagParameters(SocialMetaTagParameters param)
			{
				link.socialMetaTagParams = param;
				return this;
			}

			public Link Build()
			{
				return link;
			}
		}

		private string url = string.Empty;

		private string domain = string.Empty;

		private AndroidParameters androidParams;

		private IosParameters iosParams;

		private GoogleAnalyticsParameters googleAnalyticsParams;

		private ItunesConnectAnalyticsParameters itunesAnalyticsParams;

		private SocialMetaTagParameters socialMetaTagParams;

		public string Url
		{
			get
			{
				return url;
			}
		}

		public string Domain
		{
			get
			{
				return domain;
			}
		}

		public AndroidParameters AndroidParams
		{
			get
			{
				return androidParams;
			}
		}

		public IosParameters IosParams
		{
			get
			{
				return iosParams;
			}
		}

		public GoogleAnalyticsParameters GoogleAnalyticsParams
		{
			get
			{
				return googleAnalyticsParams;
			}
		}

		public ItunesConnectAnalyticsParameters ItunesAnalyticsParams
		{
			get
			{
				return itunesAnalyticsParams;
			}
		}

		public SocialMetaTagParameters SocialMetaTagParams
		{
			get
			{
				return socialMetaTagParams;
			}
		}

		private Link()
		{
		}

		public Dictionary<string, object> Serialize()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("link", url);
			dictionary.Add("domain", domain);
			if (androidParams != null)
			{
				dictionary.Add("android_params", androidParams.Serialize());
			}
			if (iosParams != null)
			{
				dictionary.Add("ios_params", iosParams.Serialize());
			}
			if (googleAnalyticsParams != null)
			{
				dictionary.Add("google_params", googleAnalyticsParams.Serialize());
			}
			if (itunesAnalyticsParams != null)
			{
				dictionary.Add("itunes_params", itunesAnalyticsParams.Serialize());
			}
			if (socialMetaTagParams != null)
			{
				dictionary.Add("social_params", socialMetaTagParams.Serialize());
			}
			return dictionary;
		}
	}
}
