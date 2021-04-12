using UnityEngine;
using System.Collections;

public class UFOScript : MonoBehaviour
{
	public float cycle;
	public float offset;

	public bool start;
	public float acceleration;

	float timeCount;
	Vector3 originalPosition;
	Rigidbody2D rigidBody;

	void Awake()
	{
		timeCount = Random.Range(0, cycle);
		originalPosition = transform.position;
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		if (start)
		{
			rigidBody.velocity = new Vector3(0, rigidBody.velocity.y + acceleration * Time.deltaTime, 0);
		}
		else
		{
			timeCount += Time.deltaTime;
			timeCount %= cycle;
			
			transform.position = originalPosition + new Vector3 (0, offset * Mathf.Sin ((2 * Mathf.PI / cycle) * timeCount), 0);
		}
	}
}
