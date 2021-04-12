using UnityEngine;
using System.Collections;

public class BackButtonScript : MonoBehaviour {

	public void GoBackHome()
	{
		Application.LoadLevel ("Home");
	}
}
