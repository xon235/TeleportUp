using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;



namespace Prime31
{
	public class GoogleIABEventListener : MonoBehaviour
	{
#if UNITY_ANDROID
		void OnEnable()
		{
			// Listen to all events for illustration purposes
			GoogleIABManager.billingSupportedEvent += billingSupportedEvent;
			GoogleIABManager.billingNotSupportedEvent += billingNotSupportedEvent;
			GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
			GoogleIABManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
			GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
			GoogleIABManager.purchaseSucceededEvent += purchaseSucceededEvent;
			GoogleIABManager.purchaseFailedEvent += purchaseFailedEvent;
			GoogleIABManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
			GoogleIABManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
		}
	
	
		void OnDisable()
		{
			// Remove all event handlers
			GoogleIABManager.billingSupportedEvent -= billingSupportedEvent;
			GoogleIABManager.billingNotSupportedEvent -= billingNotSupportedEvent;
			GoogleIABManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
			GoogleIABManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
			GoogleIABManager.purchaseCompleteAwaitingVerificationEvent -= purchaseCompleteAwaitingVerificationEvent;
			GoogleIABManager.purchaseSucceededEvent -= purchaseSucceededEvent;
			GoogleIABManager.purchaseFailedEvent -= purchaseFailedEvent;
			GoogleIABManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
			GoogleIABManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
		}
	
	
	
		void billingSupportedEvent()
		{
			Debug.Log( "billingSupportedEvent" );
		}
	
	
		void billingNotSupportedEvent( string error )
		{
			Debug.Log( "billingNotSupportedEvent: " + error );
		}
	
	
		void queryInventorySucceededEvent( List<GooglePurchase> purchases, List<GoogleSkuInfo> skus )
		{
			Debug.Log( string.Format( "queryInventorySucceededEvent. total purchases: {0}, total skus: {1}", purchases.Count, skus.Count ) );
			Prime31.Utils.logObject( purchases );
			Prime31.Utils.logObject( skus );

			foreach(GooglePurchase p in purchases)
			{
				if(p.productId == "teleport_up_no_ads")
				{
					Debug.Log("Found!");
					PlayerPrefs.SetInt("NoAds", 1);
					return;
				}
			}

            Debug.Log("Not Found!");
            PlayerPrefs.SetInt("NoAds", 0);
        }
	
	
		void queryInventoryFailedEvent( string error )
		{
			Debug.Log( "queryInventoryFailedEvent: " + error );
		}
	
	
		void purchaseCompleteAwaitingVerificationEvent( string purchaseData, string signature )
		{
			Debug.Log( "purchaseCompleteAwaitingVerificationEvent. purchaseData: " + purchaseData + ", signature: " + signature );
		}
	
	
		void purchaseSucceededEvent( GooglePurchase purchase )
		{
			Debug.Log( "purchaseSucceededEvent: " + purchase );

			if (purchase.productId == "teleport_up_no_ads")
			{
				Debug.Log("Purchased!");
				PlayerPrefs.SetInt("NoAds", 1);
			}
		}
	
	
		void purchaseFailedEvent( string error, int response )
		{
			Debug.Log( "purchaseFailedEvent: " + error + ", response: " + response );
		}
	
	
		void consumePurchaseSucceededEvent( GooglePurchase purchase )
		{
			Debug.Log( "consumePurchaseSucceededEvent: " + purchase );
		}
	
	
		void consumePurchaseFailedEvent( string error )
		{
			Debug.Log( "consumePurchaseFailedEvent: " + error );
		}
	
	
#endif
	}

}
	
	
