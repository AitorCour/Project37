using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour 
{
	public GameObject CameraOn;
	public GameObject CameraOff;
	public GameObject CameraOff2;

	private void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
		{
			Debug.Log("Entered");
			CameraOn.SetActive(true);
			CameraOff.SetActive(false);
			CameraOff2.SetActive(false);
		}
		
	}
	
	//La camara se pone por defecto a cualquiera. Ahora tengo pocas y puede poner un hueco mas. Solucionar para que por defecto la 1 sea la primera.
	//solucionar que lo unico que hace que cambie de camara sea el player

}
