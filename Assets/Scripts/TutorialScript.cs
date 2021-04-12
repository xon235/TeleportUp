using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialScript : MonoBehaviour {

	public GameObject tutorialObject;
	public GameObject uiAudio;
	public Text text;

	public string[] descriptions;

	public int touchCount;

	void Awake()
	{
		text.text = descriptions [0];
		tutorialObject.GetComponent<Animator>().SetInteger("AnimationState", 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
		{
			touchCount += 1;
			uiAudio.GetComponent<AudioSource>().Play();

			if(touchCount >= descriptions.Length)
			{
				Application.LoadLevel ("Main");
			}

			tutorialObject.GetComponent<Animator>().SetInteger("AnimationState", touchCount);
			text.text = descriptions[touchCount];
		}
	}

	public void SkipTutorial()
	{
		PlayerPrefs.SetInt ("ShowTutorial", 0);
		Application.LoadLevel ("Main");
	}
}
