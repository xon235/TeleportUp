using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HomeManagerScript : MonoBehaviour
{
	public List<GameObject> ufos = new List<GameObject>();
	public GameObject body;
	public GameObject homeCanvas;
	public GameObject tutorialCanvas;
	public RectTransform[] buttonsTransforms;

	public float fadeSpeed;

	public bool fadeBGM;
	public float fadeBGMSpeed;

	public Sprite soundOn;
	public Sprite soundOff;
	
	Vector2 screenSize;
	Vector2 referenceScreenSize;
	float scale;

	bool fadeOutGUI;

	public void Awake()
	{
		fadeBGM = false;

		screenSize.x = Screen.width;
		screenSize.y = Screen.height;

		referenceScreenSize = homeCanvas.GetComponent<CanvasScaler> ().referenceResolution;

		scale = screenSize.x / referenceScreenSize.x;

		fadeOutGUI = false;

		GoogleManagerScript.ShowBanner ();
	}

	public void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Application.Quit();
		}

		if (fadeOutGUI)
		{
			homeCanvas.GetComponent<CanvasGroup>().alpha -= fadeSpeed * Time.deltaTime;
			if(homeCanvas.GetComponent<CanvasGroup>().alpha <= 0)
			{
				homeCanvas.SetActive(false);
				fadeOutGUI = false;
			}
		}

		if (fadeBGM)
		{
			GetComponent<AudioSource> ().volume -= fadeBGMSpeed * Time.deltaTime;

			if(GetComponent<AudioSource> ().volume <= 0)
			{
				GetComponent<AudioSource> ().volume = 0;
				GetComponent<AudioSource> ().Stop();
			}
		}
	}

	public void LoadMain()
	{
		fadeBGM = true;
		ufos [0].GetComponent<AudioSource> ().Play ();

		foreach (GameObject ufo in ufos)
		{
			ufo.GetComponent<UFOScript> ().start = true;
		}

		Vector2 playerPos = AnchorPositionToUnits (buttonsTransforms [Random.Range (0, 4)].anchoredPosition);
		body.transform.position = playerPos;
		body.SetActive (true);

		PlayerPrefs.SetInt ("PlayerPosSet", 1);
		PlayerPrefs.SetFloat ("PlayerPosX", playerPos.x);
		PlayerPrefs.SetFloat ("PlayerPosY", playerPos.y);

		fadeOutGUI = true;
	}

	public void ShowAchievements()
	{
		GoogleManagerScript.ShowAchievements ();
	}

	public void ShowLeaderboards()
	{
		GoogleManagerScript.ShowLeaderBoards ();
	}

	public void LoadInfo()
	{
		PlayerPrefs.SetInt ("SeenInfo", 1);
		Application.LoadLevel ("Info");
	}

	public void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "UFO")
		{
			ufos.Remove(coll.gameObject);
			if(ufos.Count == 0)
			{
				if(PlayerPrefs.GetInt("ShowTutorial", 1) == 0)
				{
					Application.LoadLevel ("Main");
				}
				else
				{
					tutorialCanvas.SetActive(true);
				}
			}
		}
	}

	Vector2 AnchorPositionToUnits(Vector2 pos)
	{
		Vector2 screenPos;
		screenPos.x = pos.x * scale + screenSize.x / 2;
		screenPos.y = pos.y * scale + screenSize.y / 2;

		return Camera.main.ScreenToWorldPoint (screenPos);
	}
}

