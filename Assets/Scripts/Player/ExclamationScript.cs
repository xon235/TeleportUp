using UnityEngine;
using System.Collections;

public class ExclamationScript : MonoBehaviour {

	public GameObject player;
	public GameObject legLeft;
	public GameObject legRight;

	public void DoDestroy()
	{
		player.GetComponent<Rigidbody2D> ().isKinematic = false;
		legLeft.GetComponent<Rigidbody2D> ().isKinematic = false;
		legRight.GetComponent<Rigidbody2D> ().isKinematic = false;
		player.GetComponent<PlayerScript> ().doUpdate = true;
		Destroy (gameObject);
	}
}