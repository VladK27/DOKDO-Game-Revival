using Boomlagoon.JSON;
using Percent.View;
using UnityEngine;
using UnityEngine.Events;

namespace Percent.Http
{
	public class TextLoader : HttpsClient
	{
		private UnityAction<bool> onReceieveText;

		public PrivacyPopup privacyPopup;

		public TextAsset percentDescription;

		public TextAsset zzooDescription;

		public string readDeafultDescription()
		{
			switch (CrossPromotion.getCompany())
			{
			case Company.Percent:
				return percentDescription.ToString();
			case Company.ZZOO:
				return zzooDescription.ToString();
			default:
				return percentDescription.ToString();
			}
		}

		public void request(UnityAction<bool> onReceieveText)
		{
			this.onReceieveText = onReceieveText;
			sendGETRequest(getUrl(), true);
		}

		private string getUrl()
		{
			string prefix = PercentHttpConfig.HOST + PercentHttpConfig.GET_REQ_V3;
			HttpsClient.addResource(ref prefix, PercentHttpConfig.REQ_RESOURCE_PRIVACY_POLICY);
			HttpsClient.addResource(ref prefix, PercentHttpConfig.SLASH);
			Parameter parameter = new Parameter(prefix);
			parameter.addParamater(PercentHttpConfig.RES_PARAM_GAME_ID, Config.GAME_ID);
			if (CrossPromotion.instance.testEURegion)
			{
				parameter.addParamater(PercentHttpConfig.RES_PARAM_COUNTRY_CODE, "ES");
			}
			else
			{
				parameter.addParamater(PercentHttpConfig.RES_PARAM_COUNTRY_CODE, Util.getRegion());
			}
			parameter.addParamater(PercentHttpConfig.RES_PARAM_PRIVACY_POLICY_VERSION, privacyPopup.privacyPolicyVersion);
			return parameter.getUrl();
		}

		internal override void onGETResponseSuccess(JSONObject json)
		{
			privacyPopup.parseData(json);
			if (onReceieveText != null)
			{
				onReceieveText(true);
			}
		}

		internal override void onGETResponseFail()
		{
			if (onReceieveText != null)
			{
				onReceieveText(false);
			}
		}
	}
}
