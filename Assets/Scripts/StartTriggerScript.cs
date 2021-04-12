using UnityEngine;
using System.Collections;

public class StartTriggerScript : MonoBehaviour {

	public GameManagerScript gameManagerScript;
	public GameObject startText;

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			gameManagerScript.startMoving = true;
			GoogleManagerScript.unlock_First();
			gameManagerScript.volumeFadeIn = true;

			Destroy(startText);
			Destroy(gameObject);
		}
	}
}
