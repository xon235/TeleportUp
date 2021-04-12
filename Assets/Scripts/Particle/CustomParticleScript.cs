using UnityEngine;
using System.Collections;

public class CustomParticleScript : MonoBehaviour {

	public Vector2 initialVelocity;
	public Vector2 maxAlphaVelocity;
	public float initialLifeTime;
	public float maxAlphalifeTime;

	Rigidbody2D rigidBody;
	SpriteRenderer spriteRenderer;
	float lifeTime;
	float lifeTimeCount;
	Vector2 originalLocalPosition;

	void Awake ()
	{
		rigidBody = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		originalLocalPosition = transform.localPosition;

		setLifeTime ();
		SetVelocity ();
	}

	void Update ()
	{
		lifeTimeCount += Time.deltaTime;

		if (lifeTimeCount >= lifeTime) {
			gameObject.SetActive (false);
			transform.localPosition = originalLocalPosition;

			Color color = spriteRenderer.color;
			color.a = 1f;
			spriteRenderer.color = color;

			setLifeTime();
			SetVelocity();
		}
		else
		{
			Color color = spriteRenderer.color;
			color.a = 1f - (lifeTimeCount / lifeTime);
			spriteRenderer.color = color;
		}
	}

	public void setLifeTime()
	{
		lifeTime = initialLifeTime + Random.Range (0, maxAlphalifeTime);
		lifeTimeCount = 0;
	}

	public void SetVelocity()
	{
		Vector2 velocity = initialVelocity;
		velocity.x += Random.Range (0, maxAlphaVelocity.x);
		velocity.y += Random.Range (0, maxAlphaVelocity.y) + GameManagerScript.currentVelocity;

		rigidBody.velocity = velocity;
	}
}
