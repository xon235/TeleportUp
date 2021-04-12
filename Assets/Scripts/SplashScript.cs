using UnityEngine;
using System.Collections;

public class SplashScript : MonoBehaviour {

	public float splashTime;
	float splashTimeCount = 0;
	
	// Update is called once per frame
	void Update ()
	{
		splashTimeCount += Time.deltaTime;

		if (splashTimeCount >= splashTime)
		{
			Application.LoadLevel("Home");
		}
	}
}
