using UnityEngine;
using System.Collections;

public class CrosshairScript : MonoBehaviour {

	public float horizontalSpeed;
	public float verticalAccel;
	public AudioSource audioSource;
	
	Rigidbody2D rigidBody;

	void Awake()
	{
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void Update()
	{
		if (transform.position.y > Camera.main.orthographicSize)
		{
			transform.position = new Vector3(transform.position.x, Camera.main.orthographicSize, transform.position.z);
		}

		Vector2 velocity = rigidBody.velocity;
		velocity.x = horizontalSpeed * Input.acceleration.x;
		velocity.y += verticalAccel * Time.deltaTime;
		
		rigidBody.velocity = velocity;
	}

	public void PlayAduio()
	{
		audioSource.Play ();
	}
}
