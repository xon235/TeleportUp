using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MuteScript : MonoBehaviour {
	
	public Sprite soundOn;
	public Sprite soundOff;

	void Awake ()
	{
		if (PlayerPrefs.GetInt ("Mute", 0) == 0) {
			GetComponent<Image> ().sprite = soundOn;
		}
		else
		{
			GetComponent<Image> ().sprite = soundOff;
		}
	}

	public void ToggleMute()
	{
		if (PlayerPrefs.GetInt ("Mute", 0) == 0)
		{
			PlayerPrefs.SetInt ("Mute", 1);
			GetComponent<Image> ().sprite = soundOff;
		}
		else
		{
			PlayerPrefs.SetInt ("Mute", 0);
			GetComponent<Image> ().sprite = soundOn;
		}
	}
}