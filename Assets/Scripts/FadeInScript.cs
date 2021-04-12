using UnityEngine;
using System.Collections;

public class FadeInScript : MonoBehaviour {

	public float fadeSpeed;

	SpriteRenderer spriteRenderer;
	float originalAlpha;

	// Use this for initialization
	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		Color color = spriteRenderer.color;
		originalAlpha = color.a;
		color.a = 0;
		spriteRenderer.color = color;
	}
	
	// Update is called once per frame
	void Update () {
		Color color = spriteRenderer.color;
		if (color.a < originalAlpha) {
			color.a += fadeSpeed * Time.deltaTime;
			spriteRenderer.color = color;
		} else {
			color.a = originalAlpha;
			enabled = false;
		}
	}
}
