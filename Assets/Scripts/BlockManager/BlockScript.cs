using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {

	Rigidbody2D rigidBody;
	AudioSource audioSource;

	void Awake ()
	{
		rigidBody = GetComponent<Rigidbody2D> ();
		audioSource = GetComponent<AudioSource> ();
	}

	void Update ()
	{
		rigidBody.velocity = new Vector2 (0, GameManagerScript.currentVelocity);
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (!audioSource.isPlaying && coll.gameObject.name == "Player")
		{
			audioSource.volume = Mathf.Clamp (coll.relativeVelocity.magnitude / 15f, 0f, 1f);
			audioSource.Play ();
		}
	}
}
