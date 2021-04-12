using UnityEngine;
using System.Collections;

public class StartTextScript : MonoBehaviour {

	float timeCount;

	void Awake()
	{
		timeCount = 0;
	}

	void Update ()
	{
		timeCount += Time.deltaTime;

		float scale = 1f + Mathf.Sin (4 * timeCount) * 0.05f;
		transform.localScale = new Vector3 (scale, scale, 0);
	}
}
