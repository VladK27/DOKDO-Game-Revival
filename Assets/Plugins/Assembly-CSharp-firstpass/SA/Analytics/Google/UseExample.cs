using System.Collections.Generic;
using SA.Analytics.Firebase;
using UnityEngine;

namespace SA.Analytics.Google
{
	public class UseExample : MonoBehaviour
	{
		private void Start()
		{
			Manager.StartTracking();
			SA.Analytics.Firebase.Analytics.Init();
		}

		private void OnGUI()
		{
			if (GUI.Button(new Rect(10f, 10f, 150f, 50f), "Page Hit"))
			{
				Manager.Client.SendPageHit("mydemo.com ", "/home", "homepage", string.Empty, string.Empty);
			}
			if (GUI.Button(new Rect(10f, 70f, 150f, 50f), "Event Hit"))
			{
				Manager.Client.SendEventHit("video", "play", "holiday", 300);
			}
			if (GUI.Button(new Rect(10f, 130f, 150f, 50f), "Transaction Hit"))
			{
				Manager.Client.SendTransactionHit("12345", "westernWear", "EUR", 50f, 32f, 12f);
			}
			if (GUI.Button(new Rect(10f, 190f, 150f, 50f), "Item Hit"))
			{
				Manager.Client.SendItemHit("12345", "sofa", "u3eqds43", 300f, 2, "furniture", "EUR");
			}
			if (GUI.Button(new Rect(190f, 10f, 150f, 50f), "Social Hit"))
			{
				Manager.Client.SendSocialHit("like", "facebook", "/home ");
			}
			if (GUI.Button(new Rect(190f, 70f, 150f, 50f), "Exception Hit"))
			{
				Manager.Client.SendExceptionHit("IOException", true);
			}
			if (GUI.Button(new Rect(190f, 130f, 150f, 50f), "Timing Hit"))
			{
				Manager.Client.SendUserTimingHit("jsonLoader", "load", 5000, "jQuery");
			}
			if (GUI.Button(new Rect(190f, 190f, 150f, 50f), "Screen Hit"))
			{
				Manager.Client.SendScreenHit("MainMenu");
			}
			if (GUI.Button(new Rect(10f, 270f, 150f, 50f), "Firebase Event"))
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("str_data", "hello_from_firebase");
				dictionary.Add("numeric_data", 10101);
				SA.Analytics.Firebase.Analytics.LogEvent("ga_event", dictionary);
			}
		}

