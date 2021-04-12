using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	public GameObject crosshair;
	public Transform eye;
	public Transform pupil;
	public Transform legLeft;
	public Transform legRight;

	public GameObject particleBody;
	public GameObject particleEye;
	public GameObject particlePupil;
	public GameObject particleSingleLeg;

	public bool doUpdate;

	void Awake ()
	{
		doUpdate = false;
		crosshair.SetActive (false);
	}

	void Update ()
	{
		if (!doUpdate)
			return;

		if (Input.touchCount > 0)
		{
			if(!crosshair.activeSelf)
			{
				crosshair.transform.position = pupil.position;
				crosshair.SetActive(true);
			}
		}
		else if(crosshair.activeSelf)
		{
			crosshair.SetActive(false);
			crosshair.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

			InstantiateParticle();

			transform.position = crosshair.transform.position;;
			transform.rotation = Quaternion.identity;
			             
			legLeft.rotation = Quaternion.identity;
			legRight.rotation = Quaternion.identity;

			crosshair.GetComponent<CrosshairScript>().PlayAduio();

			GetComponent<Animator>().SetTrigger("Warp");
		}
	}

	void InstantiateParticle()
	{
		Instantiate (particleBody, transform.position, Quaternion.identity);
		Instantiate (particleEye, eye.position, Quaternion.identity);
		Instantiate (particlePupil, pupil.position, Quaternion.identity);

		//Left
		GameObject l = (GameObject)Instantiate (particleSingleLeg, legLeft.position, legLeft.rotation);
		l.transform.localScale = new Vector3 (-1f, 1f, 1f);

		//Right
		Instantiate (particleSingleLeg, legRight.position, legRight.rotation);
	}
}
