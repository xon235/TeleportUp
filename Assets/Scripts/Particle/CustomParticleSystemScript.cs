using UnityEngine;
using System.Collections;

public class CustomParticleSystemScript : MonoBehaviour {

	void Update()
	{
		Transform[] t = GetComponentsInChildren<Transform> ();
		if (t.Length <= 1)
		{
			Destroy(gameObject);
		}
	}
}
