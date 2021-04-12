using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour {
	
	public List<GameObject> buildings = new List<GameObject>();
	public List<float> buildingDistances = new List<float> ();

	// Update is called once per frame
	void Update ()
	{
		Transform[] t = GetComponentsInChildren<Transform> ();
		if (t.Length <= 1)
		{
			Destroy(gameObject);
		}

		for (int i = 0; i < buildings.Count; i++)
		{
			buildings[i].GetComponent<Rigidbody2D>().velocity = new Vector3(0, GameManagerScript.currentVelocity / buildingDistances[i]);
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.tag == "Background")
		{
			buildings.Remove(coll.gameObject);
			Destroy(coll.gameObject);
		}
	}
}