		public void CustomBuildersExamples()
		{
			Manager.Client.CreateHit(HitType.PAGEVIEW);
			Manager.Client.SetDocumentHostName("mydemo.com");
			Manager.Client.SetDocumentPath("/home");
			Manager.Client.SetDocumentTitle("homepage");
			Manager.Client.Send();
			Manager.Client.CreateHit(HitType.EVENT);
			Manager.Client.SetEventCategory("video");
			Manager.Client.SetEventAction("play");
			Manager.Client.SetEventLabel("holiday");
			Manager.Client.SetEventValue(300);
			Manager.Client.Send();
			Manager.Client.CreateHit(HitType.PAGEVIEW);
			Manager.Client.SetDocumentHostName("mydemo.com");
			Manager.Client.SetDocumentPath("/receipt");
			Manager.Client.SetDocumentTitle("Receipt Page");
			Manager.Client.SetTransactionID("T12345");
			Manager.Client.SetTransactionAffiliation("Google Store - Online");
			Manager.Client.SetTransactionRevenue(37.39f);
			Manager.Client.SetTransactionTax(2.85f);
			Manager.Client.SetTransactionShipping(5.34f);
			Manager.Client.SetTransactionCouponCode("SUMMER2013");
			Manager.Client.SetProductAction("purchase");
			Manager.Client.SetProductSKU(1, "P12345");
			Manager.Client.SetSetProductName(1, "Android Warhol T-Shirt");
			Manager.Client.SetProductCategory(1, "Apparel");
			Manager.Client.SetProductBrand(1, "Google");
			Manager.Client.SetProductVariant(1, "Black");
			Manager.Client.SetProductPosition(1, 1);
			Manager.Client.Send();
			Manager.Client.CreateHit(HitType.EVENT);
			Manager.Client.SetEventCategory("Ecommerce");
			Manager.Client.SetEventAction("Refund");
			Manager.Client.SetNonInteractionFlag();
			Manager.Client.SetTransactionID("T12345");
			Manager.Client.SetProductAction("refund");
			Manager.Client.Send();
			Manager.Client.CreateHit(HitType.EVENT);
			Manager.Client.SetEventCategory("Ecommerce");
			Manager.Client.SetEventAction("Refund");
			Manager.Client.SetNonInteractionFlag();
			Manager.Client.SetTransactionID("T12345");
			Manager.Client.SetProductAction("refund");
			Manager.Client.SetProductSKU(1, "P12345");
			Manager.Client.SetProductQuantity(1, 1);
			Manager.Client.Send();
			Manager.Client.CreateHit(HitType.PAGEVIEW);
			Manager.Client.SetDocumentHostName("mydemo.com");
			Manager.Client.SetDocumentPath("/receipt");
			Manager.Client.SetDocumentTitle("Receipt Page");
			Manager.Client.SetTransactionID("T12345");
			Manager.Client.SetTransactionAffiliation("Google Store - Online");
			Manager.Client.SetTransactionRevenue(37.39f);
			Manager.Client.SetTransactionTax(2.85f);
			Manager.Client.SetTransactionShipping(5.34f);
			Manager.Client.SetTransactionCouponCode("SUMMER2013");
			Manager.Client.SetProductAction("purchase");
			Manager.Client.SetProductSKU(1, "P12345");
			Manager.Client.SetSetProductName(1, "Android Warhol T-Shirt");
			Manager.Client.SetProductCategory(1, "Apparel");
			Manager.Client.SetProductBrand(1, "Google");
			Manager.Client.SetProductVariant(1, "Black");
			Manager.Client.SetProductPrice(1, 29.9f);
			Manager.Client.SetProductQuantity(1, 1);
			Manager.Client.SetCheckoutStep(1);
			Manager.Client.SetCheckoutStepOption("Visa");
			Manager.Client.Send();
			Manager.Client.CreateHit(HitType.EVENT);
			Manager.Client.SetEventCategory("Checkout");
			Manager.Client.SetEventAction("Option");
			Manager.Client.SetProductAction("checkout_option");
			Manager.Client.SetCheckoutStep(2);
			Manager.Client.SetCheckoutStepOption("FedEx");
			Manager.Client.Send();
			Manager.Client.CreateHit(HitType.PAGEVIEW);
			Manager.Client.SetDocumentHostName("mydemo.com");
			Manager.Client.SetDocumentPath("/home");
			Manager.Client.SetDocumentTitle("homepage");
			Manager.Client.SetPromotionID(1, "PROMO_1234");
			Manager.Client.SetPromotionName(1, "Summer Sale");
			Manager.Client.SetPromotionCreative(1, "summer_banner2");
			Manager.Client.SetPromotionPosition(1, "banner_slot1");
			Manager.Client.Send();
			Manager.Client.CreateHit(HitType.EVENT);
			Manager.Client.SetEventCategory("Internal Promotions");
			Manager.Client.SetEventAction("click");
			Manager.Client.SetEventLabel("Summer Sale");
			Manager.Client.SetPromotionAction("click");
			Manager.Client.SetPromotionID(1, "PROMO_1234");
			Manager.Client.SetPromotionName(1, "Summer Sale");
			Manager.Client.SetPromotionCreative(1, "summer_banner2");
			Manager.Client.SetPromotionPosition(1, "banner_slot1");
			Manager.Client.Send();
			Manager.Client.CreateHit(HitType.TRANSACTION);
			Manager.Client.SetTransactionID("12345");
			Manager.Client.SetTransactionAffiliation("westernWear");
			Manager.Client.SetTransactionRevenue(50f);
			Manager.Client.SetTransactionShipping(32f);
			Manager.Client.SetTransactionTax(12f);
			Manager.Client.SetCurrencyCode("EUR");
			Manager.Client.Send();
			Manager.Client.CreateHit(HitType.ITEM);
			Manager.Client.SetTransactionID("12345");
			Manager.Client.SetItemName("sofa");
			Manager.Client.SetItemPrice(300f);
			Manager.Client.SetItemQuantity(2);
			Manager.Client.SetItemCode("u3eqds43");
			Manager.Client.SetItemCategory("furniture");
			Manager.Client.SetCurrencyCode("EUR");
			Manager.Client.Send();
			Manager.Client.CreateHit(HitType.SOCIAL);
			Manager.Client.SetSocialAction("like");
			Manager.Client.SetSocialNetwork("facebook");
			Manager.Client.SetSocialActionTarget("/home  ");
			Manager.Client.Send();
			Manager.Client.CreateHit(HitType.EXCEPTION);
			Manager.Client.SetExceptionDescription("IOException");
			Manager.Client.SetIsFatalException(true);
			Manager.Client.Send();
			Manager.Client.CreateHit(HitType.TIMING);
			Manager.Client.SetUserTimingCategory("jsonLoader");
			Manager.Client.SetUserTimingVariableName("load");
			Manager.Client.SetUserTimingTime(5000);
			Manager.Client.SetUserTimingLabel("jQuery");
			Manager.Client.SetDNSTime(100);
			Manager.Client.SetPageDownloadTime(20);
			Manager.Client.SetRedirectResponseTime(32);
			Manager.Client.SetTCPConnectTime(56);
			Manager.Client.SetServerResponseTime(12);
			Manager.Client.Send();
			Manager.Client.CreateHit(HitType.PAGEVIEW);
			Manager.Client.SetDocumentHostName("mydemo.com");
			Manager.Client.SetDocumentPath("/home");
			Manager.Client.SetDocumentTitle("homepage");
			Manager.Client.Send();
			Manager.Client.CreateHit(HitType.PAGEVIEW);
			Manager.Client.AppendData("dh", "mydemo.com");
			Manager.Client.AppendData("dp", "/home");
			Manager.Client.AppendData("dt", "homepage");
			Manager.Client.Send();
		}
	}
}
