using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GoogleMobileAds.Api;
using Prime31;

public class GoogleManagerScript : MonoBehaviour {

	//GPGS
	static string leaderBoardIdScore = "CgkImsLKle4UEAIQAA";
	static string leaderBoardIdWork = "CgkImsLKle4UEAIQBw";
	static string AchievementId_First = "CgkImsLKle4UEAIQAg";
	static string AchievementId_100m = "CgkImsLKle4UEAIQAw";
	static string AchievementId_250m = "CgkImsLKle4UEAIQBA";
	static string AchievementId_500m = "CgkImsLKle4UEAIQBQ";
	static string AchievementId_1000m = "CgkImsLKle4UEAIQBg";
	static string AchievementId_10times = "CgkImsLKle4UEAIQCA";
	static string AchievementId_30times = "CgkImsLKle4UEAIQCQ";
	static string AchievementId_50times = "CgkImsLKle4UEAIQCg";
	static string AchievementId_100times = "CgkImsLKle4UEAIQCw";
	static string AchievementId_250times = "CgkImsLKle4UEAIQDA";
	static string AchievementId_500times = "CgkImsLKle4UEAIQDQ";
	//--------------------------------------------------

	//AdMob
	static string bannerAdUnitId = "ca-app-pub-2703191969786856/2276147326";
	static string interstitialAdUnitId = "ca-app-pub-2703191969786856/8645581723";
	static string testDeviceId = "2075E6A96349530519C7349C867E59DA";
	static bool noAds = false;
	static bool bannerAdFail = false;
	static bool interstitialAdFail = false;
	static BannerView bannerView;
	static AdRequest bannerAdRequest;
	static InterstitialAd interstitialAd;
	static AdRequest interstitialAdRequest;
	static int playCount = 0;
	static int interstitialShowRate = 3;
	//---------------------------------------------------------

	//IAB
	static string key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAg6cGUEk+zI+ZpQFGfg3DsVDN+mkTTpkLwmtxivGyFn+G522y63mCwNxFRd7sRktmOhg51IVv520W6l6E0uh8SvmNtbbY9019JQZJiIIWkxRGcFkcn+gdf7sEVfCGADp+1EkGQoHraXNSWa4yV5RGLjQBIPobqCqnWMIefFmW9Oi3Tf8GjiEwyLdwywl4sj4WCchqo7Xr88haOI1m7KRkv2uug7spMb/BLshxDwk5wSpzU0CD9B9auOM4ljDcTlXZX7jSjAAmKUuxQ+7p/oZzgF/hV7OvinnfJnpidzmawpNKPXVpDJB9JM4+gkIb4ow7rJigiIemjDuyqsM0EnegXwIDAQAB";
	static string[] skus = {"teleport_up_no_ads"};
	//----------------------------------------------------------

	// Use this for initialization
	void Start ()
	{
		DontDestroyOnLoad (gameObject);
		InitGPGS ();
		InitAdMob ();
		InitIAB ();
	}

	void InitGPGS()
	{
		PlayGamesPlatform.Activate ();
		Social.localUser.Authenticate((bool success) => {
			// handle success or failure
		});
	}

	void InitAdMob()
	{
		playCount = 0;
		bannerView = new BannerView (bannerAdUnitId, AdSize.Banner, AdPosition.Bottom);
		bannerAdRequest = new AdRequest.Builder ().AddTestDevice (AdRequest.TestDeviceSimulator).AddTestDevice (testDeviceId).Build ();
		bannerView.AdFailedToLoad += HandleBannerAdFailedToLoad;
		bannerView.LoadAd (bannerAdRequest);
		
		interstitialAd = new InterstitialAd (interstitialAdUnitId);
		interstitialAdRequest = new AdRequest.Builder().AddTestDevice (AdRequest.TestDeviceSimulator).AddTestDevice (testDeviceId).Build ();
		interstitialAd.AdFailedToLoad += HandleInterstitialAdFailedToLoad;
		interstitialAd.LoadAd (interstitialAdRequest);
	}

	void InitIAB()
	{
		GoogleIAB.init (key);
		GoogleIAB.queryInventory (skus);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//
		if (PlayerPrefs.GetInt ("NoAds", 0) == 1 && !noAds) {
			Debug.Log ("admob destroy");
			bannerView.Destroy ();
			interstitialAd.Destroy ();
			noAds = true;
		} else if(!noAds){
			if(bannerAdFail)
			{
				bannerAdFail = false;
				bannerAdRequest = new AdRequest.Builder ().AddTestDevice (AdRequest.TestDeviceSimulator).AddTestDevice (testDeviceId).Build ();
				bannerView.LoadAd (bannerAdRequest);
				bannerView.Show();
			}

			if(interstitialAdFail)
			{
				interstitialAdFail = false;
				interstitialAdRequest = new AdRequest.Builder().AddTestDevice (AdRequest.TestDeviceSimulator).AddTestDevice (testDeviceId).Build ();
				interstitialAd.LoadAd (interstitialAdRequest);
			}
		}
	}

