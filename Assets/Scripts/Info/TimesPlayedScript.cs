using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimesPlayedScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Text>().text = "Times played : " + PlayerPrefs.GetInt ("PlayCount", 0);
	}
}
