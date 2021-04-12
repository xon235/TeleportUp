using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	public GameObject scoreCanvas;
	public GameObject yourScore;
	public GameObject highScore;
	public GameObject newIcon;
	public GameObject player;
	public GameObject crosshair;
	public GameObject ground;

	float maxVelocity;
	public float initMaxVelocity;
	public float initPitch;
	public float[] heights;
	public float[] maxVelocities;
	public float[] pitches;
	public static float currentVelocity;
	public float acceleration;
	public bool startMoving;

	public float unitsInMeters;
	public static float currentHeight;
	
	float playerHalfHeight;
	float groundHalfHeight;

	float bestHeight;

	AudioSource audioSource;
	public bool volumeFadeIn;
	public bool volumeFadeOut;
	public float volumeFadeInSpeed;
	public float volumeFadeOutSpeed;

	void Awake ()
	{
		currentHeight = 0;
		playerHalfHeight = player.GetComponent<SpriteRenderer>().bounds.size.y / 2;
		groundHalfHeight = ground.GetComponent<SpriteRenderer>().bounds.size.y / 2;

		bestHeight = PlayerPrefs.GetFloat ("BestHeight");
		GoogleManagerScript.PostHighScore (Mathf.FloorToInt (bestHeight));

		if (PlayerPrefs.GetInt ("PlayerPosSet") == 1)
		{
			Vector3 playerPos = Vector3.zero;
			playerPos.x = PlayerPrefs.GetFloat ("PlayerPosX");
			playerPos.y = PlayerPrefs.GetFloat ("PlayerPosY");
			player.transform.position = playerPos;
		}

		maxVelocity = initMaxVelocity;
		audioSource = GetComponent<AudioSource> ();
		audioSource.volume = 0;
		audioSource.pitch = initPitch;
	}
	

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Application.Quit();
		}

		if(volumeFadeOut)
		{
			audioSource.volume -= volumeFadeOutSpeed * Time.deltaTime;
			
			if(audioSource.volume <= 0)
			{
				audioSource.volume = 0;
			}
		}
		else if(volumeFadeIn)
		{
			audioSource.volume += volumeFadeInSpeed * Time.deltaTime;
			
			if(audioSource.volume >= 1)
			{
				audioSource.volume = 1;
			}
		}

		if (player != null)
		{
			// speed Up
			for(int i = 0; i < heights.Length; i++)
			{
				if(currentHeight > heights[i])
				{
					maxVelocity = maxVelocities[i];
					audioSource.pitch = pitches[i];
				}
			}

			if (startMoving && Mathf.Abs(currentVelocity) < Mathf.Abs(maxVelocity))
			{
				currentVelocity += acceleration * Time.deltaTime;
				
				if(Mathf.Abs(currentVelocity) > Mathf.Abs(maxVelocity))
				{
					currentVelocity = maxVelocity;
				}
			}
			//------------------------------
			
			currentHeight = player.transform.position.y - ground.transform.position.y;
			currentHeight -= playerHalfHeight;
			currentHeight -= groundHalfHeight;
			currentHeight /= unitsInMeters;
			currentHeight += 0.005f; //Adjustment for physics error

			if(currentHeight < 0)
			{
				currentHeight = 0f;
			}

			if(currentHeight >= 100)
			{
				GoogleManagerScript.unlock_100m();

				if(currentHeight >= 250)
				{
					GoogleManagerScript.unlock_250m();

					if(currentHeight >= 500)
					{
						GoogleManagerScript.unlock_500m();

						if(currentHeight >= 1000)
						{
							GoogleManagerScript.unlock_1000m();
						}
					}
				}

			}
			
			if (player.transform.position.y < -10)
			{
				volumeFadeOut = true;
				Destroy(player);
				Destroy(crosshair);
				PlayerPrefs.SetInt ("PlayerPosSet", 0);
				int playCount = PlayerPrefs.GetInt("PlayCount", 0);
				playCount += 1;
				PlayerPrefs.SetInt("PlayCount", playCount);
				GoogleManagerScript.PostHighWork(playCount);

				if(playCount >= 10)
				{
					GoogleManagerScript.unlock_10times();

					if(playCount >= 30)
					{
						GoogleManagerScript.unlock_30times();

						if(playCount >= 50)
						{
							GoogleManagerScript.unlock_50times();

							if(playCount >= 100)
							{
								GoogleManagerScript.unlock_100times();

								if(playCount >= 250)
								{
									GoogleManagerScript.unlock_250times();

									if(playCount >= 500)
									{
										GoogleManagerScript.unlock_500times();
									}
								}
							}
						}
					}
				}

				if(currentHeight > bestHeight)
				{
					PlayerPrefs.SetFloat("BestHeight", currentHeight);
					bestHeight = currentHeight;
					newIcon.SetActive(true);
					GoogleManagerScript.PostHighScore (Mathf.FloorToInt (bestHeight));
				}

				scoreCanvas.SetActive(true);
				scoreCanvas.GetComponent<ScoreCanvasScript>().fadeIn = true;
				yourScore.GetComponent<Text>().text = "Your Score\n" + Mathf.FloorToInt(currentHeight) + "m";
				highScore.GetComponent<Text>().text = "High Score\n" + Mathf.FloorToInt(bestHeight) + "m";

				GoogleManagerScript.IncrementPlayCount();
			}
		}
	}

	public void Restart()
	{
		currentHeight = 0;
		currentVelocity = 0;
		Application.LoadLevel (Application.loadedLevel);
	}

	public void LaodHome()
	{
		currentHeight = 0;
		currentVelocity = 0;
		Application.LoadLevel ("Home");
	}

	public void ShowLeaderboards()
	{
		GoogleManagerScript.ShowLeaderBoards ();
	}

	public void PurchaseNoAds()
	{
		GoogleManagerScript.PurchaseNoAds ();
	}

	public void ShareScreenshot()
	{
		GoogleManagerScript.Share ();
	}
}