	//GPGS
	public static void ShowLeaderBoards ()
	{
		PlayGamesPlatform.Instance.ShowLeaderboardUI();
	}
	
	
	public static void PostHighScore(long highScore)
	{
		Social.ReportScore(highScore, leaderBoardIdScore, (bool success) => {
			// handle success or failure
		});
	}
	
	public static void PostHighWork(long highWork)
	{
		Social.ReportScore(highWork, leaderBoardIdWork, (bool success) => {
			// handle success or failure
		});
	}
	
	public static void ShowAchievements ()
	{
		Social.ShowAchievementsUI();
	}
	
	public static void unlock_First()
	{
		Social.ReportProgress(AchievementId_First, 100.0f, (bool success) => {
			// handle success or failure
		});
	}
	
	public static void unlock_100m()
	{
		Social.ReportProgress(AchievementId_100m, 100.0f, (bool success) => {
			// handle success or failure
		});
	}
	
	public static void unlock_250m()
	{
		Social.ReportProgress(AchievementId_250m, 100.0f, (bool success) => {
			// handle success or failure
		});
	}
	
	public static void unlock_500m()
	{
		Social.ReportProgress(AchievementId_500m, 100.0f, (bool success) => {
			// handle success or failure
		});
	}
	
	public static void unlock_1000m()
	{
		Social.ReportProgress(AchievementId_1000m, 100.0f, (bool success) => {
			// handle success or failure
		});
	}

	public static void unlock_10times()
	{
		Social.ReportProgress(AchievementId_10times, 100.0f, (bool success) => {
			// handle success or failure
		});
	}

	public static void unlock_30times()
	{
		Social.ReportProgress(AchievementId_30times, 100.0f, (bool success) => {
			// handle success or failure
		});
	}

	public static void unlock_50times()
	{
		Social.ReportProgress(AchievementId_50times, 100.0f, (bool success) => {
			// handle success or failure
		});
	}

	public static void unlock_100times()
	{
		Social.ReportProgress(AchievementId_100times, 100.0f, (bool success) => {
			// handle success or failure
		});
	}

	public static void unlock_250times()
	{
		Social.ReportProgress(AchievementId_250times, 100.0f, (bool success) => {
			// handle success or failure
		});
	}

	public static void unlock_500times()
	{
		Social.ReportProgress(AchievementId_500times, 100.0f, (bool success) => {
			// handle success or failure
		});
	}
	//----------------------------------------------------------------------

	//AdMobs
	public static void IncrementPlayCount()
	{
		if (!noAds)
		{
			playCount += 1;
			
			if (playCount % interstitialShowRate == 0)
			{
				interstitialAd.Show();
				interstitialAd = new InterstitialAd (interstitialAdUnitId);
				interstitialAdRequest = new AdRequest.Builder().AddTestDevice (AdRequest.TestDeviceSimulator).AddTestDevice (testDeviceId).Build ();
				interstitialAd.LoadAd (interstitialAdRequest);
			}
		}
	}

	public static void ShowBanner()
	{
		bannerView.Show ();
	}

	public void HandleBannerAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		bannerAdFail = true;
	}

	public void HandleInterstitialAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		interstitialAdFail = true;
	}
	//-------------------------------------------------------

	//IAB
	public static void PurchaseNoAds()
	{
		GoogleIAB.purchaseProduct (skus [0]);
	}
	//----------------------------------------------------------

	//Share
	public static void Share()
	{
		Application.CaptureScreenshot ("expl.png");
		AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
		intentObject.Call <AndroidJavaObject>("setAction", intentClass.GetStatic <string>("ACTION_SEND"));
		AndroidJavaClass uriClass = new AndroidJavaClass ("android.net.Uri");
		AndroidJavaObject uriObject = uriClass.CallStatic <AndroidJavaObject>("parse", "file://" + Application.persistentDataPath + "/expl.png");                                       
		intentObject.Call <AndroidJavaObject>("putExtra", intentClass.GetStatic <string>("EXTRA_STREAM"), uriObject);                                        
		intentObject.Call <AndroidJavaObject>("setType", "image/png");                                      
		AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");                                        
		AndroidJavaObject currentActivity = unity.GetStatic <AndroidJavaObject>("currentActivity");                                     
		currentActivity.Call ("startActivity", intentObject);
	}
	//S------------------------------------------------------------------
}
