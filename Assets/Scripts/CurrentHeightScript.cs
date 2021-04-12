using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrentHeightScript : MonoBehaviour {

	void Update ()
	{
		GetComponent<Text> ().text = Mathf.FloorToInt (GameManagerScript.currentHeight) + "m";
	}
}
