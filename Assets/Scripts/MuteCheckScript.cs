using UnityEngine;
using System.Collections;

public class MuteCheckScript : MonoBehaviour {
	
	void Update ()
	{
		if (PlayerPrefs.GetInt ("Mute", 0) == 0)
		{
			AudioListener.pause = false;
		}
		else
		{
			AudioListener.pause = true;
		}
	}
}
