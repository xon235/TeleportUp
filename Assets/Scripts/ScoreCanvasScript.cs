using UnityEngine;
using System.Collections;

public class ScoreCanvasScript : MonoBehaviour {

	public bool fadeIn;
	public float fadeInSpeed;

	CanvasGroup canvasGroup;

	void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup> ();
		canvasGroup.alpha = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (fadeIn)
		{
			canvasGroup.alpha += fadeInSpeed * Time.deltaTime;

			if(canvasGroup.alpha >= 1)
			{
				canvasGroup.alpha = 1;
				fadeIn = false;
			}
		}
	}
}
