using UnityEngine;
using System.Collections;

public class GroundScript : MonoBehaviour {

	Rigidbody2D rigidBody;

	AudioSource audioSource;
	
	void Start ()
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
		if (!audioSource.isPlaying)
		{
			audioSource.volume = Mathf.Clamp (coll.relativeVelocity.magnitude / 14f, 0f, 1f);
			audioSource.Play ();
		}
	}
}
