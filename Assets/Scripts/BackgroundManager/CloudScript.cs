using UnityEngine;
using System.Collections;

public class CloudScript : MonoBehaviour {

	public float horizontalVelocity;
	public float distanace;
	
	Rigidbody2D rigidBody;
	
	void Awake ()
	{
		rigidBody = GetComponent<Rigidbody2D> ();
	}
	
	void Update ()
	{
		rigidBody.velocity = new Vector2 (horizontalVelocity, GameManagerScript.currentVelocity / distanace);
	}
}
