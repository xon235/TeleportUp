using UnityEngine;
using System.Collections;

public class NewInfoScript : MonoBehaviour {

	public GameObject newForInfo;

	void Start () {
		if (PlayerPrefs.GetInt ("SeenInfo", 0) == 0)
			newForInfo.SetActive (true);
	}
}
