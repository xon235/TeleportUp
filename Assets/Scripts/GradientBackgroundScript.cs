using UnityEngine;
using System.Collections;

public class GradientBackgroundScript : MonoBehaviour {

	// Use this for initialization
	void Awake ()
	{
		DontDestroyOnLoad (gameObject);
	}
}
