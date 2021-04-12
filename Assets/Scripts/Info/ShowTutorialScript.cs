using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowTutorialScript : MonoBehaviour {

	void Awake ()
	{
		if (PlayerPrefs.GetInt ("ShowTutorial", 1) == 0)
		{
			GetComponent<Toggle>().isOn = false;
		}
		else
		{
			GetComponent<Toggle>().isOn = true;
		}
	}

	public void ToggleShowTutorial()
	{
		if (GetComponent<Toggle>().isOn)
		{
			PlayerPrefs.SetInt ("ShowTutorial", 1);
		}
		else
		{
			PlayerPrefs.SetInt ("ShowTutorial", 0);
		}
	}
}
