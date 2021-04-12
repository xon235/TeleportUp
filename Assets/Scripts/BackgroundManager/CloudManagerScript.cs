using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudManagerScript : MonoBehaviour {
	
	public GameObject cloudPrefab;
	public List<GameObject> activeClouds = new List<GameObject>();
	public List<GameObject> deactiveClouds = new List<GameObject>();

	public float bigCloudsHeight;

	public float spawnMargin;

	public float cloudVelocity0;
	public float cloudVelocity1;
	public float cloudVelocity2;

	public float cloudDistance0;
	public float cloudDistance1;
	public float cloudDistance2;

	public float cloudAlpha0;
	public float cloudAlpha1;
	public float cloudAlpha2;
	
	// Use this for initialization
	void Awake ()
	{
		SpawnClouds ();
	}
	
	void Update ()
	{
		SpawnClouds ();
	}
	
	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.tag == "Cloud")
		{
			coll.gameObject.SetActive (false);
			activeClouds.Remove(coll.gameObject);
			deactiveClouds.Add(coll.gameObject);
		}
	}
	
	void SpawnClouds()
	{
		Transform lastBlockTransform = activeClouds [activeClouds.Count - 1].transform;
		
		while (lastBlockTransform.localPosition.x < -spawnMargin)
		{
			GameObject cloud;
			if(deactiveClouds.Count > 0)
			{
				cloud = deactiveClouds[0];
				deactiveClouds.RemoveAt(0);
				cloud.SetActive(true);
			}
			else
			{
				cloud = Instantiate<GameObject>(cloudPrefab);
				cloud.transform.SetParent(transform);
			}


			float randomY = Random.Range(-2, 4);

			randomY *= 2f;
			int randomZ = Random.Range(0, 3);

			if(GameManagerScript.currentHeight < bigCloudsHeight)
			{
				randomZ = 2;
			}

			Color color = cloud.GetComponent<SpriteRenderer>().color;

			switch(randomZ)
			{
			case 0:
				cloud.transform.localScale = new Vector3(2, 2, 1);
				cloud.GetComponent<CloudScript>().horizontalVelocity = cloudVelocity0;
				cloud.GetComponent<CloudScript>().distanace = cloudDistance0;
				cloud.GetComponent<SpriteRenderer>().sortingOrder = 4;
				color.a = cloudAlpha0;
				break;
			case 1:
				cloud.transform.localScale = new Vector3(1, 1, 1);
				cloud.GetComponent<CloudScript>().horizontalVelocity = cloudVelocity1;
				cloud.GetComponent<CloudScript>().distanace = cloudDistance1;
				cloud.GetComponent<SpriteRenderer>().sortingOrder = -1;
				color.a = cloudAlpha1;
				break;
			case 2:
				cloud.transform.localScale = new Vector3(0.38f, 0.38f, 1);
				cloud.GetComponent<CloudScript>().horizontalVelocity = cloudVelocity2;
				cloud.GetComponent<CloudScript>().distanace = cloudDistance2;
				cloud.GetComponent<SpriteRenderer>().sortingOrder = -4;
				color.a = cloudAlpha2;
				break;
			}

			cloud.GetComponent<SpriteRenderer>().color = color;

			float x = lastBlockTransform.localPosition.x + spawnMargin + cloud.GetComponent<SpriteRenderer>().bounds.size.x / 2;
			cloud.transform.localPosition = new Vector3(x, randomY, 0);

			activeClouds.Add(cloud);
			lastBlockTransform = cloud.transform;
		}
	}
}
