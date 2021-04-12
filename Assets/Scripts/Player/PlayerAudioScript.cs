using UnityEngine;
using System.Collections;

public class PlayerAudioScript : MonoBehaviour {
	
	public AudioClip fallAudio;

	public float velocityThreshold;

	public float radius;
	public bool isOnGround;

	Rigidbody2D rigidBody;
	AudioSource audioSource;

	Vector2 velocity;

	bool playOnce;

	void Awake()
	{
		rigidBody = GetComponent<Rigidbody2D> ();
		audioSource = GetComponent<AudioSource> ();

		velocity = rigidBody.velocity;
		velocity.y -= GameManagerScript.currentVelocity;

		playOnce = false;
	}

	void Update ()
	{
		velocity = rigidBody.velocity;
		velocity.y -= GameManagerScript.currentVelocity;

		bool wasOnGround = isOnGround;
		isOnGround = Physics2D.OverlapCircle (transform.position, radius, LayerMask.GetMask ("Platforms"));

		if (isOnGround && audioSource.clip == fallAudio)
		{
			audioSource.Stop();
		}

		if (isOnGround && !wasOnGround)
		{
			playOnce = true;
		}


		if (velocity.y < velocityThreshold)
		{
			audioSource.clip = fallAudio;
			audioSource.volume = Mathf.Clamp (Mathf.Abs(velocity.y) / 15f, 0f, 1f);

			if(!audioSource.isPlaying && playOnce)
			{
				audioSource.Play ();
				playOnce = false;
			}
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere (transform.position, radius);
	}
}
