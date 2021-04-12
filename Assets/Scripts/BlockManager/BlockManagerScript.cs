using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockManagerScript : MonoBehaviour
{
	public GameObject blockPrefab;
	public List<GameObject> activeBlocks = new List<GameObject>();
	public List<GameObject> deactiveBlocks = new List<GameObject>();

	public float spawnMargin;
	
	float blockSpawnMargin;

	// Use this for initialization
	void Awake ()
	{
		float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
		float blockWidth = blockPrefab.GetComponent<Renderer> ().bounds.size.x;
		blockSpawnMargin = screenWidth / blockWidth;

		SpawnBlocks ();
	}

	void Update ()
	{
		SpawnBlocks ();
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.tag == "Block")
		{
			coll.gameObject.SetActive (false);
			activeBlocks.Remove(coll.gameObject);
			deactiveBlocks.Add(coll.gameObject);
		}
	}

	void SpawnBlocks()
	{
		Transform lastBlockTransform = activeBlocks [activeBlocks.Count - 1].transform;

		while (Mathf.Abs(lastBlockTransform.localPosition.y) > spawnMargin)
		{
			GameObject block;
			if(deactiveBlocks.Count > 0)
			{
				block = deactiveBlocks[0];
				deactiveBlocks.RemoveAt(0);
				block.SetActive(true);
			}
			else
			{
				block = Instantiate<GameObject>(blockPrefab);
			}
			
			block.transform.position = randomBlockPosition();
			block.transform.SetParent(transform);
			activeBlocks.Add(block);

			lastBlockTransform = block.transform;
		}
	}
	
	Vector2 randomBlockPosition()
	{
		Vector2 position = transform.position;
		position.x -= blockSpawnMargin * 1.5f;
		position.x += blockSpawnMargin * Random.Range (0, 4);

		int count = activeBlocks.Count;
		if (count > 0)
		{
			position.y = activeBlocks[count - 1].transform.position.y + spawnMargin;
		}

		return position;
	}
}