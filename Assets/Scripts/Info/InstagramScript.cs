using UnityEngine;
using System.Collections;

public class InstagramScript : MonoBehaviour {

	public string instaId;

	public void OpenInstagram ()
	{
		if(HasInstagramApp())
			Application.OpenURL ("instagram://user?username=" + instaId);
		else
			Application.OpenURL ("http://www.instagram.com/" + instaId);
	}

	bool HasInstagramApp()
	{
		try
		{
			AndroidJavaClass up = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			AndroidJavaObject ca = up.GetStatic<AndroidJavaObject> ("currentActivity");
			AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");
			AndroidJavaObject launchIntent = null;
			launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", "com.instagram.android");
		}
		catch
		{
			return false;
		}

		return true;
	}
}
