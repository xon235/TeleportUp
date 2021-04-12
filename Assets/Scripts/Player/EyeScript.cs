using UnityEngine;
using System.Collections;

public class EyeScript : MonoBehaviour {

	public float radius;
	public GameObject crosshair;
	public Transform eye;
	public Transform pupil;

	public float bodyRadius;
	public float eyeRadius;
	public float pupilRadius;

	public float eyeSpeed;

	float screenHalfWidth;
	
	void Awake ()
	{
		screenHalfWidth =  Camera.main.orthographicSize * Camera.main.aspect;
	}

	void Update()
	{
		movePupil ();
		moveEye ();
	}

	void movePupil()
	{
		Vector3 pos;
		if (crosshair.activeSelf) {
			pos = crosshair.transform.position - transform.position;
			if (pos.magnitude > screenHalfWidth) {
				pos = pos.normalized * screenHalfWidth;
			}
			
			pos *= (bodyRadius - pupilRadius) / screenHalfWidth;
		}
		else
		{
			pos = Vector3.zero;
			pos.x += (bodyRadius - pupilRadius) * Input.acceleration.x;

			if(Mathf.Abs(pos.x - pupil.localPosition.x) < 0.01)
			{
				pos.x = pupil.localPosition.x;
			}
		}

		pupil.localPosition = pos;
	}

	void moveEye()
	{
		Vector3 deltaEyePupil = pupil.localPosition - eye.localPosition;
		if (deltaEyePupil.magnitude > 0.01)
		{
			Vector3 pos = eye.localPosition;
			deltaEyePupil = deltaEyePupil.normalized * eyeSpeed;
			pos += deltaEyePupil;
			
			if(pos.magnitude > (bodyRadius - eyeRadius))
			{
				pos = pos.normalized * (bodyRadius - eyeRadius);
			}
			
			eye.localPosition = pos;
		}
	}
}
