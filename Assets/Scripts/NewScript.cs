using UnityEngine;
using System.Collections;

public class NewScript : MonoBehaviour {

	public float cycle;
	public float offset;

	float timeCount;
	Vector3 originalScale;
	
	void Awake()
	{
		timeCount = Random.Range(0, cycle);
		originalScale = transform.localScale;
	}
	
	void Update ()
	{
		timeCount += Time.deltaTime;
		timeCount %= cycle;

		float sinVal = offset * Mathf.Sin ((2 * Mathf.PI / cycle) * timeCount);
		transform.localScale = originalScale + new Vector3 (sinVal, sinVal, 0);
	}
}
